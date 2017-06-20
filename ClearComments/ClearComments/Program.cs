using System.IO;
using System.Text.RegularExpressions;

/// <summary>
/// a program that removes all text starting with two or mode '/' and ending with newline from a file
/// </summary>
public class OddLines
{
    public static void Main(string[] args)
    {
        string input = File.ReadAllText(args[0]);
        string output = string.Empty;

        string regex = @"\/\/.*\n";
        output = Regex.Replace(input, regex, "");

        File.WriteAllText(args[0], output.ToString());
    }
}