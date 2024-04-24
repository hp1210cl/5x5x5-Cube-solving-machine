using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

//大写字母URFDLB表示单层，前面加数字表示最外面向里数的单层层数，缺省为1
//小写字母urfdlb表示多层，前面加数字表示最外面向里数的连续层数，缺省为2，注意1f=F，其他类似
//UwRwFwDwLwBw与小写字母urfdlb含义相同，用法一样
//大写字母MES表示中间层，对偶数魔方无效,前缀无效,对于奇数阶魔方等效于中间单层反转
//小写字母mes表示除了最外层的中间层,低于3阶无效,前缀无效
//M、m以贯穿LR面中心的线为轴线逆时针旋转90°，观看面为R
//E、e以贯穿UD面中心的线为轴线逆时针旋转90°，观看面为U
//S、s以贯穿FB面中心的线为轴线逆时针旋转90°，观看面为F
//x表示整个魔方以贯穿LR面中心的线为轴线顺时针旋转90°，观看面为R，等效于Lw前缀为魔方阶数
//y表示整个魔方以贯穿UD面中心的线为轴线顺时针旋转90°，观看面为U，等效于Uw前缀为魔方阶数
//z表示整个魔方以贯穿FB面中心的线为轴线顺时针旋转90°，观看面为F，等效于Fw前缀为魔方阶数

//以上的操作如果后面加数字代表转动角度，缺省为1，代表顺时针转90°，2代表转180°，3代表逆时针转90°
//后面加‘与加数字3含义相同
//

namespace CubeSolver
{
    internal class TwistEventArgs: EventArgs
    {
        public TwistEventArgs(int twistIndex,int twistCount,string twistName) 
        {
            this.TwistIndex = twistIndex;
            this.TwistCount = twistCount;
            this.TwistName = twistName;
        }
        public int TwistIndex { set; get; }
        public int TwistCount { set; get; }
        public string TwistName { set; get;}
    }
    /// <summary>
    /// X旋转器对着U，负责UD面的旋转，Y旋转器对着L，负责LR面的旋转，Z旋转器对着F，负责FB面的旋转
    /// A推杆负责UD层的控制，B推杆负责LR层的控制，C推杆负责FB层的控制
    /// </summary>
    internal class Machine
    {
        public const int CubeDimesion = 5;
        public bool Waiting = false;

        public event EventHandler<TwistEventArgs> TwistBeginning;

        private SerialPort serialPort;
        private string PortName;
        private Task readTask;
        private bool doReading;
        public bool Connected;

        private Pole currentPole = Pole.C;
        private CubePos currentPolePos = CubePos.Pos0;//存储当前推杆位置

        private TwistType[] OriginalTwists;
        private TwistType[] AdjustedTwists;

        private int[] stepOfPos = new int[] 
        { 
            CubeSolverConsts.STEPS_OF_POS0 * CubeSolverConsts.MICROSTEP,
            CubeSolverConsts.STEPS_OF_POS1 * CubeSolverConsts.MICROSTEP,
            CubeSolverConsts.STEPS_OF_POS2 * CubeSolverConsts.MICROSTEP,
            CubeSolverConsts.STEPS_OF_POS3 * CubeSolverConsts.MICROSTEP,
            CubeSolverConsts.STEPS_OF_POS4 * CubeSolverConsts.MICROSTEP,
            CubeSolverConsts.STEPS_OF_POS5 * CubeSolverConsts.MICROSTEP
        };
        private int[] redundancyOfPos = new int[]
        {
            CubeSolverConsts.REDUNDANCY_OF_POS0,
            CubeSolverConsts.REDUNDANCY_OF_POS1,
            CubeSolverConsts.REDUNDANCY_OF_POS2,
            CubeSolverConsts.REDUNDANCY_OF_POS3,
            CubeSolverConsts.REDUNDANCY_OF_POS4,
            CubeSolverConsts.REDUNDANCY_OF_POS5

        };
        private int currentRedundancy =0;

