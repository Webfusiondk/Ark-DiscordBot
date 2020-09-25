﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ark_DiscordBot
{
    public struct ConfigJson
    {
        [JsonProperty("token")]
        public string Token { get; private set; }
        [JsonProperty("prefix")]
        public string Prefix { get; private set; }
    }
}
