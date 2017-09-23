using Newtonsoft.Json.Serialization;

namespace nZadarma.Models
{
    public class UserBalance 
    {
        public double Balance { get; set; }
        public string Currency { get; set; }
    }
}