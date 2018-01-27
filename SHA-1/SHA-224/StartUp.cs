namespace SHA_224
{
    using System;

    public class StartUp
    {
        public static void Main()
        {
            string input = Console.ReadLine();
            Console.WriteLine(SHA224.Calculate(input));
        }
    }
}