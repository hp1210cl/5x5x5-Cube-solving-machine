using System;
using System.Collections.Generic;
using System.Text;

namespace cs.cube555
{

	class Phase5Search : PhaseSearch
	{
		public new static int[] VALID_MOVES = new int[] {
		Util.Ux1, Util.Ux2, Util.Ux3, Util.Rx2, Util.Fx2, Util.Dx1, Util.Dx2, Util.Dx3, Util.Lx2, Util.Bx2,
		Util.rx2, Util.fx2, Util.lx2, Util.bx2
	};

		static long[] SKIP_MOVES = Util.genSkipMoves(VALID_MOVES);

		static int[][] LEdgeMove;
		static int[] LEdgeSym2Raw;
		static int[] LEdgeSelfSym;
		static int[] LEdgeRaw2Sym;
		static int[][] LEdgeSymMove;
		static int[] LEdgeMirror;
		static int[] UDCenterMirror;
		static int[][] CenterMove;
		static int[][] UDCenterConj;
		static Util.PruningTable CenterPrun;
		static Util.PruningTable LEdgeSymCenterPrun;

		public static void init()
		{
			initLEdgeSymMove();
			initCenterMove();
			initEdgeMove();
			initPrun();
		}

		static void initEdgeMove()
		{
			//LEdgeMove = new int[40320][VALID_MOVES.Length];
			LEdgeMove = new int[40320][];
			for (int i = 0; i < 40320; i++)
			{
				LEdgeMove[i] = new int[VALID_MOVES.Length];
			}
			Phase5Edge edge = new Phase5Edge();
			for (int i = 0; i < 40320; i++)
			{
				for (int m = 0; m < VALID_MOVES.Length; m++)
				{
					edge.setLEdge(i);
					edge.doMove(m);
					LEdgeMove[i][m] = edge.getLEdge();
				}
			}
		}

		static void initLEdgeSymMove()
		{
			Phase5Edge edge = new Phase5Edge();

			LEdgeMirror = new int[40320];
			for (int i = 0; i < LEdgeMirror.Length; i++)
			{
				edge.setLEdge(i);
				edge.doConj(2);
				LEdgeMirror[i] = edge.getHEdge();
			}

			int symCnt = 0;
			LEdgeSym2Raw = new int[5288 * 8];
			LEdgeSelfSym = new int[5288];
			LEdgeRaw2Sym = new int[40320];
			for (int i = 0; i < LEdgeRaw2Sym.Length; i++)
			{
				if (LEdgeRaw2Sym[i] != 0)
				{
					continue;
				}
				edge.setLEdge(i);
				for (int sym = 0; sym < 8; sym++)
				{
					int idx = edge.getLEdge();
					LEdgeRaw2Sym[idx] = symCnt << 3 | sym;
					if (idx == i)
					{
						LEdgeSelfSym[symCnt] |= 1 << sym;
					}
					edge.doConj(0);
					if ((sym & 3) == 3)
					{
						edge.doConj(1);
					}
				}
				LEdgeSym2Raw[symCnt] = i;
				symCnt++;
			}
			//LEdgeSymMove = new int[symCnt][VALID_MOVES.Length];
			LEdgeSymMove = new int[symCnt][];
			for (int i = 0; i < symCnt; i++)
			{
				LEdgeSymMove[i] = new int[VALID_MOVES.Length];
			}
			for (int i = 0; i < symCnt; i++)
			{
				for (int m = 0; m < VALID_MOVES.Length; m++)
				{
					edge.setLEdge(LEdgeSym2Raw[i]);
					edge.doMove(m);
					LEdgeSymMove[i][m] = LEdgeRaw2Sym[edge.getLEdge()];
				}
			}
		}

		static void initCenterMove()
		{
			//int[][] RFLBMove = new int[36][VALID_MOVES.Length];
			//int[][] TMove = new int[70][VALID_MOVES.Length];
			//int[][] XMove = new int[70][VALID_MOVES.Length];
			//int[][] TConj = new int[70][8];
			//int[][] XConj = new int[70][8];
			int[][] RFLBMove = new int[36][];
			int[][] TMove = new int[70][];
			int[][] XMove = new int[70][];
			int[][] TConj = new int[70][];
			int[][] XConj = new int[70][];
			for (int i = 0; i < 36; i++)
			{
				RFLBMove[i] = new int[VALID_MOVES.Length];
			}
			for (int i = 0; i < 70; i++)
			{
				TMove[i] = new int[VALID_MOVES.Length];
				XMove[i] = new int[VALID_MOVES.Length];
				TConj[i] = new int[8];
				XConj[i] = new int[8];
			}
			Phase5Center center = new Phase5Center();
			for (int i = 0; i < 70; i++)
			{
				for (int m = 0; m < VALID_MOVES.Length; m++)
				{
					center.setTCenter(i);
					center.setXCenter(i);
					center.doMove(m);
					TMove[i][m] = center.getTCenter();
					XMove[i][m] = center.getXCenter();
				}
				center.setTCenter(i);
				center.setXCenter(i);
				for (int sym = 0; sym < 8; sym++)
				{
					TConj[i][CubieCube.SymMultInv[0][sym]] = center.getTCenter();
					XConj[i][CubieCube.SymMultInv[0][sym]] = center.getXCenter();
					center.doConj(0);
					if ((sym & 3) == 3)
					{
						center.doConj(1);
					}
				}
			}
			for (int i = 0; i < 36; i++)
			{
				for (int m = 0; m < VALID_MOVES.Length; m++)
				{
					center.setRFLBCenter(i);
					center.doMove(m);
					RFLBMove[i][m] = center.getRFLBCenter();
				}
			}

			//CenterMove = new int[70 * 70 * 36][VALID_MOVES.Length];
			CenterMove = new int[70 * 70 * 36][];
			for (int i = 0; i < 70 * 70 * 36; i++)
			{
				CenterMove[i] = new int[VALID_MOVES.Length];
			}
			for (int i = 0; i < 70 * 70 * 36; i++)
			{
				int tCenter = i % 70;
				int xCenter = i / 70 % 70;
				int rflbCenter = i / 70 / 70;
				for (int m = 0; m < VALID_MOVES.Length; m++)
				{
					CenterMove[i][m] = (RFLBMove[rflbCenter][m] * 70 + XMove[xCenter][m]) * 70 + TMove[tCenter][m];
				}
			}

			UDCenterMirror = new int[4900];
			//UDCenterConj = new int[70 * 70][8];
			UDCenterConj = new int[70 * 70][];
			for (int i = 0; i < 70 * 70; i++)
			{
				UDCenterConj[i] = new int[8];
			}
			for (int i = 0; i < 4900; i++)
			{
				int tCenter = i % 70;
				int xCenter = i / 70 % 70;
				center.setTCenter(tCenter);
				center.setXCenter(xCenter);
				center.doConj(2);
				UDCenterMirror[i] = center.getXCenter() * 70 + center.getTCenter();
				for (int s = 0; s < 8; s++)
				{
					UDCenterConj[i][s] = XConj[xCenter][s] * 70 + TConj[tCenter][s];
				}
			}
		}

