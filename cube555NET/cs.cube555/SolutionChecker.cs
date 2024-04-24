using System;
using System.Collections.Generic;
using System.Text;

namespace cs.cube555
{

	public class SolutionChecker
	{

		internal SolvingCube[] ccList;

		public SolutionChecker(SolvingCube[] ccList)
		{
			this.ccList = ccList;
		}

		public int check(int[] solution, int length, int ccidx)
		{
			SolvingCube sc = new SolvingCube(ccList[ccidx]);
			sc.doMove(copySolution(solution, length));
			sc.addCheckPoint();
			return check(sc);
		}

		public virtual int check(SolvingCube sc)
		{
			return 0;
		}

		static int[] copySolution(int[] solution, int length)
		{
			int[] solutionCopy = new int[length];
			for (int i = 0; i < length; i++)
			{
				solutionCopy[i] = solution[i];
			}
			return solutionCopy;
		}
	}
}
