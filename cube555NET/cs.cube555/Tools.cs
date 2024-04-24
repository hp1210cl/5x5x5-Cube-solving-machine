using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.Json;

namespace cs.cube555
{

	public class Tools
	{

		public static bool SaveToFile(string filename, int[] obj)
		{
			try
			{
				//string s = JsonSerializer.Serialize(obj);
				//File.WriteAllText(filename, s);
				using(FileStream fileStream=new FileStream(filename, FileMode.CreateNew))
                {
					byte[] b = new byte[obj.Length * 4];
					Buffer.BlockCopy(obj, 0, b, 0, b.Length);
					fileStream.Write(b);
                }
			}
			catch (Exception e)
			{
#if DEBUG
                Debug.WriteLine(e);
#endif
				return false;
			}
			return true;
		}

		public static int[] LoadFromFile(string filename)
		{
			int[] ret;
			try
			{
				//string s = File.ReadAllText(filename);
				//ret = JsonSerializer.Deserialize<int[]>(s);
				using(FileStream fileStream=new FileStream(filename, FileMode.Open))
                {
					byte[] b = new byte[fileStream.Length];
					fileStream.Read(b, 0, b.Length);
					ret = new int[b.Length / 4];
					Buffer.BlockCopy(b, 0, ret, 0, b.Length);
                }
			}
			catch (Exception e)
			{
#if DEBUG
                Debug.WriteLine(e);
#endif
				return null;
			}
			return ret;
		}

		static Random gen = new Random();

		static CubieCube randomCubieCube(Random gen)
		{
			CubieCube cc = new CubieCube();
			for (int i = 0; i < 23; i++)
			{
				Util.swap(cc.xCenter, i, i + gen.Next(24 - i));
				Util.swap(cc.tCenter, i, i + gen.Next(24 - i));
				Util.swap(cc.wEdge, i, i + gen.Next(24 - i));
			}
			int eoSum = 0;
			int eParity = 0;
			for (int i = 0; i < 11; i++)
			{
				int swap = gen.Next(12 - i);
				if (swap != 0)
				{
					Util.swap(cc.mEdge, i, i + swap);
					eParity ^= 1;
				}
				int flip = gen.Next(2);
				cc.mEdge[i] ^= flip;
				eoSum ^= flip;
			}
			cc.mEdge[11] ^= eoSum;
			int cp = 0;
			do
			{
				cp = gen.Next(40320);
			} while (eParity != Util.getParity(cp, 8));
			cc.corner.copy(new CubieCube.CornerCube(cp, gen.Next(2187)));
			return cc;
		}

		static CubieCube randomCubieCube()
		{
			return randomCubieCube(gen);
		}

		public static string randomCube(Random gen)
		{
			return randomCubieCube(gen).toFacelet();
		}

		public static string randomCube()
		{
			return randomCube(gen);
		}
	}
}