		static void initPrun()
		{
			//int[][] UDCenterMove = new int[4900][VALID_MOVES.Length];
			int[][] UDCenterMove = new int[4900][];
			for (int i = 0; i < 4900; i++)
			{
				UDCenterMove[i] = new int[VALID_MOVES.Length];
			}
			for (int i = 0; i < 4900; i++)
			{
				for (int j = 0; j < VALID_MOVES.Length; j++)
				{
					UDCenterMove[i][j] = CenterMove[i][j] % 4900;
				}
			}
			CenterPrun = new Util.PruningTable(CenterMove, null, "Phase5Center");
			LEdgeSymCenterPrun = new Util.PruningTable(
				new Util.TableSymCoord(LEdgeSymMove, LEdgeSelfSym, 8),
				new Util.TableRawCoord(UDCenterMove, UDCenterConj),
				null, "Phase5LEdgeSymCenter");
		}

		class Phase5Node : Node
		{
			public int lEdge;
			public int hEdgem;
			public int center;
			public override int getPrun()
			{
				int lEdges = LEdgeRaw2Sym[lEdge];
				int hEdges = LEdgeRaw2Sym[hEdgem];
				return Math.Max(
						   CenterPrun.getPrun(center),
						   Math.Max(LEdgeSymCenterPrun.getPrun(lEdges >> 3, UDCenterConj[center % 4900][lEdges & 0x7]),
									LEdgeSymCenterPrun.getPrun(hEdges >> 3, UDCenterConj[UDCenterMirror[center % 4900]][hEdges & 0x7]))
					   );
			}
			public override int doMovePrun(Node node0, int move, int maxl)
			{
				Phase5Node node = (Phase5Node)node0;
				center = CenterMove[node.center][move];
				lEdge = LEdgeMove[node.lEdge][move];
				hEdgem = LEdgeMove[node.hEdgem][SymMove[8][move]];
				return getPrun();
			}
		}

		static int[][] SymMove;

		public Phase5Search()
		{
			base.VALID_MOVES = VALID_MOVES;
			for (int i = 0; i < searchNode.Length; i++)
			{
				searchNode[i] = new Phase5Node();
			}
		}

        //public override Node[] initFrom(CubieCube cc)
        public override List<Node> initFrom(CubieCube cc)
        {
            if (SymMove == null)
			{
				SymMove = CubieCube.getSymMove(VALID_MOVES, 16);
			}
			Phase5Edge edge = new Phase5Edge();
			Phase5Center center = new Phase5Center();
			int mask = 0;
			for (int i = 0; i < 8; i++)
			{
				mask |= 1 << (cc.mEdge[i] >> 1);
			}
			for (int i = 0; i < 8; i++)
			{
				int e = cc.mEdge[i] >> 1;
				edge.mEdge[i] = Util.IntegerbitCount(mask & ((1 << e) - 1));
				e = cc.wEdge[i] % 12;
				edge.lEdge[i] = Util.IntegerbitCount(mask & ((1 << e) - 1));
				e = cc.wEdge[i + 12] % 12;
				edge.hEdge[i] = Util.IntegerbitCount(mask & ((1 << e) - 1));
				center.xCenter[i] = cc.xCenter[i] == 0 ? 0 : -1;
				center.tCenter[i] = cc.tCenter[i] == 0 ? 0 : -1;
				center.rflbCenter[i] = cc.tCenter[9 + i * 2] == 1 || cc.tCenter[9 + i * 2] == 2 ? 0 : -1;
			}
			edge.isStd = false;
			Phase5Node node = new Phase5Node();
			node.lEdge = edge.getLEdge();
			node.hEdgem = LEdgeMirror[edge.getHEdge()];
			node.center = (center.getRFLBCenter() * 70 + center.getXCenter()) * 70 + center.getTCenter();
			return new List<Node>() { node };
		}
	}
}
