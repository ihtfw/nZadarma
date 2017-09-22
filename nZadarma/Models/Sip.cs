using Newtonsoft.Json;

namespace nZadarma.Models
{
    public class Sip
    {
        public string Id { get; set; }
        
        [JsonProperty("display_name")]
        public string DisplayName { get; set; }
        
        public int Lines { get; set; }
    }
}