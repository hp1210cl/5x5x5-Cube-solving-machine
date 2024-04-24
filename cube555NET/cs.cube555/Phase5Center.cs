using System;
using System.Collections.Generic;
using System.Text;

namespace cs.cube555
{

	/*
					0	0	1
					3		1
					3	2	2

	7		6		1		0		5		4		3		2

					4	4	5
					7		5
					7	6	6
	*/

	class Phase5Center
	{
		public int[] tCenter = new int[8];
		public int[] xCenter = new int[8];
		public int[] rflbCenter = new int[8];

		public Phase5Center()
		{
			setRFLBCenter(0);
			setXCenter(0);
			setTCenter(0);
		}

		public void setRFLBCenter(int idx)
		{
			int[] fbCenter = new int[4];
			int[] rlCenter = new int[4];
			Util.setComb(fbCenter, idx % 6, 2);
			Util.setComb(rlCenter, idx / 6, 2);
			for (int i = 0; i < 4; i++)
			{
				rflbCenter[i] = fbCenter[i];
				rflbCenter[i + 4] = rlCenter[i];
			}
		}

		public int getRFLBCenter()
		{
			int[] fbCenter = new int[4];
			int[] rlCenter = new int[4];
			for (int i = 0; i < 4; i++)
			{
				fbCenter[i] = rflbCenter[i];
				rlCenter[i] = rflbCenter[i + 4];
			}
			return Util.getComb(rlCenter, 2) * 6 + Util.getComb(fbCenter, 2);
		}

		public void setXCenter(int idx)
		{
			Util.setComb(xCenter, idx, 4);
		}

		public int getXCenter()
		{
			return Util.getComb(xCenter, 4);
		}

		public void setTCenter(int idx)
		{
			Util.setComb(tCenter, idx, 4);
		}

		public int getTCenter()
		{
			return Util.getComb(tCenter, 4);
		}

		public void doMove(int move)
		{
			move = Phase5Search.VALID_MOVES[move];
			int pow = move % 3;
			switch (move)
			{
				case Util.Ux1:
				case Util.Ux2:
				case Util.Ux3:
					Util.swap(tCenter, 0, 1, 2, 3, pow);
					Util.swap(xCenter, 0, 1, 2, 3, pow);
					break;
				case Util.rx2:
					Util.swap(tCenter, 1, 5);
					Util.swap(xCenter, 1, 5);
					Util.swap(xCenter, 2, 6);
					Util.swap(rflbCenter, 0, 3);
					Util.swap(rflbCenter, 4, 5);
					break;
				case Util.Rx2:
					Util.swap(rflbCenter, 4, 5);
					break;
				case Util.fx2:
					Util.swap(tCenter, 2, 4);
					Util.swap(xCenter, 2, 4);
					Util.swap(xCenter, 3, 5);
					Util.swap(rflbCenter, 5, 6);
					Util.swap(rflbCenter, 0, 1);
					break;
				case Util.Fx2:
					Util.swap(rflbCenter, 0, 1);
					break;
				case Util.Dx1:
				case Util.Dx2:
				case Util.Dx3:
					Util.swap(tCenter, 4, 5, 6, 7, pow);
					Util.swap(xCenter, 4, 5, 6, 7, pow);
					break;
				case Util.lx2:
					Util.swap(tCenter, 3, 7);
					Util.swap(xCenter, 3, 7);
					Util.swap(xCenter, 0, 4);
					Util.swap(rflbCenter, 1, 2);
					Util.swap(rflbCenter, 6, 7);
					break;
				case Util.Lx2:
					Util.swap(rflbCenter, 6, 7);
					break;
				case Util.bx2:
					Util.swap(tCenter, 0, 6);
					Util.swap(xCenter, 0, 6);
					Util.swap(xCenter, 1, 7);
					Util.swap(rflbCenter, 4, 7);
					Util.swap(rflbCenter, 2, 3);
					break;
				case Util.Bx2:
					Util.swap(rflbCenter, 2, 3);
					break;
			}
		}

		public void doConj(int conj)
		{
			switch (conj)
			{
				case 0: //y
					Util.swap(tCenter, 0, 1, 2, 3, 0);
					Util.swap(xCenter, 0, 1, 2, 3, 0);
					Util.swap(tCenter, 4, 5, 6, 7, 2);
					Util.swap(xCenter, 4, 5, 6, 7, 2);
					for (int i = 0; i < 4; i++)
					{
						rflbCenter[i] = -1 - rflbCenter[i];
					}
					Util.swap(rflbCenter, 1, 7, 3, 5, 0);
					Util.swap(rflbCenter, 0, 6, 2, 4, 0);
					break;
				case 1: //x2
					Util.swap(tCenter, 0, 4);
					Util.swap(tCenter, 1, 5);
					Util.swap(tCenter, 2, 6);
					Util.swap(tCenter, 3, 7);
					Util.swap(xCenter, 0, 4);
					Util.swap(xCenter, 1, 5);
					Util.swap(xCenter, 2, 6);
					Util.swap(xCenter, 3, 7);
					Util.swap(rflbCenter, 0, 3);
					Util.swap(rflbCenter, 4, 5);
					Util.swap(rflbCenter, 1, 2);
					Util.swap(rflbCenter, 6, 7);
					for (int i = 0; i < 8; i++)
					{
						tCenter[i] = -1 - tCenter[i];
						xCenter[i] = -1 - xCenter[i];
					}
					for (int i = 0; i < 4; i++)
					{
						rflbCenter[i] = -1 - rflbCenter[i];
					}
					break;
				case 2: //lr mirror
					Util.swap(tCenter, 1, 3);
					Util.swap(tCenter, 5, 7);
					Util.swap(xCenter, 0, 1);
					Util.swap(xCenter, 2, 3);
					Util.swap(xCenter, 4, 5);
					Util.swap(xCenter, 6, 7);
					Util.swap(rflbCenter, 0, 1);
					Util.swap(rflbCenter, 2, 3);
					Util.swap(rflbCenter, 4, 7);
					Util.swap(rflbCenter, 5, 6);
					for (int i = 4; i < 8; i++)
					{
						rflbCenter[i] = -1 - rflbCenter[i];
					}
					break;
			}
		}
	}
}
