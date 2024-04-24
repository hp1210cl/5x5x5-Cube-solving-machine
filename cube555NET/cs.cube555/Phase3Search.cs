using System;
using System.Collections.Generic;
using System.Text;

namespace cs.cube555
{

	class Phase3Search : PhaseSearch
	{

		public new static int[] VALID_MOVES = new int[] {
		Util.Ux1, Util.Ux2, Util.Ux3, Util.Rx1, Util.Rx2, Util.Rx3, Util.Fx1, Util.Fx2, Util.Fx3, Util.Dx1, Util.Dx2, Util.Dx3, Util.Lx1, Util.Lx2, Util.Lx3, Util.Bx1, Util.Bx2, Util.Bx3,
		Util.ux2, Util.rx2, Util.fx2, Util.dx2, Util.lx2, Util.bx2
	};

		static int[][] SymMove;

		static long[] SKIP_MOVES = Util.genSkipMoves(VALID_MOVES);
		static int NEXT_AXIS = 0x12492;

		static int[][] CenterMove;
		static int[][] MEdgeMove;
		static int[][] MEdgeConj;
		static Util.PruningTable CenterMEdgePrun;

		static int[][] WEdgeSymMove;
		static int[] WEdgeSym2Raw;
		static int[] WEdgeSelfSym;
		static int[] WEdgeRaw2Sym;

		public static void init()
		{
			initWEdgeSymMove();
			initMEdgeMove();
			initCenterMove();
			initPrun();
		}

		static void initWEdgeSymMove()
		{
			Phase3Edge edge = new Phase3Edge();
			int symCnt = 0;
			WEdgeSym2Raw = new int[86048];
			WEdgeSelfSym = new int[86048];
			WEdgeRaw2Sym = new int[2704156];
			for (int i = 0; i < WEdgeRaw2Sym.Length; i++)
			{
				if (WEdgeRaw2Sym[i] != 0)
				{
					continue;
				}
				edge.setWEdge(i);
				for (int sym = 0; sym < 32; sym++)
				{
					int idx = edge.getWEdge();
					WEdgeRaw2Sym[idx] = symCnt << 5 | sym;
					if (idx == i)
					{
						WEdgeSelfSym[symCnt] |= 1 << sym;
					}
					edge.doConj(0);
					if ((sym & 3) == 3)
					{
						edge.doConj(1);
					}
					if ((sym & 7) == 7)
					{
						edge.doConj(2);
					}
					if ((sym & 0xf) == 0xf)
					{
						edge.doConj(3);
					}
				}
				WEdgeSym2Raw[symCnt] = i;
				symCnt++;
			}
			//WEdgeSymMove = new int[symCnt][VALID_MOVES.Length];
			WEdgeSymMove = new int[symCnt][];
			for (int i = 0; i < symCnt; i++)
			{
				WEdgeSymMove[i] = new int[VALID_MOVES.Length];
			}
			for (int i = 0; i < symCnt; i++)
			{
				for (int m = 0; m < VALID_MOVES.Length; m++)
				{
					edge.setWEdge(WEdgeSym2Raw[i]);
					edge.doMove(m);
					WEdgeSymMove[i][m] = WEdgeRaw2Sym[edge.getWEdge()];
				}
			}
		}

		static void initMEdgeMove()
		{
			Phase3Edge edge = new Phase3Edge();
			//MEdgeMove = new int[2048][VALID_MOVES.Length];
			//MEdgeConj = new int[2048][32];
			MEdgeMove = new int[2048][];
			MEdgeConj = new int[2048][];
			for (int i = 0; i < 2048; i++)
			{
				MEdgeMove[i] = new int[VALID_MOVES.Length];
				MEdgeConj[i] = new int[32];

			}
			for (int i = 0; i < 2048; i++)
			{
				for (int m = 0; m < VALID_MOVES.Length; m++)
				{
					edge.setMEdge(i);
					edge.doMove(m);
					MEdgeMove[i][m] = edge.getMEdge();
				}

				edge.setMEdge(i);
				for (int sym = 0; sym < 32; sym++)
				{
					MEdgeConj[i][CubieCube.SymMultInv[0][sym & 0xf] | sym & 0x10] = edge.getMEdge();
					edge.doConj(0);
					if ((sym & 3) == 3)
					{
						edge.doConj(1);
					}
					if ((sym & 7) == 7)
					{
						edge.doConj(2);
					}
					if ((sym & 0xf) == 0xf)
					{
						edge.doConj(3);
					}
				}
			}
		}

		static void initCenterMove()
		{
			Phase3Center center = new Phase3Center();
			//CenterMove = new int[1225][VALID_MOVES.Length];
			CenterMove = new int[1225][];
			for (int i = 0; i < 1225; i++)
			{
				CenterMove[i] = new int[VALID_MOVES.Length];
			}
			for (int i = 0; i < 1225; i++)
			{
				for (int m = 0; m < VALID_MOVES.Length; m++)
				{
					center.setCenter(i);
					center.doMove(m);
					CenterMove[i][m] = center.getCenter();
				}
			}
		}

		static Util.PruningTable WMEdgeSymPrun;

