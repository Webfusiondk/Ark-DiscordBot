using System;
using System.Collections.Generic;
using System.Text;
using DSharpPlus.Entities;

namespace Ark_DiscordBot
{
    class Member
    {
        List<WorkTasks> listOfTasks;
        public string Name { get; set; }
        public ulong MemberID { get; set; }
        public IEnumerable<DiscordRole> Roles { get; set; }
        public Contributions MyContributions { get; set; }
        public Member(string name, ulong id, IEnumerable<DiscordRole> roles)
        {
            Name = name;
            MemberID = id;
            Roles = roles;
            listOfTasks = new List<WorkTasks>();
            MyContributions = new Contributions();
        }
    }
}