        //因为魔方与旋转器之间有空隙，必须要多转动才能确保魔方正好是转动90度，那么就需要把多转的修正回来
        //转动修正量与TwistType枚举顺序对应
        //弃用此种修正，考虑为每层指定一个冗余值常量，可以简化修正过程
        private int[] AmendmentValues = new int[]
        {
        //None,
        0,
        //U, R, F, D, L, B,
        2,2,2,2,2,2,
        //U1, R1, F1, D1, L1, B1,
        2,2,2,2,2,2,
        //U2, R2, F2, D2, L2, B2,
        2,2,2,2,2,2,
        //Uw, Rw, Fw, Dw, Lw, Bw,
        2,2,2,2,2,2,
        //Uw1, Rw1, Fw1, Dw1, Lw1, Bw1,
        2,2,2,2,2,2,
        //Uw2, Rw2, Fw2, Dw2, Lw2, Bw2,
        2,2,2,2,2,2,
        //u, r, f, d, l, b,
        2,2,2,2,2,2,
        //u1, r1, f1, d1, l1, b1,
        2,2,2,2,2,2,
        //u2, r2, f2, d2, l2, b2,
        2,2,2,2,2,2,
        };

        //旋转器与TwistType枚举顺序对应，
        private Rotater[] RotaterIndex = new Rotater[]
        {
        //None,
        0,
        //U, R, F, D, L, B,
        Rotater.X,Rotater.Y,Rotater.Z,Rotater.X,Rotater.Y,Rotater.Z,
        //U1, R1, F1, D1, L1, B1,
        Rotater.X,Rotater.Y,Rotater.Z,Rotater.X,Rotater.Y,Rotater.Z,
        //U2, R2, F2, D2, L2, B2,
        Rotater.X,Rotater.Y,Rotater.Z,Rotater.X,Rotater.Y,Rotater.Z,
        //Uw, Rw, Fw, Dw, Lw, Bw,
        Rotater.X,Rotater.Y,Rotater.Z,Rotater.X,Rotater.Y,Rotater.Z,
        //Uw1, Rw1, Fw1, Dw1, Lw1, Bw1,
        Rotater.X,Rotater.Y,Rotater.Z,Rotater.X,Rotater.Y,Rotater.Z,
        //Uw2, Rw2, Fw2, Dw2, Lw2, Bw2,
        Rotater.X,Rotater.Y,Rotater.Z,Rotater.X,Rotater.Y,Rotater.Z,
        //u, r, f, d, l, b,
        Rotater.X,Rotater.Y,Rotater.Z,Rotater.X,Rotater.Y,Rotater.Z,
        //u1, r1, f1, d1, l1, b1,
        Rotater.X,Rotater.Y,Rotater.Z,Rotater.X,Rotater.Y,Rotater.Z,
        //u2, r2, f2, d2, l2, b2,
        Rotater.X,Rotater.Y,Rotater.Z,Rotater.X,Rotater.Y,Rotater.Z,
        };
        //旋转器转动的方向与TwistType枚举顺序对应，
        private RotateDirection[] RotateDirectionIndex = new RotateDirection[]
        {
        //None,
        0,
        //U, R, F, D, L, B,
        RotateDirection.CW,RotateDirection.CW,RotateDirection.CW,RotateDirection.CW,RotateDirection.CW,RotateDirection.CW,
        //U1, R1, F1, D1, L1, B1,
        RotateDirection.CCW,RotateDirection.CCW,RotateDirection.CCW,RotateDirection.CCW,RotateDirection.CCW,RotateDirection.CCW,
        //U2, R2, F2, D2, L2, B2,
        RotateDirection.CW2,RotateDirection.CW2,RotateDirection.CW2,RotateDirection.CW2,RotateDirection.CW2,RotateDirection.CW2,
        //Uw, Rw, Fw, Dw, Lw, Bw,
        RotateDirection.CW,RotateDirection.CW,RotateDirection.CW,RotateDirection.CW,RotateDirection.CW,RotateDirection.CW,
        //Uw1, Rw1, Fw1, Dw1, Lw1, Bw1,
        RotateDirection.CCW,RotateDirection.CCW,RotateDirection.CCW,RotateDirection.CCW,RotateDirection.CCW,RotateDirection.CCW,
        //Uw2, Rw2, Fw2, Dw2, Lw2, Bw2,
        RotateDirection.CW2,RotateDirection.CW2,RotateDirection.CW2,RotateDirection.CW2,RotateDirection.CW2,RotateDirection.CW2,
        //u, r, f, d, l, b,
        RotateDirection.CW,RotateDirection.CW,RotateDirection.CW,RotateDirection.CW,RotateDirection.CW,RotateDirection.CW,
        //u1, r1, f1, d1, l1, b1,
        RotateDirection.CCW,RotateDirection.CCW,RotateDirection.CCW,RotateDirection.CCW,RotateDirection.CCW,RotateDirection.CCW,
        //u2, r2, f2, d2, l2, b2,
        RotateDirection.CW2,RotateDirection.CW2,RotateDirection.CW2,RotateDirection.CW2,RotateDirection.CW2,RotateDirection.CW2,
        };
        //旋转器转动的角度与TwistType枚举顺序对应，
        private RotateAngle[] RotateAngleIndex = new RotateAngle[]
        {
        //None,
        0,
        //U, R, F, D, L, B,
        RotateAngle.D90,RotateAngle.D90,RotateAngle.D90,RotateAngle.D90,RotateAngle.D90,RotateAngle.D90,
        //U1, R1, F1, D1, L1, B1,
        RotateAngle.D90,RotateAngle.D90,RotateAngle.D90,RotateAngle.D90,RotateAngle.D90,RotateAngle.D90,
        //U2, R2, F2, D2, L2, B2,
        RotateAngle.D180,RotateAngle.D180,RotateAngle.D180,RotateAngle.D180,RotateAngle.D180,RotateAngle.D180,
        //Uw, Rw, Fw, Dw, Lw, Bw,
        RotateAngle.D90,RotateAngle.D90,RotateAngle.D90,RotateAngle.D90,RotateAngle.D90,RotateAngle.D90,
        //Uw1, Rw1, Fw1, Dw1, Lw1, Bw1,
        RotateAngle.D90,RotateAngle.D90,RotateAngle.D90,RotateAngle.D90,RotateAngle.D90,RotateAngle.D90,
        //Uw2, Rw2, Fw2, Dw2, Lw2, Bw2,
        RotateAngle.D180,RotateAngle.D180,RotateAngle.D180,RotateAngle.D180,RotateAngle.D180,RotateAngle.D180,
        //u, r, f, d, l, b,
        RotateAngle.D90,RotateAngle.D90,RotateAngle.D90,RotateAngle.D90,RotateAngle.D90,RotateAngle.D90,
        //u1, r1, f1, d1, l1, b1,
        RotateAngle.D90,RotateAngle.D90,RotateAngle.D90,RotateAngle.D90,RotateAngle.D90,RotateAngle.D90,
        //u2, r2, f2, d2, l2, b2,
        RotateAngle.D180,RotateAngle.D180,RotateAngle.D180,RotateAngle.D180,RotateAngle.D180,RotateAngle.D180,
        };

