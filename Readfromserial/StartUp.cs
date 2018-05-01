using System;
using System.IO.Ports;
using System.Threading;
using System.IO;

public class StartUp
{
    public static void Main()
    {
        const int ArraysToCycle = 200;
        const int ArraySize = 10_000;

        Console.WriteLine("Enter file path with file name: ");
        string filePath = Console.ReadLine();

        if (!File.Exists(filePath))
        {
            using (var stream = File.Create(filePath))
            {
            }
        }

        var port = new SerialPort("COM5", 9600, Parity.None, 8, StopBits.One);
        port.Open();


        Thread.Sleep(3000);

        for (int j = 0; j < ArraysToCycle; j++)
        {
            byte[] bytes = new byte[ArraySize];
            for (int i = 0; i < ArraySize; i++)
            {
                var portInput = port.ReadLine();

                try
                {
                    bytes[i] = byte.Parse(portInput);
                }
                catch
                {
                    Console.WriteLine($"Error parsing: {portInput}");
                    if (i > 0)
                    {
                        i--;
                    }
                }
            }

            Console.WriteLine($"Processed this far: {(j + 1) * ArraySize}");
            AppendAllBytes(filePath, bytes);
        }

        port.Close();
    }

    public static void AppendAllBytes(string path, byte[] bytes)
    {
        //argument-checking here.

        using (var stream = new FileStream(path, FileMode.Append))
        {
            stream.Write(bytes, 0, bytes.Length);
        }
    }
}