		static void initPrun()
		{
			CenterMEdgePrun = new Util.PruningTable(
				CenterMove, MEdgeMove,
				Phase3Center.SOLVED_CENTER, new int[] { 0, 2047 },
				"Phase3CenterMEdge");

			int[] mEdgeFlip = new int[1];
			WMEdgeSymPrun = new Util.PruningTable(new SymCoord3(mEdgeFlip) /*{
			{
				N_IDX = 86048;
		N_MOVES = VALID_MOVES.Length;
		N_SYM = 16;
		SelfSym = WEdgeSelfSym;
	}
	int getMoved(int move)
	{
		int val = WEdgeSymMove[idx][move];
		mEdgeFlip[0] = (val & 0x10) == 0 ? 0 : 0x7ff;
		return val >> 1 & ~0xf | val & 0xf;
	}
}*/, new RawCoord3(mEdgeFlip) /*{
			{
				N_IDX = 2048;
			}
			int getMoved(int move) {
				return MEdgeMove[idx][move] ^ mEdgeFlip[0];
			}
			int getConj(int idx, int conj) {
				return MEdgeConj[idx][conj];
			}
		}*/, null, "Phase3MWEdgeSym");
		}

		class SymCoord3 : Util.SymCoord
		{
			int[] mEdgeFlip;
			public SymCoord3(int[] mEdgeFlip_)
			{
				this.mEdgeFlip = mEdgeFlip_;
				N_IDX = 86048;
				N_MOVES = VALID_MOVES.Length;
				N_SYM = 16;
				SelfSym = WEdgeSelfSym;
			}
			public override int getMoved(int move)
			{
				int val = WEdgeSymMove[idx][move];
				mEdgeFlip[0] = (val & 0x10) == 0 ? 0 : 0x7ff;
				return val >> 1 & ~0xf | val & 0xf;
			}
		}

		class RawCoord3 : Util.RawCoord
		{
			int[] mEdgeFlip;
			public RawCoord3(int[] mEdgeFlip_)
			{
				this.mEdgeFlip = mEdgeFlip_;
				N_IDX = 2048;
			}
			public override int getMoved(int move)
			{
				return MEdgeMove[idx][move] ^ mEdgeFlip[0];
			}
			public override int getConj(int idx, int conj)
			{
				return MEdgeConj[idx][conj];
			}
		}

		class Phase3Node : Node
		{
			public int center;
			public int mEdge;
			public int wEdge;
			public override int getPrun()
			{
				return Math.Max(
						   CenterMEdgePrun.getPrun(center, mEdge),
						   WMEdgeSymPrun.getPrun(wEdge >> 5, MEdgeConj[mEdge][wEdge & 0x1f]));
			}
			public override int doMovePrun(Node node0, int move, int maxl)
			{
				Phase3Node node = (Phase3Node)node0;
				center = CenterMove[node.center][move];
				mEdge = MEdgeMove[node.mEdge][move];
				wEdge = WEdgeSymMove[node.wEdge >> 5][SymMove[node.wEdge & 0xf][move]] ^ (node.wEdge & 0x10);
				wEdge = wEdge & ~0xf | CubieCube.SymMult[wEdge & 0xf][node.wEdge & 0xf];
				return getPrun();
			}
		}

		public Phase3Search()
		{
			base.VALID_MOVES = VALID_MOVES;
			for (int i = 0; i < searchNode.Length; i++)
			{
				searchNode[i] = new Phase3Node();
			}
		}

        //public override Node[] initFrom(CubieCube cc)
        public override List<Node> initFrom(CubieCube cc)
        {
            if (SymMove == null)
			{
				SymMove = CubieCube.getSymMove(VALID_MOVES, 16);
			}
			Phase3Center ct = new Phase3Center();
			Phase3Edge ed = new Phase3Edge();
			for (int i = 0; i < 8; i++)
			{
				ct.xCenter[i] = cc.xCenter[16 + (i & 4) + (i + 1) % 4] == 1 ? 0 : -1;
				ct.tCenter[i] = cc.tCenter[16 + (i & 4) + (i + 1) % 4] == 1 ? 0 : -1;
			}
			int center = ct.getCenter();
			List<Node> nodes = new List<Node>();
			for (int filter = 8; nodes.Count == 0; filter++)
			{
				for (int idx = 0; idx < 1024; idx++)
				{
					Phase3Node node = new Phase3Node();
					int flip = idx << 1 | (Util.IntegerbitCount(idx) & 1);
					flip = (flip ^ 0xfff) << 12 | flip;
					for (int i = 0; i < 12; i++)
					{
						ed.mEdge[i] = cc.mEdge[i] & 1;
						ed.mEdge[i] ^= flip >> (cc.mEdge[i] >> 1) & 1;
					}
					for (int i = 0; i < 24; i++)
					{
						ed.wEdge[i] = (flip >> cc.wEdge[i] & 1) == 0 ? 0 : -1;
					}
					node.mEdge = ed.getMEdge();
					node.wEdge = WEdgeRaw2Sym[ed.getWEdge()];
					node.center = center;
					if (node.getPrun() > filter)
					{
						continue;
					}
					nodes.Add(node);
				}
			}
            //return nodes.ToArray();
            return nodes;
        }

        //public static void main(String[] args)
        //{
        //	initMEdgeMove();
        //	initCenterMove();
        //	initWEdgeSymMove();
        //	initPrun();
        //}
    }
}
