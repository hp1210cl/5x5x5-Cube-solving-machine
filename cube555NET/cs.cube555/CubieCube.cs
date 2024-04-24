using System;
using System.Collections.Generic;
using System.Text;

namespace cs.cube555
{

	/*

	Facelet:
							U1	U2	U3	U4	U5
							U6	U7	U8	U9	U10
							U11	U12	U13	U14	U15
							U16	U17	U18	U19	U20
							U21	U22	U23	U24	U25

	L1	L2	L3	L4	L5		F1	F2	F3	F4	F5		R1	R2	R3	R4	R5		B1	B2	B3	B4	B5
	L6	L7	L8	L9	L10		F6	F7	F8	F9	F10		R6	R7	R8	R9	R10		B6	B7	B8	B9	B10
	L11	L12	L13	L14	L15		F11	F12	F13	F14	F15		R11	R12	R13	R14	R15		B11	B12	B13	B14	B15
	L16	L17	L18	L19	L20		F16	F17	F18	F19	F20		R16	R17	R18	R19	R20		B16	B17	B18	B19	B20
	L21	L22	L23	L24	L25		F21	F22	F23	F24	F25		R21	R22	R23	R24	R25		B21	B22	B23	B24	B25

							D1	D2	D3	D4	D5
							D6	D7	D8	D9	D10
							D11	D12	D13	D14	D15
							D16	D17	D18	D19	D20
							D21	D22	D23	D24	D25

	Center:
			   0  0  1
			   3     1
			   3  2  2

	20 20 21   8  8  9    16 16 17   12 12  13
	23    21   11    9    19    17   15     13
	23 22 22   11 10 10   19 18 18   15 14  14

			   4  4  5
			   7     5
			   7  6  6

	Edge:
						13	1
					4			17
					16			5
						0	12
		4	16			0	12			5	17			1	13
	9			20	20			11	11			22	22			9
	21			8	8			23	23			10	10			21
		19	7			15	3			18	6			14	2
						15	3
					7			18
					19			6
						2	14
	 */

	public class CubieCube
	{
		// For pretty print
		static int[] PRINT_FACELET = new int[] {
		Util.U1, Util.U2, Util.U3,Util.U4,Util. U5,
		Util.U6, Util.U7, Util.U8,Util.U9,Util. U10,
		Util.U11,Util. U12, Util.U13, Util.U14, Util.U15,
		Util.U16,Util. U17, Util.U18, Util.U19, Util.U20,
		Util.U21,Util. U22, Util.U23, Util.U24, Util.U25,
		Util.L1, Util.L2, Util.L3, Util.L4,Util. L5, Util.F1, Util.F2, Util.F3, Util.F4, Util.F5,Util. R1,Util. R2,Util. R3, Util.R4, Util.R5, Util.B1, Util.B2, Util.B3, Util.B4, Util.B5,
		Util.L6, Util.L7, Util.L8, Util.L9,Util. L10,Util. F6, Util.F7, Util.F8, Util.F9, Util.F10, Util.R6,Util. R7, Util.R8, Util.R9, Util.R10, Util.B6, Util.B7, Util.B8, Util.B9, Util.B10,
		Util.L11,Util. L12, Util.L13, Util.L14, Util.L15, Util.F11, Util.F12, Util.F13, Util.F14, Util.F15, Util.R11, Util.R12, Util.R13, Util.R14, Util.R15, Util.B11, Util.B12, Util.B13, Util.B14, Util.B15,
		Util.L16,Util. L17, Util.L18, Util.L19, Util.L20, Util.F16, Util.F17, Util.F18, Util.F19, Util.F20, Util.R16, Util.R17, Util.R18, Util.R19, Util.R20, Util.B16, Util.B17, Util.B18, Util.B19, Util.B20,
		Util.L21,Util. L22, Util.L23, Util.L24, Util.L25, Util.F21, Util.F22, Util.F23, Util.F24, Util.F25, Util.R21, Util.R22, Util.R23, Util.R24, Util.R25, Util.B21, Util.B22, Util.B23, Util.B24, Util.B25,
		Util.D1, Util.D2, Util.D3, Util.D4, Util.D5,
		Util.D6, Util.D7, Util.D8, Util.D9, Util.D10,
		Util.D11, Util.D12, Util.D13, Util.D14, Util.D15,
		Util.D16, Util.D17, Util.D18, Util.D19, Util.D20,
		Util.D21, Util.D22, Util.D23, Util.D24, Util.D25
	};

