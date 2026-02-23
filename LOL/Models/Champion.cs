using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LOL.Models
{
    internal class Champion
    {

        [JsonPropertyName("name")]
        public string Name { get;  set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("blurb")]
        public string Blurb { get; set; }

        [JsonPropertyName("info")]
        public Info Info { get; set; }

        [JsonPropertyName("tags")]
        public List<string> Tags { get; set; }
    }
}
