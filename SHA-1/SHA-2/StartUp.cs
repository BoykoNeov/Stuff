﻿namespace SHA_256
{
    using System;

    public class StartUp
    {
        public static void Main()
        {
            string input = Console.ReadLine();
            Console.WriteLine(SHA256.Calculate(input));
        }
    }
}