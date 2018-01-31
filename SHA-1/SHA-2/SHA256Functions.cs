namespace SHA_256
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public static class SHA256Functions
    {
        public static uint Ch(uint x, uint y, uint z)
        {
            uint result = (x & y) ^ (~x & z);
            return result;
        }

        public static uint Maj(uint x, uint y, uint z)
        {
            uint result = (x & y) ^ (x & z) ^ (y & z);
            return result;
        }

        public static uint EpsilonZero(uint x)
        {
            uint result = RotateRight(x, 2) ^ (RotateRight(x, 13)) ^ (RotateRight(x, 22));
            return result;
        }

        public static uint EpsilonOne(uint x)
        {
            uint result = RotateRight(x, 6) ^ (RotateRight(x, 11)) ^ (RotateRight(x, 25));
            return result;
        }

        public static void MainCycling(List<uint> words, uint[] overallHashes, uint[] kConstants)
        {
            uint a = overallHashes[0];
            uint b = overallHashes[1];
            uint c = overallHashes[2];
            uint d = overallHashes[3];
            uint e = overallHashes[4];
            uint f = overallHashes[5];
            uint g = overallHashes[6];
            uint h = overallHashes[7];

            for (int i = 0; i < 64; i++)
            {
                uint t1 = h + EpsilonOne(e) + Ch(e, f, g) + kConstants[i] + words[i];
                uint t2 = EpsilonZero(a) + Maj(a, b, c);
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

        public static uint SigmaZero(uint x)
        {
            uint result = RotateRight(x, 7) ^ RotateRight(x, 18) ^ (x >> 3);
            return result;
        }

        public static uint SigmaOne(uint x)
        {
            uint result = RotateRight(x, 17) ^ RotateRight(x, 19) ^ (x >> 10);
            return result;
        }

        public static uint RotateRight(uint input, int numberOfRotations)
        {
            return input >> numberOfRotations | input << 32 - numberOfRotations;
        }

        public static IEnumerable<string> SplitToChunks(string str, int chunkSize)
        {
            return Enumerable.Range(0, str.Length / chunkSize)
                .Select(i => str.Substring(i * chunkSize, chunkSize));
        }

        public static string PadToMod512(string data)
        {
            // Convert input to binary
            StringBuilder sb = new StringBuilder();

            foreach (char c in data.ToCharArray())
            {
                sb.Append(Convert.ToString(c, 2).PadLeft(8, '0'));
            }

            string convertedToBinary = sb.ToString();
            string messageLengthInBinary = Convert.ToString(convertedToBinary.Length, 2).PadLeft(128, '0');

            // Add 1
            string message = convertedToBinary + "1";

            // Pad to 448 (448 + 64 bits for message length = 512)
            if ((message.Length + 128 % 512) > 0)
            {
                message = message.PadRight((((message.Length + 127) / 512) + 1) * 512 - 128, '0');
            }

            message = message + messageLengthInBinary;
            return message;
        }

        public static List<uint> MessageSchedule(string block)
        {
            List<uint> words = SplitToChunks(block, 32).Select(x => Convert.ToUInt32(x, 2)).ToList();
            for (int i = 16; i < 64; i++)
            {
                uint currentWord = SigmaOne(words[i - 2]) + words[i - 7] + SigmaZero(words[i - 15]) + words[i - 16];
                words.Add(currentWord);
            }

            return words;
        }
    }
}