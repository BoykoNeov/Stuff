namespace SHA_256alternative
{
    using System;
    using System.Text;
    using System.Linq;
    using System.Collections.Generic;

    public static class Program
    {
        static void Main(string[] args)
        {
            string t1 = Console.ReadLine();
            byte[] testV = Encoding.ASCII.GetBytes(t1);

            byte[] input = new byte[] { 1, 128, 250, 255 };

            input = testV;

            uint messageLengthInBits = (uint)input.Length * 8;

            var array = new byte[] { 10, 20, 30, 40, 50 };
            List<byte[]> splitArray = array.Split(2).ToList();




            Console.WriteLine(PadToMod512(testV, messageLengthInBits));
        }

        public static string PadToMod512(byte[] data, uint originalMessageLengthInBits)
        {
            List<byte> input = data.ToList();

            // same as add 0b10000000 as per SHA padding scheme 
            input.Add(128);

            string messageLengthInBinary = Convert.ToString(originalMessageLengthInBits, 2).PadLeft(128, '0');

            StringBuilder sb = new StringBuilder();

            foreach (byte b in input)
            {
                sb.Append(Convert.ToString(b, 2).PadLeft(8, '0'));
            }

            if ((sb.Length + 128 % 512) > 0)
            {
                string message = sb.ToString();
                message = message.PadRight((((message.Length + 127) / 512) + 1) * 512 - 128, '0');
                sb.Clear();
                sb.Append(message);
            }

            sb.Append(messageLengthInBinary);

            string result = sb.ToString();
            return result;
        }

        public static IEnumerable<byte[]> Split(this byte[] array, int size)
        {
            for (var i = 0; i < (float)array.Length / size; i++)
            {
                yield return array.Skip(i * size).Take(size).ToArray();
            }
        }
    }
}
