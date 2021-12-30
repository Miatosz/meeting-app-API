using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using Microsoft.AspNetCore.Authorization;

namespace MeetingAppAPI
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CustomAuthorization : AuthorizeAttribute
{
    protected void HandleUnauthorizedRequest(HttpActionContext actionContext)
    {
        actionContext.Response = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.Forbidden,
            Content = new StringContent("You are unauthorized to access this resource")
        };
    }
}
}