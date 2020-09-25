using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace Ark_DiscordBot
{
    public class WorkTasks
    {
        public DiscordChannel Channel { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
        public int Duration { get; set; }
        public Timer Timer { get; set; }

        public WorkTasks(string title, string desc, int duration, DiscordChannel channel)
        {
            Title = title;
            Desc = desc;
            Duration = duration;
            Channel = channel;
            Timer = new Timer( SetTimer(duration));
            Timer.Start();
        }

        double SetTimer(int duration)
        {
            double millisecounds = 0;

            for (int i = 0; i < duration; i++)
            {
                millisecounds += 3600000;
            }
            return millisecounds;
        }
    }
}
