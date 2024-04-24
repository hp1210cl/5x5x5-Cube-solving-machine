using cs.cube555;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

	class Phase1Search : PhaseSearch
	{

		public new static int[] VALID_MOVES = new int[] {
		Util.Ux1, Util.Ux2, Util.Ux3, Util.Rx1, Util.Rx2, Util.Rx3, Util.Fx1, Util.Fx2, Util.Fx3, Util.Dx1, Util.Dx2, Util.Dx3, Util.Lx1, Util.Lx2, Util.Lx3, Util.Bx1, Util.Bx2, Util.Bx3,
		Util.ux1, Util.ux2, Util.ux3, Util.rx1, Util.rx2, Util.rx3, Util.fx1, Util.fx2, Util.fx3, Util.dx1, Util.dx2, Util.dx3, Util.lx1, Util.lx2, Util.lx3, Util.bx1, Util.bx2, Util.bx3
	};

		static Util.PruningTable TCenterPrun;
		static Util.PruningTable XCenterPrun;
		public static Util.PruningTable CenterSymPrun;

		public static Util.PruningTable TCenterSymPrun;
		public static Util.PruningTable XCenterSymPrun;
		public static int[] TCenterSym2RawF;
		public static int[] TCenterRaw2Sym;
		public static int[][] TCenterSymMove;
		public static int[] XCenterSym2Raw;
		public static int[] XCenterRaw2Sym;
		public static int[][] XCenterSymMove;

		public static int[][] SymMove;

		public static void init()
		{
			initCenter();
		}

		static void initCenter()
		{
			Phase1Center center = new Phase1Center();
			int symCnt = 0;
			TCenterSym2RawF = new int[46935 * 16];
			TCenterRaw2Sym = new int[735471];
			int[] TCenterSelfSym = new int[46935];
			for (int i = 0; i < TCenterRaw2Sym.Length; i++)
			{
				if (TCenterRaw2Sym[i] != 0)
				{
					continue;
				}
				center.setTCenter(i);
				for (int sym = 0; sym < 16; sym++)
				{
					int idx = center.getTCenter();
					TCenterRaw2Sym[idx] = symCnt << 4 | sym;
					TCenterSym2RawF[symCnt << 4 | sym] = idx;
					if (idx == i)
					{
						TCenterSelfSym[symCnt] |= 1 << sym;
					}
					center.doConj(0);
					if ((sym & 3) == 3)
					{
						center.doConj(1);
					}
					if ((sym & 7) == 7)
					{
						center.doConj(2);
					}
				}
				symCnt++;
			}
			//TCenterSymMove = new int[symCnt][VALID_MOVES.Length];
			TCenterSymMove = new int[symCnt][];
			for (int i = 0; i < symCnt; i++)
			{
				TCenterSymMove[i] = new int[VALID_MOVES.Length];

			}
			for (int i = 0; i < symCnt; i++)
			{
				for (int m = 0; m < VALID_MOVES.Length; m++)
				{
					center.setTCenter(TCenterSym2RawF[i << 4]);
					center.doMove(m);
					TCenterSymMove[i][m] = TCenterRaw2Sym[center.getTCenter()];
				}
			}

			symCnt = 0;
			XCenterSym2Raw = new int[46371];
			XCenterRaw2Sym = new int[735471];
			int[] XCenterSelfSym = new int[46371];
			for (int i = 0; i < XCenterRaw2Sym.Length; i++)
			{
				if (XCenterRaw2Sym[i] != 0)
				{
					continue;
				}
				center.setXCenter(i);
				for (int sym = 0; sym < 16; sym++)
				{
					int idx = center.getXCenter();
					XCenterRaw2Sym[idx] = symCnt << 4 | sym;
					if (idx == i)
					{
						XCenterSelfSym[symCnt] |= 1 << sym;
					}
					center.doConj(0);
					if ((sym & 3) == 3)
					{
						center.doConj(1);
					}
					if ((sym & 7) == 7)
					{
						center.doConj(2);
					}
				}
				XCenterSym2Raw[symCnt] = i;
				symCnt++;
			}
			//XCenterSymMove = new int[symCnt][VALID_MOVES.Length];
			XCenterSymMove = new int[symCnt][];
			for (int i = 0; i < symCnt; i++)
			{
				XCenterSymMove[i] = new int[VALID_MOVES.Length];

			}
			for (int i = 0; i < symCnt; i++)
			{
				for (int m = 0; m < VALID_MOVES.Length; m++)
				{
					center.setXCenter(XCenterSym2Raw[i]);
					center.doMove(m);
					XCenterSymMove[i][m] = XCenterRaw2Sym[center.getXCenter()];
				}
			}

			Util.SymCoord XCenterSymCoord = new Util.TableSymCoord(XCenterSymMove, XCenterSelfSym, 16);

			TCenterSymPrun = new Util.PruningTable(
				new Util.TableSymCoord(TCenterSymMove, TCenterSelfSym, 16),
				null, "Phase1TCenterSym");

			XCenterSymPrun = new Util.PruningTable(
				XCenterSymCoord,
				null, "Phase1XCenterSym");

			SymMove = CubieCube.getSymMove(VALID_MOVES, 16);

			CenterSymPrun = new Util.PruningTable(XCenterSymCoord, new RawCoord1(), null, 7, 1 << 26, "Phase1CenterSym");
		}

         class RawCoord1 : Util.RawCoord
        {
            public RawCoord1()
            {
                N_IDX = 735471;
            }
            public override void set(int idx)
            {
                this.idx = TCenterRaw2Sym[idx];
            }
            public override int getMoved(int move)
            {
                int ret = TCenterSymMove[idx >> 4][SymMove[idx & 0xf][move]];
                ret = ret & ~0xf | CubieCube.SymMult[ret & 0xf][idx & 0xf];
                return TCenterSym2RawF[ret];
            }
            public override int getConj(int idx, int conj)
            {
                idx = TCenterRaw2Sym[idx];
                idx = idx & ~0xf | CubieCube.SymMultInv[idx & 0xf][conj];
                return TCenterSym2RawF[idx];
            }
        }


        class Phase1Node : Node
		{
			public int tCenter;
			public int xCenter;
			public override int getPrun()
			{
				return Math.Max(
						   Math.Max(
							   TCenterSymPrun.getPrun(tCenter >> 4),
							   XCenterSymPrun.getPrun(xCenter >> 4)),
						   CenterSymPrun.getPrun(xCenter >> 4, TCenterSym2RawF[tCenter & ~0xf | CubieCube.SymMultInv[tCenter & 0xf][xCenter & 0xf]]));
			}
			public override int doMovePrun(Node node0, int move, int maxl)
			{
				Phase1Node node = (Phase1Node)node0;

				tCenter = TCenterSymMove[node.tCenter >> 4][SymMove[node.tCenter & 0xf][move]];
				tCenter = tCenter & ~0xf | CubieCube.SymMult[tCenter & 0xf][node.tCenter & 0xf];

				xCenter = XCenterSymMove[node.xCenter >> 4][SymMove[node.xCenter & 0xf][move]];
				xCenter = xCenter & ~0xf | CubieCube.SymMult[xCenter & 0xf][node.xCenter & 0xf];

				return getPrun();
			}
		}

		public Phase1Search()
		{
			base.VALID_MOVES = VALID_MOVES;
			base.MIN_BACK_DEPTH = 5;
			for (int i = 0; i < searchNode.Length; i++)
			{
				searchNode[i] = new Phase1Node();
			}
		}

        //public override Node[] initFrom(CubieCube cc)

        public override List<Node> initFrom(CubieCube cc)
        {
            if (SymMove == null)
			{
				SymMove = CubieCube.getSymMove(VALID_MOVES, 16);
			}
			Phase1Center ct = new Phase1Center();
			for (int i = 0; i < 24; i++)
			{
				ct.xCenter[i] = cc.xCenter[i] == 1 || cc.xCenter[i] == 4 ? 0 : -1;
				ct.tCenter[i] = cc.tCenter[i] == 1 || cc.tCenter[i] == 4 ? 0 : -1;
			}
			Phase1Node node = new Phase1Node();
			node.xCenter = XCenterRaw2Sym[ct.getXCenter()];
			node.tCenter = TCenterRaw2Sym[ct.getTCenter()];
            //return new Node[] { node };
            return new List<Node>() { node };
        }
    }
}
