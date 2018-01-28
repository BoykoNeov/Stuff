namespace SHA_512
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    internal class SHA512
    {
        internal static string Calculate(string input)
         {
            string paddedInput = SHA512Functions.PadToMod1024(input);
            string[] blocks = SHA512Functions.SplitToChunks(paddedInput, 1024).ToArray();
            ulong[] overallHashes = new ulong[8];
            Array.Copy(SHA512Constants.InitialHashes, overallHashes, 8);

            for (int j = 0; j < blocks.Length; j++)
            {          
                // prepare message schedule
                List<ulong> words = SHA512Functions.MessageSchedule(blocks[j]);

                // Core function, overallHashes are modified
                SHA512Functions.MainCycling(words, overallHashes, SHA512Constants.KConstants);
            }

            // display result
            StringBuilder resultSb = new StringBuilder();
            foreach (var hash in overallHashes)
            {
                resultSb.Append(string.Format("{0:X16}", hash).PadLeft(16, '0'));
            }
            string result = resultSb.ToString();
            return result;
        }
    }
}