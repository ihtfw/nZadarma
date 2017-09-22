using System.Threading.Tasks;
using nZadarma.Core;
using nZadarma.Models;
using Newtonsoft.Json;

namespace nZadarma
{
    public class ZadarmaApi
    {
        private string baseUrl = "https://api.zadarma.com";

        private string apiKey;
        private string secretKey;

        public ZadarmaApi(string apiKey, string secretKey)
        {
            this.apiKey = apiKey;
            this.secretKey = secretKey;
        }

        private Request CreateGetRequest(string methodUrl, QueryInfo queryInfo = null)
        {
            var requestInfo = new RequestInfo("GET", baseUrl, methodUrl);
            var authorizeInfo = new AuthorizeInfo(apiKey, secretKey);
            
            var request = new Request(requestInfo, queryInfo ?? new QueryInfo(), authorizeInfo);

            return request;
        }

        private async Task<T> ProcessRequest<T>(Request request) where T : ResponseObject
        {
            var response = await request.GetResponse().ConfigureAwait(false);
            var content = response.Content();
            
            var responseObject = JsonConvert.DeserializeObject<T>(content);
            responseObject.Validate();
            
            return responseObject;
        }
        
        /// <summary>
        /// баланс пользователя
        ///  url '/v1/info/balance/'
        /// </summary>
        /// <returns></returns>
        public Task<UserBalance> Balance()
        {
            var request = CreateGetRequest(@"/v1/info/balance/");

            return ProcessRequest<UserBalance>(request);
        }
        
        /// <summary>
        /// список SIP-номеров пользователя.
        /// url '/v1/sip/ '
        /// </summary>
        /// <returns></returns>
        public Task<UserSips> Sips()
        {
            var request = CreateGetRequest(@"/v1/sip/");

            return ProcessRequest<UserSips>(request);
        }
        
        /// <summary>
        /// запрос на callback.
        /// url '/v1/request/callback/'
        /// </summary>
        /// <returns></returns>
        public Task<Callback> Callback(string from, string to)
        {
            var queryInfo = new QueryInfo()
                .Add("from", from)
                .Add("to", to)
                .Add("predicted", true);
            
            var request = CreateGetRequest(@"/v1/request/callback/", queryInfo);

            return ProcessRequest<Callback>(request);
        }
        
    }
}