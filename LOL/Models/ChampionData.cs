using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LOL.Models
{
    public class ChampionData
    {
        [JsonPropertyName("data")]
        public Dictionary<string, Champion> Data { get; set; }
    }
}
