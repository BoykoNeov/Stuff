namespace SlayTheHydra
{
    using System;

    public class HydraLauncher
    {
        public static void Main()
        {
            Hydra<int> hydra = new Hydra<int>();
            hydra.AddHead(1);
            hydra.AddHead(5, 0);
            hydra.AddHead(6, 0);
            hydra.AddHead(7, 0);
            hydra.AddHead(20, 0 , 0);
            hydra.AddHead(30, 0, 0);
            hydra.AddHead(40, 0, 1);
            hydra.AddHead(50, 0, 1);
            hydra.AddHead(60, 0, 2);
            hydra.AddHead(70, 0, 2);

            hydra.GrowHeads(hydra.Body.SubHeads[0]);


            hydra.DrawHydra();
        }
    }
}