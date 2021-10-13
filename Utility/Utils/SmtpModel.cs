using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace JKM.UTILITY.Utils
{
    public class SmtpModel
    {
        [JsonProperty("host")]
        public string Host { get; set; }
        [JsonProperty("port")]
        public int Port { get; set; }
        [JsonProperty("username")]
        public string Username { get; set; }
        [JsonProperty("password")]
        public string Password { get; set; }
        [JsonProperty("from")]
        public string From { get; set; }
        [JsonProperty("displayName")]
        public string DisplayName { get; set; }
    }
}
