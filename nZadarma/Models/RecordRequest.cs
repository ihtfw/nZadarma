using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace nZadarma.Models
{
    public class RecordRequest 
    {
        public string Link { get; set; }
        
        /// <summary>
        /// Если указан только pbx_call_id, ссылок может быть несколько
        /// </summary>
        public List<string> Links { get; set; }
        
        [JsonProperty("lifetime_till")]
        public DateTime LifetimeTill { get; set; }
    }
}