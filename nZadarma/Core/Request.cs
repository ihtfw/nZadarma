using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace nZadarma.Core
{
//    public class Request
//    {
//        private readonly QueryInfo queryInfo;
//        private HttpWebRequest request;
//        
//        public RequestInfo RequestInfo { get; }
//        public AuthorizeInfo AuthorizeInfo { get; }
//
//        public Request(RequestInfo requestInfo, QueryInfo queryInfo, AuthorizeInfo authorizeInfo = null)
//        {
//            this.queryInfo = queryInfo;
//            RequestInfo = requestInfo;
//            AuthorizeInfo = authorizeInfo;
//        }
//
//        /// <summary>
//        /// In milliseconds
//        /// </summary>
//        public int? Timeout { get; set; } = 5000;
//
//        public async Task<string> GetResponse()
//        {
//            using (var client = new HttpClient())
//            {
//                if (Timeout.HasValue)
//                {
//                    client.Timeout = TimeSpan.FromMilliseconds(Timeout.Value);
//                }
//                
//                client.DefaultRequestHeaders
//                    .Accept
//                    .Add(new MediaTypeWithQualityHeaderValue("application/json"));
//                
//                client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", Sign());
//                var request = new HttpRequestMessage {
//                    RequestUri = new Uri(RequestInfo.BaseUrl + RequestInfo.MethodUrl + "?" + queryInfo.Build()),
//                    Method = HttpMethod.Get
//                };
//                
//                using (var response = await client.SendAsync(request).ConfigureAwait(false))
//                {
//                    response.EnsureSuccessStatusCode();
//                    
//                    using (HttpContent content = response.Content)
//                    {
//                        // ... Read the string.
//                        string result = await content.ReadAsStringAsync().ConfigureAwait(false);
//
//                        return result;
//                    }
//                }
//            }
//        }
//        
//        private string Sign()
//        {
//            string queryStr = queryInfo.Build(true);
//            var md5 = Hashes.MD5(queryStr);
//            var sha1Hash = Hashes.HMACSHA1(RequestInfo.MethodUrl + queryStr + md5, AuthorizeInfo.SecretKey);
//            
//            string fullQueryStr = Convert.ToBase64String(Encoding.UTF8.GetBytes(sha1Hash));
//
//            return $"{AuthorizeInfo.ApiKey}:{fullQueryStr}";
//        }
//    }
    public class Request
    {
        private readonly QueryInfo queryInfo;
        private HttpWebRequest request;
        
        public RequestInfo RequestInfo { get; }
        public AuthorizeInfo AuthorizeInfo { get; }

        public Request(RequestInfo requestInfo, QueryInfo queryInfo, AuthorizeInfo authorizeInfo = null)
        {
            this.queryInfo = queryInfo;
            RequestInfo = requestInfo;
            AuthorizeInfo = authorizeInfo;
        }

        /// <summary>
        /// In milliseconds
        /// </summary>
        public int? Timeout { get; set; } = 5000;

        private void Create()
        {
            var url = RequestInfo.BaseUrl + RequestInfo.MethodUrl + "?" + queryInfo.Build();
            request = (HttpWebRequest) WebRequest.Create(url);
            
            if (Timeout.HasValue)
            {
                request.Timeout = Timeout.Value;
                request.ContinueTimeout = Timeout.Value;
                request.ReadWriteTimeout = Timeout.Value;
            }

            request.Method = RequestInfo.RequestMethod;
            
            switch (RequestInfo.RequestMethod)
            {
                case "POST":
                case "GET":
                    request.Accept = "application/json";
                    break;
                case "PUT":
                    break;
                default:
                    throw new NotSupportedException(RequestInfo.RequestMethod);
            }

            Sign();
        }
        
        public async Task<HttpWebResponse> GetResponse()
        {
            Create();
            return (HttpWebResponse)(await Task.Factory.FromAsync(request.BeginGetResponse, asyncResult => request.EndGetResponse(asyncResult), request).ConfigureAwait(false));
        }
        
        private void Sign()
        {
            if (AuthorizeInfo == null)
                return;
            
            string queryStr = queryInfo.Build(true);
            var md5 = Hashes.MD5(queryStr);
            var sha1Hash = Hashes.HMACSHA1(RequestInfo.MethodUrl + queryStr + md5, AuthorizeInfo.SecretKey);
            
            string fullQueryStr = Convert.ToBase64String(Encoding.UTF8.GetBytes(sha1Hash));
            
//            request.Headers.Add($"Authorization: {AuthorizeInfo.ApiKey}:{fullQueryStr}");
            request.Headers.Add($"Authorization", $"{AuthorizeInfo.ApiKey}:{fullQueryStr}");
        }
    }
}