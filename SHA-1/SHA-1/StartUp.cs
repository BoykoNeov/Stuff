namespace SHA_1
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class StartUp
    {
        public static void Main()
        {
            string input = Console.ReadLine();
            string output = PadToMod512(input);
            Console.WriteLine(output);
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
                message = message.PadRight(message.Length + (448 - message.Length % 512)  % 448, '0');
            }

            int test = message.Length + ((448 - message.Length % 512) % 448);

            message = message + messageLengthInBinary;

            return message + $" message length = {message.Length}, convertedtobinarylengthplus1 = {convertedToBinary.Length + 1}";
        }
    }
}