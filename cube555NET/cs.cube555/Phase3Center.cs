using System;
using System.Collections.Generic;
using System.Text;

namespace cs.cube555
{

	/*

	7	7	4						3	3	0
	6		4						2		0
	6	5	5						2	1	1
	*/

	class Phase3Center
	{

		static int[] SOLVED_XCENTER = new int[] { 0, 9, 14, 23, 27, 28 };
		static int[] SOLVED_TCENTER = new int[] { 0, 2, 4, 5, 7, 9, 11, 12, 14, 16, 18, 23, 25, 27, 28, 30, 32, 34 };
		public static int[] SOLVED_CENTER = new int[108];
		static Phase3Center()
		{
			for (int i = 0; i < 6; i++)
			{
				for (int j = 0; j < 18; j++)
				{
					SOLVED_CENTER[i * 18 + j] = SOLVED_XCENTER[i] * 35 + SOLVED_TCENTER[j];
				}
			}
		}

		public int[] tCenter = new int[8];
		public int[] xCenter = new int[8];

		public Phase3Center()
		{
		}

		public string toString()
		{
			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < 8; i++)
			{
				sb.Append(tCenter[i]).Append(' ');
			}
			sb.Append('|');
			for (int i = 0; i < 8; i++)
			{
				sb.Append(xCenter[i]).Append(' ');
			}
			return sb.ToString();
		}

		public void setCenter(int idx)
		{
			Util.setComb(xCenter, idx / 35, 4);
			Util.setComb(tCenter, idx % 35, 4);
		}

		public int getCenter()
		{
			return Util.getSComb(xCenter) * 35 + Util.getSComb(tCenter);
		}

		public void doMove(int move)
		{
			move = Phase3Search.VALID_MOVES[move];
			int pow = move % 3;
			switch (move)
			{
				case Util.ux2:
					Util.swap(tCenter, 3, 7);
					Util.swap(xCenter, 3, 7);
					Util.swap(xCenter, 0, 4);
					break;
				case Util.Ux1:
				case Util.Ux2:
				case Util.Ux3:
					break;
				case Util.rx2:
				case Util.Rx1:
				case Util.Rx2:
				case Util.Rx3:
					Util.swap(tCenter, 0, 1, 2, 3, pow);
					Util.swap(xCenter, 0, 1, 2, 3, pow);
					break;
				case Util.fx2:
					Util.swap(tCenter, 2, 4);
					Util.swap(xCenter, 2, 4);
					Util.swap(xCenter, 3, 5);
					break;
				case Util.Fx1:
				case Util.Fx2:
				case Util.Fx3:
					break;
				case Util.dx2:
					Util.swap(tCenter, 1, 5);
					Util.swap(xCenter, 1, 5);
					Util.swap(xCenter, 2, 6);
					break;
				case Util.Dx1:
				case Util.Dx2:
				case Util.Dx3:
					break;
				case Util.lx2:
				case Util.Lx1:
				case Util.Lx2:
				case Util.Lx3:
					Util.swap(tCenter, 4, 5, 6, 7, pow);
					Util.swap(xCenter, 4, 5, 6, 7, pow);
					break;
				case Util.bx2:
					Util.swap(tCenter, 0, 6);
					Util.swap(xCenter, 0, 6);
					Util.swap(xCenter, 1, 7);
					break;
				case Util.Bx2:
					break;
			}
		}

		void doConj(int conj)
		{
			switch (conj)
			{
				case 0: //x
					doMove(Util.Rx1);
					doMove(Util.Lx3);
					break;
				case 1: //y2
					Util.swap(tCenter, 0, 4);
					Util.swap(tCenter, 1, 5);
					Util.swap(tCenter, 2, 6);
					Util.swap(tCenter, 3, 7);
					Util.swap(xCenter, 0, 4);
					Util.swap(xCenter, 1, 5);
					Util.swap(xCenter, 2, 6);
					Util.swap(xCenter, 3, 7);
					break;
				case 2: //lr2
					Util.swap(tCenter, 0, 6);
					Util.swap(tCenter, 1, 5);
					Util.swap(tCenter, 2, 4);
					Util.swap(tCenter, 3, 7);
					Util.swap(xCenter, 0, 7);
					Util.swap(xCenter, 1, 6);
					Util.swap(xCenter, 2, 5);
					Util.swap(xCenter, 3, 4);
					break;
			}
		}
	}
}
