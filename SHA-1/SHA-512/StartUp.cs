namespace SHA_512
{
    using System;

    public class StartUp
    {
        public static void Main()
        {
            string input = Console.ReadLine();
            Console.WriteLine(SHA512.Calculate(input));
        }
    }
}