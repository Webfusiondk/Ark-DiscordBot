using System;
using System.Collections.Generic;
using System.Text;
using DSharpPlus.CommandsNext;
using DSharpPlus;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext.Attributes;

namespace Ark_DiscordBot
{
    public class DinoStatsCommand 
    {
        List<Dino> dinoList = new List<Dino>();
        [Command("AddDino")]
        [Description("Add Dino to the database")]
        public async Task AddDino(CommandContext ctx, string dinoName, int hp, int stam, int ox, int food, int weight, int melee, int movement)
        { 
            dinoList.Add(new Dino(dinoName, hp, stam, ox, food, weight, melee, movement));
            await ctx.Channel.SendMessageAsync(dinoName + " added to list").ConfigureAwait(false);
        }
        [Command("DinoStats")]
        [Description("Print the stats of specific dino")]
        public async Task DinoStats(CommandContext ctx)
        {
            foreach (Dino dino in dinoList)
            {
                await ctx.Channel.SendMessageAsync(dino.Name + " H: " + dino.Health + " S: " + dino.Stam + " OX: " + dino.Oxygen + " F: " + dino.Food + " W: " + dino.Weight + " ME: " + dino.Melee + " MS: " + dino.Movement).ConfigureAwait(false);
            }
        }

        [Command("UpdateStats")]
        [Description("Update all stats on the dino")]
        public async Task UpdateStats(CommandContext ctx, string name, int hp, int stam, int ox, int food, int weight, int melee, int movement)
        {
            foreach (Dino dino in dinoList)
            {
                if (dino.Name == name)
                {
                    dino.Health = hp;
                    dino.Stam = stam;
                    dino.Oxygen = ox;
                    dino.Food = food;
                    dino.Weight = weight;
                    dino.Melee = melee;
                    dino.Movement = movement;
                    await ctx.Channel.SendMessageAsync(dino.Name + " stats updated").ConfigureAwait(false);
                }
            }
        }

        [Command("UpdateStat")]
        [Description("Updating specfic stats for the dino")]
        public async Task UpdateStat(CommandContext ctx, string name, string stat, int value)
        {
           string updated = "There is no dinos in the database";
            foreach (Dino dino in dinoList)
            {
                if (dino.Name == name)
                {
                    updated = dino.Name + " stats updated";
                    switch (stat.ToLower())
                    {
                        case "h":
                            dino.Health = value;
                            break;
                        case "s":
                            dino.Stam = value;
                            break;
                        case "ox":
                            dino.Oxygen = value;
                            break;
                        case "f":
                            dino.Food = value;
                            break;
                        case "w":
                            dino.Weight = value;
                            break;
                        case "me":
                            dino.Melee = value;
                            break;
                        case "ms":
                            dino.Movement = value;
                            break;
                        default:
                            updated = "Wrong input";
                            break;
                    }
                }
                else
                    updated = "There is no dinos with that name" + "\n" + "try adding the dino or check if you input name wrong";
            }
            await ctx.Channel.SendMessageAsync(updated).ConfigureAwait(false);
        }
    }
}
