using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace cs.cube555
{

	public class Search
	{

		public static int USE_SEPARATOR = 0x1;

		static int phase1SolsSize = 200;
		static int phase2SolsSize = 500;
		static int phase3SolsSize = 500;
		static int phase4SolsSize = 500;
		static int phase5SolsSize = 1;

		public static class Logger
		{
			static bool DEBUG = true;
			static Stopwatch timer;
			public static long cumSolLen = 0;
			static long[] cumPhaseT = new long[5];
			public static void start()
			{
				timer=Stopwatch.StartNew();
			}
			public static void logTime(int phase)
			{
				cumPhaseT[phase] += timer.ElapsedMilliseconds;
				//Debug.WriteLine(string.Format("Phase%d Finished in %d ms", phase + 1, timer.ElapsedMilliseconds));
				//Debug.WriteLine($"Phase{phase + 1} Finished in {timer.ElapsedMilliseconds} ms");
				timer = Stopwatch.StartNew();
			}
			public static void print(int div)
			{
				//Debug.WriteLine(
				//	string.Format(
				//		"AvgLen=%.2f P1T=%4dms P2T=%4dms P3T=%4dms P4T=%4dms P5T=%4dms TOT=%4dms",
				//		cumSolLen * 1.0 / div,
				//		cumPhaseT[0] / div / 1000000,
				//		cumPhaseT[1] / div / 1000000,
				//		cumPhaseT[2] / div / 1000000,
				//		cumPhaseT[3] / div / 1000000,
				//		cumPhaseT[4] / div / 1000000,
				//		(cumPhaseT[0] + cumPhaseT[1] + cumPhaseT[2] + cumPhaseT[3] + cumPhaseT[4]) / div / 1000000
				//	));
				//Debug.WriteLine($"AvgLen={cumSolLen * 1.0 / div} P1T={cumPhaseT[0]}ms P2T={cumPhaseT[1]}ms P3T={cumPhaseT[2]}ms P4T={cumPhaseT[3]}ms P5T={cumPhaseT[4]}ms " +
				//	$"TOT={cumPhaseT[0] + cumPhaseT[1] + cumPhaseT[2] + cumPhaseT[3] + cumPhaseT[4]}ms");
			}
		}

		static bool isInited = false;

		public static void init()
		{
			if (isInited)
			{
				return;
			}
			CubieCube.init();
            //Parallel.Invoke(
            //	() => { Phase1Search.init(); },
            //	() => { Phase2Search.init(); },
            //	() => { Phase3Search.init(); },
            //	() => { Phase4Search.init(); },
            //	() => { Phase5Search.init(); });
            Phase1Search.init();
			Phase2Search.init();
			Phase3Search.init();
			Phase4Search.init();
			Phase5Search.init();
			isInited = true;
		}

		Phase1Search p1search = new Phase1Search();
		Phase2Search p2search = new Phase2Search();
		Phase3Search p3search = new Phase3Search();
		Phase4Search p4search = new Phase4Search();
		Phase5Search p5search = new Phase5Search();
		List<SolvingCube> p1sols = new List<SolvingCube>();
		List<SolvingCube> p2sols = new List<SolvingCube>();
		List<SolvingCube> p3sols = new List<SolvingCube>();
		List<SolvingCube> p4sols = new List<SolvingCube>();
		List<SolvingCube> p5sols = new List<SolvingCube>();
		SolvingCube[] p1cc;
		SolvingCube[] p2cc;
		SolvingCube[] p3cc;
		SolvingCube[] p4cc;
		SolvingCube[] p5cc;

		public string[] solveReduction(string facelet, int verbose)
		{
			CubieCube cc = new CubieCube();
			int verifyReduction = cc.fromFacelet(facelet);
			if (verifyReduction != 0)
			{
				//Debug.WriteLine(verifyReduction);
				return new string[] { "Error " + verifyReduction, null };
			}
			p1sols.Clear();
			p2sols.Clear();
			p3sols.Clear();
			p4sols.Clear();
			p5sols.Clear();

			Logger.start();
			//Debug.WriteLine(cc);

			SolvingCube sc = new SolvingCube(cc);

			SolvingCube[] p1cc = new SolvingCube[3];
			for (int i = 0; i < 3; i++)
			{
				p1cc[i] = new SolvingCube(sc);
				sc.doConj(16);
			}
			p1search.solve(p1cc, new SolutionChecker1(p1cc,p1sols) /*{
			@Override
			int check(SolvingCube sc) {
				p1sols.add(sc);
			return p1sols.size() >= phase1SolsSize ? 0 : 1;
		}
	}*/);
			Logger.logTime(0);

			p2cc = p1sols.ToArray();
			p2search.solve(p2cc, new SolutionChecker2(p2cc,p2sols)
			/*	{
					@Override
						int check(SolvingCube sc)
					{
						for (int i = 0; i < 3; i++)
						{
							p2sols.add(new SolvingCube(sc));
							sc.doConj(16);
						}
						return p2sols.size() >= phase2SolsSize ? 0 : 1;
					}
				}*/);
			Logger.logTime(1);

			p3cc = p2sols.ToArray();
			p3search.solve(p3cc, new SolutionChecker3(p3cc,p3sols)
			/*	{
					@Override
						int check(SolvingCube sc)
					{
						int maskY = 0;
						int maskZ = 0;
						for (int i = 0; i < 4; i++)
						{
							maskY |= 1 << (sc.wEdge[8 + i] % 12);
							maskY |= 1 << (sc.wEdge[8 + i + 12] % 12);
							maskY |= 1 << (sc.mEdge[8 + i] >> 1);
							maskZ |= 1 << (sc.wEdge[4 + i] % 12);
							maskZ |= 1 << (sc.wEdge[4 + i + 12] % 12);
							maskZ |= 1 << (sc.mEdge[4 + i] >> 1);
						}
						if (Integer.bitCount(maskY) <= 8)
						{
							p3sols.add(sc);
						}
						if (Integer.bitCount(maskZ) <= 8)
						{
							sc.doConj(1);
							p3sols.add(new SolvingCube(sc));
						}
						return p3sols.size() >= phase3SolsSize ? 0 : 1;
					}
				}*/);
			Logger.logTime(2);

			p4cc = p3sols.ToArray();
			p4search.solve(p4cc, new SolutionChecker4(p4cc,p4sols)
			/*	{
					@Override
						int check(SolvingCube sc)
					{
						sc.doConj(1);
						p4sols.add(sc);
						return p4sols.size() >= phase4SolsSize ? 0 : 1;
					}
				}*/);
			Logger.logTime(3);

			p5cc = p4sols.ToArray();
			p5search.solve(p5cc, new SolutionChecker5(p5cc,p5sols)
			/*	{
					@Override
						int check(SolvingCube sc)
					{
						p5sols.add(sc);
						return p5sols.size() >= phase5SolsSize ? 0 : 1;
					}
				}*/);
			Logger.logTime(4);

			sc = p5sols[0];
			//Debug.WriteLine(sc);
			//Debug.WriteLine("Reduction: " + sc.length());
			Logger.cumSolLen += sc.length();

			cc.doMove(sc.getSolution());
			cc.doCornerMove(sc.getSolution());
			String[] ret = new string [2];
			ret[0] = sc.toSolutionString(verbose);
			ret[1] = CubieCube.to333Facelet(cc.toFacelet());
			return ret;
		}

		class SolutionChecker1 : SolutionChecker
		{
			List<SolvingCube> p1sols;
			public SolutionChecker1(SolvingCube[] ccList, List<SolvingCube> p1sols_) :base(ccList)
			{
				p1sols = p1sols_;
			}
			public override int check(SolvingCube sc)
			{
				p1sols.Add(sc);
				return p1sols.Count >= phase1SolsSize ? 0 : 1;
			}
		}

		class SolutionChecker2 : SolutionChecker
		{
			List<SolvingCube> p2sols;
			public SolutionChecker2(SolvingCube[] ccList, List<SolvingCube> p2sols_) : base(ccList)
			{
				p2sols = p2sols_;
			}
			public override int check(SolvingCube sc)
			{
				for (int i = 0; i < 3; i++)
				{
					p2sols.Add(new SolvingCube(sc));
					sc.doConj(16);
				}
				return p2sols.Count >= phase2SolsSize ? 0 : 1;
			}

		}

		class SolutionChecker3 : SolutionChecker
		{
			List<SolvingCube> p3sols;
			public SolutionChecker3(SolvingCube[] ccList, List<SolvingCube> p3sols_) : base(ccList)
			{
				p3sols = p3sols_;
			}
			public override int check(SolvingCube sc)
			{
				int maskY = 0;
				int maskZ = 0;
				for (int i = 0; i < 4; i++)
				{
					maskY |= 1 << (sc.wEdge[8 + i] % 12);
					maskY |= 1 << (sc.wEdge[8 + i + 12] % 12);
					maskY |= 1 << (sc.mEdge[8 + i] >> 1);
					maskZ |= 1 << (sc.wEdge[4 + i] % 12);
					maskZ |= 1 << (sc.wEdge[4 + i + 12] % 12);
					maskZ |= 1 << (sc.mEdge[4 + i] >> 1);
				}
				if (Util.IntegerbitCount(maskY) <= 8)
				{
					p3sols.Add(sc);
				}
				if (Util.IntegerbitCount(maskZ) <= 8)
				{
					sc.doConj(1);
					p3sols.Add(new SolvingCube(sc));
				}
				return p3sols.Count >= phase3SolsSize ? 0 : 1;
			}

		}

		class SolutionChecker4 : SolutionChecker
		{
			List<SolvingCube> p4sols;
			public SolutionChecker4(SolvingCube[] ccList, List<SolvingCube> p4sols_) : base(ccList)
			{
				p4sols = p4sols_;
			}
			public override int check(SolvingCube sc)
			{
				sc.doConj(1);
				p4sols.Add(sc);
				return p4sols.Count >= phase4SolsSize ? 0 : 1;
			}
		}

		class SolutionChecker5 : SolutionChecker
		{
			List<SolvingCube> p5sols;
			public SolutionChecker5(SolvingCube[] ccList, List<SolvingCube> p5sols_) : base(ccList)
			{
				p5sols = p5sols_;
			}
			public override int check(SolvingCube sc)
			{
				p5sols.Add(sc);
				return p5sols.Count >= phase5SolsSize ? 0 : 1;
			}

		}
	}
}
