using Newtonsoft.Json;
using SuperShoes.Api.Dtos.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http.Filters;

namespace SuperShoes.Api.Utils.Filters
{
    /// <summary>
    /// All the requests will follow a simple basic format for the errors and another one for the success.
    /// </summary>
    public class GeneralResponseFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Response.StatusCode != HttpStatusCode.OK)
            {
                ErrorResponse error = new ErrorResponse(actionExecutedContext.Response.StatusCode, actionExecutedContext.Response.ReasonPhrase, actionExecutedContext.Request);
                actionExecutedContext.Response = error.actionExecuted();
            }
            else
            {
                base.OnActionExecuted(actionExecutedContext);
            }
        }
    }
}