using Newtonsoft.Json.Serialization;

namespace nZadarma.Models
{
    public class UserBalance : ResponseObject
    {
        public double Balance { get; set; }
        public string Currency { get; set; }
    }
}