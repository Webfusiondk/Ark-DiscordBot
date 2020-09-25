using DSharpPlus.CommandsNext;
using System;
using System.Collections.Generic;
using System.Text;
using DSharpPlus;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;

namespace Ark_DiscordBot
{
    class MemberCommand
    {
        [Command("Mystats")]
        [Description("Print your stats")]
        public async Task Mystats(CommandContext ctx)
        {
            var t = ctx.Guild.Members;
            foreach (var item in t)
            {
                Console.WriteLine(item.Nickname);
                Console.WriteLine(item.Id);
                Console.WriteLine(item.DisplayName);
                Console.WriteLine(item.Email);
                Console.WriteLine(item.Username);
            }
            Console.WriteLine();
            await ctx.Channel.SendMessageAsync("getting member data");
           
           //string name = member.DisplayName;
           //ulong Id = member.Id;
           //string nickname = member.Nickname;
           // Console.WriteLine(name);
           // Console.WriteLine(Id);
           // Console.WriteLine(nickname);
        }
    }
}