		static int[] MAP333_FACELET = new int[] {
		Util.U1, Util.U3, Util.U5, Util.U11, Util.U13, Util.U15, Util.U21, Util.U23, Util.U25,
		Util.R1, Util.R3, Util.R5, Util.R11, Util.R13, Util.R15, Util.R21, Util.R23, Util.R25,
		Util.F1, Util.F3, Util.F5, Util.F11, Util.F13, Util.F15, Util.F21, Util.F23, Util.F25,
		Util.D1, Util.D3, Util.D5, Util.D11, Util.D13, Util.D15, Util.D21, Util.D23, Util.D25,
		Util.L1, Util.L3, Util.L5, Util.L11, Util.L13, Util.L15, Util.L21, Util.L23, Util.L25,
		Util.B1, Util.B3, Util.B5, Util.B11, Util.B13, Util.B15, Util.B21, Util.B23, Util.B25
	};

		static int[] TCENTER = new int[] {
		Util.U8, Util.U14, Util.U18, Util.U12,
		Util.D8, Util.D14, Util.D18, Util.D12,
		Util.F8, Util.F14, Util.F18, Util.F12,
		Util.B8, Util.B14, Util.B18, Util.B12,
		Util.R8, Util.R14, Util.R18, Util.R12,
		Util.L8, Util.L14, Util.L18, Util.L12
	};

		static int[] XCENTER = new int[] {
		Util.U7, Util.U9, Util.U19, Util.U17,
		Util.D7, Util.D9, Util.D19, Util.D17,
		Util.F7, Util.F9, Util.F19, Util.F17,
		Util.B7, Util.B9, Util.B19, Util.B17,
		Util.R7, Util.R9, Util.R19, Util.R17,
		Util.L7, Util.L9, Util.L19, Util.L17
	};

		static int[][] MEDGE = new int[][] {
		new int[]{Util.U23, Util.F3}, new int[]{Util.U3,Util. B3}, new int[]{Util.D23, Util.B23}, new int[]{Util.D3, Util.F23},
		new int[]{Util.U11, Util.L3}, new int[]{Util.U15, Util.R3}, new int[]{Util.D15, Util.R23}, new int[]{Util.D11, Util.L23},
		new int[]{Util.L15, Util.F11},new int[] {Util.L11,Util. B15}, new int[]{Util.R15, Util.B11}, new int[]{Util.R11, Util.F15}
	};

		static int[][] WEDGE = new int[][] {
		new int[]{Util.U22,Util. F2}, new int[]{Util.U4, Util.B2}, new int[]{Util.D22,Util. B24}, new int[]{Util.D4, Util.F24},
		new int[]{Util.U6, Util.L2}, new int[]{Util.U20, Util.R2}, new int[]{Util.D20, Util.R24}, new int[]{Util.D6, Util.L24},
		new int[]{Util.L20,Util. F16}, new int[]{Util.L6,Util. B10}, new int[]{Util.R20, Util.B16}, new int[]{Util.R6,Util. F10},
		new int[]{Util.F4, Util.U24}, new int[]{Util.B4, Util.U2}, new int[]{Util.B22, Util.D24}, new int[]{Util.F22, Util.D2},
		new int[]{Util.L4, Util.U16}, new int[]{Util.R4, Util.U10}, new int[]{Util.R22,Util. D10}, new int[]{Util.L22,Util. D16},
		new int[]{Util.F6, Util.L10}, new int[]{Util.B20,Util. L16}, new int[]{Util.B6,Util. R10}, new int[]{Util.F20,Util. R16}
	};

		static int[][] CORNER = new int[][] {
		new int[]{Util.U25,Util. R1, Util.F5}, new int[]{Util.U21, Util.F1, Util.L5}, new int[]{Util.U1, Util.L1, Util.B5}, new int[]{Util.U5, Util.B1, Util.R5},
		new int[]{Util.D5, Util.F25, Util.R21}, new int[]{Util.D1, Util.L25, Util.F21}, new int[]{Util.D21, Util.B25, Util.L21}, new int[]{Util.D25, Util.R25, Util.B21}
	};

		static CubieCube SOLVED = new CubieCube();

		public int[] tCenter = new int[24];
		public int[] xCenter = new int[24];
		public int[] mEdge = new int[12];
		public int[] wEdge = new int[24];
		public CornerCube corner = new CornerCube();

