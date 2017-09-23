using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace nZadarma.Models
{
    public class StatisticsPbx 
    {
        public DateTime Start { get; set; }   
        public DateTime End { get; set; }

        public List<StatisticPbx> Stats { get; set; }
    }

    public class StatisticPbx
    {
        [JsonProperty("call_id")]
        public string CallId { get; set; }

        public string Sip { get; set; }

        public DateTime Callstart { get; set; }

        public string Clid { get; set; }

        public string Destination { get; set; }

        public string Disposition { get; set; }

        public int Seconds { get; set; }

        [JsonProperty("is_recorded")]
        public bool IsRecorded { get; set; }
        
        [JsonProperty("pbx_call_id")]
        public string PbxCallId { get; set; }
    }
}