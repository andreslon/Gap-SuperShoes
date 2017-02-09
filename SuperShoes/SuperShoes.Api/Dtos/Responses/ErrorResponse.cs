using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Net.Http;
using System.Web.Http;
using System.Threading;
using System.Threading.Tasks;

namespace SuperShoes.Api.Dtos.Responses
{
    public class ErrorResponse : IHttpActionResult
    {
        [JsonProperty("success")]
        public bool success { get; set; }
        [JsonProperty("error_code")]
        public int Error_Code { get; set; }
        [JsonProperty("error_msg")]
        public string Error_Msg { get; set; }

        private HttpStatusCode statusCode; 
        private HttpRequestMessage request;

        public ErrorResponse(HttpStatusCode statusCode, string reasonPhrase, HttpRequestMessage request)
        {
            this.statusCode = statusCode;
            this.request = request;
            this.Error_Code = (int)statusCode;
            this.Error_Msg = reasonPhrase;
        }

        internal HttpResponseMessage actionExecuted()
        {
            var response = request.CreateResponse(statusCode);
            response.Content = new StringContent(JsonConvert.SerializeObject(this), System.Text.Encoding.UTF8, "application/json");
            return response;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(actionExecuted()); 
        }
    }
}