//using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSGSIWebClient.Models
{
    public class SteamPlayer
    {
        //[JsonProperty(PropertyName = "personaname")]
        //public string PersonaName { get; set; }

        //[JsonProperty(PropertyName = "avatar")]
        //public string Avatar { get; set; }

        //[JsonProperty(PropertyName = "avatarmedium")]
        //public string AvatarMedium { get; set; }

        //[JsonProperty(PropertyName = "avatarfull")]
        //public string AvatarFull { get; set; }

        public string steamid { get; set; }
        public int communityvisibilitystate { get; set; }
        public int profilestate { get; set; }
        public string personaname { get; set; }
        public int lastlogoff { get; set; }
        public string profileurl { get; set; }
        public string avatar { get; set; }
        public string avatarmedium { get; set; }
        public string avatarfull { get; set; }
        public int personastate { get; set; }
        public string primaryclanid { get; set; }
        public int timecreated { get; set; }
        public int personastateflags { get; set; }
        public string loccountrycode { get; set; }
        public string locstatecode { get; set; }
    }
}
