using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace CubeSolver
{
    public partial class Form1 : Form
    {
        delegate void SafeAddInfoToListBox(string info, InfoDir infodir);

        private Machine machine;
        private VideoCapture videoCapture;
        private bool captureInProgress;
        private Mat frame = new Mat();
        private bool showTimer;
        private bool counting;
        private Point timerPosition;
        private Point statePosition;
        private Point twistinfoPosition;
        private MCvScalar timerColor;
        private MCvScalar stateColor;
        private MCvScalar twistinfoColor;
        private DateTime starttime;
        private string eclipsedTime = string.Empty;
        private string currentState = string.Empty;
        private string twistInfo = string.Empty;
        private Rectangle frameRectangle;
        private Rectangle captureRectangle;
        private Mat captureView;
        private bool filterImage = false;
        private RadioButton twistPrefix;
        private RadioButton twistPostfix;

        private CancellationTokenSource cts;


        public Form1()
        {
            InitializeComponent();

            ListSerialPorts(comboBox1);
            groupBox1.Enabled = false;
            if (comboBox1.Items.Count > 0)
            {
                btnConnect.Enabled = true;
                btnConnect.BackColor = Color.Green;
            }
            this.FormClosing += Form1_FormClosing;
            twistPrefix = radioButton1;
            twistPostfix = radioButton13;
            machine = new Machine("");
            cts = new CancellationTokenSource();

            frameRectangle = new Rectangle(CubeSolverConsts.ROI_LEFT, CubeSolverConsts.ROI_TOP, CubeSolverConsts.ROI_WIDTH, CubeSolverConsts.ROI_HEIGHT);
            captureRectangle = new Rectangle(CubeSolverConsts.VIEW_LEFT, CubeSolverConsts.VIEW_TOP, CubeSolverConsts.VIEW_WIDTH, CubeSolverConsts.VIEW_HEIGHT);

            timerPosition = new Point(50, 150);
            timerColor = new MCvScalar(0, 0, 255);
            statePosition = new Point(50, 300);
            stateColor = new MCvScalar(0, 255, 0);
            twistinfoPosition = new Point(100, 550);
            twistinfoColor = new MCvScalar(255, 0, 0);
        }

        private void Form1_FormClosing(object? sender, FormClosingEventArgs e)
        {
            ReleaseCapture();
        }

        private void ListSerialPorts(ComboBox combobox)
        {
            combobox.Items.Clear();
            foreach (string s in SerialPort.GetPortNames())
            {
                combobox.Items.Add(s);
            }
            if (combobox.Items.Count > 0)
            {
                combobox.Text = combobox.Items[combobox.Items.Count - 1].ToString();
            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (btnConnect.Text == "Connect")
            {
                try
                {

                    machine = new Machine(comboBox1.Text);
                    machine.TwistBeginning += Machine_TwistBeginning;
                    if (machine.Connect())
                    {
                        btnConnect.Text = "Disconnect";
                        btnConnect.BackColor = Color.Red;
                        groupBox1.Enabled = true;
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                machine.DisConnect();
                btnConnect.Text = "Connect";
                btnConnect.BackColor = Color.Green;
                groupBox1.Enabled = false;
            }

        }

        private void Machine_TwistBeginning(object? sender, TwistEventArgs e)
        {
            twistInfo = $"{e.TwistName.Replace("1", "'")} {e.TwistIndex + 1}-{e.TwistCount}";
        }

        private void button15_Click(object sender, EventArgs e)
        {
            machine.StepperAct(CommandHeader.EN, 1, 0);
        }

        private void button16_Click(object sender, EventArgs e)
        {
            machine.StepperAct(CommandHeader.EN, 0, 0);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            machine.StepperAct(CommandHeader.X, -1, 0);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            machine.StepperAct(CommandHeader.X, 1, 0);
        }

        private void button23_Click(object sender, EventArgs e)
        {
            machine.RotateRotater(Rotater.X, RotateDirection.CW, RotateAngle.D90);
        }

        private void button22_Click(object sender, EventArgs e)
        {
            machine.RotateRotater(Rotater.X, RotateDirection.CCW, RotateAngle.D90);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            machine.StepperAct(CommandHeader.Y, -1, 0);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            machine.StepperAct(CommandHeader.Y, 1, 0);
        }

        private void button24_Click(object sender, EventArgs e)
        {
            machine.RotateRotater(Rotater.Y, RotateDirection.CW, RotateAngle.D90);
        }

        private void button25_Click(object sender, EventArgs e)
        {
            machine.RotateRotater(Rotater.Y, RotateDirection.CCW, RotateAngle.D90);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            machine.StepperAct(CommandHeader.Z, -1, 0);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            machine.StepperAct(CommandHeader.Z, 1, 0);
        }

        private void button27_Click(object sender, EventArgs e)
        {
            machine.RotateRotater(Rotater.Z, RotateDirection.CW, RotateAngle.D90);
        }

        private void button26_Click(object sender, EventArgs e)
        {
            machine.RotateRotater(Rotater.Z, RotateDirection.CCW, RotateAngle.D90);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            machine.StepperAct(CommandHeader.A, 1, 0);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            machine.StepperAct(CommandHeader.A, -1, 0);
        }

        private void button21_Click(object sender, EventArgs e)
        {
            int pos = (int)machine.CurrentPos;
            pos = pos + 1;
            if (pos > 5) pos = 5;
            CubePos newPos = (CubePos)pos;
            machine.PushPullPole(Pole.A, newPos);
        }

        private void button20_Click(object sender, EventArgs e)
        {
            int pos = (int)machine.CurrentPos;
            pos = pos - 1;
            if (pos < 0) pos = 0;
            CubePos newPos = (CubePos)pos;
            machine.PushPullPole(Pole.A, newPos);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            machine.StepperAct(CommandHeader.B, 1, 0);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            machine.StepperAct(CommandHeader.B, -1, 0);
        }

        private void button19_Click(object sender, EventArgs e)
        {
            int pos = (int)machine.CurrentPos;
            pos = pos + 1;
            if (pos > 5) pos = 5;
            CubePos newPos = (CubePos)pos;
            machine.PushPullPole(Pole.B, newPos);
        }

        private void button18_Click(object sender, EventArgs e)
        {
            int pos = (int)machine.CurrentPos;
            pos = pos - 1;
            if (pos < 0) pos = 0;
            CubePos newPos = (CubePos)pos;
            machine.PushPullPole(Pole.B, newPos);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            machine.StepperAct(CommandHeader.C, 1, 0);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            machine.StepperAct(CommandHeader.C, -1, 0);
        }

        private void button17_Click(object sender, EventArgs e)
        {
            int pos = (int)machine.CurrentPos;
            pos = pos + 1;
            if (pos > 5) pos = 5;
            CubePos newPos = (CubePos)pos;
            machine.PushPullPole(Pole.C, newPos);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            int pos = (int)machine.CurrentPos;
            pos = pos - 1;
            if (pos < 0) pos = 0;
            CubePos newPos = (CubePos)pos;
            machine.PushPullPole(Pole.C, newPos);
        }

        private void button28_Click(object sender, EventArgs e)
        {
            machine.PushPullPole(Pole.C, CubePos.Pos5);
        }

        private void button29_Click(object sender, EventArgs e)
        {
            machine.PushPullPole(Pole.C, CubePos.Pos0);
        }

        private void button30_Click(object sender, EventArgs e)
        {
            machine.PushPullPole(Pole.B, CubePos.Pos5);
        }

        private void button31_Click(object sender, EventArgs e)
        {
            machine.PushPullPole(Pole.B, CubePos.Pos0);
        }

        private void button46_Click(object sender, EventArgs e)
        {
            machine.PushPullPole(Pole.A, CubePos.Pos5);
        }

        private void button47_Click(object sender, EventArgs e)
        {
            machine.PushPullPole(Pole.A, CubePos.Pos0);
        }

        private void button33_Click(object sender, EventArgs e)
        {
            string t = textBox1.Text;
            t = t.Replace("'", "1");
            TwistType tt = Enum.Parse<TwistType>(t);

            machine.PerformATwist(tt);
        }

        private void button34_Click(object sender, EventArgs e)
        {
            machine.PullAllPoleToPos0();
        }

        private void button35_Click(object sender, EventArgs e)
        {
            if (videoCapture == null)
            {
                try
                {
                    videoCapture = new VideoCapture(0, VideoCapture.API.DShow);
                    var xx = videoCapture.Get(Emgu.CV.CvEnum.CapProp.Autofocus);
                    if (xx == 1)
                    {
                        checkBox1.Checked = true;
                    }
                    videoCapture.Set(Emgu.CV.CvEnum.CapProp.FrameWidth, CubeSolverConsts.FRAMEWIDTH);
                    videoCapture.Set(Emgu.CV.CvEnum.CapProp.FrameHeight, CubeSolverConsts.FRAMEHEIGHT);
                    videoCapture.Set(Emgu.CV.CvEnum.CapProp.Fps, 30);
                    //默认YUY2,高分辨率会很卡，使用MJPG，可以调fps到30，解决卡的问题
                    videoCapture.Set(Emgu.CV.CvEnum.CapProp.FourCC, VideoWriter.Fourcc('M', 'J', 'P', 'G'));
                    videoCapture.ImageGrabbed += VideoCapture_ImageGrabbed; ;
                    frame = new Mat();

                }
                catch (NullReferenceException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            if (videoCapture != null)
            {
                eclipsedTime = "";
                currentState = "";
                twistInfo = "";
                if (captureInProgress)
                {
                    button35.Text = "启动摄像头";
                    videoCapture.Pause();
                }
                else
                {
                    button35.Text = "关闭摄像头";
                    videoCapture.Start();
                }
                captureInProgress = !captureInProgress;
            }
        }

        private void VideoCapture_ImageGrabbed(object? sender, EventArgs e)
        {
            if (videoCapture != null && videoCapture.Ptr != IntPtr.Zero)
            {
                videoCapture.Retrieve(frame, 0);
                CvInvoke.Rectangle(frame, frameRectangle, new MCvScalar(255, 255, 255), 2);
                captureView = new Mat(frame, captureRectangle);
                if (showTimer)
                {
                    if (counting)
                    {
                        eclipsedTime = $"Time :{(DateTime.Now - starttime).ToString(@"mm\:ss\.ff")}";
                    }
                    CvInvoke.PutText(captureView, eclipsedTime, timerPosition, FontFace.HersheyComplex, 4, timerColor, 3);
                    CvInvoke.PutText(captureView, currentState, statePosition, FontFace.HersheyComplex, 3, stateColor, 2);
                    CvInvoke.PutText(captureView, twistInfo, twistinfoPosition, FontFace.HersheyComplex, 5, twistinfoColor, 4);
                }
                imageBox1.Image = captureView;
            }
        }

        private void ReleaseCapture()
        {
            if (videoCapture != null)
            {
                if (videoCapture.IsOpened)
                {
                    videoCapture.Stop();
                }
                videoCapture.Dispose();
            }
        }



        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (videoCapture != null)
            {
                if (checkBox1.Checked)
                {
                    videoCapture.Set(Emgu.CV.CvEnum.CapProp.Autofocus, 1);
                    hScrollBar1.Enabled = false;
                }
                else
                {
                    videoCapture.Set(Emgu.CV.CvEnum.CapProp.Autofocus, 0);
                    var xx = videoCapture.Get(Emgu.CV.CvEnum.CapProp.Focus);
                    hScrollBar1.Value = (int)xx;
                    hScrollBar1.Enabled = true;
                }
            }
        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            if (videoCapture != null && checkBox1.Checked == false)
            {
                videoCapture.Set(Emgu.CV.CvEnum.CapProp.Focus, hScrollBar1.Value);
            }
        }

        private Task<string> recognizeCubeColorAsync()
        {
            //capture1->image1->l->true
            //capture2->image2->f->true
            //capture3->image3->r->true
            //capture4->image4->b->true
            //capture5->image5->u->false
            //capture6->image6->d->true


            return Task.Run<string>(async () =>
            {
                CubeRecognizer cr = new CubeRecognizer();
                string[] facestring = new string[6];

                machine.PushPullPole(Pole.C, CubePos.Pos5);
                while (machine.Waiting) ;
                await Task.Delay(300);
                cr.Image1 = videoCapture.QueryFrame();
                facestring[4] = cr.RegonizeFace(cr.Image1, true, "l.jpg", filterImage);

                machine.RotateRotater(Rotater.Z, RotateDirection.CW, CubeSolverConsts.STEPS_OF_90DEGREE * CubeSolverConsts.MICROSTEP);
                while (machine.Waiting) ;
                await Task.Delay(300);
                cr.Image2 = videoCapture.QueryFrame();
                facestring[2] = cr.RegonizeFace(cr.Image2, true, "f.jpg", filterImage);

                machine.RotateRotater(Rotater.Z, RotateDirection.CW, CubeSolverConsts.STEPS_OF_90DEGREE * CubeSolverConsts.MICROSTEP);
                while (machine.Waiting) ;
                await Task.Delay(300);
                cr.Image3 = videoCapture.QueryFrame();
                facestring[1] = cr.RegonizeFace(cr.Image3, true, "r.jpg", filterImage);

                machine.RotateRotater(Rotater.Z, RotateDirection.CW, CubeSolverConsts.STEPS_OF_90DEGREE * CubeSolverConsts.MICROSTEP);
                while (machine.Waiting) ;
                await Task.Delay(300);
                cr.Image4 = videoCapture.QueryFrame();
                facestring[5] = cr.RegonizeFace(cr.Image4, true, "b.jpg", filterImage);

                machine.PushPullPole(Pole.C, CubePos.Pos0);
                while (machine.Waiting) ;
                machine.PushPullPole(Pole.B, CubePos.Pos5);
                while (machine.Waiting) ;
                machine.RotateRotater(Rotater.Y, RotateDirection.CW, CubeSolverConsts.STEPS_OF_90DEGREE * CubeSolverConsts.MICROSTEP);
                while (machine.Waiting) ;
                machine.PushPullPole(Pole.B, CubePos.Pos0);
                while (machine.Waiting) ;
                machine.PushPullPole(Pole.C, CubePos.Pos5);
                while (machine.Waiting) ;
                await Task.Delay(300);
                cr.Image5 = videoCapture.QueryFrame();
                facestring[0] = cr.RegonizeFace(cr.Image5, false, "u.jpg", filterImage);

                machine.RotateRotater(Rotater.Z, RotateDirection.CW, CubeSolverConsts.STEPS_OF_180DEGREE * CubeSolverConsts.MICROSTEP);
                while (machine.Waiting) ;
                await Task.Delay(300);
                cr.Image6 = videoCapture.QueryFrame();
                facestring[3] = cr.RegonizeFace(cr.Image6, true, "d.jpg", filterImage);


                return string.Join("\n", facestring);

            });

        }

        private async void button42_Click(object sender, EventArgs e)
        {
            if(!machine.Connected)
            {
                MessageBox.Show("Connect machine first!");
                return;
            }

            showTimer = true;
            eclipsedTime = "";
            starttime = DateTime.Now;
            currentState = "Color Recognizing......";
            counting = true;

            textBox2.Text = await recognizeCubeColorAsync();

            counting = false;
            currentState = "";

        }

        private void radioButton_CheckedChanged1(object sender, EventArgs e)
        {
            twistPrefix = (RadioButton)sender;
            textBox1.Text = twistPrefix.Text + twistPostfix.Text;
        }

        private void radioButton_CheckedChanged2(object sender, EventArgs e)
        {
            twistPostfix = (RadioButton)sender;
            textBox1.Text = twistPrefix.Text + twistPostfix.Text;
        }

        private void button36_Click(object sender, EventArgs e)
        {
            machine.StepperAct(CommandHeader.O, 1, 0);
        }

        private void button37_Click(object sender, EventArgs e)
        {
            machine.StepperAct(CommandHeader.O, 0, 0);
        }

        private void button38_Click(object sender, EventArgs e)
        {
            machine.StepperAct(CommandHeader.S, (int)numericUpDown1.Value, (int)numericUpDown2.Value);
        }

        private Task<string> findSolutionAsync(string cubestring)
        {
            return Task.Run<string>(() =>
            {
                String[] ret = new string[0];
                String result = string.Empty;
                cs.cube555.Search search = new cs.cube555.Search();
                cs.min2phase.Search search333 = new cs.min2phase.Search();
                cs.cube555.Search.init();

                ret = search.solveReduction(cubestring, 0);
                result = ret[0];
                if (!result.Contains("Error"))
                {
                    String solution333 = search333.getsolution(ret[1], 21, int.MaxValue, 500, 0);
                    result += solution333;
                }

                return result.Replace("  ", " ");
            });
        }

        private string checkCubestring()
        {
            string cubestring = textBox2.Text.Replace("\n", "");
            int[] urfdlb = new int[6];
            for (int i = 0; i < cubestring.Length; i++)
            {
                switch (cubestring[i])
                {
                    case 'U':
                        urfdlb[0] += 1;
                        break;
                    case 'R':
                        urfdlb[1] += 1;
                        break;
                    case 'F':
                        urfdlb[2] += 1;
                        break;
                    case 'D':
                        urfdlb[3] += 1;
                        break;
                    case 'L':
                        urfdlb[4] += 1;
                        break;
                    case 'B':
                        urfdlb[5] += 1;
                        break;
                }
            }
            if (urfdlb[0] != 25 || urfdlb[1] != 25 || urfdlb[2] != 25 || urfdlb[3] != 25 || urfdlb[4] != 25 || urfdlb[5] != 25)
            {
                return "";
            }
            return cubestring;
        }
        private async void button32_Click(object sender, EventArgs e)
        {
            showTimer = true;
            eclipsedTime = "";
            starttime = DateTime.Now;
            currentState = "Finding solution...";
            counting = true;

            string cubestring = checkCubestring();
            textBox3.Text = "";
            if (cubestring == "")
            {
                currentState = "Color error!";
            }
            else
            {
                textBox3.Text = await findSolutionAsync(cubestring);
                if (textBox3.Text.Contains("Error"))
                {
                    currentState = "Solution Error!";
                }
                currentState = "";
            }

            counting = false;
        }

        private void button40_Click(object sender, EventArgs e)
        {
            if (!machine.Connected)
            {
                MessageBox.Show("Connect machine first!");
                return;
            }

            int pos = (int)machine.CurrentPos;
            pos = pos + 2;
            if (pos > 5) pos = 5;
            CubePos newPos = (CubePos)pos;
            machine.PushPullPole(Pole.A, newPos);
        }

        private async void button39_Click(object sender, EventArgs e)
        {
            if (!machine.Connected)
            {
                MessageBox.Show("Connect machine first!");
                return;
            }


            showTimer = true;
            eclipsedTime = "";
            starttime = DateTime.Now;
            currentState = "Auto solving...";
            counting = true;

            counting = true;

            textBox2.Text = await recognizeCubeColorAsync();

            string cubestring = checkCubestring();
            textBox3.Text = "";
            if (cubestring == "")
            {
                currentState = "Color error!";
            }
            else
            {
                textBox3.Text = await findSolutionAsync(cubestring);
                if (textBox3.Text.Contains("Error"))
                {
                    currentState = "Solution Error!";
                }
                currentState = "";
            }

            cts = new CancellationTokenSource();

            string[] ss = textBox3.Text.Trim().Replace("  ", " ").Replace("'", "1").Split(" ");
            TwistType[] tts = new TwistType[ss.Length];
            for (int i = 0; i < ss.Length; i++)
            {
                tts[i] = Enum.Parse<TwistType>(ss[i]);
            }

            await machine.PerformTwistsAsync(tts, cts.Token);

            counting = false;
            currentState = "";
        }


        private async void button44_Click(object sender, EventArgs e)
        {
            if (!machine.Connected)
            {
                MessageBox.Show("Connect machine first!");
                return;
            }

            showTimer = true;
            eclipsedTime = "";
            starttime = DateTime.Now;
            currentState = "Performing solution...";
            counting = true;

            cts = new CancellationTokenSource();

            string[] ss = textBox3.Text.Trim().Replace("  ", " ").Replace("'", "1").Split(" ");
            TwistType[] tts = new TwistType[ss.Length];
            for (int i = 0; i < ss.Length; i++)
            {
                if (ss[i] != "")
                {
                    tts[i] = Enum.Parse<TwistType>(ss[i]);
                }
            }

            await machine.PerformTwistsAsync(tts, cts.Token);

            counting = false;
            currentState = "";
        }

        private void button41_Click(object sender, EventArgs e)
        {
            cts.Cancel();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            filterImage = checkBox2.Checked;
        }

        private async void button43_Click(object sender, EventArgs e)
        {
            if (!machine.Connected)
            {
                MessageBox.Show("Connect machine first!");
                return;
            }

            showTimer = true;
            eclipsedTime = "";
            starttime = DateTime.Now;
            currentState = "Scrambling cube...";
            counting = true;

            string randomcube = cs.cube555.Tools.randomCube();
            string scramble=await findSolutionAsync(randomcube);
            if (!scramble.Contains("Error"))
            {
                textBox3.Text = scramble;
            }

            cts = new CancellationTokenSource();

            string[] ss = textBox3.Text.Trim().Replace("  ", " ").Replace("'", "1").Split(" ");
            TwistType[] tts = new TwistType[ss.Length];
            for (int i = 0; i < ss.Length; i++)
            {
                tts[i] = Enum.Parse<TwistType>(ss[i]);
            }

            await machine.PerformTwistsAsync(tts, cts.Token);

            counting = false;
            currentState = "";

        }
    }
}