		public CubieCube()
		{
			for (int i = 0; i < 24; i++)
			{
				tCenter[i] = TCENTER[i] / 25;
				xCenter[i] = XCENTER[i] / 25;
				wEdge[i] = i;
			}
			for (int i = 0; i < 12; i++)
			{
				mEdge[i] = i << 1;
			}
		}

		CubieCube(CubieCube cc)
		{
			copy(cc);
		}

		public virtual void copy(CubieCube cc)
		{
			for (int i = 0; i < 24; i++)
			{
				tCenter[i] = cc.tCenter[i];
				xCenter[i] = cc.xCenter[i];
				wEdge[i] = cc.wEdge[i];
			}
			for (int i = 0; i < 12; i++)
			{
				mEdge[i] = cc.mEdge[i];
			}
		}

		public static string to333Facelet(string facelet)
		{
			StringBuilder sb = new StringBuilder();
			//for (int i : MAP333_FACELET)
			foreach (int i in MAP333_FACELET)
			{
				sb.Append(facelet[i]);
			}
			return sb.ToString();
		}

		static string fill333Facelet(string facelet, string facelet333)
		{
			StringBuilder sb = new StringBuilder(facelet);
			for (int i = 0; i < MAP333_FACELET.Length; i++)
			{
				sb[MAP333_FACELET[i]] = facelet333[i];
			}
			return sb.ToString();
		}

		public int fromFacelet(string facelet)
		{
			int[] face = new int[150];
			long colorCnt = 0;
			try
			{
				string colors = new string(
					new char[] {
					facelet[Util.U13], facelet[Util.R13], facelet[Util.F13],
					facelet[Util.D13], facelet[Util.L13], facelet[Util.B13]
					}
				);
				for (int i = 0; i < 150; i++)
				{
					face[i] = colors.IndexOf(facelet[i]);
					if (face[i] == -1)
					{
						return -1;
					}
					colorCnt += 1L << face[i] * 8;
				}
			}
			catch (Exception e)
			{
				return -1;
			}
			int tCenterCnt = 0;
			int xCenterCnt = 0;
			for (int i = 0; i < 24; i++)
			{
				tCenter[i] = face[TCENTER[i]];
				xCenter[i] = face[XCENTER[i]];
				tCenterCnt += 1 << (tCenter[i] << 2);
				xCenterCnt += 1 << (xCenter[i] << 2);
			}
			int mEdgeCnt = 0;
			int mEdgeChk = 0;
			int ori;
			for (int i = 0; i < 12; i++)
			{
				for (int j = 0; j < 12; j++)
				{
					if (face[MEDGE[i][0]] == MEDGE[j][0] / 25
							&& face[MEDGE[i][1]] == MEDGE[j][1] / 25
							|| face[MEDGE[i][0]] == MEDGE[j][1] / 25
							&& face[MEDGE[i][1]] == MEDGE[j][0] / 25
					   )
					{
						ori = face[MEDGE[i][0]] == MEDGE[j][0] / 25 ? 0 : 1;
						mEdge[i] = j << 1 | ori;
						mEdgeCnt |= 1 << j;
						mEdgeChk ^= ori;
						break;
					}
				}
			}
			int wEdgeCnt = 0;
			for (int i = 0; i < 24; i++)
			{
				for (int j = 0; j < 24; j++)
				{
					if (face[WEDGE[i][0]] == WEDGE[j][0] / 25
							&& face[WEDGE[i][1]] == WEDGE[j][1] / 25)
					{
						wEdge[i] = j;
						wEdgeCnt |= 1 << j;
						break;
					}
				}
			}
			ori = 0;
			int cornerCnt = 0;
			int cornerChk = 0;
			for (int i = 0; i < 8; i++)
			{
				for (ori = 0; ori < 3; ori++)
				{
					if (face[CORNER[i][ori]] == 0 || face[CORNER[i][ori]] == 3)
					{
						break;
					}
				}
				int col1 = face[CORNER[i][(ori + 1) % 3]];
				int col2 = face[CORNER[i][(ori + 2) % 3]];
				for (int j = 0; j < 8; j++)
				{
					if (col1 == CORNER[j][1] / 25 && col2 == CORNER[j][2] / 25)
					{
						corner.cp[i] = j;
						corner.co[i] = ori;
						cornerChk += ori;
						cornerCnt |= 1 << j;
						break;
					}
				}
			}
			int[] ep = new int[12];
			for (int i = 0; i < 12; i++)
			{
				ep[i] = mEdge[i] >> 1;
			}
			if (colorCnt != 0x191919191919L)
			{
				return -1;
			}
			else if (tCenterCnt != 0x444444)
			{
				return -2;
			}
			else if (xCenterCnt != 0x444444)
			{
				return -3;
			}
			else if (mEdgeCnt != 0xfff)
			{
				return -4;
			}
			else if (wEdgeCnt != 0xffffff)
			{
				return -5;
			}
			else if (cornerCnt != 0xff)
			{
				return -6;
			}
			else if (mEdgeChk != 0)
			{
				return -7;
			}
			else if (cornerChk % 3 != 0)
			{
				return -8;
			}
			else if (Util.getParity(ep) != Util.getParity(corner.cp))
			{
				return -9;
			}
			return 0;
		}

