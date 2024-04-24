using System;
using System.Collections.Generic;
using System.Text;

namespace cs.cube555
{

	/*
					0	0	1
					3		1
					3	2	2

	7	7	4						3	3	0
	6		4						2		0
	6	5	5						2	1	1

					4	4	5
					7		5
					7	6	6
	*/

	class Phase4Center
	{

		public int[] udtCenter = new int[8];
		public int[] udxCenter = new int[8];
		public int[] rltCenter = new int[8];
		public int[] rlxCenter = new int[8];

		public Phase4Center()
		{
		}

		public string toString()
		{
			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < 8; i++)
			{
				sb.Append(udtCenter[i]).Append(' ');
			}
			sb.Append('|');
			for (int i = 0; i < 8; i++)
			{
				sb.Append(udxCenter[i]).Append(' ');
			}
			sb.Append('|');
			for (int i = 0; i < 8; i++)
			{
				sb.Append(rltCenter[i]).Append(' ');
			}
			sb.Append('|');
			for (int i = 0; i < 8; i++)
			{
				sb.Append(rlxCenter[i]).Append(' ');
			}
			return sb.ToString();
		}

		public void setUDCenter(int idx)
		{
			Util.setComb(udxCenter, idx / 70, 4);
			Util.setComb(udtCenter, idx % 70, 4);
		}

		public int getUDCenter()
		{
			return Util.getComb(udxCenter, 4) * 70 + Util.getComb(udtCenter, 4);
		}

		public void setRLCenter(int idx)
		{
			Util.setComb(rlxCenter, idx / 70, 4);
			Util.setComb(rltCenter, idx % 70, 4);
		}

		public int getRLCenter()
		{
			if (rlxCenter[7] != -1)
			{
				for (int i = 0; i < 8; i++)
				{
					rlxCenter[i] = -1 - rlxCenter[i];
				}
				for (int i = 0; i < 4; i++)
				{
					rltCenter[i << 1 | 1] = -1 - rltCenter[i << 1 | 1];
				}
			}
			return Util.getComb(rlxCenter, 4) * 70 + Util.getComb(rltCenter, 4);
		}

		public void doMove(int move)
		{
			move = Phase4Search.VALID_MOVES[move];
			int pow = move % 3;
			switch (move)
			{
				case Util.ux2:
					Util.swap(rltCenter, 3, 7);
					Util.swap(rlxCenter, 3, 7);
					Util.swap(rlxCenter, 0, 4);
					Util.swap(udtCenter, 0, 1, 2, 3, pow);
					Util.swap(udxCenter, 0, 1, 2, 3, pow);
					break;
				case Util.Ux1:
				case Util.Ux2:
				case Util.Ux3:
					Util.swap(udtCenter, 0, 1, 2, 3, pow);
					Util.swap(udxCenter, 0, 1, 2, 3, pow);
					break;
				case Util.rx2:
					Util.swap(udtCenter, 1, 5);
					Util.swap(udxCenter, 1, 5);
					Util.swap(udxCenter, 2, 6);
					Util.swap(rltCenter, 0, 1, 2, 3, pow);
					Util.swap(rlxCenter, 0, 1, 2, 3, pow);
					break;
				case Util.Rx2:
					Util.swap(rltCenter, 0, 1, 2, 3, pow);
					Util.swap(rlxCenter, 0, 1, 2, 3, pow);
					break;
				case Util.fx2:
					Util.swap(udtCenter, 2, 4);
					Util.swap(udxCenter, 2, 4);
					Util.swap(udxCenter, 3, 5);
					Util.swap(rltCenter, 2, 4);
					Util.swap(rlxCenter, 2, 4);
					Util.swap(rlxCenter, 3, 5);
					break;
				case Util.Fx2:
					break;
				case Util.dx2:
					Util.swap(rltCenter, 1, 5);
					Util.swap(rlxCenter, 1, 5);
					Util.swap(rlxCenter, 2, 6);
					Util.swap(udtCenter, 4, 5, 6, 7, pow);
					Util.swap(udxCenter, 4, 5, 6, 7, pow);
					break;
				case Util.Dx1:
				case Util.Dx2:
				case Util.Dx3:
					Util.swap(udtCenter, 4, 5, 6, 7, pow);
					Util.swap(udxCenter, 4, 5, 6, 7, pow);
					break;
				case Util.lx2:
					Util.swap(udtCenter, 3, 7);
					Util.swap(udxCenter, 3, 7);
					Util.swap(udxCenter, 0, 4);
					Util.swap(rltCenter, 4, 5, 6, 7, pow);
					Util.swap(rlxCenter, 4, 5, 6, 7, pow);
					break;
				case Util.Lx2:
					Util.swap(rltCenter, 4, 5, 6, 7, pow);
					Util.swap(rlxCenter, 4, 5, 6, 7, pow);
					break;
				case Util.bx2:
					Util.swap(udtCenter, 0, 6);
					Util.swap(udxCenter, 0, 6);
					Util.swap(udxCenter, 1, 7);
					Util.swap(rltCenter, 0, 6);
					Util.swap(rlxCenter, 0, 6);
					Util.swap(rlxCenter, 1, 7);
					break;
				case Util.Bx2:
					break;
			}
		}

