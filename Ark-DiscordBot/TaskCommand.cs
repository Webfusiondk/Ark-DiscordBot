using System;
using System.Collections.Generic;
using System.Text;
using DSharpPlus.CommandsNext;
using DSharpPlus;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext.Attributes;
using System.Timers;
using System.Collections.ObjectModel;

namespace Ark_DiscordBot
{
    class TaskCommand
    {

        [Command("CreateTask")]
        [Description("Make a new task that will have a timer untill its completed \n The timer will ")]


        public async Task CreateTask(CommandContext ctx, [Description("Give the worktask an title")] string title, [Description("Give the worktask an descripton of the worktask")] string desc, [Description("Set the amount of time before the task sould be done")] int time)
        {
            await ctx.Message.DeleteAsync();
            await ctx.Channel.Guild.GetChannel(758954904462688307).SendMessageAsync(
                "Task: **" + title + "**"
               + "```" + "\n" + "Desc: " + desc + "\n" +
                "Hours: " + time +
                "\n" + "Task created" + "```");



            TaskManager.Instance.Tasks.Add(new WorkTasks(title, desc, time, ctx.Guild.GetChannel(758683186183405621)));
        }
        [Command("StopTask")]
        public async Task StopTask(CommandContext ctx, string title)
        {
            for (int i = 0; i < TaskManager.Instance.Tasks.Count; i++)
            {
                if (TaskManager.Instance.Tasks[i].Title == title)
                {
                    await ctx.Guild.GetChannel(758683186183405621).SendMessageAsync("Task is done");
                    TaskManager.Instance.Tasks[i].Timer.Stop();
                }
            }
        }

        private string title, desc, temp = String.Empty;
        [Command("Task")]
        [Description("This command is made so you can start a task easy that will all ways have a duration for 24 hours \nIf you want to know what routine task you can start do !Task ?")]
        public async Task StartTask(CommandContext ctx, [Description("Insert the name of the routine task you want to start.")] string task)
        {
            int time = 24;
            await ctx.Message.DeleteAsync();
            switch (task.ToLower())
            {
                case "metal":
                    temp = "Task:** Metal" + " **"
                    + "```" + "\n" + "Desc: Farm 100K metal ore." + "\n" +
                    "Hours: 24" +
                    "\n" + "Task created" + "```";
                    title = "Metal";
                    desc = "Desc: Farm 100K metal ore.";
                    break;
                case "wood":
                    temp = "Task:** Wood" + " **"
                    + "```" + "\n" + "Desc: Farm 100K wood." + "\n" +
                    "Hours: 24" +
                    "\n" + "Task created" + "```";
                    title = "Wood";
                    desc = "Desc: Farm 100K wood.";
                    break;
                case "flint":
                    temp = "Task:** Flint" + " **"
                    + "```" + "\n" + "Desc: Farm 100K flint." + "\n" +
                    "Hours: 24" +
                    "\n" + "Task created" + "```";
                    title = "Flint";
                    desc = "Desc: Farm 100K flint.";
                    break;
                case "stone":
                    temp = "Task:** Stone" + " **"
                    + "```" + "\n" + "Desc: Farm 100K stone." + "\n" +
                    "Hours: 24" +
                    "\n" + "Task created" + "```";
                    title = "Stone";
                    desc = "Desc: Farm 100K stone.";
                    break;
                case "gunpowder":
                    temp = "Task:** Gunpowder" + " **"
                    + "```" + "\n" + "Desc: To craft 100k gunpowder you need" +
                    "\n" + "16,6k Spark powder" +
                    "\n" + "16,6k Charcoal" +
                    "\n" + "Hours: 24" +
                    "\n" + "Task created" + "```";
                    title = "Gunpowder";
                    desc = "Desc: To craft 100k gunpowder you need";
                    break;
                case "arb":
                    temp = "Task:** Arb" + " **"
                    + "```" + "\n" + "Desc: To craft 10k Arb you need" +
                    "\n" + "5k Ingots" +
                    "\n" + "45k Gunpowder" +
                    "\n" + "Hours: 24" +
                    "\n" + "Task created" + "```";
                    title = "Arb";
                    desc = "Desc: To craft 10k Arb you need";
                    break;
                case "meat":
                    temp = "Task:** Meat" + " **"
                    + "```" + "\n" + "Desc: Farm 30K meat." + "\n" +
                    "Hours: 1" +
                    "\n" + "Task created" + "```";
                    title = "Stone";
                    time = 1;
                    desc = "Desc: Farm 30K meat.";
                    break;
                case "berrys":
                    temp = "Task:** Berrys" + " **"
                    + "```" + "\n" + "Desc: Farm 30K berrys." + "\n" +
                    "Hours: 1" +
                    "\n" + "Task created" + "```";
                    title = "Stone";
                    time = 1;
                    desc = "Desc: Farm 30K berrys.";
                    break;
                case "pearls":
                    temp = "Task:** Pearls" + " **"
                    + "```" + "\n" + "Desc: Farm 100K pearls." + "\n" +
                    "Hours: 24" +
                    "\n" + "Task created" + "```";
                    title = "Stone";
                    desc = "Desc: Farm 100K pearls.";
                    break;
                default:
                    await ctx.Channel.SendMessageAsync("**There is 9 diffrent routine task**. ```\nMetal \nMeat \nBerrys \nWood \nFlint \nStone \nPearls \nGunpowder \nArb```");
                    break;
            }
            await ctx.Channel.Guild.GetChannel(758954904462688307).SendMessageAsync(temp);

            if (title != String.Empty)
            {
                TaskManager.Instance.Tasks.Add(new WorkTasks(title, desc, time, ctx.Guild.GetChannel(758683186183405621)));

            }
        }
    }
}
