using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace MvcTest.AuthFilter
{
    public class Authorization : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //base.OnActionExecuting(filterContext);
            if (HttpContext.Current.Session["UserEmail"] == null)
            {
                filterContext.Result = new RedirectResult("/Auth/Login");
            }
        }
    }
}