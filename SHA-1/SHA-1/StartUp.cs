namespace SHA_1
{
    using System;

    public class StartUp
    {
        public static void Main()
        {
            string input = Console.ReadLine();
            Console.WriteLine(SHA1.Calculate(input));
        }
    }
}