namespace GuestService.Controllers.Html
{
    using GuestService.Data;
    using GuestService.Data.Survey;
    using System;
    using System.Web.Mvc;

    public class SurveyController : Controller
    {
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Index(SurveyResultsModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model");
            }
            if (SurveyProvider.GetInvitationInfo(model.accesscode) == null)
            {
                ((dynamic)base.ViewBag).NotFound = true;
            }
            else
            {
                SurveyProvider.SetSurveyResult(model);
                ((dynamic)base.ViewBag).Thanks = true;
            }
            return base.View();
        }

        [HttpGet]
        public ActionResult Index(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                ((dynamic)base.ViewBag).NotFound = true;
            }
            InvitationInfo invitationInfo = SurveyProvider.GetInvitationInfo(id);
            if (invitationInfo == null)
            {
                ((dynamic)base.ViewBag).NotFound = true;
            }
            else
            {
                ((dynamic)base.ViewBag).Invitation = invitationInfo;
                if (invitationInfo.CanSurvey)
                {
                    ((dynamic)base.ViewBag).Questionnaire = SurveyProvider.GetQuestionnaire(invitationInfo.ObjectType, invitationInfo.Language);
                }
            }
            return base.View();
        }
    }
}

