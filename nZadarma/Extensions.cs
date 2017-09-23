using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace nZadarma
{
    
    public static class Extensions
    {
        public static async Task<string> Content(this HttpWebResponse response)
        {
            using (var responseStream = response.GetResponseStream())
            {
                using (var sr = new StreamReader(responseStream))
                {
                    string strContent = await sr.ReadToEndAsync().ConfigureAwait(false);
                    return strContent;
                }
            }
        }
    }
}