		public string toFacelet()
		{
			char[] face = new char[150];
			string colors = "URFDLB";
			for (int i = 0; i < 150; i++)
			{
				face[i] = i % 25 == 12 ? colors[i / 25] : '-';
			}
			for (int i = 0; i < 24; i++)
			{
				face[TCENTER[i]] = colors[tCenter[i]];
				face[XCENTER[i]] = colors[xCenter[i]];
				for (int j = 0; j < 2; j++)
				{
					face[WEDGE[i][j]] = colors[WEDGE[wEdge[i]][j] / 25];
				}
			}
			for (int i = 0; i < 12; i++)
			{
				int perm = mEdge[i] >> 1;
				int ori = mEdge[i] & 1;// Orientation of this cubie
				for (int j = 0; j < 2; j++)
				{
					face[MEDGE[i][j ^ ori]] = colors[MEDGE[perm][j] / 25];
				}
			}
			for (int i = 0; i < 8; i++)
			{
				int j = corner.cp[i];
				int ori = corner.co[i];
				for (int n = 0; n < 3; n++)
				{
					face[CORNER[i][(n + ori) % 3]] = colors[CORNER[j][n] / 25];
				}
			}
			return new string(face);
		}

		public override string ToString()
		{
			string facelet = toFacelet();
			//	string colors = "URFDLB-";
			//	string[] controls = new string [] {
			//	"\033[37m", "\033[31m", "\033[32m", "\033[33m", "\033[35m", "\033[34m", "\033[30m"
			//};
			//	string[] arr = new string [150];
			//	for (int i = 0; i < 150; i++)
			//	{
			//		char val = facelet[PRINT_FACELET[i]];
			//		arr[i] = controls[colors.IndexOf(val)] + " " + val + "\033[0m";
			//	}
			//	return string.Format(
			//			   "           %s%s%s%s%s\n" +
			//			   "           %s%s%s%s%s\n" +
			//			   "           %s%s%s%s%s\n" +
			//			   "           %s%s%s%s%s\n" +
			//			   "           %s%s%s%s%s\n" +
			//			   "%s%s%s%s%s %s%s%s%s%s %s%s%s%s%s %s%s%s%s%s\n" +
			//			   "%s%s%s%s%s %s%s%s%s%s %s%s%s%s%s %s%s%s%s%s\n" +
			//			   "%s%s%s%s%s %s%s%s%s%s %s%s%s%s%s %s%s%s%s%s\n" +
			//			   "%s%s%s%s%s %s%s%s%s%s %s%s%s%s%s %s%s%s%s%s\n" +
			//			   "%s%s%s%s%s %s%s%s%s%s %s%s%s%s%s %s%s%s%s%s\n" +
			//			   "           %s%s%s%s%s\n" +
			//			   "           %s%s%s%s%s\n" +
			//			   "           %s%s%s%s%s\n" +
			//			   "           %s%s%s%s%s\n" +
			//			   "           %s%s%s%s%s\033[0m\n",
			//			   (Object[])arr);
			StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 5; i++)
            {
				sb.Append($"      {facelet.Substring(i * 5, 5)}\n");
            }
            for (int i = 0; i < 5; i++)
            {
				sb.Append($"{facelet.Substring(25+i*5,5)} {facelet.Substring(50 + i * 5, 5)} {facelet.Substring(100 + i * 5, 5)} {facelet.Substring(125 + i * 5, 5)}\n");
            }
            for (int i = 0; i < 5; i++)
            {
				sb.Append($"      {facelet.Substring(75 + i * 5, 5)}\n");
            }
			return sb.ToString();
		}

