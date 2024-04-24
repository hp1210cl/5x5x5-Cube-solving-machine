using System;
using System.Collections.Generic;
using System.Text;

namespace cs.min2phase
{
    class Util
    {
        //Moves
        static  byte Ux1 = 0;
        static  byte Ux2 = 1;
        static  byte Ux3 = 2;
        static  byte Rx1 = 3;
        static  byte Rx2 = 4;
        static  byte Rx3 = 5;
        static  byte Fx1 = 6;
        static  byte Fx2 = 7;
        static  byte Fx3 = 8;
        static  byte Dx1 = 9;
        static  byte Dx2 = 10;
        static  byte Dx3 = 11;
        static  byte Lx1 = 12;
        static  byte Lx2 = 13;
        static  byte Lx3 = 14;
        static  byte Bx1 = 15;
        static  byte Bx2 = 16;
        static  byte Bx3 = 17;

        //Facelets
        public static  byte U1 = 0;
        public static  byte U2 = 1;
        public static  byte U3 = 2;
        public static  byte U4 = 3;
        public static  byte U5 = 4;
        public static  byte U6 = 5;
        public static  byte U7 = 6;
        public static  byte U8 = 7;
        public static  byte U9 = 8;
        public static  byte R1 = 9;
        public static  byte R2 = 10;
        public static  byte R3 = 11;
        public static  byte R4 = 12;
        public static  byte R5 = 13;
        public static  byte R6 = 14;
        public static  byte R7 = 15;
        public static  byte R8 = 16;
        public static  byte R9 = 17;
        public static  byte F1 = 18;
        public static  byte F2 = 19;
        public static  byte F3 = 20;
        public static  byte F4 = 21;
        public static  byte F5 = 22;
        public static  byte F6 = 23;
        public static  byte F7 = 24;
        public static  byte F8 = 25;
        public static  byte F9 = 26;
        public static  byte D1 = 27;
        public static  byte D2 = 28;
        public static  byte D3 = 29;
        public static  byte D4 = 30;
        public static  byte D5 = 31;
        public static  byte D6 = 32;
        public static  byte D7 = 33;
        public static  byte D8 = 34;
        public static  byte D9 = 35;
        public static  byte L1 = 36;
        public static  byte L2 = 37;
        public static  byte L3 = 38;
        public static  byte L4 = 39;
        public static  byte L5 = 40;
        public static  byte L6 = 41;
        public static  byte L7 = 42;
        public static  byte L8 = 43;
        public static  byte L9 = 44;
        public static  byte B1 = 45;
        public static  byte B2 = 46;
        public static  byte B3 = 47;
        public static  byte B4 = 48;
        public static  byte B5 = 49;
        public static  byte B6 = 50;
        public static  byte B7 = 51;
        public static  byte B8 = 52;
        public static  byte B9 = 53;

        //Colors
        static  byte U = 0;
        static  byte R = 1;
        static  byte F = 2;
        static  byte D = 3;
        static  byte L = 4;
        static  byte B = 5;

        static  byte[][] cornerFacelet = {
            new byte[]{ U9, R1, F3 }, new byte[]{ U7, F1, L3 }, new byte[]{ U1, L1, B3 }, new byte[]{ U3, B1, R3 },
            new byte[]{ D3, F9, R7 }, new byte[]{ D1, L9, F7 }, new byte[]{ D7, B9, L7 }, new byte[]{ D9, R9, B7 }
        };
        static  byte[][] edgeFacelet = {
            new byte[]{ U6, R2 }, new byte[]{ U8, F2 }, new byte[]{ U4, L2 }, new byte[]{ U2, B2 }, new byte[]{ D6, R8 }, new byte[]{ D2, F8 },
            new byte[]{ D4, L8 }, new byte[]{ D8, B8 }, new byte[]{ F6, R4 }, new byte[]{ F4, L6 }, new byte[]{ B6, L4 }, new byte[]{ B4, R6 }
        };

        static int[][] Cnk = { new int[13], new int[13], new int[13], new int[13], new int[13], new int[13], new int[13],
            new int[13],new int[13],new int[13],new int[13],new int[13],new int[13]}; //new int[13][13];
        static  string [] move2str = {
            "U ", "U2", "U'", "R ", "R2", "R'", "F ", "F2", "F'",
            "D ", "D2", "D'", "L ", "L2", "L'", "B ", "B2", "B'"
        };
        public static int[] ud2std = { Ux1, Ux2, Ux3, Rx2, Fx2, Dx1, Dx2, Dx3, Lx2, Bx2, Rx1, Rx3, Fx1, Fx3, Lx1, Lx3, Bx1, Bx3 };
        public static int[] std2ud = new int[18];
        public static int[] ckmv2bit = new int[11];
        
        public class Solution
        {
            public static int length = 0;
            static int depth1 = 0;
            static int verbose = 0;
            static int urfIdx = 0;
            static int[] moves = new int[31];


