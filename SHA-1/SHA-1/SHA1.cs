namespace SHA_1
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public static class SHA1
    {
        /// <summary>
        /// Returns the SHA-1 hash value of a string
        /// </summary>
        /// <param name="input">String to calculate SHA-1 hash</param>
        /// <returns></returns>
        public static string Calculate(string input)
        {
            string paddedInput = PadToMod512(input);
            string[] blocks = SplitToChunks(paddedInput, 512).ToArray();

            uint[] overallHashes = new uint[5] { Constants.H0, Constants.H1, Constants.H2, Constants.H3, Constants.H4 };

            for (int j = 0; j < blocks.Length; j++)
            {
                List<uint> words = SplitToChunks(blocks[j], 32).Select(x => Convert.ToUInt32(x, 2)).ToList();

                for (int i = 16; i < 80; i++)
                {
                    // Perform XOR
                    uint currentWord = words[i - 3] ^ words[i - 8] ^ words[i - 14] ^ words[i - 16];

                    // Rotate left by 1
                    currentWord = RotateLeft(currentWord, 1);
                    words.Add(currentWord);
                }

                // initialize working variables
                uint a = overallHashes[0];
                uint b = overallHashes[1];
                uint c = overallHashes[2];
                uint d = overallHashes[3];
                uint e = overallHashes[4];

                for (int i = 0; i < 80; i++)
                {
                    uint constant = 0;

                    if (i <= 19)
                    {
                        constant = Constants.Kt_0to_19;
                    }
                    else if (i <= 39)
                    {
                        constant = Constants.Kt_20to39;
                    }
                    else if (i <= 59)
                    {
                        constant = Constants.Kt_40to59;
                    }
                    else
                    {
                        constant = Constants.Kt_60to79;
                    }

                    uint temp = RotateLeft(a, 5) + ProcessingFunction(i, b, c, d) + e + constant + words[i];
                    e = d;
                    d = c;
                    c = RotateLeft(b, 30);
                    b = a;
                    a = temp;
                }

                // Compute intermidiate hash values
                overallHashes[0] += a;
                overallHashes[1] += b;
                overallHashes[2] += c;
                overallHashes[3] += d;
                overallHashes[4] += e;
            }

            // display result
            string result = string.Join("", overallHashes.Select(x => Convert.ToString(x, 16).PadLeft(8, '0')));
            return result;
        }

        // SHA-1 "Processing function"
        private static uint ProcessingFunction(int round, uint b, uint c, uint d)
        {
            uint result = 0;

            if (round <= 19)
            {
                result = (b & c) | ((~b) & d);
            }
            else if (round <= 39)
            {
                result = b ^ c ^ d;
            }
            else if (round <= 59)
            {
                result = (b & c) | (b & d) | (c & d);
            }
            else
            {
                result = b ^ c ^ d;
            }

            return result;
        }

        /// <summary>
        /// Rotates an uint bitwise by a specified ammount
        /// </summary>
        /// <param name="input">uint to be rotated</param>
        /// <param name="numberOfRotations">positions to rotate</param>
        /// <returns></returns>
        // ... which would perform a circular shift of a 32 bit value.
        // As a generalization to circular shift left n bits, on a b bit variable:
        /*some unsigned numeric type*/
        // var result = input << n | input >> (b - n);
        private static uint RotateLeft(uint input, int numberOfRotations)
        {
            return input << numberOfRotations | input >> 32 - numberOfRotations;
        }


        private static IEnumerable<string> SplitToChunks(string str, int chunkSize)
        {
            return Enumerable.Range(0, str.Length / chunkSize)
                .Select(i => str.Substring(i * chunkSize, chunkSize));
        }

        private static string PadToMod512(string data)
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