        //推杆与TwistType枚举顺序对应，
        private Pole[] PoleIndex = new Pole[]
        {
        //None,
        0,
        //U, R, F, D, L, B,
        Pole.A,Pole.B,Pole.C,Pole.A,Pole.B,Pole.C,
        //U1, R1, F1, D1, L1, B1,
        Pole.A,Pole.B,Pole.C,Pole.A,Pole.B,Pole.C,
        //U2, R2, F2, D2, L2, B2,
        Pole.A,Pole.B,Pole.C,Pole.A,Pole.B,Pole.C,
        //Uw, Rw, Fw, Dw, Lw, Bw,
        Pole.A,Pole.B,Pole.C,Pole.A,Pole.B,Pole.C,
        //Uw1, Rw1, Fw1, Dw1, Lw1, Bw1,
        Pole.A,Pole.B,Pole.C,Pole.A,Pole.B,Pole.C,
        //Uw2, Rw2, Fw2, Dw2, Lw2, Bw2,
        Pole.A,Pole.B,Pole.C,Pole.A,Pole.B,Pole.C,
        //u, r, f, d, l, b,
        Pole.A,Pole.B,Pole.C,Pole.A,Pole.B,Pole.C,
        //u1, r1, f1, d1, l1, b1,
        Pole.A,Pole.B,Pole.C,Pole.A,Pole.B,Pole.C,
        //u2, r2, f2, d2, l2, b2,
        Pole.A,Pole.B,Pole.C,Pole.A,Pole.B,Pole.C,
        };

