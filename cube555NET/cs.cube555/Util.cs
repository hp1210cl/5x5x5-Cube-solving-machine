using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace cs.cube555
{

	public static class Util
	{
		public static int U1 = 0;
		public static int U2 = 1;
		public static int U3 = 2;
		public static int U4 = 3;
		public static int U5 = 4;
		public static int U6 = 5;
		public static int U7 = 6;
		public static int U8 = 7;
		public static int U9 = 8;
		public static int U10 = 9;
		public static int U11 = 10;
		public static int U12 = 11;
		public static int U13 = 12;
		public static int U14 = 13;
		public static int U15 = 14;
		public static int U16 = 15;
		public static int U17 = 16;
		public static int U18 = 17;
		public static int U19 = 18;
		public static int U20 = 19;
		public static int U21 = 20;
		public static int U22 = 21;
		public static int U23 = 22;
		public static int U24 = 23;
		public static int U25 = 24;
		public static int R1 = 25;
		public static int R2 = 26;
		public static int R3 = 27;
		public static int R4 = 28;
		public static int R5 = 29;
		public static int R6 = 30;
		public static int R7 = 31;
		public static int R8 = 32;
		public static int R9 = 33;
		public static int R10 = 34;
		public static int R11 = 35;
		public static int R12 = 36;
		public static int R13 = 37;
		public static int R14 = 38;
		public static int R15 = 39;
		public static int R16 = 40;
		public static int R17 = 41;
		public static int R18 = 42;
		public static int R19 = 43;
		public static int R20 = 44;
		public static int R21 = 45;
		public static int R22 = 46;
		public static int R23 = 47;
		public static int R24 = 48;
		public static int R25 = 49;
		public static int F1 = 50;
		public static int F2 = 51;
		public static int F3 = 52;
		public static int F4 = 53;
		public static int F5 = 54;
		public static int F6 = 55;
		public static int F7 = 56;
		public static int F8 = 57;
		public static int F9 = 58;
		public static int F10 = 59;
		public static int F11 = 60;
		public static int F12 = 61;
		public static int F13 = 62;
		public static int F14 = 63;
		public static int F15 = 64;
		public static int F16 = 65;
		public static int F17 = 66;
		public static int F18 = 67;
		public static int F19 = 68;
		public static int F20 = 69;
		public static int F21 = 70;
		public static int F22 = 71;
		public static int F23 = 72;
		public static int F24 = 73;
		public static int F25 = 74;
		public static int D1 = 75;
		public static int D2 = 76;
		public static int D3 = 77;
		public static int D4 = 78;
		public static int D5 = 79;
		public static int D6 = 80;
		public static int D7 = 81;
		public static int D8 = 82;
		public static int D9 = 83;
		public static int D10 = 84;
		public static int D11 = 85;
		public static int D12 = 86;
		public static int D13 = 87;
		public static int D14 = 88;
		public static int D15 = 89;
		public static int D16 = 90;
		public static int D17 = 91;
		public static int D18 = 92;
		public static int D19 = 93;
		public static int D20 = 94;
		public static int D21 = 95;
		public static int D22 = 96;
		public static int D23 = 97;
		public static int D24 = 98;
		public static int D25 = 99;
		public static int L1 = 100;
		public static int L2 = 101;
		public static int L3 = 102;
		public static int L4 = 103;
		public static int L5 = 104;
		public static int L6 = 105;
		public static int L7 = 106;
		public static int L8 = 107;
		public static int L9 = 108;
		public static int L10 = 109;
		public static int L11 = 110;
		public static int L12 = 111;
		public static int L13 = 112;
		public static int L14 = 113;
		public static int L15 = 114;
		public static int L16 = 115;
		public static int L17 = 116;
		public static int L18 = 117;
		public static int L19 = 118;
		public static int L20 = 119;
		public static int L21 = 120;
		public static int L22 = 121;
		public static int L23 = 122;
		public static int L24 = 123;
		public static int L25 = 124;
		public static int B1 = 125;
		public static int B2 = 126;
		public static int B3 = 127;
		public static int B4 = 128;
		public static int B5 = 129;
		public static int B6 = 130;
		public static int B7 = 131;
		public static int B8 = 132;
		public static int B9 = 133;
		public static int B10 = 134;
		public static int B11 = 135;
		public static int B12 = 136;
		public static int B13 = 137;
		public static int B14 = 138;
		public static int B15 = 139;
		public static int B16 = 140;
		public static int B17 = 141;
		public static int B18 = 142;
		public static int B19 = 143;
		public static int B20 = 144;
		public static int B21 = 145;
		public static int B22 = 146;
		public static int B23 = 147;
		public static int B24 = 148;
		public static int B25 = 149;

		public const byte Ux1 = 0;
		public const byte Ux2 = 1;
		public const byte Ux3 = 2;
		public const byte Rx1 = 3;
		public const byte Rx2 = 4;
		public const byte Rx3 = 5;
		public const byte Fx1 = 6;
		public const byte Fx2 = 7;
		public const byte Fx3 = 8;
		public const byte Dx1 = 9;
		public const byte Dx2 = 10;
		public const byte Dx3 = 11;
		public const byte Lx1 = 12;
		public const byte Lx2 = 13;
		public const byte Lx3 = 14;
		public const byte Bx1 = 15;
		public const byte Bx2 = 16;
		public const byte Bx3 = 17;
		public const byte ux1 = 18;
		public const byte ux2 = 19;
		public const byte ux3 = 20;
		public const byte rx1 = 21;
		public const byte rx2 = 22;
		public const byte rx3 = 23;
		public const byte fx1 = 24;
		public const byte fx2 = 25;
		public const byte fx3 = 26;
		public const byte dx1 = 27;
		public const byte dx2 = 28;
		public const byte dx3 = 29;
		public const byte lx1 = 30;
		public const byte lx2 = 31;
		public const byte lx3 = 32;
		public const byte bx1 = 33;
		public const byte bx2 = 34;
		public const byte bx3 = 35;

		public static string[] move2str = new string[] {
		"U ", "U2", "U'", "R ", "R2", "R'", "F ", "F2", "F'",
		"D ", "D2", "D'", "L ", "L2", "L'", "B ", "B2", "B'",
		"u ", "u2", "u'", "r ", "r2", "r'", "f ", "f2", "f'",
		"d ", "d2", "d'", "l ", "l2", "l'", "b ", "b2", "b'"
	};

		public static int[][] Cnk = new int[25][];
		static int[] fact = new int[13];

		static Util()
		{
			//static int[][] Cnk = new int[25][25];
			for (int i = 0; i < 25; i++)
			{
				Cnk[i] = new int[25];
			}
			for (int i = 0; i < 25; i++)
			{
				Cnk[i][i] = 1;
				Cnk[i][0] = 1;
			}
			for (int i = 1; i < 25; i++)
			{
				for (int j = 1; j <= i; j++)
				{
					Cnk[i][j] = Cnk[i - 1][j] + Cnk[i - 1][j - 1];
				}
			}
			fact[0] = 1;
			for (int i = 1; i < 13; i++)
			{
				fact[i] = fact[i - 1] * i;
			}
		}

		public static long[] genSkipMoves(int[] VALID_MOVES)
		{
			long[] ret = new long[VALID_MOVES.Length + 1];
			for (int last = 0; last < VALID_MOVES.Length; last++)
			{
				ret[last] = 0;
				int la = VALID_MOVES[last] / 3;
				for (int move = 0; move < VALID_MOVES.Length; move++)
				{
					int axis = VALID_MOVES[move] / 3;
					if (axis == la || axis % 3 == la % 3 && axis >= la)
					{
						ret[last] |= (long)(1 << move);
					}
				}
			}
			return ret;
		}

		public static long genNextAxis(int[] VALID_MOVES)
		{
			long ret = 0;
			for (int i = 0; i < VALID_MOVES.Length; i++)
			{
				if (VALID_MOVES[i] % 3 == 0)
				{
					//if Mx1 makes state farther, Mx2 and Mx3 should be skipped
					// (next_axis >> i & 3) == 2 for Mx1, 1 for Mx2, 0 for Mx3
					ret |= 1L << (i + 1);
				}
			}
			return ret;
		}
		public static int[] setPerm(int[] arr, int idx, int n, bool even)
		{
			//ulong val = 0xFEDCBA9876543210L;
			long val = -81985529216486896;
			int parity = 0;
			if (even)
			{
				idx <<= 1;
			}
			n--;
			for (int i = 0; i < n; ++i)
			{
				int p = fact[n - i];
				//int v = ~~(idx / p);
				int v = idx / p;
				parity ^= v;
				idx %= p;
				v <<= 2;
				arr[i] = (int)(val >> v & 0xf);
				long m = (1L << v) - 1;
				val = (val & m) + (val >> 4 & ~m);
			}
			if (even && (parity & 1) != 0)
			{
				arr[n] = arr[n - 1];
				arr[n - 1] = (int)(val & 0xf);
			}
			else
			{
				arr[n] = (int)(val & 0xf);
			}
			return arr;
		}

		public static int[] setPerm(int[] arr, int idx, int n)
		{
			return setPerm(arr, idx, n, false);
		}

		public static int[] setPerm(int[] arr, int idx)
		{
			return setPerm(arr, idx, arr.Length, false);
		}

		public static int getPerm(int[] arr, int n, bool even)
		{
			int idx = 0;
			//ulong val = 0xFEDCBA9876543210L;
			long val = -81985529216486896;
			for (int i = 0; i < n - 1; ++i)
			{
				int v = arr[i] << 2;
				idx = (n - i) * idx + (int)(val >> v & 0xf);
				val -= 1229782938247303440 << v;
			}
			return even ? (idx >> 1) : idx;
		}

		public static int getPerm(int[] arr, int n)
		{
			return getPerm(arr, n, false);
		}

		public static int getPerm(int[] arr)
		{
			return getPerm(arr, arr.Length, false);
		}

		public static int[] setComb(int[] arr, int idx, int r, int n)
		{
			for (int i = n - 1; i >= 0; i--)
			{
				if (idx >= Cnk[i][r])
				{
					idx -= Cnk[i][r--];
					arr[i] = 0;
				}
				else
				{
					arr[i] = -1;
				}
			}
			return arr;
		}

		public static int[] setComb(int[] arr, int idx, int r)
		{
			return setComb(arr, idx, r, arr.Length);
		}

		public static int getComb(int[] arr, int r, int n)
		{
			int idx = 0;
			for (int i = n - 1; i >= 0; i--)
			{
				if (arr[i] != -1)
				{
					idx += Cnk[i][r--];
				}
			}
			return idx;
		}

		public static int getComb(int[] arr, int r)
		{
			return getComb(arr, r, arr.Length);
		}

		public static int getSComb(int[] arr, int n)
		{
			int idx = 0;
			int r = n / 2;
			for (int i = n - 1; i >= 0; i--)
			{
				if (arr[i] != arr[n - 1])
				{
					idx += Cnk[i][r--];
				}
			}
			return idx;
		}

		public static int getSComb(int[] arr)
		{
			return getSComb(arr, arr.Length);
		}

		public static void copyFromComb(int[] src, int[] dst)
		{
			int r = 0;
			for (int i = 0; i < src.Length; i++)
			{
				if (src[i] != -1)
				{
					dst[r++] = src[i];
				}
			}
		}

		public static void copyToComb(int[] src, int[] dst)
		{
			int r = 0;
			for (int i = 0; i < dst.Length; i++)
			{
				if (dst[i] != -1)
				{
					dst[i] = src[r++];
				}
			}
		}

		public static int getParity(int idx, int n)
		{
			int parity = 0;
			for (int i = n - 2; i >= 0; --i)
			{
				parity ^= idx % (n - i);
				idx /= n - i;
			}
			return parity & 1;
		}

		public static int getParity(int[] arr)
		{
			int parity = 0;
			for (int i = 0; i < arr.Length - 1; i++)
			{
				for (int j = i + 1; j < arr.Length; j++)
				{
					if (arr[i] > arr[j])
					{
						parity ^= 1;
					}
				}
			}
			return parity;
		}

		public static void swap(int[] arr, int a, int b)
		{
			int temp = arr[a];
			arr[a] = arr[b];
			arr[b] = temp;
		}

		static void swapCorner(int[] arr, int a, int b, int c, int d, int pow)
		{
			int temp;
			switch (pow)
			{
				case 0:
					temp = (arr[d] + 8) % 24;
					arr[d] = (arr[c] + 16) % 24;
					arr[c] = (arr[b] + 8) % 24;
					arr[b] = (arr[a] + 16) % 24;
					arr[a] = temp;
					return;
				case 1:
					temp = arr[a];
					arr[a] = arr[c];
					arr[c] = temp;
					temp = arr[b];
					arr[b] = arr[d];
					arr[d] = temp;
					return;
				case 2:
					temp = (arr[a] + 8) % 24;
					arr[a] = (arr[b] + 16) % 24;
					arr[b] = (arr[c] + 8) % 24;
					arr[c] = (arr[d] + 16) % 24;
					arr[d] = temp;
					return;
			}
		}

		public static void swap(int[] arr, int a, int b, int c, int d, int pow, bool flip)
		{
			int xor = flip ? 1 : 0;
			int temp;
			switch (pow)
			{
				case 0:
					temp = arr[d] ^ xor;
					arr[d] = arr[c] ^ xor;
					arr[c] = arr[b] ^ xor;
					arr[b] = arr[a] ^ xor;
					arr[a] = temp;
					return;
				case 1:
					temp = arr[a];
					arr[a] = arr[c];
					arr[c] = temp;
					temp = arr[b];
					arr[b] = arr[d];
					arr[d] = temp;
					return;
				case 2:
					temp = arr[a] ^ xor;
					arr[a] = arr[b] ^ xor;
					arr[b] = arr[c] ^ xor;
					arr[c] = arr[d] ^ xor;
					arr[d] = temp;
					return;
			}
		}

		public static void swap(int[] arr, int a, int b, int c, int d, int pow)
		{
			swap(arr, a, b, c, d, pow, false);
		}

		public static int indexOf(int[] arr, int value)
		{
			for (int i = 0; i < arr.Length; i++)
			{
				if (arr[i] == value)
				{
					return i;
				}
			}
			return -1;
		}


		public class Coord
        {
			public int N_IDX = 1;
            public int N_MOVES = 0;
            public int idx;

            public virtual void set(int idx)
            {
                this.idx = idx;
            }
            public virtual int getMoved(int move)
			{ 
				return 0; 
			}
        }


        public class SymCoord : Coord
        {
            public int N_SYM;
            public int[] SelfSym;
        }

        public class RawCoord : Coord
        {
            public virtual int getConj(int idx, int conj)
            {
                return idx;
            }
        }


        public class TableSymCoord : SymCoord
        {
            int[][] moveTable;
            public TableSymCoord(int[][] moveTable, int[] SelfSym, int N_SYM)
            {
                this.moveTable = moveTable;
                base.SelfSym = SelfSym;
                base.N_SYM = N_SYM;
				base.N_IDX = moveTable.Length;
                base.N_MOVES = moveTable[0].Length;
            }
            public override int getMoved(int move)
            {
                return moveTable[idx][move];
            }
        }

        public class TableRawCoord : RawCoord
        {
            int[][] moveTable;
            int[][] conjTable;
            public TableRawCoord(int[][] moveTable, int[][] conjTable)
            {
                this.moveTable = moveTable;
                this.conjTable = conjTable;
                this.N_IDX = moveTable.Length;
                this.N_MOVES = moveTable[0].Length;
            }
            public override int getMoved(int move)
            {
                return moveTable[idx][move];
            }
            public override int getConj(int idx, int conj)
            {
                return conjTable[idx][conj];
            }
        }


		public static int[][] packSolved(int[] Solved1, int[] Solved2)
		{
			if (Solved1 == null)
			{
				Solved1 = new int[] { 0 };
			}
			if (Solved2 == null)
			{
				Solved2 = new int[] { 0 };
			}
			int[][] Solved = new int[Solved1.Length * Solved2.Length][];
			int idx = 0;
			//	for (int idx1 : Solved1) {
			//	for (int idx2 : Solved2)
			//	{
			//		Solved[idx++] = new int[] { idx1, idx2 };
			//	}
			//}
			foreach (int idx1 in Solved1)
			{
				foreach (int idx2 in Solved2)
				{
					Solved[idx++] = new int[] { idx1, idx2 };
				}
			}
			return Solved;
		}

		public static int IntegerbitCount(int i)
		{
			i = i - ((i >> 1) & 0x55555555);
			i = (i & 0x33333333) + ((i >> 2) & 0x33333333);
			i = (i + (i >> 4)) & 0x0f0f0f0f;
			i = i + (i >> 8);
			i = i + (i >> 16);
			return i & 0x3f;
		}


		public class PruningTable
		{
			int N_STATE;
			int N_STATE2;
			int[] Prun;
			int TABLE_MASK = 0x7fffffff;

			private static void setPrun(int[] Prun, int idx, int xorval)
			{
				Prun[idx >> 3] ^= xorval << (idx << 2);
			}

			public static int getPrun(int[] Prun, int idx)
			{
				return Prun[idx >> 3] >> (idx << 2) & 0xf;
			}

			public PruningTable(Coord coord, int[] Solved, string filename)
			{
				initPrunTable(coord, Solved, filename);
			}

			public PruningTable(SymCoord coord, int[] Solved, string filename)
			{
				if (Solved == null)
				{
					Solved = new int[] { 0 };
				}
				int[][] Solved2 = new int[Solved.Length][];
				for (int i = 0; i < Solved.Length; i++)
				{
					Solved2[i] = new int[2];
				}
				for (int i = 0; i < Solved.Length; i++)
				{
					Solved2[i][0] = Solved[i];
				}
				initPrunTable(coord, new RawCoord0(), Solved2, filename);
			}

			class RawCoord0 : RawCoord
            {
				public override int getMoved(int move)
				{
					return 0;
				}
			}


			public PruningTable(SymCoord symCoord, RawCoord rawCoord, int[][] Solved, string filename)
			{
				initPrunTable(symCoord, rawCoord, Solved, filename);
			}

			public PruningTable(SymCoord symCoord, RawCoord rawCoord, int[][] Solved, int maxl, int TABLE_SIZE, string filename)
			{
				initPrunTablePartial(symCoord, rawCoord, Solved, maxl, TABLE_SIZE, filename);
			}

			public PruningTable(int[][] Move, int[] Solved, string filename)
			{
				initPrunTable(new TableRawCoord(Move, null), Solved, filename);
			}

			public PruningTable(int[][] Move1, int[][] Move2, int[] Solved1, int[] Solved2, string filename)
			{
				N_STATE2 = Move2.Length;
				N_STATE = Move1.Length * Move2.Length;
				if (Solved1 == null)
				{
					Solved1 = new int[] { 0 };
				}
				if (Solved2 == null)
				{
					Solved2 = new int[] { 0 };
				}
				int[] Solved = new int[Solved1.Length * Solved2.Length];
				int idx = 0;
				//for (int idx1 : Solved1)
				//{
				//	for (int idx2 : Solved2)
				//	{
				//		Solved[idx++] = idx1 * N_STATE2 + idx2;
				//	}
				//}
				foreach (int idx1 in Solved1)
				{
					foreach (int idx2 in Solved2)
					{
						Solved[idx++] = idx1 * N_STATE2 + idx2;
					}
				}
				initPrunTable(new Coord1(Move1, Move2, N_STATE2), Solved, filename);
			}

            class Coord1 : Coord
            {
                int[][] Move1;
                int[][] Move2;
                int N_STATE2;
                public Coord1(int[][] Move1_, int[][] Move2_, int N_STATE2_)
                {
                    Move1 = Move1_;
                    Move2 = Move2_;
                    N_STATE2 = N_STATE2_;
					N_IDX = Move1.Length * Move2.Length;
					N_MOVES = Move1[0].Length;
				}
                int state2 = 0;
                public override void set(int i)
                {
                    idx = i / N_STATE2;
                    state2 = i % N_STATE2;
                }
                public override int getMoved(int move)
                {
                    return Move1[idx][move] * N_STATE2 + Move2[state2][move];
                }
            }


            private void initPrunTablePartial(SymCoord symCoord, RawCoord rawCoord, int[][] Solved, int maxl, int TABLE_SIZE, string filename)
			{
				N_STATE = symCoord.N_IDX * rawCoord.N_IDX;
				N_STATE2 = rawCoord.N_IDX;
				int N_SYM = symCoord.N_SYM;
				int N_MOVES = symCoord.N_MOVES;
				TABLE_MASK = TABLE_SIZE - 1;
				Prun = Tools.LoadFromFile(filename + "Prun.jpdata");
				if (Prun != null)
				{
					return;
				}
				if (Solved == null)
				{
					Solved = new int[][] { new int[] { 0, 0 } };
				}
				//java.util.HashMap<Long, Byte> PrunP = new java.util.HashMap<Long, Byte>();
				Dictionary<long, byte> PrunP = new Dictionary<long, byte>();
				int done = 0;
				long realDone = 0;
				int depth = 0;
				//	for (int[] val : Solved) {
				//	long idx = val[0] * (long)N_STATE2 + val[1];
				//	PrunP.put(idx, (byte)0);
				//	done++;
				//	realDone += N_SYM / Integer.bitCount(symCoord.SelfSym[val[0]]);
				//}
				foreach (int[] val in Solved)
				{
					long idx = val[0] * (long)N_STATE2 + val[1];
					PrunP.Add(idx, 0);
					done++;
					realDone += N_SYM / IntegerbitCount(symCoord.SelfSym[val[0]]);
				}
				int cumDone = done;
				long cumRealDone = cumDone;
				Stopwatch timer = Stopwatch.StartNew();

				do
				{
                    //Debug.WriteLine(String.Format("%s:%2d%,14d%,14d%,16d%,16d%10dms", filename,
                    //								 depth, done, cumDone, realDone, cumRealDone,
                    //								 (DateTime.Now.Ticks - startTime) / 1000000));
#if DEBUG
                    Debug.WriteLine($"{filename}:{depth,2}  {done,14}  {cumDone,14}  {realDone,14}  {cumRealDone,14}  {timer.ElapsedMilliseconds,10}ms");
#endif
					done = 0;
					realDone = 0;
					byte fill = (byte)(depth + 1);
					//java.util.HashMap<Long, Byte> PrunPClone = (java.util.HashMap<Long, Byte>)PrunP.clone();
					Dictionary<long, byte> PrunPClone = new Dictionary<long, byte>();
                    foreach (var item in PrunP)
                    {
						PrunPClone.Add(item.Key,item.Value);
                    }
					//for (java.util.Map.Entry<Long, Byte> entry : PrunPClone.entrySet())
					foreach (var entry in PrunPClone)
					{
						if (entry.Value != depth)
						{
							continue;
						}
						long i = entry.Key;
						symCoord.set((int)(i / N_STATE2));
						rawCoord.set((int)(i % N_STATE2));
						for (int m = 0; m < N_MOVES; m++)
						{
							int newSym = symCoord.getMoved(m);
							int newRaw = rawCoord.getConj(rawCoord.getMoved(m), newSym % N_SYM);
							newSym /= N_SYM;
							long newIdx = newSym * (long)N_STATE2 + newRaw;
							if (PrunPputIfAbsent(PrunP, newIdx, fill) != null)
							{
								continue;
							}
							done++;
							realDone += N_SYM / IntegerbitCount(symCoord.SelfSym[newSym]);
							for (int j = 1, symState = symCoord.SelfSym[newSym]; (symState >>= 1) != 0; j++)
							{
								if ((symState & 1) != 1)
								{
									continue;
								}
								long newIdx2 = newSym * (long)N_STATE2 + rawCoord.getConj(newRaw, j);
								if (PrunPputIfAbsent(PrunP, newIdx2, fill) != null)
								{
									continue;
								}
								done++;
								realDone += N_SYM / IntegerbitCount(symCoord.SelfSym[newSym]);
							}
						}
					}
					cumDone += done;
					cumRealDone += realDone;
					depth++;
				} while (done > 0 && depth < maxl);
				//Debug.WriteLine(String.Format("%s:%2d%,14d%,14d%,16d%,16d%10dms", filename,
				//								 depth, done, cumDone, realDone, cumRealDone,
				//								 (DateTime.Now.Ticks - startTime) / 1000000));
#if DEBUG
				Debug.WriteLine($"{ filename}:{depth,2}  {done,14}  {cumDone,14}  {realDone,14}  {cumRealDone,14}  {timer.ElapsedMilliseconds,10}ms");
#endif

                Prun = new int[TABLE_SIZE >> 3];
				for (int i = 0; i < Prun.Length; i++)
				{
					Prun[i] = 0x11111111 * (maxl + 1);
				}
				//for (java.util.Map.Entry<Long, Byte> entry : PrunP.entrySet())
				foreach (var entry in PrunP)
				{
					int idx = (int)(entry.Key & TABLE_MASK);
					int val = entry.Value;
					int prun = getPrun(Prun, idx);
					if (val < prun)
					{
						setPrun(Prun, idx, val ^ prun);
					}
				}
				int[] depthCnt = new int[16];
				for (int i = 0; i < TABLE_SIZE; i++)
				{
					depthCnt[getPrun(Prun, i)]++;
				}
				for (int i = 0; i < 16; i++)
				{
					if (depthCnt[i] != 0)
					{
                        //Debug.WriteLine(String.Format("%s-%2d%,14d", filename, i, depthCnt[i]));
#if DEBUG
                        Debug.WriteLine($"{filename}- {i,2}  {depthCnt[i],14}");
#endif
                    }
                }
				timer.Stop();
                Tools.SaveToFile(filename + "Prun.jpdata", Prun);
            }

			private byte? PrunPputIfAbsent(Dictionary<long, byte> PrunP, long key, byte value)
			{
				if (PrunP.ContainsKey(key))
				{
					return PrunP[key];
				}
				else
				{
					PrunP.Add(key, value);
					return null;
				}
			}

			private void initPrunTable(SymCoord symCoord, RawCoord rawCoord, int[][] Solved, string filename)
			{
				N_STATE = symCoord.N_IDX * rawCoord.N_IDX;
				N_STATE2 = rawCoord.N_IDX;
				int N_SYM = symCoord.N_SYM;
				int N_MOVES = symCoord.N_MOVES;
				Prun = Tools.LoadFromFile(filename + "Prun.jhdata");
				if (Prun != null)
				{
					return;
				}
				if (Solved == null)
				{
					Solved = new int[][] { new int[] { 0, 0 } };
				}
				Prun = new int[(N_STATE + 7) / 8];
				for (int i = 0; i < Prun.Length; i++)
				{
					Prun[i] = -1;
				}
				int done = 0;
				long realDone = 0;
				int depth = 0;
				//	for (int[] val : Solved) {
				//	int idx = val[0] * N_STATE2 + val[1];
				//	setPrun(Prun, idx, 0xf);
				//	done++;
				//	realDone += N_SYM / Integer.bitCount(symCoord.SelfSym[val[0]]);
				//}
				foreach (int[] val in Solved)
				{
					int idx = val[0] * N_STATE2 + val[1];
					setPrun(Prun, idx, 0xf);
					done++;
					realDone += N_SYM / IntegerbitCount(symCoord.SelfSym[val[0]]);

				}
				int cumDone = done;
				long cumRealDone = cumDone;
				Stopwatch timer=Stopwatch.StartNew();
				do
				{
					//Debug.WriteLine(String.Format("%s:%2d%,14d%,14d%,16d%,16d%10dms", filename,
					//								 depth, done, cumDone, realDone, cumRealDone,
					//								 (DateTime.Now.Ticks - startTime) / 1000000));
#if DEBUG
					Debug.WriteLine($"{filename}:{depth,2}  {done,14}  {cumDone,14}  {realDone,14}  {cumRealDone,14}  {timer.ElapsedMilliseconds,10}ms");
#endif
                    done = 0;
					realDone = 0;
					bool inv = cumDone > N_STATE / 2;
					// bool inv = true;
					int select = inv ? 0xf : depth;
					int check = inv ? depth : 0xf;
					int fill = depth + 1;
					depth++;
					int val = 0;
					for (int i = 0; i < N_STATE; i++, val >>= 4)
					{
						if ((i & 7) == 0)
						{
							val = Prun[i >> 3];
							if (!inv && val == -1)
							{
								i += 7;
								continue;
							}
						}
						if ((val & 0xf) != select)
						{
							continue;
						}
						symCoord.set(i / N_STATE2);
						rawCoord.set(i % N_STATE2);
						for (int m = 0; m < N_MOVES; m++)
						{
							int newSym = symCoord.getMoved(m);
							int newRaw = rawCoord.getConj(rawCoord.getMoved(m), newSym % N_SYM);
							newSym /= N_SYM;
							int newIdx = newSym * N_STATE2 + newRaw;
							if (getPrun(Prun, newIdx) != check)
							{
								continue;
							}
							done++;
							if (inv)
							{
								setPrun(Prun, i, fill ^ 0xf);
								realDone += N_SYM / IntegerbitCount(symCoord.SelfSym[i / N_STATE2]);
								break;
							}
							setPrun(Prun, newIdx, fill ^ 0xf);
							realDone += N_SYM / IntegerbitCount(symCoord.SelfSym[newSym]);

							for (int j = 1, symState = symCoord.SelfSym[newSym]; (symState >>= 1) != 0; j++)
							{
								if ((symState & 1) != 1)
								{
									continue;
								}
								int newIdx2 = newSym * N_STATE2 + rawCoord.getConj(newRaw, j);
								if (getPrun(Prun, newIdx2) != check)
								{
									continue;
								}
								setPrun(Prun, newIdx2, fill ^ 0xf);
								done++;
								realDone += N_SYM / IntegerbitCount(symCoord.SelfSym[newSym]);
							}
						}
					}
					cumDone += done;
					cumRealDone += realDone;
				} while (done > 0 && depth < 15);
				timer.Stop();
                Tools.SaveToFile(filename + "Prun.jhdata", Prun);
            }

			private void initPrunTable(Coord coord, int[] Solved, string filename)
			{
				N_STATE = coord.N_IDX;
				Prun = Tools.LoadFromFile(filename + "Prun.jhdata");
				if (Prun != null)
				{
					return;
				}
				if (Solved == null)
				{
					Solved = new int[] { 0 };
				}
				Prun = new int[(N_STATE + 7) / 8];
				for (int i = 0; i < Prun.Length; i++)
				{
					Prun[i] = -1;
				}
				int done = 0;
				int depth = 0;
				//	for (int idx : Solved) {
				//	setPrun(Prun, idx, 0xf);
				//	done++;
				//}
				foreach (int idx in Solved)
				{
					setPrun(Prun, idx, 0xf);
					done++;
				}
				Stopwatch timer=Stopwatch.StartNew();
				int cumDone = done;
				do
				{
					//Debug.WriteLine(string.Format("%s:%2d%,14d%,14d%10dms", filename,
					//								 depth, done, cumDone,
					//								 (DateTime.Now.Ticks - startTime) / 1000000));
#if DEBUG
					Debug.WriteLine($"{filename}:{depth,2}  {done,14}  {cumDone,14}  {timer.ElapsedMilliseconds,10}ms");
#endif
                    done = 0;
					bool inv = cumDone > N_STATE / 2;
					int select = inv ? 0xf : depth;
					int check = inv ? depth : 0xf;
					int fill = depth + 1;
					depth++;
					int val = 0;
					for (int i = 0; i < N_STATE; i++, val >>= 4)
					{
						if ((i & 7) == 0)
						{
							val = Prun[i >> 3];
							if (!inv && val == -1)
							{
								i += 7;
								continue;
							}
						}
						if ((val & 0xf) != select)
						{
							continue;
						}
						coord.set(i);
						for (int m = 0; m < coord.N_MOVES; m++)
						{
							int newIdx = coord.getMoved(m);
							if (getPrun(Prun, newIdx) != check)
							{
								continue;
							}
							done++;
							if (inv)
							{
								setPrun(Prun, i, fill ^ 0xf);
								break;
							}
							setPrun(Prun, newIdx, fill ^ 0xf);
						}
					}
					cumDone += done;
				} while (done > 0 && depth <= 15);
				timer.Stop();
                Tools.SaveToFile(filename + "Prun.jhdata", Prun);
            }

			public int getPrun(int state1, int state2)
			{
				return getPrun(Prun, (state1 * N_STATE2 + state2) & TABLE_MASK);
			}

			public int getPrun(int state)
			{
				return getPrun(Prun, state & TABLE_MASK);
			}

		}
	}
}
