using System;
using System.Configuration;
using System.Net;

namespace GuestService.Notifications
{
    public class SmsSender
    {
        public SmsSender()
        {

        }

        public void SendMessage<T>(string to, string templateName, string language, T model, bool throwException = false)
        {
            try
            {
                var content = TemplateParser.ParseSms(templateName, language, model);

                var message = PrepareMessage(to, content, ConfigurationManager.AppSettings["sms_name"]);

                WebClient wcl = new WebClient();
                wcl.Encoding = new System.Text.UTF8Encoding();

                wcl.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";

                wcl.UploadString(new Uri(ConfigurationManager.AppSettings["sms_url"]), "POST", message);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                if (throwException) throw ex;
            }

        }
        private static string PrepareMessage(string to, string body, string name)
        {
            var message = "<?xml version = \"1.0\" encoding=\"UTF-8\"?>" +
                           "<request>" +
                            "<auth>" +
                            "<login>" + ConfigurationManager.AppSettings["sms_login"] + "</login>" +
                            "<password>" + ConfigurationManager.AppSettings["sms_password"] + "</password>" +
                            "</auth>" +
                            "<message>" +
                            "<from>" + name + "</from>" +
                            "<text>" + body + "</text>" +
                            "<recipient>" + to + "</recipient>" +
                            "</message>" +
                            "</request>";

            return message;
        }
    }
}
