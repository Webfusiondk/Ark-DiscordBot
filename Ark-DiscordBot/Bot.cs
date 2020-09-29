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
using System.Linq;

namespace Ark_DiscordBot
{
    class Bot
    {
        Json j = new Json();
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
            AddMembersToList(j.ReadMembers());
            GetAllMembers();
            AddNewRole();
            NewMemberJoin();
            RoleUpdate();

            await Task.Delay(-1);
        }
        private void RoleUpdate()
        {
            Client.GuildMemberUpdated += Client_GuildMemberUpdated;
        }

        private Task Client_GuildMemberUpdated(GuildMemberUpdateEventArgs e)
        {
            e.Guild.CurrentMember.Roles;
            if (CheckForNewUser(e.Guild.CurrentMember.Id) == true)
            {
                for (int i = 0; i < listOfMembers.Count; i++)
                {
                    if (listOfMembers[i].MemberID == e.Guild.CurrentMember.Id)
                    {
                        listOfMembers.RemoveAt(i);
                        listOfMembers.Add(new Member(e.Guild.CurrentMember.DisplayName,e.Guild.CurrentMember.Id,e.Guild.CurrentMember.Roles));
                    }
                }
            }
           j.UpdateMembers(listOfMembers);
            return Task.CompletedTask;
        }

        private void AddNewRole()
        {
            Client.GuildAvailable += NewRole;
        }

        private Task NewRole(GuildCreateEventArgs e)
        {
            bool isCreated = false;
            var temp = e.Guild.Roles;
            foreach (var item in temp)
            {
                if (item.Name == "Member")
                {
                    isCreated = true;
                }
                Console.WriteLine(item.Id);
            }
            if (isCreated == false)
            {
                e.Guild.CreateRoleAsync("Member", Permissions.None, DiscordColor.SapGreen, true, true, "New Member");
            }
            return Task.CompletedTask;
        }
        private void AddMembersToList(List<Member> list)
        {
            if (list.Count == 0)
            {
                Console.WriteLine("The Json file is empty");
            }
            else
            {
                foreach (Member member in list)
                {
                    listOfMembers.Add(member);
                }
            }
        }
        private void NewMemberJoin()
        {
            Client.GuildMemberAdded += Client_GuildMemberAdded;
        }
        private async Task Client_GuildMemberAdded(GuildMemberAddEventArgs e)
        {

            DiscordRole role = e.Guild.Roles.FirstOrDefault(role => role.Name == "Member");
            await e.Member.GrantRoleAsync(role,"New member");
            j.SaveOneMember(new Member(e.Member.Username,e.Member.Id,e.Member.Roles));
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
                j.SaveMembers(listOfMembers);
            }
            else
            {
                foreach (DiscordMember dMember in e.Guild.Members)
                {
                    if (CheckForNewUser(dMember.Id) == false)
                    {
                        listOfMembers.Add(new Member(dMember.DisplayName, dMember.Id, dMember.Roles));
                    }
                }
            }
            return Task.CompletedTask;
        }
        
        private bool CheckForNewUser(ulong id)
        {
            for (int i = 0; i < listOfMembers.Count; i++)
            {
                if (listOfMembers[i].MemberID == id)
                {
                    return !!true;
                }
            }
            return !!false;
        }
        private Task OnStart(ReadyEventArgs e)
        {
            return Task.CompletedTask;
        }
    }
}
