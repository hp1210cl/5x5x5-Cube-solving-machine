using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace cs.cube555
{

	public class PhaseSearch
	{
		SolutionChecker callback = null;
		int[] solution = new int[255];
		int ccidx;

		public class PhaseEntry : IComparable<PhaseEntry>
		{
			public Node node;
			public int prun;
			public int cumCost;
			public int estCost;
			public int ccidx;


			public int CompareTo([AllowNull] PhaseEntry entry)
			{
				if (this == entry)
				{
					return 0;
				}
				if (estCost != entry.estCost)
				{
					return estCost - entry.estCost;
				}
				if (cumCost != entry.cumCost)
				{
					return cumCost - entry.cumCost;
				}
				return 1;
			}
		}

		public void solve(SolvingCube[] cc, SolutionChecker callback)
		{
			solve(cc, callback, int.MaxValue);
		}


		public virtual void solve(SolvingCube[] cc, SolutionChecker callback, int trySize)
		{
			if (SKIP_MOVES == null)
			{
				SKIP_MOVES = Util.genSkipMoves(VALID_MOVES);
				NEXT_AXIS = Util.genNextAxis(VALID_MOVES);
			}
			this.callback = callback;
			//long startTime = System.nanoTime();

			SortedSet<PhaseEntry> entries = new SortedSet<PhaseEntry>();
			for (ccidx = 0; ccidx < cc.Length; ccidx++)
			{
				//Node[] nodes = initFrom(cc[ccidx]);
				//int cumCost = cc[ccidx].length();
				//for (int i = 0; i < nodes.Length; i++)
				List<Node> nodes = initFrom(cc[ccidx]);
				int cumCost = cc[ccidx].length();
				for (int i = 0; i < nodes.Count; i++)
				{
					PhaseEntry entry = new PhaseEntry();
					entry.node = nodes[i];
					entry.prun = nodes[i].getPrun();
					entry.cumCost = cumCost;
					entry.estCost = cumCost + entry.prun;
					entry.ccidx = ccidx;
					entries.Add(entry);
					if (entries.Count > trySize)
					{
						//entries.pollLast();
						//never occur
						entries.Remove(entries.Max);
					}
				}
			}
			// nodeCnt = 0;
			for (int maxl = 0; maxl < 100; maxl++)
			{
				foreach (PhaseEntry entry in entries)
				{
					ccidx = entry.ccidx;
					if (maxl >= entry.estCost && idaSearch(entry.node, 0, maxl - entry.cumCost, VALID_MOVES.Length, entry.prun) == 0)
					{
						goto outBreak;
					}
				}
			}
		outBreak:;
			// Debug.WriteLine(nodeCnt);
		}


        //20231216 更改Node[] 为List<Node>，因为只是暂存，却不停地调用toarray，
        //public virtual Node[] initFrom(CubieCube cc)
        //{
        //    return null;
        //}
        public virtual List<Node> initFrom(CubieCube cc)
        {
            return null;
        }

        public class Node
		{
			/**
			 *  other requirements besides getPrun() == 0
			 */
			public bool isSolved()
			{
				return true;
			}
			public virtual int doMovePrun(Node node, int move, int maxl) { return 0; }
			public virtual int getPrun() { return 0; }
		}

		public Node[] searchNode = new Node[30];
		// static int nodeCnt = 0;

		private int idaSearch(Node node, int depth, int maxl, int lm, int prun)
		{
			if (prun == 0 && node.isSolved() && maxl < MIN_BACK_DEPTH)
			{
				return maxl != 0 ? 1 : callback.check(solution, depth, ccidx);
			}
			long skipMoves = SKIP_MOVES[lm];
			for (int move = 0; move < VALID_MOVES.Length; move++)
			{
				if ((skipMoves >> move & 1) != 0)
				{
					continue;
				}
				// nodeCnt++;
				prun = searchNode[depth].doMovePrun(node, move, maxl);
				if (prun >= maxl)
				{
					move += (int)(NEXT_AXIS >> move & 3 & (maxl - prun));
					continue;
				}
				solution[depth] = VALID_MOVES[move];
				int ret = idaSearch(searchNode[depth], depth + 1, maxl - 1, move, prun);
				if (ret == 0)
				{
					return 0;
				}
			}
			return 1;
		}

		long[] SKIP_MOVES;
		public int[] VALID_MOVES;
		long NEXT_AXIS;
		public int MIN_BACK_DEPTH = 1;
	}
}
