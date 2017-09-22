﻿using System.IO;
using System.Net;

namespace nZadarma
{
    
    public static class Extensions
    {
        public static string Content(this HttpWebResponse response)
        {
            using (var responseStream = response.GetResponseStream())
            {
                using (var sr = new StreamReader(responseStream))
                {
                    string strContent = sr.ReadToEnd();
                    return strContent;
                }
            }
        }
    }
}