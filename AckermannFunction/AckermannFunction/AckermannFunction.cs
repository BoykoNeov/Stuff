using System;
using System.Threading;

/// <summary>
/// Computing Ackermann function
/// </summary>
public class AckermannFunction
{
	public static void Main()
	{
		// without increasing the stack size, Ackermann cannot be computed for m=5 n=0 or m=4 n=1
		Thread thread = new Thread(new ThreadStart(A), 100000000);
		thread.Start();
	}

	private static void A()
	{
		Console.WriteLine("Please enter m");
		long m = int.Parse(Console.ReadLine());

		Console.WriteLine("Please enter n");
		long n = int.Parse(Console.ReadLine());

		Console.WriteLine("Computing");
		Console.WriteLine("Result is: " + Ackermann(m, n));
	}

	private static long Ackermann(long m, long n)
	{
		long result = 0;

		if (m == 0)
		{
			result = n + 1;
		}
		else if (m > 0 && n == 0)
		{
			result = Ackermann(m - 1, 1);
		}
		else if (m > 0 && n > 0)
		{
			result = Ackermann(m - 1, Ackermann(m, n - 1));
		}
		else
		{
			result = 0;
		}

		// Console.WriteLine("Result is: " + result);
		return result;
	}
}