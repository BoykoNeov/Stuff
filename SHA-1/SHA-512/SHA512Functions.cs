﻿namespace SHA_512
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public static class SHA512Functions
    {
        public static IEnumerable<string> SplitToChunks(string str, int chunkSize)
        {
            return Enumerable.Range(0, str.Length / chunkSize)
                .Select(i => str.Substring(i * chunkSize, chunkSize));
        }

        public static string PadToMod1024(string data)
        {
            // Convert input to binary
            StringBuilder sb = new StringBuilder();

            foreach (char c in data.ToCharArray())
            {
                sb.Append(Convert.ToString(c, 2).PadLeft(8, '0'));
            }

            string convertedToBinary = sb.ToString();
            // According to spec this should be a 128 bit integer, but I doubt that C# can handle it if it was larger than 2^64 anyway...
            string messageLengthInBinary = Convert.ToString(convertedToBinary.Length, 2).PadLeft(128, '0');

            // Add 1
            string message = convertedToBinary + "1";

            // Pad to 896 (896 + 128 bits for message length = 1024)
            if (message.Length  + 128 % 1024 > 0)
            {
                message = message.PadRight((((message.Length + 127) / 1024) + 1) * 1024 - 128, '0');
            }

            message = message + messageLengthInBinary;
            return message;
        }

        internal static void MainCycling(List<ulong> words, ulong[] overallHashes, ulong[] kConstants)
        {
            ulong a = overallHashes[0];
            ulong b = overallHashes[1];
            ulong c = overallHashes[2];
            ulong d = overallHashes[3];
            ulong e = overallHashes[4];
            ulong f = overallHashes[5];
            ulong g = overallHashes[6];
            ulong h = overallHashes[7];

            for (int i = 0; i < 80; i++)
            {
                ulong t1 = h + EpsilonOne(e) + Ch(e, f, g) + kConstants[i] + words[i];
                ulong t2 = EpsilonZero(a) + Maj(a, b, c);
                h = g;
                g = f;
                f = e;
                e = d + t1;
                d = c;
                c = b;
                b = a;
                a = t1 + t2;
            }

            overallHashes[0] += a;
            overallHashes[1] += b;
            overallHashes[2] += c;
            overallHashes[3] += d;
            overallHashes[4] += e;
            overallHashes[5] += f;
            overallHashes[6] += g;
            overallHashes[7] += h;
        }

        internal static List<ulong> MessageSchedule(string block)
        {
            List<ulong> words = SplitToChunks(block, 64).Select(x => Convert.ToUInt64(x, 2)).ToList();
            for (int i = 16; i < 80; i++)
            {
                ulong currentWord = SigmaOne(words[i - 2]) + words[i - 7] + SigmaZero(words[i - 15]) + words[i - 16];
                words.Add(currentWord);
            }

            return words;
        }

        public static ulong Ch(ulong x, ulong y, ulong z)
        {
            ulong result = (x & y) ^ (~x & z);
            return result;
        }

        public static ulong Maj(ulong x, ulong y, ulong z)
        {
            ulong result = (x & y) ^ (x & z) ^ (y & z);
            return result;
        }

        public static ulong EpsilonZero(ulong x)
        {
            ulong result = RotateRight(x, 28) ^ (RotateRight(x, 34)) ^ (RotateRight(x, 39));
            return result;
        }

        public static ulong EpsilonOne(ulong x)
        {
            ulong result = RotateRight(x, 14) ^ (RotateRight(x, 18)) ^ (RotateRight(x, 41));
            return result;
        }

        public static ulong SigmaZero(ulong x)
        {
            ulong result = RotateRight(x, 1) ^ RotateRight(x, 8) ^ (x >> 7);
            return result;
        }

        public static ulong SigmaOne(ulong x)
        {
            ulong result = RotateRight(x, 19) ^ RotateRight(x, 61) ^ (x >> 6);
            return result;
        }

        public static ulong RotateRight(ulong input, int numberOfRotations)
        {
            return input >> numberOfRotations | input << 64 - numberOfRotations;
        }
    }
}