            public void setArgs(int _verbose, int _urfIdx, int _depth1)
            {
                length = 0;
                verbose = _verbose;
                urfIdx = _urfIdx;
                depth1 = _depth1;
            }

            public void appendSolMove(int curMove)
            {
                if (length == 0)
                {
                    moves[length++] = curMove;
                    return;
                }
                int axisCur = curMove / 3;
                int axisLast = moves[length - 1] / 3;
                if (axisCur == axisLast)
                {
                    int pow = (curMove % 3 + moves[length - 1] % 3 + 1) % 4;
                    if (pow == 3)
                    {
                        length--;
                    }
                    else
                    {
                        moves[length - 1] = axisCur * 3 + pow;
                    }
                    return;
                }
                if (length > 1
                        && axisCur % 3 == axisLast % 3
                        && axisCur == moves[length - 2] / 3)
                {
                    int pow = (curMove % 3 + moves[length - 2] % 3 + 1) % 4;
                    if (pow == 3)
                    {
                        moves[length - 2] = moves[length - 1];
                        length--;
                    }
                    else
                    {
                        moves[length - 2] = axisCur * 3 + pow;
                    }
                    return;
                }
                moves[length++] = curMove;
            }

            public string toString()
            {
                //StringBuffer sb = new StringBuffer();
                StringBuilder sb = new StringBuilder();
                int urf = (verbose & Search.INVERSE_SOLUTION) != 0 ? (urfIdx + 3) % 6 : urfIdx;
                if (urf < 3)
                {
                    for (int s = 0; s < length; s++)
                    {
                        if ((verbose & Search.USE_SEPARATOR) != 0 && s == depth1)
                        {
                            sb.Append(".  ");
                        }
                        sb.Append(move2str[CubieCube.urfMove[urf][moves[s]]]).Append(' ');
                    }
                }
                else
                {
                    for (int s = length - 1; s >= 0; s--)
                    {
                        sb.Append(move2str[CubieCube.urfMove[urf][moves[s]]]).Append(' ');
                        if ((verbose & Search.USE_SEPARATOR) != 0 && s == depth1)
                        {
                            sb.Append(".  ");
                        }
                    }
                }
                if ((verbose & Search.APPEND_LENGTH) != 0)
                {
                    sb.Append("(").Append(length).Append("f)");
                }
                return sb.ToString();
            }
        }

        public static void toCubieCube(byte[] f, CubieCube ccRet)
        {
            byte ori;
            for (int i = 0; i < 8; i++)
            {
                ccRet.ca[i] = 0;
            }
            for (int i = 0; i < 12; i++)
            {
                ccRet.ea[i] = 0;
            }
            byte col1, col2;
            for (byte i = 0; i < 8; i++)
            {
                for (ori = 0; ori < 3; ori++)
                {
                    if (f[cornerFacelet[i][ori]] == U || f[cornerFacelet[i][ori]] == D)
                        break;
                }
                col1 = f[cornerFacelet[i][(ori + 1) % 3]];
                col2 = f[cornerFacelet[i][(ori + 2) % 3]];
                for (byte j = 0; j < 8; j++)
                {
                    if (col1 == cornerFacelet[j][1] / 9 && col2 == cornerFacelet[j][2] / 9)
                    {
                        ccRet.ca[i] = (byte)(ori % 3 << 3 | j);
                        break;
                    }
                }
            }
            for (byte i = 0; i < 12; i++)
            {
                for (byte j = 0; j < 12; j++)
                {
                    if (f[edgeFacelet[i][0]] == edgeFacelet[j][0] / 9
                            && f[edgeFacelet[i][1]] == edgeFacelet[j][1] / 9)
                    {
                        ccRet.ea[i] = (byte)(j << 1);
                        break;
                    }
                    if (f[edgeFacelet[i][0]] == edgeFacelet[j][1] / 9
                            && f[edgeFacelet[i][1]] == edgeFacelet[j][0] / 9)
                    {
                        ccRet.ea[i] = (byte)(j << 1 | 1);
                        break;
                    }
                }
            }
        }

        public static  string  toFaceCube(CubieCube cc)
        {
            char[] f = new char[54];
            char[] ts = { 'U', 'R', 'F', 'D', 'L', 'B' };
            for (int i = 0; i < 54; i++)
            {
                f[i] = ts[i / 9];
            }
            for (byte c = 0; c < 8; c++)
            {
                int j = cc.ca[c] & 0x7;
                int ori = cc.ca[c] >> 3;
                for (byte n = 0; n < 3; n++)
                {
                    f[cornerFacelet[c][(n + ori) % 3]] = ts[cornerFacelet[j][n] / 9];
                }
            }
            for (byte e = 0; e < 12; e++)
            {
                int j = cc.ea[e] >> 1;
                int ori = cc.ea[e] & 1;
                for (byte n = 0; n < 2; n++)
                {
                    f[edgeFacelet[e][(n + ori) % 2]] = ts[edgeFacelet[j][n] / 9];
                }
            }
            return new  string (f);
        }