        //推杆的首次位置与魔方基本扭动的对应关系，不区分ABC推杆，因为一次扭动只能有一个位置
        private CubePos[] PolePositionIndex = new CubePos[]
        {
        //None,
        0,
        //U, R, F, D, L, B,
        CubePos.Pos1,CubePos.Pos4,CubePos.Pos1,CubePos.Pos4,CubePos.Pos1,CubePos.Pos4,
        //U1, R1, F1, D1, L1, B1,
        CubePos.Pos1,CubePos.Pos4,CubePos.Pos1,CubePos.Pos4,CubePos.Pos1,CubePos.Pos4,
        //U2, R2, F2, D2, L2, B2,
        CubePos.Pos1,CubePos.Pos4,CubePos.Pos1,CubePos.Pos4,CubePos.Pos1,CubePos.Pos4,
        //Uw, Rw, Fw, Dw, Lw, Bw,
        CubePos.Pos2,CubePos.Pos3,CubePos.Pos2,CubePos.Pos3,CubePos.Pos2,CubePos.Pos3,
        //Uw1, Rw1, Fw1, Dw1, Lw1, Bw1,
        CubePos.Pos2,CubePos.Pos3,CubePos.Pos2,CubePos.Pos3,CubePos.Pos2,CubePos.Pos3,
        //Uw2, Rw2, Fw2, Dw2, Lw2, Bw2,
        CubePos.Pos2,CubePos.Pos3,CubePos.Pos2,CubePos.Pos3,CubePos.Pos2,CubePos.Pos3,
        //u, r, f, d, l, b,
        CubePos.Pos2,CubePos.Pos3,CubePos.Pos2,CubePos.Pos3,CubePos.Pos2,CubePos.Pos3,
        //u1, r1, f1, d1, l1, b1,
        CubePos.Pos2,CubePos.Pos3,CubePos.Pos2,CubePos.Pos3,CubePos.Pos2,CubePos.Pos3,
        //u2, r2, f2, d2, l2, b2,
        CubePos.Pos2,CubePos.Pos3,CubePos.Pos2,CubePos.Pos3,CubePos.Pos2,CubePos.Pos3,
        };

        //推杆步进电机转动的步数，与CubePos枚举对应
        private int[] PushStepValues = new int[]
        {
            //StartPushPos,
            //RotatePos1,
            //RotatePos2,
            //RotatePos3,
            //RotatePos4,
            //RotatePos5
        };

        public CubePos CurrentPos
        {
            get
            {
                return currentPolePos;
            }
        }

        public Machine(string portName)
        {
            this.PortName = portName;
            Connected = false;
            doReading = false;
            Waiting = true;
            serialPort = new SerialPort();
            OriginalTwists = new TwistType[0];
            AdjustedTwists= new TwistType[0];
        }

