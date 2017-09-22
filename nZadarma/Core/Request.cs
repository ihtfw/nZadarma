using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace nZadarma.Core
{
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
        public int? Timeout { get; set; }

        private void Create()
        {
            request = (HttpWebRequest) WebRequest.Create(RequestInfo.BaseUrl + RequestInfo.MethodUrl + "?" + queryInfo.Build());

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

            Sign(request);
        }

        public async Task<HttpWebResponse> GetResponse()
        {
            Create();
            return (HttpWebResponse)(await Task.Factory.FromAsync(request.BeginGetResponse, asyncResult => request.EndGetResponse(asyncResult), request).ConfigureAwait(false));
        }
        
        private void Sign(HttpWebRequest request)
        {
            if (AuthorizeInfo == null)
                return;
            
            string queryStr = queryInfo.Build();
            var md5 = Hashes.MD5(queryStr);
            var sha1Hash = Hashes.HMACSHA1(RequestInfo.MethodUrl + queryStr + md5, AuthorizeInfo.SecretKey);
            
            string fullQueryStr = Convert.ToBase64String(Encoding.UTF8.GetBytes(sha1Hash));
            
            request.Headers.Add($"Authorization: {AuthorizeInfo.ApiKey}:{fullQueryStr}");
        }
    }
}