		public void doConj(int conj)
		{
			switch (conj)
			{
				case 0: //x2
					Util.swap(udtCenter, 0, 4);
					Util.swap(udtCenter, 1, 5);
					Util.swap(udtCenter, 2, 6);
					Util.swap(udtCenter, 3, 7);
					Util.swap(udxCenter, 0, 4);
					Util.swap(udxCenter, 1, 5);
					Util.swap(udxCenter, 2, 6);
					Util.swap(udxCenter, 3, 7);
					Util.swap(rltCenter, 0, 1, 2, 3, 1);
					Util.swap(rltCenter, 4, 5, 6, 7, 1);
					Util.swap(rlxCenter, 0, 1, 2, 3, 1);
					Util.swap(rlxCenter, 4, 5, 6, 7, 1);
					for (int i = 0; i < 8; i++)
					{
						udtCenter[i] = -1 - udtCenter[i];
						udxCenter[i] = -1 - udxCenter[i];
					}
					break;
				case 1: //y2
					Util.swap(rltCenter, 0, 4);
					Util.swap(rltCenter, 1, 5);
					Util.swap(rltCenter, 2, 6);
					Util.swap(rltCenter, 3, 7);
					Util.swap(rlxCenter, 0, 4);
					Util.swap(rlxCenter, 1, 5);
					Util.swap(rlxCenter, 2, 6);
					Util.swap(rlxCenter, 3, 7);
					Util.swap(udtCenter, 0, 1, 2, 3, 1);
					Util.swap(udxCenter, 0, 1, 2, 3, 1);
					Util.swap(udtCenter, 4, 5, 6, 7, 1);
					Util.swap(udxCenter, 4, 5, 6, 7, 1);
					for (int i = 0; i < 8; i++)
					{
						rltCenter[i] = -1 - rltCenter[i];
						rlxCenter[i] = -1 - rlxCenter[i];
					}
					break;
				case 2: //lr mirror
					Util.swap(udtCenter, 1, 3);
					Util.swap(udtCenter, 5, 7);
					Util.swap(udxCenter, 0, 1);
					Util.swap(udxCenter, 2, 3);
					Util.swap(udxCenter, 4, 5);
					Util.swap(udxCenter, 6, 7);
					Util.swap(rltCenter, 0, 6);
					Util.swap(rltCenter, 1, 5);
					Util.swap(rltCenter, 2, 4);
					Util.swap(rltCenter, 3, 7);
					Util.swap(rlxCenter, 0, 7);
					Util.swap(rlxCenter, 1, 6);
					Util.swap(rlxCenter, 2, 5);
					Util.swap(rlxCenter, 3, 4);
					for (int i = 0; i < 8; i++)
					{
						rltCenter[i] = -1 - rltCenter[i];
						rlxCenter[i] = -1 - rlxCenter[i];
					}
					break;
			}
		}
	}
}
