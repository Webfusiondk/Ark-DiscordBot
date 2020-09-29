using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Ark_DiscordBot
{
    class Json
    {
        private string path = "Members.json";
        public void UpdateMembers(List<Member> members)
        {
            string json = JsonConvert.SerializeObject(members.ToArray());
            File.WriteAllText(path, json);
        }
        public void SaveMembers(List<Member> members)
        {
            string json = JsonConvert.SerializeObject(members.ToArray());
            File.WriteAllText(path,json);
            
        }
        public void SaveOneMember(Member member)
        {
            List<Member> temp = new List<Member>();
            temp = ReadMembers();
            temp.Add(member);
            string json = JsonConvert.SerializeObject(temp.ToArray());
            File.WriteAllText(path, json);

        }
        public List<Member> ReadMembers()
        {
            string jString = File.ReadAllText(path);
            if (jString == string.Empty)
            {
                return new List<Member>();
            }
            List<Member> members = JsonConvert.DeserializeObject<List<Member>>(jString);
            return members;
        }
    }
}
