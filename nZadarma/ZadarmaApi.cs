using System;
using System.Net;
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

        /// <summary>
        /// баланс пользователя
        ///  url '/v1/info/balance/'
        /// </summary>
        /// <returns></returns>
        public Task<UserBalance> Balance()
        {
            return ProcessGetRequest<UserBalance>(@"/v1/info/balance/");
        }
        
        /// <summary>
        /// список SIP-номеров пользователя.
        /// url '/v1/sip/ '
        /// </summary>
        /// <returns></returns>
        public Task<UserSips> Sips()
        {
            return ProcessGetRequest<UserSips>(@"/v1/sip/");
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
            
            return ProcessGetRequest<Callback>(@"/v1/request/callback/", queryInfo);
        }
        
        
        /// <summary>
        /// запрос на файл записи разговора
        /// url '/v1/pbx/record/request/'
        /// достаточно указать один из двух параметров идентификации (pbx_call_id или call_id), при указании pbx_call_id ссылок в ответе на запрос может быть несколько
        /// </summary>
        /// <returns></returns>
        public Task<RecordRequest> RecordRequest(string callId, string pbxCallId, int lifetimeInSeconds = 1800)
        {
            var queryInfo = new QueryInfo()
                .Add("call_id", callId)
                .Add("pbx_call_id", pbxCallId)
                .Add("lifetime", 1800);
            
            return ProcessGetRequest<RecordRequest>(@"/v1/pbx/record/request/", queryInfo);
        }
        
        
        /// <summary>
        /// статистика по АТС
        /// url '/v1/statistics/pbx/'
        /// </summary>
        /// <returns></returns>
        public Task<StatisticsPbx> StatisticsPbx(DateTime? start, DateTime? end)
        {
//            var dateFormat = "yyyy-MM-dd HH:mm:ss"; //should be like this, but it doesn't work
            var dateFormat = "yyyy-MM-dd";
            var queryInfo = new QueryInfo()
                .Add("start", start?.ToString(dateFormat))
                .Add("end", end?.ToString(dateFormat));
            
            return ProcessGetRequest<StatisticsPbx>(@"/v1/statistics/pbx/", queryInfo);
        }

        #region Helpers
        
        private Request CreateGetRequest(string methodUrl, QueryInfo queryInfo = null)
        {
            var requestInfo = new RequestInfo("GET", baseUrl, methodUrl);
            var authorizeInfo = new AuthorizeInfo(apiKey, secretKey);

            queryInfo = queryInfo ?? new QueryInfo();
            queryInfo.Add("format", "json");
            var request = new Request(requestInfo, queryInfo, authorizeInfo);

            return request;
        }

        private Task<T> ProcessGetRequest<T>(string methodUrl, QueryInfo queryInfo = null)
        {
            var request = CreateGetRequest(methodUrl, queryInfo);
            return ProcessRequest<T>(request);
        }

        private async Task<T> ProcessRequest<T>(Request request)
        {
            var response = await request.GetResponse().ConfigureAwait(false);
            var content = await response.Content();
            
            var responseObject = JsonConvert.DeserializeObject<ResponseObject>(content);
            responseObject.Validate();
            
            return JsonConvert.DeserializeObject<T>(content);;
        }

        #endregion
    }
}