		public void doCornerMove(params int[] moves)
		{
			//for (int move : moves)
			foreach (int move in moves)

			{
				corner.doMove(move % 18);
			}
		}

		public virtual void doMove(params int[] moves)
		{
			//for (int move : moves)
			foreach (int move in moves)
			{
				int pow = move % 3;
				switch (move)
				{
					case Util.ux1:
					case Util.ux2:
					case Util.ux3:
						Util.swap(xCenter, 8, 20, 12, 16, pow);
						Util.swap(xCenter, 9, 21, 13, 17, pow);
						Util.swap(tCenter, 8, 20, 12, 16, pow);
						Util.swap(wEdge, 9, 22, 11, 20, pow);
						Util.swap(xCenter, 0, 1, 2, 3, pow);
						Util.swap(tCenter, 0, 1, 2, 3, pow);
						Util.swap(mEdge, 0, 4, 1, 5, pow);
						Util.swap(wEdge, 0, 4, 1, 5, pow);
						Util.swap(wEdge, 12, 16, 13, 17, pow);
						break;
					case Util.Ux1:
					case Util.Ux2:
					case Util.Ux3:
						Util.swap(xCenter, 0, 1, 2, 3, pow);
						Util.swap(tCenter, 0, 1, 2, 3, pow);
						Util.swap(mEdge, 0, 4, 1, 5, pow);
						Util.swap(wEdge, 0, 4, 1, 5, pow);
						Util.swap(wEdge, 12, 16, 13, 17, pow);
						break;
					case Util.rx1:
					case Util.rx2:
					case Util.rx3:
						Util.swap(xCenter, 1, 15, 5, 9, pow);
						Util.swap(xCenter, 2, 12, 6, 10, pow);
						Util.swap(tCenter, 1, 15, 5, 9, pow);
						Util.swap(wEdge, 1, 14, 3, 12, pow);
						Util.swap(xCenter, 16, 17, 18, 19, pow);
						Util.swap(tCenter, 16, 17, 18, 19, pow);
						Util.swap(mEdge, 5, 10, 6, 11, pow, true);
						Util.swap(wEdge, 5, 22, 6, 23, pow);
						Util.swap(wEdge, 17, 10, 18, 11, pow);
						break;
					case Util.Rx1:
					case Util.Rx2:
					case Util.Rx3:
						Util.swap(xCenter, 16, 17, 18, 19, pow);
						Util.swap(tCenter, 16, 17, 18, 19, pow);
						Util.swap(mEdge, 5, 10, 6, 11, pow, true);
						Util.swap(wEdge, 5, 22, 6, 23, pow);
						Util.swap(wEdge, 17, 10, 18, 11, pow);
						break;
					case Util.fx1:
					case Util.fx2:
					case Util.fx3:
						Util.swap(xCenter, 2, 19, 4, 21, pow);
						Util.swap(xCenter, 3, 16, 5, 22, pow);
						Util.swap(tCenter, 2, 19, 4, 21, pow);
						Util.swap(wEdge, 5, 18, 7, 16, pow);
						Util.swap(xCenter, 8, 9, 10, 11, pow);
						Util.swap(tCenter, 8, 9, 10, 11, pow);
						Util.swap(mEdge, 0, 11, 3, 8, pow);
						Util.swap(wEdge, 0, 11, 3, 8, pow);
						Util.swap(wEdge, 12, 23, 15, 20, pow);
						break;
					case Util.Fx1:
					case Util.Fx2:
					case Util.Fx3:
						Util.swap(xCenter, 8, 9, 10, 11, pow);
						Util.swap(tCenter, 8, 9, 10, 11, pow);
						Util.swap(mEdge, 0, 11, 3, 8, pow);
						Util.swap(wEdge, 0, 11, 3, 8, pow);
						Util.swap(wEdge, 12, 23, 15, 20, pow);
						break;
					case Util.dx1:
					case Util.dx2:
					case Util.dx3:
						Util.swap(xCenter, 10, 18, 14, 22, pow);
						Util.swap(xCenter, 11, 19, 15, 23, pow);
						Util.swap(tCenter, 10, 18, 14, 22, pow);
						Util.swap(wEdge, 8, 23, 10, 21, pow);
						Util.swap(xCenter, 4, 5, 6, 7, pow);
						Util.swap(tCenter, 4, 5, 6, 7, pow);
						Util.swap(mEdge, 2, 7, 3, 6, pow);
						Util.swap(wEdge, 2, 7, 3, 6, pow);
						Util.swap(wEdge, 14, 19, 15, 18, pow);
						break;
					case Util.Dx1:
					case Util.Dx2:
					case Util.Dx3:
						Util.swap(xCenter, 4, 5, 6, 7, pow);
						Util.swap(tCenter, 4, 5, 6, 7, pow);
						Util.swap(mEdge, 2, 7, 3, 6, pow);
						Util.swap(wEdge, 2, 7, 3, 6, pow);
						Util.swap(wEdge, 14, 19, 15, 18, pow);
						break;
					case Util.lx1:
					case Util.lx2:
					case Util.lx3:
						Util.swap(xCenter, 0, 8, 4, 14, pow);
						Util.swap(xCenter, 3, 11, 7, 13, pow);
						Util.swap(tCenter, 3, 11, 7, 13, pow);
						Util.swap(wEdge, 0, 15, 2, 13, pow);
						Util.swap(xCenter, 20, 21, 22, 23, pow);
						Util.swap(tCenter, 20, 21, 22, 23, pow);
						Util.swap(mEdge, 4, 8, 7, 9, pow, true);
						Util.swap(wEdge, 4, 20, 7, 21, pow);
						Util.swap(wEdge, 16, 8, 19, 9, pow);
						break;
					case Util.Lx1:
					case Util.Lx2:
					case Util.Lx3:
						Util.swap(xCenter, 20, 21, 22, 23, pow);
						Util.swap(tCenter, 20, 21, 22, 23, pow);
						Util.swap(mEdge, 4, 8, 7, 9, pow, true);
						Util.swap(wEdge, 4, 20, 7, 21, pow);
						Util.swap(wEdge, 16, 8, 19, 9, pow);
						break;
					case Util.bx1:
					case Util.bx2:
					case Util.bx3:
						Util.swap(xCenter, 1, 20, 7, 18, pow);
						Util.swap(xCenter, 0, 23, 6, 17, pow);
						Util.swap(tCenter, 0, 23, 6, 17, pow);
						Util.swap(wEdge, 4, 19, 6, 17, pow);
						Util.swap(xCenter, 12, 13, 14, 15, pow);
						Util.swap(tCenter, 12, 13, 14, 15, pow);
						Util.swap(mEdge, 1, 9, 2, 10, pow);
						Util.swap(wEdge, 1, 9, 2, 10, pow);
						Util.swap(wEdge, 13, 21, 14, 22, pow);
						break;
					case Util.Bx1:
					case Util.Bx2:
					case Util.Bx3:
						Util.swap(xCenter, 12, 13, 14, 15, pow);
						Util.swap(tCenter, 12, 13, 14, 15, pow);
						Util.swap(mEdge, 1, 9, 2, 10, pow);
						Util.swap(wEdge, 1, 9, 2, 10, pow);
						Util.swap(wEdge, 13, 21, 14, 22, pow);
						break;
				}
			}
		}

