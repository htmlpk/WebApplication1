using BlackJack.BusinessLogicLayer.Exceptions;
using BlackJack.UI.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Web.Http;

namespace BlackJack.UI.Controllers
{
    [AllowAnonymous]
    public class BaseController : Controller
    {
        protected string AccessToken { set; get; }
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.Exception != null)
            {
                filterContext.ExceptionHandled = true;
                
                if (filterContext.Exception.GetType() == typeof(LoginException))
                {
                    filterContext.Result = BadRequest(BadRequestTypes.LoginError);
                }
                filterContext.Result = BadRequest(BadRequestTypes.Error); 
            }
        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
        }
    }
}

