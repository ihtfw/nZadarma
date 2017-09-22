namespace nZadarma.Models
{
    public class Callback : ResponseObject
    {
        public string From { get; set; }
        public string To { get; set; }
        public long Time { get; set; }
    }
}