using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuperShoes.Api.Dtos.Responses
{
    public class BaseResponse<T>
    {
        public dynamic Get(T obj, int totalElements, string field)
        {
            dynamic response = new JObject();
            var json = JsonConvert.SerializeObject(obj);
            response[field] = json;
            response["success"] = true;
            if (totalElements>0)
            {
                response["total_elements"] = totalElements;
            }
            return response;
        }
    }
}