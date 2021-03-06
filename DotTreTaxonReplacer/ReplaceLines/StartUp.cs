﻿using System.IO;
using System.Text;
using System.Collections.Generic;
using System;

public class StartUp
{
    /// <summary>
    /// Replaces number key strings each with another value strings plus key string
    /// The use is to replace names in .tre files generated by MrBayes
    /// </summary>
    public static void Main()
    {
        Console.WriteLine("Enter source file path containing dictionary in the format 'withthat - replacethis' :");
        string diciotnaryFilePath = Console.ReadLine();
        string[] dictInput = File.ReadAllLines(diciotnaryFilePath);

        Dictionary<string, string> replacingDictionary = new Dictionary<string, string>();

        foreach (string inputLine in dictInput)
        {
            string[] arguments = inputLine.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);

            try
            {
                replacingDictionary.Add(arguments[1], arguments[0]);
            }
            catch
            {
                Console.WriteLine("Error reading data from replacement dictionary, incorrect format or repeating entries");
                return;
            }
        }


        Console.WriteLine("Enter file to search and replace:");
        string replacementFilePath = Console.ReadLine();
        string[] replacementFile = File.ReadAllLines(replacementFilePath);
        StringBuilder sb = new StringBuilder();


        foreach (string line in replacementFile)
        {
            string outputLine = line;

            foreach (KeyValuePair<string,string> dictionaryEntry in replacingDictionary)
            {
                outputLine = outputLine.Replace(dictionaryEntry.Key, dictionaryEntry.Value);
            }

            sb.AppendLine(outputLine);
        }

        File.WriteAllText(replacementFilePath, sb.ToString());
    }
}