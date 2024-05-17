using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheKings_Szymon.Models
{
    public class Monarch
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("nm")]
        public string Name { get; set; }

        [JsonProperty("cty")]
        public string Country { get; set; }

        [JsonProperty("hse")]
        public string House { get; set; }

        [JsonProperty("yrs")]
        public string Years { get; set; }

        public int Duration { get; set; }
        public int GetReignDuration()
        {
            var years = Years.Split('-');
            if (years.Length == 2 && int.TryParse(years[0], out int startYear) && int.TryParse(years[1], out int endYear))
            {
                return endYear - startYear;
            }
            return 0;
        }
    }
}
