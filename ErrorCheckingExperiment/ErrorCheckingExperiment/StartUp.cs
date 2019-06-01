using System;
using System.Collections.Generic;

public class StartUp
{
    static void Main()
    {
        while (true)
        {

            const int collectionLength = 1000;

            byte[] collectionA = new byte[collectionLength];
            byte[] collectionB = new byte[collectionLength];
            Random rng = new Random();

            for (int i = 0; i < collectionA.Length; i++)
            {
                byte collectionItem = (byte)rng.Next(255);
                collectionA[i] = collectionItem;
                collectionB[i] = collectionItem;
            }

            int cycleCount = 1_000_000;
            uint collisionsCount = 0;

            for (int i = 0; i < cycleCount; i++)
            {

                int variablePositionsTarget = 5;
                int currentlyVariedPositions = 0;

                HashSet<uint> variedPositions = new HashSet<uint>();

                while (currentlyVariedPositions < variablePositionsTarget)
                {
                    byte randomByte = (byte)rng.Next(255);
                    int arrayPosition = rng.Next(collectionLength);

                    if (randomByte == collectionA[arrayPosition] || randomByte == collectionB[arrayPosition])
                    {
                        continue;
                    }

                    variedPositions.Add((uint)arrayPosition);
                    collectionB[arrayPosition] = randomByte;
                    currentlyVariedPositions++;

                    uint resultCollectionA = 0;
                    uint resultCollectionB = 0;

                    for (int j = 0; j < collectionLength; j++)
                    {
                        resultCollectionA += (uint)collectionA[j] * (uint)j;
                        resultCollectionB += (uint)collectionB[j] * (uint)j;
                    }

                    if (resultCollectionA == resultCollectionB)
                    {
                        collisionsCount++;
                    }

                    foreach (uint position in variedPositions)
                    {
                        collectionA[position] = collectionB[position];
                    }
                }
            }

            Console.WriteLine(collisionsCount);
        }
    }
}