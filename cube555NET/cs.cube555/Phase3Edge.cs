using System;
using System.Collections.Generic;
using System.Text;

namespace cs.cube555
{

	/*
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

	class Phase3Edge
	{

		public int[] mEdge = new int[12];
		public int[] wEdge = new int[24];

		public Phase3Edge()
		{
			for (int i = 0; i < 12; i++)
			{
				mEdge[i] = 0;
				wEdge[i] = 0;
				wEdge[i + 12] = -1;
			}
		}

		public string toString()
		{
			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < 12; i++)
			{
				sb.Append(mEdge[i]).Append(' ');
			}
			sb.Append('|');
			for (int i = 0; i < 24; i++)
			{
				sb.Append(wEdge[i]).Append(' ');
			}
			return sb.ToString();
		}

		public void setMEdge(int idx)
		{
			int parity = 0;
			for (int i = 0; i < 11; i++)
			{
				mEdge[i] = idx & 1;
				idx >>= 1;
				parity ^= mEdge[i];
			}
			mEdge[11] = parity;
		}

		public int getMEdge()
		{
			int idx = 0;
			for (int i = 0; i < 11; i++)
			{
				idx |= mEdge[i] << i;
			}
			return idx;
		}

		public void setWEdge(int idx)
		{
			Util.setComb(wEdge, idx, 12);
		}

		public int getWEdge()
		{
			return Util.getComb(wEdge, 12);
		}

		public void doMove(int move)
		{
			move = Phase3Search.VALID_MOVES[move];
			int pow = move % 3;
			switch (move)
			{
				case Util.ux2:
					Util.swap(wEdge, 9, 22, 11, 20, pow);
					Util.swap(mEdge, 0, 4, 1, 5, pow);
					Util.swap(wEdge, 0, 4, 1, 5, pow);
					Util.swap(wEdge, 12, 16, 13, 17, pow);
					break;
				case Util.Ux1:
				case Util.Ux2:
				case Util.Ux3:
					Util.swap(mEdge, 0, 4, 1, 5, pow);
					Util.swap(wEdge, 0, 4, 1, 5, pow);
					Util.swap(wEdge, 12, 16, 13, 17, pow);
					break;
				case Util.rx2:
					Util.swap(wEdge, 1, 14, 3, 12, pow);
					Util.swap(mEdge, 5, 10, 6, 11, pow, true);
					Util.swap(wEdge, 5, 22, 6, 23, pow);
					Util.swap(wEdge, 17, 10, 18, 11, pow);
					break;
				case Util.Rx1:
				case Util.Rx2:
				case Util.Rx3:
					Util.swap(mEdge, 5, 10, 6, 11, pow, true);
					Util.swap(wEdge, 5, 22, 6, 23, pow);
					Util.swap(wEdge, 17, 10, 18, 11, pow);
					break;
				case Util.fx2:
					Util.swap(wEdge, 5, 18, 7, 16, pow);
					Util.swap(mEdge, 0, 11, 3, 8, pow);
					Util.swap(wEdge, 0, 11, 3, 8, pow);
					Util.swap(wEdge, 12, 23, 15, 20, pow);
					break;
				case Util.Fx1:
				case Util.Fx2:
				case Util.Fx3:
					Util.swap(mEdge, 0, 11, 3, 8, pow);
					Util.swap(wEdge, 0, 11, 3, 8, pow);
					Util.swap(wEdge, 12, 23, 15, 20, pow);
					break;
				case Util.dx2:
					Util.swap(wEdge, 8, 23, 10, 21, pow);
					Util.swap(mEdge, 2, 7, 3, 6, pow);
					Util.swap(wEdge, 2, 7, 3, 6, pow);
					Util.swap(wEdge, 14, 19, 15, 18, pow);
					break;
				case Util.Dx1:
				case Util.Dx2:
				case Util.Dx3:
					Util.swap(mEdge, 2, 7, 3, 6, pow);
					Util.swap(wEdge, 2, 7, 3, 6, pow);
					Util.swap(wEdge, 14, 19, 15, 18, pow);
					break;
				case Util.lx2:
					Util.swap(wEdge, 0, 15, 2, 13, pow);
					Util.swap(mEdge, 4, 8, 7, 9, pow, true);
					Util.swap(wEdge, 4, 20, 7, 21, pow);
					Util.swap(wEdge, 16, 8, 19, 9, pow);
					break;
				case Util.Lx1:
				case Util.Lx2:
				case Util.Lx3:
					Util.swap(mEdge, 4, 8, 7, 9, pow, true);
					Util.swap(wEdge, 4, 20, 7, 21, pow);
					Util.swap(wEdge, 16, 8, 19, 9, pow);
					break;
				case Util.bx2:
					Util.swap(wEdge, 4, 19, 6, 17, pow);
					Util.swap(mEdge, 1, 9, 2, 10, pow);
					Util.swap(wEdge, 1, 9, 2, 10, pow);
					Util.swap(wEdge, 13, 21, 14, 22, pow);
					break;
				case Util.Bx1:
				case Util.Bx2:
				case Util.Bx3:
					Util.swap(mEdge, 1, 9, 2, 10, pow);
					Util.swap(wEdge, 1, 9, 2, 10, pow);
					Util.swap(wEdge, 13, 21, 14, 22, pow);
					break;
			}
		}

		public void doConj(int conj)
		{
			switch (conj)
			{
				case 0: //x
					Util.swap(wEdge, 1, 14, 3, 12, 0);
					Util.swap(wEdge, 5, 22, 6, 23, 0);
					Util.swap(wEdge, 17, 10, 18, 11, 0);
					Util.swap(wEdge, 0, 15, 2, 13, 2);
					Util.swap(wEdge, 4, 20, 7, 21, 2);
					Util.swap(wEdge, 16, 8, 19, 9, 2);
					Util.swap(mEdge, 5, 10, 6, 11, 0, true);
					Util.swap(mEdge, 4, 8, 7, 9, 2, true);
					Util.swap(mEdge, 0, 1, 2, 3, 0, true);
					for (int i = 0; i < 12; i++)
					{
						mEdge[i] ^= 1;
						wEdge[i] = -1 - wEdge[i];
						wEdge[i + 12] = -1 - wEdge[i + 12];
					}
					break;
				case 1: //y2
					Util.swap(wEdge, 9, 22, 11, 20, 1);
					Util.swap(wEdge, 0, 4, 1, 5, 1);
					Util.swap(wEdge, 12, 16, 13, 17, 1);
					Util.swap(wEdge, 8, 23, 10, 21, 1);
					Util.swap(wEdge, 2, 7, 3, 6, 1);
					Util.swap(wEdge, 14, 19, 15, 18, 1);
					Util.swap(mEdge, 0, 4, 1, 5, 1);
					Util.swap(mEdge, 2, 7, 3, 6, 1);
					Util.swap(mEdge, 8, 9, 10, 11, 1, true);
					break;
				case 2: //lr2
					Util.swap(wEdge, 0, 12);
					Util.swap(wEdge, 1, 13);
					Util.swap(wEdge, 2, 14);
					Util.swap(wEdge, 3, 15);
					Util.swap(wEdge, 4, 17);
					Util.swap(wEdge, 5, 16);
					Util.swap(wEdge, 6, 19);
					Util.swap(wEdge, 7, 18);
					Util.swap(wEdge, 8, 23);
					Util.swap(wEdge, 9, 22);
					Util.swap(wEdge, 10, 21);
					Util.swap(wEdge, 11, 20);
					Util.swap(mEdge, 4, 5);
					Util.swap(mEdge, 6, 7);
					Util.swap(mEdge, 8, 11);
					Util.swap(mEdge, 9, 10);
					for (int i = 0; i < 24; i++)
					{
						wEdge[i] = -1 - wEdge[i];
					}
					break;
				case 3: //change lh edges
					for (int i = 0; i < 24; i++)
					{
						wEdge[i] = -1 - wEdge[i];
					}
					for (int i = 0; i < 12; i++)
					{
						mEdge[i] ^= 1;
					}
					break;
			}
		}
	}
}
