using System;
using System.Collections.Generic;
using System.Text;

namespace Ark_DiscordBot
{
    public class Dino
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public int Stam { get; set; }
        public int Oxygen { get; set; }
        public int Food { get; set; }
        public int Weight { get; set; }
        public int Melee { get; set; }
        public int Movement { get; set; }
        public Dino(string name, int hp, int stam, int ox, int food, int weight, int melee, int movement)
        {
            Name = name;
            Health = hp;
            Stam = stam;
            Oxygen = ox;
            Food = food;
            Weight = weight;
            Melee = melee;
            Movement = movement;
        }
    }
}
