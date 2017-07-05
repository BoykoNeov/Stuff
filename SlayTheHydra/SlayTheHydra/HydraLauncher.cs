/// <summary>
/// Inspired by PBS series "Kill the mathematical Hydra"
/// </summary>
namespace SlayTheHydra
{
    using System;

    /// <summary>
    /// Contains Main() method
    /// </summary>
    public class HydraLauncher
    {
        public static void Main()
        {
            Hydra<int> hydra = new Hydra<int>();
            hydra.AddHead(1);
            hydra.AddHead(2, 0);
            hydra.AddHead(2, 0);
            hydra.AddHead(2, 0);

            // hydra.AddHead(3, 0, 0);
            hydra.AddHead(3, 0, 0);
            hydra.AddHead(3, 0, 0);


            //  hydra.AddHead(4, 0, 0, 0);
            //hydra.AddHead(4, 0, 0, 0);

            //  hydra.GrowHeads(hydra.Body.SubHeads[0].SubHeads[2]);
            hydra.DrawHydra();
            Console.WriteLine("steps to slay:" + hydra.Slay());
            hydra.DrawHydra();
        }
    }
}