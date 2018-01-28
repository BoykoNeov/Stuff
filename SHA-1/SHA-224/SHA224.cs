namespace SHA_224
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using SHA_256;

    public static class SHA224
    {
        public static string Calculate(string input)
        {
            string paddedInput = SHA256Functions.PadToMod512(input);
            string[] blocks = SHA256Functions.SplitToChunks(paddedInput, 512).ToArray();
            uint[] overallHashes = new uint[8];
            Array.Copy(SHA224Constants.InitialHashes, overallHashes, 8);

            for (int j = 0; j < blocks.Length; j++)
            {
                // prepare message schedule
                List<uint> words = SHA256Functions.MessageSchedule(blocks[j]);

                // Core function, overallHashes are modified / the functions are the same as SHA-256, the constants are different
                SHA256Functions.MainCycling(words, overallHashes, SHA224Constants.KConstants);
            }

            // display result. Result is trimmed to 224 bits
            string result = string.Join("", overallHashes.Select(x => Convert.ToString(x, 16).PadLeft(8, '0'))).Substring(0, 56).ToUpper();
            return result;
        }
    }
}