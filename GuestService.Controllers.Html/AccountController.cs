namespace GuestService.Controllers.Html
{
    using DotNetOpenAuth.AspNet;
    using GuestService;
    using GuestService.Code;
    using GuestService.Data;
    using GuestService.Models;
    using GuestService.Resources;
    using GuestService.Notifications;
    using Microsoft.Web.WebPages.OAuth;
    using Sm.System.Mvc.Language;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Web.Mvc;
    using System.Web.Security;
    using System.Xml.Linq;
    using WebMatrix.WebData;
    using System.Web;
    using System.Configuration;

    using System.IO;

    [Authorize, HttpPreferences, UrlLanguage, WebSecurityInitializer]
    public class AccountController : BaseController
    {
        private static string cookieSalt = "securesalt";

        private static string GetMd5Hash(string inp)
        {
            byte[] keyInBytes = System.Text.UTF8Encoding.UTF8.GetBytes("secret_key");
            byte[] payloadInBytes = System.Text.UTF8Encoding.UTF8.GetBytes(inp);

            var md5 = new System.Security.Cryptography.HMACMD5(keyInBytes);
            byte[] hash = md5.ComputeHash(payloadInBytes);

            var result = BitConverter.ToString(hash).Replace("-", string.Empty);
            return result;
        }

        [AllowAnonymous]
        public ActionResult Confirm(string email, string token)
        {
            ((dynamic)base.ViewBag).Confirmed = WebSecurity.ConfirmAccount(email, token);
            return base.View();
        }

        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            switch (createStatus)
            {
                case MembershipCreateStatus.InvalidUserName:
                    return AccountStrings.ErrorInvalidUserName;

                case MembershipCreateStatus.InvalidPassword:
                    return AccountStrings.ErrorInvalidPassword;

                case MembershipCreateStatus.InvalidQuestion:
                    return AccountStrings.ErrorInvalidQuestion;

                case MembershipCreateStatus.InvalidAnswer:
                    return AccountStrings.ErrorInvalidAnswer;

                case MembershipCreateStatus.InvalidEmail:
                    return AccountStrings.ErrorInvalidEmail;

                case MembershipCreateStatus.DuplicateUserName:
                    return AccountStrings.ErrorDuplicateUserName;

                case MembershipCreateStatus.DuplicateEmail:
                    return AccountStrings.ErrorDuplicateEmail;

                case MembershipCreateStatus.UserRejected:
                    return AccountStrings.ErrorUserRejected;

                case MembershipCreateStatus.ProviderError:
                    return AccountStrings.ErrorProviderError;
            }
            return AccountStrings.ErrorUnknownError;
        }

        [ValidateAntiForgeryToken, AllowAnonymous, HttpPost]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            return new ExternalLoginResult(provider, base.Url.Action("ExternalLoginCallback", new { ReturnUrl = returnUrl }));
        }

        [AllowAnonymous]
        public ActionResult ExternalLoginCallback(string returnUrl)
        {
            AuthenticationResult result = OAuthWebSecurity.VerifyAuthentication(base.Url.Action("ExternalLoginCallback", new { ReturnUrl = returnUrl }));
            if (!result.IsSuccessful)
            {
                return base.RedirectToAction("ExternalLoginFailure");
            }
            bool flag = false;
            string userName = OAuthWebSecurity.GetUserName(result.Provider, result.ProviderUserId);
            if (userName != null)
            {
                flag = WebSecurity.IsConfirmed(userName);
                if (flag)
                {
                    bool createPersistentCookie = false;
                    if (OAuthWebSecurity.Login(result.Provider, result.ProviderUserId, createPersistentCookie))
                    {
                        return this.RedirectToLocal(returnUrl);
                    }
                    if (base.User.Identity.IsAuthenticated)
                    {
                        OAuthWebSecurity.CreateOrUpdateAccount(result.Provider, result.ProviderUserId, base.User.Identity.Name);
                        return this.RedirectToLocal(returnUrl);
                    }
                }
                else
                {
                    string userConfirmationToken = MembershipHelper.GetUserConfirmationToken(WebSecurity.GetUserId(userName));
                    if (userConfirmationToken != null)
                    {
                        this.SendRegistrationConfirmMail(ConfirmMailOperation.confirm, userName, userConfirmationToken, "client");
                    }
                }
            }
            ((dynamic)base.ViewBag).NotConfirmedEmail = (userName != null) && !flag;
            string str3 = OAuthWebSecurity.SerializeProviderUserId(result.Provider, result.ProviderUserId);
            AuthenticationClientData oAuthClientData = OAuthWebSecurity.GetOAuthClientData(result.Provider);
            ((dynamic)base.ViewBag).ProviderDisplayName = oAuthClientData.DisplayName;
            ((dynamic)base.ViewBag).ReturnUrl = returnUrl;
            RegisterExternalLoginModel model = new RegisterExternalLoginModel
            {
                UserName = (result.UserName.Contains("@") && result.UserName.Contains(".")) ? result.UserName : "",
                ExternalLoginData = str3
            };
            return base.View("ExternalLoginConfirmation", model);
        }

        [AllowAnonymous, ValidateAntiForgeryToken, HttpPost]
        public ActionResult ExternalLoginConfirmation(RegisterExternalLoginModel model, string returnUrl)
        {
            string providerName = null;
            string providerUserId = null;
            if (!(!base.User.Identity.IsAuthenticated && OAuthWebSecurity.TryDeserializeProviderUserId(model.ExternalLoginData, out providerName, out providerUserId)))
            {
                return this.RedirectToLocal(returnUrl);
            }
            if (base.ModelState.IsValid)
            {
                try
                {
                    string password = Guid.NewGuid().ToString();
                    bool requireConfirmationToken = true;
                    string confirmationToken = WebSecurity.CreateUserAndAccount(model.UserName, password, null, requireConfirmationToken);
                    OAuthWebSecurity.CreateOrUpdateAccount(providerName, providerUserId, model.UserName);
                    this.SendRegistrationConfirmMail(ConfirmMailOperation.confirm, model.UserName, confirmationToken,"client");
                    return base.RedirectToAction("registersuccess", new { returnUrl = returnUrl });
                }
                catch (MembershipCreateUserException exception)
                {
                    base.ModelState.AddModelError("", ErrorCodeToString(exception.StatusCode));
                }
            }
            ((dynamic)base.ViewBag).NotConfirmedEmail = false;
            ((dynamic)base.ViewBag).ProviderDisplayName = OAuthWebSecurity.GetOAuthClientData(providerName).DisplayName;
            ((dynamic)base.ViewBag).ReturnUrl = returnUrl;
            return base.View(model);
        }

        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return base.View();
        }

        [AllowAnonymous, ChildActionOnly]
        public ActionResult ExternalLoginsList(string returnUrl)
        {
            ((dynamic)base.ViewBag).ReturnUrl = returnUrl;
            return this.PartialView("_ExternalLoginsListPartial", OAuthWebSecurity.RegisteredClientData);
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            LoginModel model = new LoginModel();
            ((dynamic)base.ViewBag).ReturnUrl = returnUrl;
            return base.View(model);
        }

        [HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (base.ModelState.IsValid)
            {
                bool rememberMe = model.RememberMe;

                if (WebSecurity.Login(model.UserName, model.Password, rememberMe))
                {
                    int userid = WebSecurity.GetUserId(model.UserName);

                    SetCookie("userid", userid.ToString(), 12);
                    SetCookie("username", model.UserName, 12);
                    SetCookie("authhash", GetMd5Hash(model.UserName + userid + cookieSalt), 12);

                    return this.RedirectToLocal(returnUrl);
                }

                int userId = WebSecurity.GetUserId(model.UserName);
                if ((userId > 0) && !WebSecurity.IsConfirmed(model.UserName))
                {
                    base.ModelState.AddModelError("", AccountStrings.AccountLogin_EmailNotConfirmed);
                    string userConfirmationToken = MembershipHelper.GetUserConfirmationToken(userId);
                    if (userConfirmationToken != null)
                    {
                        base.ModelState.AddModelError("", AccountStrings.RegisterEmailNotConfirmedNote);
                        this.SendRegistrationConfirmMail(ConfirmMailOperation.confirm, model.UserName, userConfirmationToken, "client");
                    }
                }
                else
                {
                    base.ModelState.AddModelError("", AccountStrings.AccountLogin_InvalidCredentails);
                }
            }
            ((dynamic)base.ViewBag).ReturnUrl = returnUrl;
            return base.View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult LogOff()
        { 
            WebSecurity.Logout();
            SetCookie("userid", "", -12);
            SetCookie("username", "", -12);
            SetCookie("authhash", "", -12);

            return base.RedirectToAction("index", "welcome");
        }

        private void SetCookie(string name, string value, int livetime)
        {
            var cCookie = new HttpCookie(name, value);
            cCookie.Expires = DateTime.Now.AddHours(livetime);
            Response.Cookies.Add(cCookie);
        }

        [AllowAnonymous]
        public ActionResult Recovery(string returnUrl)
        {
            ((dynamic)base.ViewBag).ReturnUrl = returnUrl;
            return base.View();
        }

        [ValidateAntiForgeryToken, AllowAnonymous, HttpPost]
        public ActionResult Recovery(RecoveryModel model, string returnUrl)
        {
            if (base.ModelState.IsValid)
            {
                try
                {
                    string confirmationToken = WebSecurity.GeneratePasswordResetToken(model.UserName, 0x5a0);
                    this.SendRegistrationConfirmMail(ConfirmMailOperation.recovery, model.UserName, confirmationToken,"client");
                    return base.RedirectToAction("recoverysuccess", new { returnUrl = returnUrl });
                }
                catch (Exception)
                {
                    base.ModelState.AddModelError("", AccountStrings.AccountRecovery_CannotRecovery);
                }
            }
            return base.View();
        }

        [AllowAnonymous]
        public ActionResult RecoverySuccess(string returnUrl)
        {
            ((dynamic)base.ViewBag).ReturnUrl = returnUrl;
            return base.View();
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (base.Url.IsLocalUrl(returnUrl))
            {
                return this.Redirect(returnUrl);
            }
            return base.RedirectToAction("index", "welcome");
        }

        [AllowAnonymous]
        public ActionResult Register(string returnUrl)
        {
            RegisterModel model = new RegisterModel();
            ((dynamic)base.ViewBag).ReturnUrl = returnUrl;
            return base.View(model);
        }

        [ValidateAntiForgeryToken, HttpPost, AllowAnonymous]
        public ActionResult Register(RegisterModel model, string returnUrl)
        {
            if (base.ModelState.IsValid)
            {
                try
                {
                    bool requireConfirmationToken = true;
                    string confirmationToken = WebSecurity.CreateUserAndAccount(model.UserName, model.Password, null, requireConfirmationToken);
                    this.SendRegistrationConfirmMail(ConfirmMailOperation.confirm, model.UserName, confirmationToken, "client");
                    return base.RedirectToAction("registersuccess", new { returnUrl = returnUrl });
                }
                catch (MembershipCreateUserException exception)
                {
                    base.ModelState.AddModelError("", ErrorCodeToString(exception.StatusCode));
                }
            }
            ((dynamic)base.ViewBag).ReturnUrl = returnUrl;
            return base.View(model);
        }

        [AllowAnonymous]
        public ActionResult RegisterSuccess(string returnUrl)
        {
            ((dynamic)base.ViewBag).ReturnUrl = returnUrl;
            return base.View();
        }

        [AllowAnonymous]
        public ActionResult RegisterProvider(string returnUrl)
        {
            RegisterProviderModel model = new RegisterProviderModel();
            ((dynamic)base.ViewBag).ReturnUrl = returnUrl;
            return base.View(model);
        }

        [ValidateAntiForgeryToken, HttpPost, AllowAnonymous]
        public ActionResult RegisterProvider(RegisterProviderModel model, string returnUrl)
        {
            if (base.ModelState.IsValid)
            {
                try
                {
                    bool requireConfirmationToken = true;
                    string confirmationToken = WebSecurity.CreateUserAndAccount(model.UserName, model.Password, null, requireConfirmationToken);
                    this.SendRegistrationConfirmMail(ConfirmMailOperation.confirm, model.UserName, confirmationToken, "partner");

                    //add info to partners table
                    PartnerProvider.AddPartnersInfo(model);

                    //send email to manager
                    var fileName = System.IO.Path.GetFileName(model.Insurance.FileName);
                    var path = System.IO.Path.Combine(Server.MapPath("~/MediaUploader"), Guid.NewGuid().ToString() + fileName);
                    model.Insurance.SaveAs(path);

                    var fileNameDocs = System.IO.Path.GetFileName(model.ConstitutiveDocs.FileName);
                    var pathDocs = System.IO.Path.Combine(Server.MapPath("~/MediaUploader"), Guid.NewGuid().ToString() + fileNameDocs);
                    model.ConstitutiveDocs.SaveAs(pathDocs);


                    new SimpleEmailService().SendEmail<RegisterProviderModel>(ConfigurationManager.AppSettings.Get("partner_registation_email"),
                                                                                "add_provider",
                                                                                "en",
                                                                                model,
                                                                                true,
                                                                                new KeyValuePair<string,string>[] { new KeyValuePair<string,string> (path,model.Insurance.ContentType ),
                                                                                                                    new KeyValuePair<string,string> (pathDocs,model.ConstitutiveDocs.ContentType)});

                    return base.RedirectToAction("registerprovidersuccess", new { returnUrl = returnUrl });
                }
                catch (MembershipCreateUserException exception)
                {
                    base.ModelState.AddModelError("", ErrorCodeToString(exception.StatusCode));
                }
            }
            ((dynamic)base.ViewBag).ReturnUrl = returnUrl;
            return base.View(model);
        }

        [AllowAnonymous]
        public ActionResult RegisterProviderSuccess(string returnUrl)
        {
            ((dynamic)base.ViewBag).ReturnUrl = returnUrl;
            return base.View();
        }


        [AllowAnonymous]
        public ActionResult RegisterAgentWeb(string returnUrl)
        {
            var model = new RegisterAgentWebModel();
            ((dynamic)base.ViewBag).ReturnUrl = returnUrl;
            return base.View(model);
        }

        [ValidateAntiForgeryToken, HttpPost, AllowAnonymous]
        public ActionResult RegisterAgentWeb(RegisterAgentWebModel model, string returnUrl)
        {
            if (base.ModelState.IsValid)
            {
                try
                {
                    bool requireConfirmationToken = true;
                    string confirmationToken = WebSecurity.CreateUserAndAccount(model.UserName, model.Password, null, requireConfirmationToken);
                    this.SendRegistrationConfirmMail(ConfirmMailOperation.confirm, model.UserName, confirmationToken, "agent");

                    //add info to partners table
                    PartnerProvider.AddPartnersInfo(model);

                    new SimpleEmailService().SendEmail<RegisterAgentWebModel>(ConfigurationManager.AppSettings.Get("partner_registation_email"),
                                                                                "add_agent_web",
                                                                                "en",
                                                                                model
                                                                               );

                    return base.RedirectToAction("registeragentwebsuccess", new { returnUrl = returnUrl });
                }
                catch (MembershipCreateUserException exception)
                {
                    base.ModelState.AddModelError("", ErrorCodeToString(exception.StatusCode));
                }
            }
          ((dynamic)base.ViewBag).ReturnUrl = returnUrl;
            return base.View(model);
        }

        [AllowAnonymous]
        public ActionResult RegisterAgentWebSuccess(string returnUrl)
        {
            ((dynamic)base.ViewBag).ReturnUrl = returnUrl;
            return base.View();
        }

        [AllowAnonymous]
        public ActionResult RegisterAgentTerm(string returnUrl)
        {
            var model = new RegisterAgentTermModel();
            ((dynamic)base.ViewBag).ReturnUrl = returnUrl;
            return base.View(model);
        }
        [ValidateAntiForgeryToken, HttpPost, AllowAnonymous]
        public ActionResult RegisterAgentTerm(RegisterAgentTermModel model, string returnUrl)
        {
            if (base.ModelState.IsValid)
            {
                try
                {
                    bool requireConfirmationToken = true;
                    string confirmationToken = WebSecurity.CreateUserAndAccount(model.UserName, model.Password, null, requireConfirmationToken);
                    this.SendRegistrationConfirmMail(ConfirmMailOperation.confirm, model.UserName, confirmationToken,"agent");

                    //add info to partners table
                    PartnerProvider.AddPartnersInfo(model);

                    new SimpleEmailService().SendEmail<RegisterAgentTermModel>(ConfigurationManager.AppSettings.Get("partner_registation_email"),
                                                                                "add_agent_term",
                                                                                "en",
                                                                                model);

                    return base.RedirectToAction("registeragenttermsuccess", new { returnUrl = returnUrl });
                }
                catch (MembershipCreateUserException exception)
                {
                    base.ModelState.AddModelError("", ErrorCodeToString(exception.StatusCode));
                }
            }
          ((dynamic)base.ViewBag).ReturnUrl = returnUrl;
            return base.View(model);
        }

        [AllowAnonymous]
        public ActionResult RegisterAgentTermSuccess(string returnUrl)
        {
            ((dynamic)base.ViewBag).ReturnUrl = returnUrl;
            return base.View();
        }

        [ChildActionOnly]
        public ActionResult RemoveExternalLogins()
        {
            ICollection<OAuthAccount> accountsFromUserName = OAuthWebSecurity.GetAccountsFromUserName(base.User.Identity.Name);
            List<GuestService.Models.ExternalLogin> model = new List<GuestService.Models.ExternalLogin>();
            foreach (OAuthAccount account in accountsFromUserName)
            {
                AuthenticationClientData oAuthClientData = OAuthWebSecurity.GetOAuthClientData(account.Provider);
                GuestService.Models.ExternalLogin item = new GuestService.Models.ExternalLogin
                {
                    Provider = account.Provider,
                    ProviderDisplayName = oAuthClientData.DisplayName,
                    ProviderUserId = account.ProviderUserId
                };
                model.Add(item);
            }
            ((dynamic)base.ViewBag).ShowRemoveButton = (model.Count > 1) || OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(base.User.Identity.Name));
            return this.PartialView("_RemoveExternalLoginsPartial", model);
        }

        [AllowAnonymous, ValidateAntiForgeryToken, HttpPost]
        public ActionResult ResetPassword(ResetPasswordModel model)
        {
            if (base.ModelState.IsValid)
            {
                try
                {
                    bool flag = WebSecurity.ResetPassword(model.Token, model.Password);
                    return base.RedirectToAction("resetpasswordresult", new { result = flag, returnUrl = base.Url.Action("index", "welcome") });
                }
                catch (Exception)
                {
                    base.ModelState.AddModelError("", AccountStrings.ResetPassword_CannotReset);
                }
            }
            return base.View();
        }

        [AllowAnonymous]
        public ActionResult ResetPassword(string token)
        {
            ResetPasswordModel model = new ResetPasswordModel
            {
                Token = token
            };
            return base.View(model);
        }

        [AllowAnonymous]
        public ActionResult ResetPasswordResult(bool? result, string returnUrl)
        {
            bool? nullable = result;
            ((dynamic)base.ViewBag).ResetPasswordResult = nullable.HasValue ? nullable.GetValueOrDefault() : false;
            ((dynamic)base.ViewBag).ReturnUrl = returnUrl;
            return base.View();
        }

        private void SendRegistrationConfirmMail(ConfirmMailOperation action, string userName, string confirmationToken, string role = "client")
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentNullException("userName");
            }
            if (string.IsNullOrEmpty(confirmationToken))
            {
                throw new ArgumentNullException("confirmationToken");
            }

            if (action == ConfirmMailOperation.confirm)
            {
                new SimpleEmailService().SendEmail<AccountConfirmationTemplate>(userName,
                                                                "send_registration_confirm",
                                                                "en",
                                                                new AccountConfirmationTemplate()
                                                                {
                                                                    Role = role,
                                                                    Token = confirmationToken,
                                                                    ConfirmUrl = new Uri(base.Request.BaseServerAddress(), base.Url.Action("confirm", new { email = userName, token = confirmationToken })).ToString()
                                                                });
            }
            else if (action == ConfirmMailOperation.recovery)
            {
                new SimpleEmailService().SendEmail<AccountConfirmationTemplate>(userName,
                                                             "send_registration_resetpassword",
                                                             UrlLanguage.CurrentLanguage,
                                                             new AccountConfirmationTemplate()
                                                             {
                                                                 Role = role,
                                                                 Token = confirmationToken,
                                                                 ConfirmUrl = new Uri(base.Request.BaseServerAddress(), base.Url.Action("resetpassword", new { token = confirmationToken })).ToString()
                                                             });
            }

            /*
          
            string content = null;
            switch (action)
            {
                case ConfirmMailOperation.confirm:
                    content = new Uri(base.Request.BaseServerAddress(), base.Url.Action("confirm", new { email = userName, token = confirmationToken })).ToString();
                    break;

                case ConfirmMailOperation.recovery:
                    content = new Uri(base.Request.BaseServerAddress(), base.Url.Action("resetpassword", new { token = confirmationToken })).ToString();
                    break;
            }
            UserToolsProvider.UmgRaiseMessage(UrlLanguage.CurrentLanguage, "Guest Service Registration", userName, "GS_REGCONFIRM", new XElement("guestServiceRegistration", new object[] { new XAttribute("action", action.ToString()), new XElement("confirmUrl", content), new XElement("email", userName) }).ToString());
            */
        }

        private enum ConfirmMailOperation
        {
            confirm,
            recovery
        }

        internal class ExternalLoginResult : ActionResult
        {
            public ExternalLoginResult(string provider, string returnUrl)
            {
                this.Provider = provider;
                this.ReturnUrl = returnUrl;
            }

            public override void ExecuteResult(ControllerContext context)
            {
                OAuthWebSecurity.RequestAuthentication(this.Provider, this.ReturnUrl);
            }

            public string Provider { get; private set; }

            public string ReturnUrl { get; private set; }
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess
        }
    }
}

