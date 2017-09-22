using System;

namespace nZadarma.Models
{
    public class ResponseObject
    {
        public string Status { get; set; }
        public string Message { get; set; }

        public void Validate()
        {
            if (Status == "success")
                return;
            
            throw new Exception(Message);
        }
    }
}