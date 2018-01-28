namespace SHA_256
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class SHA256
    {
        public static string Calculate(string input)
        {
            string paddedInput = SHA256Functions.PadToMod512(input);
            string[] blocks = SHA256Functions.SplitToChunks(paddedInput, 512).ToArray();
            uint[] overallHashes = new uint[8];
            Array.Copy(SHA256Constants.InitialHashes, overallHashes, 8);

            for (int j = 0; j < blocks.Length; j++)
            {
                // prepare message schedule
                List<uint> words = SHA256Functions.MessageSchedule(blocks[j]);

                // Core function, overallHashes are modified
                SHA256Functions.MainCycling(words, overallHashes, SHA256Constants.KConstants);
            }

            // display result
            string result = string.Join("", overallHashes.Select(x => Convert.ToString(x, 16).PadLeft(8, '0'))).ToUpper();
            return result;
        }
    }
}