		public virtual void doConj(int idx)
		{
			CubieCube a = new CubieCube(this);
			CubieCube sinv = CubeSym[SymMultInv[0][idx]];
			CubieCube s = CubeSym[idx];
			for (int i = 0; i < 12; i++)
			{
				this.mEdge[i] = sinv.mEdge[a.mEdge[s.mEdge[i] >> 1] >> 1]
								^ (a.mEdge[s.mEdge[i] >> 1] & 1)
								^ (s.mEdge[i] & 1);
			}
			for (int i = 0; i < 24; i++)
			{
				this.tCenter[i] = SOLVED.tCenter[sinv.tCenter[COLOR_TO_CENTER[a.tCenter[s.tCenter[i]]]]];
				this.xCenter[i] = SOLVED.xCenter[sinv.xCenter[COLOR_TO_CENTER[a.xCenter[s.xCenter[i]]]]];
				this.wEdge[i] = sinv.wEdge[a.wEdge[s.wEdge[i]]];
			}
		}

		static CubieCube[] CubeSym = new CubieCube[48];
		public static int[][] SymMult = new int[48][];
		public static int[][] SymMultInv = new int[48][];
		public static int[][] SymMove = new int[48][];

		static CubieCube()
		{
			//public static int[][] SymMult = new int[48][48];
			//public static int[][] SymMultInv = new int[48][48];
			//static int[][] SymMove = new int[48][36];
			for (int i = 0; i < 48; i++)
			{
				SymMult[i] = new int[48];
				SymMultInv[i] = new int[48];
				SymMove[i] = new int[36];

			}

		}

