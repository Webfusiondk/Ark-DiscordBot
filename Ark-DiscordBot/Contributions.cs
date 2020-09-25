using System;
using System.Collections.Generic;
using System.Text;

namespace Ark_DiscordBot
{
    class Contributions
    {
        public int MetalRunComplet { get; set; }
        public int WoodRunComplet { get; set; }
        public int FlintRunComplet { get; set; }
        public int StoneRunComplet { get; set; }
        public int GunpowderRunComplet { get; set; }
        public int PearlsRunComplet { get; set; }
        public int ArbRunComplet { get; set; }
        public int MeatRunComplet { get; set; }
        public int BerryRunComplet { get; set; }
        public int RoutineTaskComplet { get {return MetalRunComplet + WoodRunComplet + FlintRunComplet + StoneRunComplet + GunpowderRunComplet + PearlsRunComplet + ArbRunComplet + MeatRunComplet +BerryRunComplet;} }
        public int SpecificTaskComplet { get; set; }
        public Contributions()
        {
            MetalRunComplet = 0;
            WoodRunComplet = 0;
            FlintRunComplet = 0;
            StoneRunComplet = 0;
            GunpowderRunComplet = 0;
            PearlsRunComplet = 0;
            ArbRunComplet = 0;
            MeatRunComplet = 0;
            BerryRunComplet = 0;
            SpecificTaskComplet = 0;
        }
    }
}
