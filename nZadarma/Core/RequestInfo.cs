namespace nZadarma.Core
{
    public class RequestInfo
    {
        public string RequestMethod { get; }
        public string BaseUrl { get; }
        public string MethodUrl { get; }

        public RequestInfo(string requestMethod, string baseUrl, string methodUrl)
        {
            RequestMethod = requestMethod.ToUpper();
            BaseUrl = baseUrl;
            MethodUrl = methodUrl;
        }
    }
}