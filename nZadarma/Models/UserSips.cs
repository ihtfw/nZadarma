using System.Collections.Generic;

namespace nZadarma.Models
{
    public class UserSips 
    {
        public int Left { get; set; }

        public List<Sip> Sips { get; set; }
    }
}