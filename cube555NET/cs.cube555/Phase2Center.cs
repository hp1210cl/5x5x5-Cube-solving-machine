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

class Phase2Center
	{

		public static int[] eParityDiff = new int[] {
		0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
		0, 1, 0, 1, 0, 0, 1, 0, 1, 0
	};

		public int[] tCenter = new int[16];
		public int[] xCenter = new int[16];
		int eParity = 0;

		public void setTCenter(int idx)
		{
			Util.setComb(tCenter, idx, 8);
		}

		public int getTCenter()
		{
			return Util.getComb(tCenter, 8);
		}

		public void setXCenter(int idx)
		{
			Util.setComb(xCenter, idx, 8);
		}

		public int getXCenter()
		{
			return Util.getComb(xCenter, 8);
		}

		void setEParity(int idx)
		{
			eParity = idx;
		}

		int getEParity()
		{
			return eParity;
		}

		public void doMove(int move)
		{
			eParity ^= eParityDiff[move];
			move = Phase2Search.VALID_MOVES[move];
			int axis = move / 3;
			int pow = move % 3;
			switch (axis)
			{
				case 6: //Uw
					Util.swap(xCenter, 8, 12);
					Util.swap(xCenter, 9, 13);
					Util.swap(tCenter, 8, 12);
					Util.swap(xCenter, 0, 1, 2, 3, pow);
					Util.swap(tCenter, 0, 1, 2, 3, pow);
					break;
				case 0: //U
					Util.swap(xCenter, 0, 1, 2, 3, pow);
					Util.swap(tCenter, 0, 1, 2, 3, pow);
					break;
				case 7: //Rw
					Util.swap(xCenter, 1, 15, 5, 9, pow);
					Util.swap(xCenter, 2, 12, 6, 10, pow);
					Util.swap(tCenter, 1, 15, 5, 9, pow);
					break;
				case 1: //R
					break;
				case 8: //Fw
					Util.swap(xCenter, 2, 4);
					Util.swap(xCenter, 3, 5);
					Util.swap(tCenter, 2, 4);
					Util.swap(xCenter, 8, 9, 10, 11, pow);
					Util.swap(tCenter, 8, 9, 10, 11, pow);
					break;
				case 2: //F
					Util.swap(xCenter, 8, 9, 10, 11, pow);
					Util.swap(tCenter, 8, 9, 10, 11, pow);
					break;
				case 9: //Dw
					Util.swap(xCenter, 10, 14);
					Util.swap(xCenter, 11, 15);
					Util.swap(tCenter, 10, 14);
					Util.swap(xCenter, 4, 5, 6, 7, pow);
					Util.swap(tCenter, 4, 5, 6, 7, pow);
					break;
				case 3: //D
					Util.swap(xCenter, 4, 5, 6, 7, pow);
					Util.swap(tCenter, 4, 5, 6, 7, pow);
					break;
				case 10: //Lw
					Util.swap(xCenter, 0, 8, 4, 14, pow);
					Util.swap(xCenter, 3, 11, 7, 13, pow);
					Util.swap(tCenter, 3, 11, 7, 13, pow);
					break;
				case 4: //L
					break;
				case 11: //Bw
					Util.swap(xCenter, 1, 7);
					Util.swap(xCenter, 0, 6);
					Util.swap(tCenter, 0, 6);
					Util.swap(xCenter, 12, 13, 14, 15, pow);
					Util.swap(tCenter, 12, 13, 14, 15, pow);
					break;
				case 5: //B
					Util.swap(xCenter, 12, 13, 14, 15, pow);
					Util.swap(tCenter, 12, 13, 14, 15, pow);
					break;
			}
		}
	}
}