        public bool Connect()
        {
            try
            {
                serialPort = new SerialPort();
                serialPort.BaudRate = 115200;
                serialPort.PortName = this.PortName;
                serialPort.Open();
                readTask = Task.Run(() => { Read(); });
                doReading = true;
                Connected = true;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool DisConnect()
        {
            doReading = false;
            if (serialPort.IsOpen)
            {
                serialPort.Close();
            }
            readTask.Wait();
            readTask.Dispose();
            return true;
        }

        private void Read()
        {
            while (doReading)
            {
                try
                {
                    if (serialPort.BytesToRead > 0)
                    {
                        string incoming = serialPort.ReadLine();
#if DEBUG
Debug.Write($"\tMachine reply :{incoming}");
#endif
                        if (incoming.IndexOf("EOD") >= 0)
                        {
                            this.Waiting = false;
                        }
                    }
                }
                catch (Exception) { }
            }
        }

        private void ExcuteCommand(CommandHeader header, int steps0, int steps1)
        {
            if(Connected)
            {
                byte[] headers = new byte[] { 0x45, 0x58, 0x59, 0x5a, 0x41, 0x42, 0x43, 0x4F, 0x35 };
                byte[] data = new byte[6];
                data[0] = headers[(int)header];
                byte[] value0 = BitConverter.GetBytes(steps0);
                data[1] = value0[1];
                data[2] = value0[0];
                byte[] value1 = BitConverter.GetBytes(steps1);
                data[3] = value1[1];
                data[4] = value1[0];
                data[5] = 0x0a;
                serialPort.Write(data, 0, 6);
#if DEBUG
                Debug.WriteLine($"{header.ToString()} {steps0.ToString()} {steps1.ToString()} sent to machine.");
#endif
                Waiting = true;
                Task taskWaiting = Task.Run(() =>
                {
                    while (Waiting)
                    {
                    }
                });
                taskWaiting.Wait();
#if DEBUG
                Debug.WriteLine("Waiting finished!");
#endif
            }
            else
            {
                throw new Exception("Port is not opened!");
            }
        }

        //用于调试目的
        public void StepperAct(CommandHeader header, int steps0, int steps1)
        {
            if (Connected)
            {
                byte[] headers = new byte[] { 0x45, 0x58, 0x59, 0x5A, 0x41, 0x42, 0x43, 0x4F, 0x35 };
                byte[] data = new byte[6];
                data[0] = headers[(int)header];
                byte[] value0 = BitConverter.GetBytes(steps0);
                data[1] = value0[1];
                data[2] = value0[0];
                byte[] value1 = BitConverter.GetBytes(steps1);
                data[3] = value1[1];
                data[4] = value1[0];
                data[5] = 0x0a;
                serialPort.Write(data, 0, 6);
#if DEBUG
                Debug.WriteLine($"{header.ToString()} {steps0.ToString()} {steps1.ToString()} sent to machine.");
#endif
            }
            else
            {
                throw new Exception("Port is not opened!");
            }
        }


        public void RotateRotater(Rotater r, RotateDirection rd, int steps)
        {
            CommandHeader ch = Enum.Parse<CommandHeader>(r.ToString());
            int steps0 = steps;
            int redundancy = currentRedundancy;
            if (rd == RotateDirection.CW)
            {
                steps0 = -steps0;
                redundancy = -redundancy;
            }
            ExcuteCommand(ch, steps0 + redundancy, -redundancy);
        }

        public void RotateRotater(Rotater r, RotateDirection rd, RotateAngle ra)
        {
            CommandHeader ch = Enum.Parse<CommandHeader>(r.ToString());
            int steps0 = ra == RotateAngle.D90 ? CubeSolverConsts.STEPS_OF_90DEGREE * CubeSolverConsts.MICROSTEP : CubeSolverConsts.STEPS_OF_180DEGREE * CubeSolverConsts.MICROSTEP;
            int redundancy = currentRedundancy;
            if (rd == RotateDirection.CW)
            {
                steps0 = -steps0;
                redundancy = -redundancy;
           }
            ExcuteCommand(ch, steps0 + redundancy, -redundancy);
        }


        public void PushPullPole(Pole p, CubePos cp)
        {
            int steps0 = 0;
            //判断之前的推杆是不是指定的推杆，如果不是要把之前的推杆先撤回到初始位置
            if (p==currentPole)
            {
                if(cp==currentPolePos)
                {
                    return;
                }
                else
                {
                    CommandHeader ch = Enum.Parse<CommandHeader>(p.ToString());
                    steps0 = stepOfPos[(int)cp] - stepOfPos[(int)currentPolePos];
                    ExcuteCommand(ch, steps0, 0);
                    currentPolePos = cp;
                }
            }
            else
            {
                PushPullPole(currentPole, CubePos.Pos0);

                CommandHeader ch = Enum.Parse<CommandHeader>(p.ToString());
                steps0 = stepOfPos[(int)cp];
                ExcuteCommand(ch, steps0, 0);
                currentPolePos = cp;
                currentPole = p;
            }
            currentRedundancy = redundancyOfPos[(int)cp];

        }


        public void PullAllPoleToPos0()
        {
            PushPullPole(currentPole, CubePos.Pos0);
        }



        public void PerformATwist(TwistType twist)
        {
            int index = (int)twist;

            PushPullPole(PoleIndex[index], PolePositionIndex[index]);
            RotateRotater(RotaterIndex[index], RotateDirectionIndex[index], RotateAngleIndex[index]);
        }

        public void PerformTwists(TwistType[] twists)
        {
            OriginalTwists = new TwistType[twists.Length];
            AdjustedTwists = new TwistType[twists.Length];
            twists.CopyTo(OriginalTwists, 0);
            twists.CopyTo(AdjustedTwists, 0);
            for (int i = 0; i < AdjustedTwists.Length; i++)
            {
                TwistBeginning?.Invoke(this, new TwistEventArgs(i, twists.Length, OriginalTwists[i].ToString()));
                PerformATwist(AdjustedTwists[i]);

                AdjustTwists(i);
            }
            PullAllPoleToPos0();
        }

        public Task PerformTwistsAsync(TwistType[] twists,CancellationToken ct)
        {
            return Task.Run(() =>{
                OriginalTwists = new TwistType[twists.Length];
                AdjustedTwists = new TwistType[twists.Length];
                twists.CopyTo(OriginalTwists, 0);
                twists.CopyTo(AdjustedTwists, 0);
                for (int i = 0; i < AdjustedTwists.Length; i++)
                {

                    if (ct.IsCancellationRequested ) { return; }

                    TwistBeginning?.Invoke(this, new TwistEventArgs(i, twists.Length, OriginalTwists[i].ToString()));
                    PerformATwist(AdjustedTwists[i]);

                    AdjustTwists(i);
                }
                PullAllPoleToPos0();
            });
        }

        public void AdjustTwists(int currentIndex)
        {
            int x = 0;

            int transformationTimes = 0;
            x = (int)AdjustedTwists[currentIndex];
            int[] times = new int[] { 3, 1, 2 };
            transformationTimes = times[((x - 1) / 6) % 3];

            //U->,R->,F->,D->,L->,B->
            switch (AdjustedTwists[currentIndex])
            {
                case TwistType.None:
                    break;
                //U,U',U2,Uw,Uw',Uw2,u,u',u2
                //L,L',L2,Lw,Lw',Lw2,l,l',l2
                //F,F',F2,Fw,Fw',Fw2,f,f',f2
                //无需变换
                case TwistType.U:
                case TwistType.U1:
                case TwistType.U2:
                case TwistType.Uw:
                case TwistType.Uw1:
                case TwistType.Uw2:
                case TwistType.u:
                case TwistType.u1:
                case TwistType.u2:
                case TwistType.L:
                case TwistType.L1:
                case TwistType.L2:
                case TwistType.Lw:
                case TwistType.Lw1:
                case TwistType.Lw2:
                case TwistType.l:
                case TwistType.l1:
                case TwistType.l2:
                case TwistType.F:
                case TwistType.F1:
                case TwistType.F2:
                case TwistType.Fw:
                case TwistType.Fw1:
                case TwistType.Fw2:
                case TwistType.f:
                case TwistType.f1:
                case TwistType.f2:
                    break;
                //R,R',R2,Rw,Rw',Rw2,r,r',r2
                //U->F,R->R,F->D,D->B,L->L,B->U
                case TwistType.R:
                case TwistType.R1:
                case TwistType.R2:
                case TwistType.Rw:
                case TwistType.Rw1:
                case TwistType.Rw2:
                case TwistType.r:
                case TwistType.r1:
                case TwistType.r2:
                    for (int i = 0; i < transformationTimes; i++)
                    {
                        RTransform(currentIndex + 1);
                    }
                    break;
                //D,D',D2,Dw,Dw'Dw2,d,d',d2
                //D变换：U->U,R->F,F->L,D->D,L->B,B->R
                case TwistType.D:
                case TwistType.D1:
                case TwistType.D2:
                case TwistType.Dw:
                case TwistType.Dw1:
                case TwistType.Dw2:
                case TwistType.d:
                case TwistType.d1:
                case TwistType.d2:
                    for (int i = 0; i < transformationTimes; i++)
                    {
                        DTransform(currentIndex + 1);
                    }
                    break;
                //B,B',B2,Bw,Bw'Bw2,b,b',b2
                //U->R,R->D,F->F,D->L,L->U,B->B
                case TwistType.B:
                case TwistType.B1:
                case TwistType.B2:
                case TwistType.Bw:
                case TwistType.Bw1:
                case TwistType.Bw2:
                case TwistType.b:
                case TwistType.b1:
                case TwistType.b2:
                    for (int i = 0; i < transformationTimes; i++)
                    {
                        BTransform(currentIndex + 1);
                    }
                    break;
                default:
                    break;
            }
        }

        private void RTransform(int startIndex)
        {
            //U, R, F, D, L, B,
            //U1, R1, F1, D1, L1, B1,
            //U2, R2, F2, D2, L2, B2,
            //Uw, Rw, Fw, Dw, Lw, Bw,
            //Uw1, Rw1, Fw1, Dw1, Lw1, Bw1,
            //Uw2, Rw2, Fw2, Dw2, Lw2, Bw2,
            //u,r,f,d,l,b
            //u1,r1,f1,d1,l1,b1
            //u2,r2,f2,d2,l2,b2
            //	1	,	2	,	3	,	4	,	5	,	6	,
            //	7	,	8	,	9	,	10	,	11	,	12	,
            //	13	,	14	,	15	,	16	,	17	,	18	,
            //	19	,	20	,	21	,	22	,	23	,	24	,
            //	25	,	26	,	27	,	28	,	29	,	30	,
            //	31	,	32	,	33	,	34	,	35	,	36	,
            //	37	,	38	,	39	,	40	,	41	,	42	,
            //	43	,	44	,	45	,	46	,	47	,	48	,
            //	49	,	50	,	51	,	52	,	53	,	54	,
            //可以使用excel剪切列插入的方法快速得到变换后的数组


            //R变换：U->F,R->R,F->D,D->B,L->L,B->U
            int[] trans = new int[]
            {
                0,
                6   ,   2   ,   1   ,   3   ,   5   ,   4   ,
                12  ,   8   ,   7   ,   9   ,   11  ,   10  ,
                18  ,   14  ,   13  ,   15  ,   17  ,   16  ,
                24  ,   20  ,   19  ,   21  ,   23  ,   22  ,
                30  ,   26  ,   25  ,   27  ,   29  ,   28  ,
                36  ,   32  ,   31  ,   33  ,   35  ,   34  ,
                42  ,   38  ,   37  ,   39  ,   41  ,   40  ,
                48  ,   44  ,   43  ,   45  ,   47  ,   46  ,
                54  ,   50  ,   49  ,   51  ,   53  ,   52  ,
            };

            for (int i = startIndex; i < AdjustedTwists.Length; i++)
            {
                AdjustedTwists[i] = (TwistType)trans[(int)AdjustedTwists[i]];
            }
        }


        private void DTransform(int startIndex)
        {
            //U, R, F, D, L, B,
            //U1, R1, F1, D1, L1, B1,
            //U2, R2, F2, D2, L2, B2,
            //Uw, Rw, Fw, Dw, Lw, Bw,
            //Uw1, Rw1, Fw1, Dw1, Lw1, Bw1,
            //Uw2, Rw2, Fw2, Dw2, Lw2, Bw2,
            //u,r,f,d,l,b
            //u1,r1,f1,d1,l1,b1
            //u2,r2,f2,d2,l2,b2

            //D变换：U->U,R->F,F->L,D->D,L->B,B->R
            int[] trans = new int[]
            {
                0,
                1   ,   6   ,   2   ,   4   ,   3   ,   5   ,
                7   ,   12  ,   8   ,   10  ,   9   ,   11  ,
                13  ,   18  ,   14  ,   16  ,   15  ,   17  ,
                19  ,   24  ,   20  ,   22  ,   21  ,   23  ,
                25  ,   30  ,   26  ,   28  ,   27  ,   29  ,
                31  ,   36  ,   32  ,   34  ,   33  ,   35  ,
                37  ,   42  ,   38  ,   40  ,   39  ,   41  ,
                43  ,   48  ,   44  ,   46  ,   45  ,   47  ,
                49  ,   54  ,   50  ,   52  ,   51  ,   53  ,
            };
            for (int i = startIndex; i < AdjustedTwists.Length; i++)
            {
                AdjustedTwists[i] = (TwistType)trans[(int)AdjustedTwists[i]];
            }
        }

        private void BTransform(int startIndex)
        {
            //U, R, F, D, L, B,
            //U1, R1, F1, D1, L1, B1,
            //U2, R2, F2, D2, L2, B2,
            //Uw, Rw, Fw, Dw, Lw, Bw,
            //Uw1, Rw1, Fw1, Dw1, Lw1, Bw1,
            //Uw2, Rw2, Fw2, Dw2, Lw2, Bw2,
            //u,r,f,d,l,b
            //u1,r1,f1,d1,l1,b1
            //u2,r2,f2,d2,l2,b2

            //B变换：U->R,R->D,F->F,D->L,L->U,B->B
            int[] trans = new int[]
            {
                0,
                5   ,   1   ,   3   ,   2   ,   4   ,   6   ,
                11  ,   7   ,   9   ,   8   ,   10  ,   12  ,
                17  ,   13  ,   15  ,   14  ,   16  ,   18  ,
                23  ,   19  ,   21  ,   20  ,   22  ,   24  ,
                29  ,   25  ,   27  ,   26  ,   28  ,   30  ,
                35  ,   31  ,   33  ,   32  ,   34  ,   36  ,
                41  ,   37  ,   39  ,   38  ,   40  ,   42  ,
                47  ,   43  ,   45  ,   44  ,   46  ,   48  ,
                53  ,   49  ,   51  ,   50  ,   52  ,   54  ,
            };

            for (int i = startIndex; i < AdjustedTwists.Length; i++)
            {
                AdjustedTwists[i] = (TwistType)trans[(int)AdjustedTwists[i]];
            }
        }

    }
}
