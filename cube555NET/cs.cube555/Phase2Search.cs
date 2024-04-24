using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace cs.cube555
{

	class Phase2Search : PhaseSearch
	{

		public new static int[] VALID_MOVES = new int[] {
		Util.Ux1, Util.Ux2, Util.Ux3, Util.Fx1, Util.Fx2, Util.Fx3, Util.Dx1, Util.Dx2, Util.Dx3, Util.Bx1, Util.Bx2, Util.Bx3,
		Util.ux2, Util.rx1, Util.rx2, Util.rx3, Util.fx2, Util.dx2, Util.lx1, Util.lx2, Util.lx3, Util.bx2
	};

		static long[] SKIP_MOVES = Util.genSkipMoves(VALID_MOVES);

		static int[][] TCenterMove;
		static int[][] XCenterMove;
		static Util.PruningTable prunTCenter;
		static Util.PruningTable prunXCenter;

		public static void init()
		{
			initCenterMove();
			initCenterPrun();
		}

		static void initCenterMove()
		{
			Phase2Center ct = new Phase2Center();
			//TCenterMove = new int[12870][VALID_MOVES.Length];
			//XCenterMove = new int[12870][VALID_MOVES.Length];
			TCenterMove = new int[12870][];
			XCenterMove = new int[12870][];
			for (int i = 0; i < 12870; i++)
			{
				TCenterMove[i] = new int[VALID_MOVES.Length];
				XCenterMove[i] = new int[VALID_MOVES.Length];
			}
			for (int i = 0; i < 12870; i++)
			{
				for (int m = 0; m < VALID_MOVES.Length; m++)
				{
					ct.setTCenter(i);
					ct.setXCenter(i);
					ct.doMove(m);
					TCenterMove[i][m] = ct.getTCenter();
					XCenterMove[i][m] = ct.getXCenter();
				}
			}
		}

		static void initCenterPrun()
		{
			//int[][] EParityMove = new int[2][VALID_MOVES.Length];
			int[][] EParityMove = new int[2][];
			for (int i = 0; i < 2; i++)
			{
				EParityMove[i] = new int[VALID_MOVES.Length];
			}
			for (int i = 0; i < VALID_MOVES.Length; i++)
			{
				EParityMove[0][i] = 0 ^ Phase2Center.eParityDiff[i];
				EParityMove[1][i] = 1 ^ Phase2Center.eParityDiff[i];
			}
			prunTCenter = new Util.PruningTable(TCenterMove, EParityMove, null, null, "Phase2TCenter");
			prunXCenter = new Util.PruningTable(XCenterMove, EParityMove, null, null, "Phase2XCenter");
		}

		class Phase2Node : Node
		{
			public int tCenter;
			public int xCenter;
			public int eParity;
			public override int getPrun()
			{
				return Math.Max(prunTCenter.getPrun(tCenter, eParity),
								prunXCenter.getPrun(xCenter, eParity));
			}
			public override int doMovePrun(Node node0, int move, int maxl)
			{
				Phase2Node node = (Phase2Node)node0;
				tCenter = TCenterMove[node.tCenter][move];
				xCenter = XCenterMove[node.xCenter][move];
				eParity = node.eParity ^ Phase2Center.eParityDiff[move];
				return getPrun();
			}
		}

		public Phase2Search()
		{
			base.VALID_MOVES = VALID_MOVES;
			base.MIN_BACK_DEPTH = 5;
			for (int i = 0; i < searchNode.Length; i++)
			{
				searchNode[i] = new Phase2Node();
			}
		}

        //public override Node[] initFrom(CubieCube cc)
        public override List<Node> initFrom(CubieCube cc)
        {
            Phase2Center ct = new Phase2Center();
			for (int i = 0; i < 16; i++)
			{
				ct.xCenter[i] = cc.xCenter[i] == 0 || cc.xCenter[i] == 3 ? 0 : -1;
				ct.tCenter[i] = cc.tCenter[i] == 0 || cc.tCenter[i] == 3 ? 0 : -1;
			}
			Phase2Node node = new Phase2Node();
			node.xCenter = ct.getXCenter();
			node.tCenter = ct.getTCenter();
			node.eParity = Util.getParity(cc.wEdge);
			return new List<Node>() { node };
		}
	}
}
