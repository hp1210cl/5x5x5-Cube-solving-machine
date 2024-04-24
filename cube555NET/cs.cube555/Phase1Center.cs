using System;
using System.Collections.Generic;
using System.Text;

namespace cs.cube555
{

	/*
			   0  0  1
			   3     1
			   3  2  2

	20 20 21   8  8  9    16 16 17   12 12  13
	23    21   11    9    19    17   15     13
	23 22 22   11 10 10   19 18 18   15 14  14

			   4  4  5
			   7     5
			   7  6  6
	*/

	class Phase1Center
	{

		public int[] tCenter = new int[24];
		public int[] xCenter = new int[24];

		public Phase1Center()
		{
			setTCenter(0);
			setXCenter(0);
		}

		public void setTCenter(int idx)
		{
			Util.setComb(tCenter, 735470 - idx, 8);
		}

		public int getTCenter()
		{
			return 735470 - Util.getComb(tCenter, 8);
		}

		public void setXCenter(int idx)
		{
			Util.setComb(xCenter, 735470 - idx, 8);
		}

		public int getXCenter()
		{
			return 735470 - Util.getComb(xCenter, 8);
		}

		public void doMove(int move)
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
					Util.swap(xCenter, 0, 1, 2, 3, pow);
					Util.swap(tCenter, 0, 1, 2, 3, pow);
					break;
				case Util.Ux1:
				case Util.Ux2:
				case Util.Ux3:
					Util.swap(xCenter, 0, 1, 2, 3, pow);
					Util.swap(tCenter, 0, 1, 2, 3, pow);
					break;
				case Util.rx1:
				case Util.rx2:
				case Util.rx3:
					Util.swap(xCenter, 1, 15, 5, 9, pow);
					Util.swap(xCenter, 2, 12, 6, 10, pow);
					Util.swap(tCenter, 1, 15, 5, 9, pow);
					Util.swap(xCenter, 16, 17, 18, 19, pow);
					Util.swap(tCenter, 16, 17, 18, 19, pow);
					break;
				case Util.Rx1:
				case Util.Rx2:
				case Util.Rx3:
					Util.swap(xCenter, 16, 17, 18, 19, pow);
					Util.swap(tCenter, 16, 17, 18, 19, pow);
					break;
				case Util.fx1:
				case Util.fx2:
				case Util.fx3:
					Util.swap(xCenter, 2, 19, 4, 21, pow);
					Util.swap(xCenter, 3, 16, 5, 22, pow);
					Util.swap(tCenter, 2, 19, 4, 21, pow);
					Util.swap(xCenter, 8, 9, 10, 11, pow);
					Util.swap(tCenter, 8, 9, 10, 11, pow);
					break;
				case Util.Fx1:
				case Util.Fx2:
				case Util.Fx3:
					Util.swap(xCenter, 8, 9, 10, 11, pow);
					Util.swap(tCenter, 8, 9, 10, 11, pow);
					break;
				case Util.dx1:
				case Util.dx2:
				case Util.dx3:
					Util.swap(xCenter, 10, 18, 14, 22, pow);
					Util.swap(xCenter, 11, 19, 15, 23, pow);
					Util.swap(tCenter, 10, 18, 14, 22, pow);
					Util.swap(xCenter, 4, 5, 6, 7, pow);
					Util.swap(tCenter, 4, 5, 6, 7, pow);
					break;
				case Util.Dx1:
				case Util.Dx2:
				case Util.Dx3:
					Util.swap(xCenter, 4, 5, 6, 7, pow);
					Util.swap(tCenter, 4, 5, 6, 7, pow);
					break;
				case Util.lx1:
				case Util.lx2:
				case Util.lx3:
					Util.swap(xCenter, 0, 8, 4, 14, pow);
					Util.swap(xCenter, 3, 11, 7, 13, pow);
					Util.swap(tCenter, 3, 11, 7, 13, pow);
					Util.swap(xCenter, 20, 21, 22, 23, pow);
					Util.swap(tCenter, 20, 21, 22, 23, pow);
					break;
				case Util.Lx1:
				case Util.Lx2:
				case Util.Lx3:
					Util.swap(xCenter, 20, 21, 22, 23, pow);
					Util.swap(tCenter, 20, 21, 22, 23, pow);
					break;
				case Util.bx1:
				case Util.bx2:
				case Util.bx3:
					Util.swap(xCenter, 1, 20, 7, 18, pow);
					Util.swap(xCenter, 0, 23, 6, 17, pow);
					Util.swap(tCenter, 0, 23, 6, 17, pow);
					Util.swap(xCenter, 12, 13, 14, 15, pow);
					Util.swap(tCenter, 12, 13, 14, 15, pow);
					break;
				case Util.Bx1:
				case Util.Bx2:
				case Util.Bx3:
					Util.swap(xCenter, 12, 13, 14, 15, pow);
					Util.swap(tCenter, 12, 13, 14, 15, pow);
					break;
			}
		}

		public void doConj(int conj)
		{
			switch (conj)
			{
				case 0: //x
					doMove(Util.rx1);
					doMove(Util.lx3);
					Util.swap(tCenter, 0, 14, 4, 8, 0);
					Util.swap(tCenter, 2, 12, 6, 10, 0);

					break;
				case 1: //y2
					doMove(Util.ux2);
					doMove(Util.dx2);
					Util.swap(tCenter, 9, 21, 13, 17, 1);
					Util.swap(tCenter, 11, 23, 15, 19, 1);
					break;
				case 2: //lr mirror
					Util.swap(tCenter, 1, 3);
					Util.swap(tCenter, 5, 7);
					Util.swap(tCenter, 9, 11);
					Util.swap(tCenter, 13, 15);
					Util.swap(tCenter, 16, 20);
					Util.swap(tCenter, 17, 23);
					Util.swap(tCenter, 18, 22);
					Util.swap(tCenter, 19, 21);
					Util.swap(xCenter, 0, 1);
					Util.swap(xCenter, 2, 3);
					Util.swap(xCenter, 4, 5);
					Util.swap(xCenter, 6, 7);
					Util.swap(xCenter, 8, 9);
					Util.swap(xCenter, 10, 11);
					Util.swap(xCenter, 12, 13);
					Util.swap(xCenter, 14, 15);
					Util.swap(xCenter, 16, 21);
					Util.swap(xCenter, 17, 20);
					Util.swap(xCenter, 18, 23);
					Util.swap(xCenter, 19, 22);
					break;
			}
		}
	}
}
