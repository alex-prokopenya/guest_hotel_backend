namespace GuestService.Controllers.Html
{
    using Sm.System.Mvc.Language;
    using System;
    using System.Web.Mvc;

    public class ErrorController : BaseController
    {
        public ActionResult Index(int? code)
        {
            ((dynamic)base.ViewBag).PageUrl = base.Request["aspxerrorpath"];
            int valueOrDefault = code.GetValueOrDefault();
            if (code.HasValue && (valueOrDefault == 0x194))
            {
                return base.View("error-404");
            }
            return base.View("error");
        }
    }
}

