using System;
using System.Collections.Generic;
using System.Diagnostics;

public class StartUp
{
    public static void Main()
    {
        HashSet<Guid> guidSet = new HashSet<Guid>();
        const long GuidCount = 400_000_000;
        long totalGuidCount = 0;
        bool colisionFound = false;

        Stopwatch iterationWatch = new Stopwatch();
        Stopwatch totalWatch = new Stopwatch();

        iterationWatch.Start();

        for (int i = 0; i < GuidCount; i++)
        {
            Guid newGuid = Guid.NewGuid();

            if (guidSet.Contains(newGuid))
            {
                Console.WriteLine($"Colllision found at {i} positions");
            }

            guidSet.Add(newGuid);
        }

        iterationWatch.Stop();
        Console.WriteLine($"Filling of {GuidCount} in hashset Guids completed in {iterationWatch.ElapsedMilliseconds / 1000} seconds");
        iterationWatch.Reset();

        iterationWatch.Start();
        totalWatch.Start();

        totalGuidCount = GuidCount;

        while (true)
        {
            for (int i = 0; i < GuidCount; i++)
            {
                Guid currentGuid = Guid.NewGuid();

                if (guidSet.Contains(currentGuid))
                {
                    Console.WriteLine("Collision!");
                    Console.WriteLine(currentGuid);
                    Console.WriteLine($"Position: {i}");
                    Console.WriteLine($"Total Guids to this moment: {totalGuidCount}");
                    Console.WriteLine();
                    colisionFound = true;
                    break;
                }
            }

            //   guidSet.Clear();
            //   GC.Collect();

            totalGuidCount += GuidCount;
            Console.WriteLine($"Chunk: {GuidCount / 1_000_000} millions , total: {(decimal)totalGuidCount / 1_000_000_000} billions");
            Console.WriteLine($"Iteration time in ms: {iterationWatch.ElapsedMilliseconds / 1000} , total time in sec: {totalWatch.ElapsedMilliseconds / 1000}" + Environment.NewLine);

            iterationWatch.Restart();

            if (colisionFound)
            {
                break;
            }
        }
    }
}