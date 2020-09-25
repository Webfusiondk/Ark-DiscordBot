using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using DSharpPlus.CommandsNext;
using DSharpPlus;

namespace Ark_DiscordBot
{
    public sealed class TaskManager
    {
        private ObservableCollection<WorkTasks> tasks = new ObservableCollection<WorkTasks>();
        public ObservableCollection<WorkTasks> Tasks
        {
            get { return tasks; }
            set { tasks = value; }
        }
        private static readonly TaskManager instance = new TaskManager();
        public static TaskManager Instance { get { return instance; } }
        static TaskManager()
        {

        }
        private TaskManager()
        {
            Tasks.CollectionChanged += Tasks_CollectionChanged;
        }
        private void Tasks_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                ((WorkTasks)sender).Timer.Elapsed += Timer_Elapsed;
            }
        }
        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            ((WorkTasks)sender).Channel.SendMessageAsync("The task is done");
        }
    }
}
