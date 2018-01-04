using System.IO;
using System.Text.RegularExpressions;
using System;

/// <summary>
/// a program that replaces a regex match in text with a new string and outputs it into a new file
/// </summary>
public class OddLines
{
    public static void Main()
    {
        Console.WriteLine("Enter source file path:");
        string sourceFilePath = Console.ReadLine();

        Console.WriteLine("Enter output file path:");
        string outputFilePath = Console.ReadLine();

        Console.WriteLine("Enter regex:");
        string regex = Console.ReadLine();

        Console.WriteLine("Enter replacing string:");
        string replacer = Console.ReadLine();

        string input = File.ReadAllText(sourceFilePath);
        string output = string.Empty;

        output = Regex.Replace(input, regex, replacer);

        File.WriteAllText(outputFilePath, output.ToString());
    }
}