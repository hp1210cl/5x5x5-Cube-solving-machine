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

			15	3
		7			18
		19			6
			2	14
	 */

	class Phase5Edge
	{

		public int[] mEdge = new int[8];
		public int[] lEdge = new int[8]; // 0 ~ 7
		public int[] hEdge = new int[8]; // 12 ~ 19
		public bool isStd = true;

		public Phase5Edge()
		{
			for (int i = 0; i < 8; i++)
			{
				mEdge[i] = i;
				hEdge[i] = i;
				lEdge[i] = i;
			}
		}

		public void setLEdge(int idx)
		{
			standardlize();
			Util.setPerm(lEdge, idx);
		}

		void setHEdge(int idx)
		{
			standardlize();
			Util.setPerm(hEdge, idx);
		}

		public int getLEdge()
		{
			standardlize();
			return Util.getPerm(lEdge);
		}

		public int getHEdge()
		{
			standardlize();
			return Util.getPerm(hEdge);
		}

		void standardlize()
		{
			if (isStd)
			{
				return;
			}
			int[] mEdgeInv = new int[8];
			for (int i = 0; i < 8; i++)
			{
				mEdgeInv[mEdge[i]] = i;
			}

			for (int i = 0; i < 8; i++)
			{
				mEdge[i] = i;
				lEdge[i] = mEdgeInv[lEdge[i]];
				hEdge[i] = mEdgeInv[hEdge[i]];
			}
			isStd = true;
		}

		public void doMove(int move)
		{
			move = Phase5Search.VALID_MOVES[move];
			int pow = move % 3;
			isStd = false;
			switch (move)
			{
				case Util.Ux1:
				case Util.Ux2:
				case Util.Ux3:
					Util.swap(mEdge, 0, 4, 1, 5, pow);
					Util.swap(lEdge, 0, 4, 1, 5, pow);
					Util.swap(hEdge, 0, 4, 1, 5, pow);
					break;
				case Util.rx2:
					Util.swap(lEdge, 1, 3);
					Util.swap(hEdge, 0, 2);
					Util.swap(mEdge, 5, 6);
					Util.swap(lEdge, 5, 6);
					Util.swap(hEdge, 5, 6);
					break;
				case Util.Rx2:
					Util.swap(mEdge, 5, 6);
					Util.swap(lEdge, 5, 6);
					Util.swap(hEdge, 5, 6);
					break;
				case Util.fx2:
					Util.swap(lEdge, 5, 7);
					Util.swap(hEdge, 4, 6);
					Util.swap(mEdge, 0, 3);
					Util.swap(lEdge, 0, 3);
					Util.swap(hEdge, 0, 3);
					break;
				case Util.Fx2:
					Util.swap(mEdge, 0, 3);
					Util.swap(lEdge, 0, 3);
					Util.swap(hEdge, 0, 3);
					break;
				case Util.Dx1:
				case Util.Dx2:
				case Util.Dx3:
					Util.swap(mEdge, 2, 7, 3, 6, pow);
					Util.swap(lEdge, 2, 7, 3, 6, pow);
					Util.swap(hEdge, 2, 7, 3, 6, pow);
					break;
				case Util.lx2:
					Util.swap(lEdge, 0, 2);
					Util.swap(hEdge, 1, 3);
					Util.swap(mEdge, 4, 7);
					Util.swap(lEdge, 4, 7);
					Util.swap(hEdge, 4, 7);
					break;
				case Util.Lx2:
					Util.swap(mEdge, 4, 7);
					Util.swap(lEdge, 4, 7);
					Util.swap(hEdge, 4, 7);
					break;
				case Util.bx2:
					Util.swap(lEdge, 4, 6);
					Util.swap(hEdge, 5, 7);
					Util.swap(mEdge, 1, 2);
					Util.swap(lEdge, 1, 2);
					Util.swap(hEdge, 1, 2);
					break;
				case Util.Bx2:
					Util.swap(mEdge, 1, 2);
					Util.swap(lEdge, 1, 2);
					Util.swap(hEdge, 1, 2);
					break;
			}
		}

		public void doConj(int conj)
		{
			isStd = false;
			switch (conj)
			{
				case 0: //y
					Util.swap(mEdge, 0, 4, 1, 5, 0);
					Util.swap(lEdge, 0, 4, 1, 5, 0);
					Util.swap(hEdge, 0, 4, 1, 5, 0);
					Util.swap(mEdge, 2, 7, 3, 6, 2);
					Util.swap(lEdge, 2, 7, 3, 6, 2);
					Util.swap(hEdge, 2, 7, 3, 6, 2);
					break;
				case 1: //x2
					Util.swap(mEdge, 0, 2);
					Util.swap(lEdge, 0, 2);
					Util.swap(hEdge, 0, 2);
					Util.swap(mEdge, 1, 3);
					Util.swap(lEdge, 1, 3);
					Util.swap(hEdge, 1, 3);
					Util.swap(mEdge, 4, 7);
					Util.swap(hEdge, 4, 7);
					Util.swap(lEdge, 4, 7);
					Util.swap(mEdge, 5, 6);
					Util.swap(hEdge, 5, 6);
					Util.swap(lEdge, 5, 6);
					break;
				case 2: //lr mirror
					for (int i = 0; i < 8; i++)
					{
						int tmp = lEdge[i];
						lEdge[i] = hEdge[i];
						hEdge[i] = tmp;
					}
					Util.swap(mEdge, 4, 5);
					Util.swap(lEdge, 4, 5);
					Util.swap(hEdge, 4, 5);
					Util.swap(mEdge, 6, 7);
					Util.swap(lEdge, 6, 7);
					Util.swap(hEdge, 6, 7);
					break;
			}
		}
	}
}
