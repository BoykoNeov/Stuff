namespace InBuiltRNGTest
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    public class StartUp
    {
        public static void Main()
        {
            const int LowerLimit = 0;
            const int UpperLimit = 100000;

            HashSet<int> testSet = new HashSet<int>();
            SortedDictionary<int, int> numberOccurences = new SortedDictionary<int, int>();
            for (int i = LowerLimit; i < UpperLimit; i++)
            {
                testSet.Add(i);
                numberOccurences.Add(i, 0);
            }

            Random random = new Random();
            Stopwatch watch = new Stopwatch();


            watch.Start();
            while (testSet.Count > 0)
            {
                int randomNumber = random.Next(LowerLimit, UpperLimit);
                testSet.Remove(randomNumber);
                numberOccurences[randomNumber] = numberOccurences[randomNumber] + 1;
            }

            watch.Stop();
            Console.Write("Elapsed miliseconds");
            Console.WriteLine(watch.ElapsedMilliseconds);

            foreach (KeyValuePair<int, int> number in numberOccurences.OrderBy(x => x.Value).Take(10))
            {
                Console.WriteLine($"number: {number.Key} occurences {number.Value}");
            }

            Console.WriteLine("    ******************************     ");

            foreach (KeyValuePair<int, int> number in numberOccurences.OrderByDescending(x => x.Value).Take(10))
            {
                Console.WriteLine($"number: {number.Key} occurences {number.Value}");
            }
        }
    }
}