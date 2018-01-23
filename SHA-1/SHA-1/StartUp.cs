namespace SHA_1
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class StartUp
    {
        public static void Main()
        {
            string input = Console.ReadLine();
            string paddedInput = PadToMod512(input);
            string[] blocks = SplitToChunks(paddedInput, 512).ToArray();

            //TODO foreach block
            List<uint> words = SplitToChunks(blocks[0], 32).Select(x => Convert.ToUInt32(x, 2)).ToList();

            for (int i = 16; i < 80; i++)
            {
                // Perform XOR
                uint currentWord = words[i-3] ^ words[i-8] ^ words[i-14] ^ words[i-16];

                // ... which would perform a circular shift of a 32 bit value.
                // As a generalization to circular shift left n bits, on a b bit variable:
                /*some unsigned numeric type*/
                // var result = input << n | input >> (b - n);

                // Rotate left
                currentWord = currentWord << 1 | currentWord >> 31;
                words.Add(currentWord);
            }



            Console.WriteLine();
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
            string messageLengthInBinary = Convert.ToString(convertedToBinary.Length, 2).PadLeft(64, '0');

            // Add 1
            string message = convertedToBinary + "1";

            // Pad to 448 (448 + 64 bits for message length = 512)
            if (message.Length % 448 > 0)
            {
                message = message.PadRight((((message.Length + 63) / 512) + 1) * 512 - 64, '0');
            }

            message = message + messageLengthInBinary;

            //  return message + $" message length = {message.Length}, convertedtobinarylengthplus1 = {convertedToBinary.Length + 1}";
            return message;
        }
    }
}