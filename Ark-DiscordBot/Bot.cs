using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.EventArgs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using DSharpPlus.Interactivity;
using DSharpPlus.Entities;

namespace Ark_DiscordBot
{
    class Bot
    {
        List<Member> listOfMembers = new List<Member>();
        public InteractivityModule Interactivity { get; private set; }
        public DiscordClient Client { get; private set; }
        public CommandsNextModule Commands { get; private set; }

        public async Task RunAsync()
        {
            string json = "";

            using (FileStream fs = File.OpenRead("config.json"))
            using (StreamReader sr = new StreamReader(fs, new UTF8Encoding(false)))
                json = await sr.ReadToEndAsync().ConfigureAwait(false);

            ConfigJson configJson = JsonConvert.DeserializeObject<ConfigJson>(json);


            DiscordConfiguration config = new DiscordConfiguration
            {
                Token = configJson.Token,
                TokenType = TokenType.Bot,
                AutoReconnect = true,
                LogLevel = LogLevel.Debug,
                UseInternalLogHandler = true
            };

            Client = new DiscordClient(config);

            Client.Ready += OnStart;
            CommandsNextConfiguration commandsConfig = new CommandsNextConfiguration
            {
                StringPrefix = configJson.Prefix,
                EnableDms = false,
                EnableMentionPrefix = true,
                CaseSensitive = false
            };

            Client.UseInteractivity(new InteractivityConfiguration
            {

            });

            Commands = Client.UseCommandsNext(commandsConfig);

            Commands.RegisterCommands<DinoStatsCommand>();
            Commands.RegisterCommands<TaskCommand>();
            Commands.RegisterCommands<MemberCommand>();
            await Client.ConnectAsync();
            GetAllMembers();
                        
            await Task.Delay(-1);
        }
        private async Task NewMemberJoin()
        {
            Client.GuildMemberAdded += Client_GuildMemberAdded;
        }

        private Task Client_GuildMemberAdded(GuildMemberAddEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void GetAllMembers()
        {
            Client.GuildAvailable += Client_GuildAvailable;
        }

        private Task Client_GuildAvailable(GuildCreateEventArgs e)
        {           
            if (listOfMembers.Count == 0)
            {
                foreach (DiscordMember dMember in e.Guild.Members)
                {
                    listOfMembers.Add(new Member(dMember.DisplayName, dMember.Id, dMember.Roles));
                }
            }
            else
            {
                foreach (DiscordMember dMember in e.Guild.Members)
                {
                    foreach (Member localMember in listOfMembers)
                    {
                        if (dMember.Id != localMember.MemberID)
                        {
                            listOfMembers.Add(new Member(dMember.DisplayName, dMember.Id, dMember.Roles));
                            foreach (var item in listOfMembers)
                            {
                                Console.WriteLine(item.Name + item.MemberID + item.Roles);
                            }
                        }
                    }
                }
            }
            return Task.CompletedTask;
        }

        private Task OnStart(ReadyEventArgs e)
        {
            foreach (DiscordGuild guild in e.Client.Guilds.Values)
            {
                guild.Roles
            }
            return Task.CompletedTask;
        }
    }
}