		static void CubeMult(CubieCube a, CubieCube b, CubieCube prod)
		{
			for (int i = 0; i < 12; i++)
			{
				prod.mEdge[i] = a.mEdge[b.mEdge[i] >> 1] ^ (b.mEdge[i] & 1);
			}
			for (int i = 0; i < 24; i++)
			{
				prod.tCenter[i] = a.tCenter[b.tCenter[i]];
				prod.xCenter[i] = a.xCenter[b.xCenter[i]];
				prod.wEdge[i] = a.wEdge[b.wEdge[i]];
			}
		}

		static int[] COLOR_TO_CENTER = new int[] { 0, 16, 8, 4, 20, 12 };

		public static void init()
		{
			CornerCube.initMove();
			CubieCube c = new CubieCube();
			for (int i = 0; i < 24; i++)
			{
				c.tCenter[i] = i;
				c.xCenter[i] = i;
			}
			for (int i = 0; i < 48; i++)
			{
				CubeSym[i] = new CubieCube(c);

				// x
				c.doMove(Util.rx1, Util.lx3);
				Util.swap(c.tCenter, 0, 14, 4, 8, 0);
				Util.swap(c.tCenter, 2, 12, 6, 10, 0);
				Util.swap(c.mEdge, 0, 1, 2, 3, 0, true);

				if ((i & 0x3) == 0x3)
				{
					// y2
					c.doMove(Util.ux2, Util.dx2);
					Util.swap(c.tCenter, 9, 21, 13, 17, 1);
					Util.swap(c.tCenter, 11, 23, 15, 19, 1);
					Util.swap(c.mEdge, 8, 9, 10, 11, 1, true);
				}
				if ((i & 0x7) == 0x7)
				{
					// lr mirror
					Util.swap(c.tCenter, 1, 3);
					Util.swap(c.tCenter, 5, 7);
					Util.swap(c.tCenter, 9, 11);
					Util.swap(c.tCenter, 13, 15);
					Util.swap(c.tCenter, 16, 20);
					Util.swap(c.tCenter, 17, 23);
					Util.swap(c.tCenter, 18, 22);
					Util.swap(c.tCenter, 19, 21);
					Util.swap(c.xCenter, 0, 1);
					Util.swap(c.xCenter, 2, 3);
					Util.swap(c.xCenter, 4, 5);
					Util.swap(c.xCenter, 6, 7);
					Util.swap(c.xCenter, 8, 9);
					Util.swap(c.xCenter, 10, 11);
					Util.swap(c.xCenter, 12, 13);
					Util.swap(c.xCenter, 14, 15);
					Util.swap(c.xCenter, 16, 21);
					Util.swap(c.xCenter, 17, 20);
					Util.swap(c.xCenter, 18, 23);
					Util.swap(c.xCenter, 19, 22);
					Util.swap(c.wEdge, 0, 12);
					Util.swap(c.wEdge, 1, 13);
					Util.swap(c.wEdge, 2, 14);
					Util.swap(c.wEdge, 3, 15);
					Util.swap(c.wEdge, 4, 17);
					Util.swap(c.wEdge, 5, 16);
					Util.swap(c.wEdge, 6, 19);
					Util.swap(c.wEdge, 7, 18);
					Util.swap(c.wEdge, 8, 23);
					Util.swap(c.wEdge, 9, 22);
					Util.swap(c.wEdge, 10, 21);
					Util.swap(c.wEdge, 11, 20);
					Util.swap(c.mEdge, 4, 5);
					Util.swap(c.mEdge, 6, 7);
					Util.swap(c.mEdge, 8, 11);
					Util.swap(c.mEdge, 9, 10);
				}
				if ((i & 0xf) == 0xf)
				{
					// URF -> RFU <=> x y
					c.doMove(Util.rx1, Util.lx3);
					Util.swap(c.tCenter, 0, 14, 4, 8, 0);
					Util.swap(c.tCenter, 2, 12, 6, 10, 0);
					Util.swap(c.mEdge, 0, 1, 2, 3, 0, true);
					c.doMove(Util.ux1, Util.dx3);
					Util.swap(c.tCenter, 9, 21, 13, 17, 0);
					Util.swap(c.tCenter, 11, 23, 15, 19, 0);
					Util.swap(c.mEdge, 8, 9, 10, 11, 0, true);
				}
			}
			for (int i = 0; i < 48; i++)
			{
				for (int j = 0; j < 48; j++)
				{
					CubeMult(CubeSym[i], CubeSym[j], c);
					for (int k = 0; k < 48; k++)
					{
						if (Arraysequals(CubeSym[k].wEdge, c.wEdge))
						{
							SymMult[i][j] = k;
							SymMultInv[k][j] = i;
							break;
						}
					}
				}
			}

			for (int move = 0; move < 36; move++)
			{
				for (int s = 0; s < 48; s++)
				{
					c = new CubieCube();
					c.doMove(move);
					c.doConj(SymMultInv[0][s]);
					for (int move2 = 0; move2 < 36; move2++)
					{
						CubieCube d = new CubieCube();
						d.doMove(move2);
						if (Arraysequals(c.wEdge, d.wEdge))
						{
							SymMove[s][move] = move2;
							break;
						}
					}
				}
			}
		}
		static bool Arraysequals(int[] a1, int[] a2)
		{
			for (int i = 0; i < a1.Length; i++)
			{
				if (a1[i] != a2[i])
				{
					return false;
				}
			}
			return true;
		}