       public static int getNParity(int idx, int n)
        {
            int p = 0;
            for (int i = n - 2; i >= 0; i--)
            {
                p ^= idx % (n - i);
                idx /= (n - i);
            }
            return p & 1;
        }

        static byte setVal(int val0, int val, bool isEdge)
        {
            return (byte)(isEdge ? (val << 1 | val0 & 1) : (val | val0 & ~7));
        }

        static int getVal(int val0, bool isEdge)
        {
            return isEdge ? val0 >> 1 : val0 & 7;
        }

        public static void setNPerm(byte[] arr, int idx, int n, bool isEdge)
        {
            long val = -81985529216486896;//=0xFEDCBA9876543210L
            long extract = 0;
            for (int p = 2; p <= n; p++)
            {
                extract = extract << 4 | idx % p;
                idx /= p;
            }
            for (int i = 0; i < n - 1; i++)
            {
                int v = ((int)extract & 0xf) << 2;
                extract >>= 4;
                arr[i] = setVal(arr[i], (int)(val >> v & 0xf), isEdge);
                long m = (1L << v) - 1;
                val = val & m | val >> 4 & ~m;
            }
            arr[n - 1] = setVal(arr[n - 1], (int)(val & 0xf), isEdge);
            //ulong val = 0xFEDCBA9876543210L;
            //long extract = 0;
            //for (int p = 2; p <= n; p++)
            //{
            //    extract = extract << 4 | idx % p;
            //    idx /= p;
            //}
            //for (int i = 0; i < n - 1; i++)
            //{
            //    int v = ((int)extract & 0xf) << 2;
            //    extract >>= 4;
            //    arr[i] = setVal(arr[i], (int)(val >> v & 0xf), isEdge);
            //    ulong m = (ulong)((1L << v) - 1);
            //    val = val & m | val >> 4 & ~m;
            //}
            //arr[n - 1] = setVal(arr[n - 1], (int)(val & 0xf), isEdge);
        }

        public static int getNPerm(byte[] arr, int n, bool isEdge)
        {
            int idx = 0;
            long val = -81985529216486896;//0xFEDCBA9876543210L;
            for (int i = 0; i < n - 1; i++)
            {
                int v = getVal(arr[i], isEdge) << 2;
                idx = (n - i) * idx + (int)(val >> v & 0xf);
                val -= 1229782938247303440 << v;//0x1111111111111110L
            }
            return idx;
            //int idx = 0;
            //ulong val = 0xFEDCBA9876543210L;
            //for (int i = 0; i < n - 1; i++)
            //{
            //    int v = getVal(arr[i], isEdge) << 2;
            //    idx = (n - i) * idx + (int)(val >> v & 0xf);
            //    val -= (ulong)(0x1111111111111110L << v);
            //}
            //return idx;
        }

        public static int getComb(byte[] arr, int mask, bool isEdge)
        {
            int end = arr.Length - 1;
            int idxC = 0, r = 4;
            for (int i = end; i >= 0; i--)
            {
                int perm = getVal(arr[i], isEdge);
                if ((perm & 0xc) == mask)
                {
                    idxC += Cnk[i][r--];
                }
            }
            return idxC;
        }

        public static void setComb(byte[] arr, int idxC, int mask, bool isEdge)
        {
            int end = arr.Length - 1;
            int r = 4, fill = end;
            for (int i = end; i >= 0; i--)
            {
                if (idxC >= Cnk[i][r])
                {
                    idxC -= Cnk[i][r--];
                    arr[i] = setVal(arr[i], r | mask, isEdge);
                }
                else
                {
                    if ((fill & 0xc) == mask)
                    {
                        fill -= 4;
                    }
                    arr[i] = setVal(arr[i], fill--, isEdge);
                }
            }
        }

        static Util(){
            for (int i = 0; i < 18; i++)
            {
                std2ud[ud2std[i]] = i;
            }
            for (int i = 0; i < 10; i++)
            {
                int ix = ud2std[i] / 3;
                ckmv2bit[i] = 0;
                for (int j = 0; j < 10; j++)
                {
                    int jx = ud2std[j] / 3;
                    ckmv2bit[i] |= ((ix == jx) || ((ix % 3 == jx % 3) && (ix >= jx)) ? 1 : 0) << j;
                }
            }
            ckmv2bit[10] = 0;
            for (int i = 0; i < 13; i++)
            {
                Cnk[i][0] = Cnk[i][i] = 1;
                for (int j = 1; j < i; j++)
                {
                    Cnk[i][j] = Cnk[i - 1][j - 1] + Cnk[i - 1][j];
                }
            }
        }
    }
}
