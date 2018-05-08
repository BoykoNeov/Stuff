// ALGORITHM
// Description
// a,b,c,d and n as ( 16 bit )  
// I and m as ( 64 bit )  
// m=2**56 
// n = 2**16-1 
// a = 0  
// b = 0  
// c = 0  
// d = 2**13 
// X[0 to n] = Initialzed with a 56 bit values

// Key-scheduling algorithm
// for i from 0 to n
//   a = (a + c +  X[i] mod n + i ) mod(n + 1 )
//   b = (b + a ) mod(n + 1 )
//   c = (c + b ) mod(n + 1 )
//   X[i] = (X[i] + a* b * c * d + a + b + c ) mod m
//endfor

//Pseudo-random generation algorithm
// i = 0
//while GeneratingOutput:  
//   X[a] = (X[b] + X[c] ) mod m
//   a = (a + c + i ) mod(n + 1 )
//   Output X[a] + X[b]
//   b = (b + a) mod(n + 1 )
//   Output X[b] + X[c]
//   c = (c + b) mod(n + 1 )
//   Output X[c] + X[a]
//   i = i + 1
//   endwhile 

using System;

namespace Melgo_RNG
{
    class Program
    {
        static void Main(string[] args)
        {
            UInt16 a = 0;
            UInt16 b = 0;
            UInt16 c = 0;

            // there is difference between algorithm description and the example JavaScript implementation, in the JS it is -1, while in the algorithm without "-1"
            UInt16 d = (ushort)(Math.Pow(2, 13)- 1);

            UInt16 n = (ushort)(Math.Pow(2, 16) - 1);

            UInt64 i;
            UInt64 m = (ulong)Math.Pow(2, 56);

            UInt64[] x = new ulong[n + 1];

            Random random = new Random();

            //initiate with values
            for (i = 0; i <= n; i++)
            {
                x[i] = (ulong)random.Next(0, (int)m);
            }

            // Key scheduling
            for (i = 0; i <= n; i++)
            {
                a = (ushort)(((ulong)a + (ulong)c + x[i] % (ulong)n + i) % ((ulong)n + 1));
                b = (ushort)((b + a) % (n + 1));
                c = (ushort)((c + b) % (n + 1));

                //again there is difference
                x[i] = (x[i] + ((ulong)a * b * c * d) + a + b + c) % m;
            }

            i = 0;

            while (true)
            {
                x[a] = (x[b] + x[c]) % m;
                a = (ushort)((a + b + (ushort)i) % (n + 1));
                Console.WriteLine(x[a] + x[b]);

                b = (ushort)((b + a) % (n + 1));
                Console.WriteLine(x[c] + x[b]);

                c = (ushort)((c + b) % (n + 1));
                Console.WriteLine(x[a] + x[c]);
            }
        }
    }
}