		public static int[][] getSymMove(int[] moves, int nsym)
		{
			int[] symList = new int[nsym];
			for (int i = 0; i < nsym; i++)
			{
				symList[i] = i;
			}
			return getSymMove(moves, symList);
		}

		static int[][] getSymMove(int[] moves, int[] symList)
		{
			//int[][] ret = new int[symList.Length][moves.Length];
			int[][] ret = new int[symList.Length][];
			for (int i = 0; i < symList.Length; i++)
			{
				ret[i] = new int[moves.Length];
			}
			for (int s = 0; s < symList.Length; s++)
			{
				for (int m = 0; m < moves.Length; m++)
				{
					ret[s][m] = Util.indexOf(moves, SymMove[symList[s]][moves[m]]);
				}
			}
			return ret;
		}

		public class CornerCube
		{

			private static CornerCube[] moveCube = new CornerCube[18];

			public int[] cp = { 0, 1, 2, 3, 4, 5, 6, 7 };
			public int[] co = { 0, 0, 0, 0, 0, 0, 0, 0 };

			public CornerCube() { }

			public CornerCube(int cperm, int twist)
			{
				Util.setPerm(cp, cperm);
				int twst = 0;
				for (int i = 6; i >= 0; i--)
				{
					twst += co[i] = twist % 3;
					twist /= 3;
				}
				co[7] = (15 - twst) % 3;
			}

			CornerCube(CornerCube c)
			{
				copy(c);
			}

			public void copy(CornerCube c)
			{
				for (int i = 0; i < 8; i++)
				{
					this.cp[i] = c.cp[i];
					this.co[i] = c.co[i];
				}
			}

			static void CornMult(CornerCube a, CornerCube b, CornerCube prod)
			{
				for (int corn = 0; corn < 8; corn++)
				{
					prod.cp[corn] = a.cp[b.cp[corn]];
					prod.co[corn] = (a.co[b.cp[corn]] + b.co[corn]) % 3;
				}
			}

			public void doMove(int move)
			{
				CornerCube cc = new CornerCube();
				CornMult(this, moveCube[move], cc);
				copy(cc);
			}

			public static void initMove()
			{
				moveCube[0] = new CornerCube(15120, 0);
				moveCube[3] = new CornerCube(21021, 1494);
				moveCube[6] = new CornerCube(8064, 1236);
				moveCube[9] = new CornerCube(9, 0);
				moveCube[12] = new CornerCube(1230, 412);
				moveCube[15] = new CornerCube(224, 137);
				for (int a = 0; a < 18; a += 3)
				{
					for (int p = 0; p < 2; p++)
					{
						moveCube[a + p + 1] = new CornerCube();
						CornMult(moveCube[a + p], moveCube[a], moveCube[a + p + 1]);
					}
				}
			}
		}
	}
}
