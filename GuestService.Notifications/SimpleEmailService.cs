using System.Net.Mail;
using System.Net;
using System;
using System.Configuration;
using System.Net.Mime;
using System.IO;
using System.Web.Mvc;
using System.Collections.Generic;

//УВЕДОМЛЕНИЯ ОТ САМО ГЕНЕРИРУЮТСЯ В ХРАНИМКЕ [dbo].[up_booking_notification_processor]
namespace GuestService.Notifications
{
    public class SimpleEmailService
    {
        public SimpleEmailService()
        {

        }

        public void SendEmail<T>(string to, string templateName, string language, T model, bool throwException, KeyValuePair<string, string>[] files = null)
        {

            try
            {
                var content = TemplateParser.ParseMessage(templateName, language, "Email", model);

                var message = BuildMessage(to, content.Subject, content.Body, files);


                var client = new SmtpClient(ConfigurationManager.AppSettings.Get("smtp_server"),
                                            Convert.ToInt32(ConfigurationManager.AppSettings.Get("smtp_port")));

                client.UseDefaultCredentials = false;

                NetworkCredential credential = new NetworkCredential(ConfigurationManager.AppSettings.Get("smtp_user"),
                                                                     ConfigurationManager.AppSettings.Get("smtp_pass"));

                client.Credentials = credential;

                client.EnableSsl = ConfigurationManager.AppSettings.Get("smtp_use_ssl") == "true";

                client.Send(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                if (throwException) throw ex;
            }
        }

        public void SendEmail<T>(string to, string templateName, string language, T model, bool throwException = false)
        {
            SendEmail<T>(to, templateName, language, model, throwException, null);
        }

        private MailMessage BuildMessage(string to, string subject, string content,  KeyValuePair<string, string>[] files = null)
        {
            string htmlBody = content;
            AlternateView avHtml = AlternateView.CreateAlternateViewFromString
                (htmlBody, new System.Text.UTF8Encoding(), MediaTypeNames.Text.Html);
            
            // Create a LinkedResource object for each embedded image
            LinkedResource pic1 = null;

            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["templates_folder"]))
            {
                var uri = new UriBuilder(ConfigurationManager.AppSettings["templates_folder"]);
                var dir = new DirectoryInfo(Uri.UnescapeDataString(uri.Path));

                pic1 = new LinkedResource(Path.Combine(dir.Parent.Parent.Parent.FullName, "Images", "logo.png"), "image/png");
            }
            else
                pic1 = new LinkedResource(Path.Combine(TemplateParser.GetAssemblyDirectory(), "Images", "logo.png"), "image/png");

            pic1.ContentId = "logo";
            avHtml.LinkedResources.Add(pic1);
            
            // Add the alternate views instead of using MailMessage.Body
            MailMessage m = new MailMessage();
            m.AlternateViews.Add(avHtml);

            if (files != null)
            {
                foreach (KeyValuePair<string, string> fileName in files)
                {
                    System.Net.Mail.Attachment attachment = new System.Net.Mail.Attachment(fileName.Key, fileName.Value );
                    var tmp = fileName.Key.Split('\\');
                    attachment.Name = tmp[tmp.Length - 1] ;  // set name here
                    m.Attachments.Add(attachment);
                }
            }

            // Address and send the message
            m.From = new MailAddress(ConfigurationManager.AppSettings.Get("smtp_user"), "ExGo.com info");
            m.To.Add(new MailAddress(to));
            m.Subject = subject;
            m.IsBodyHtml = true;
            m.BodyEncoding = new System.Text.UTF8Encoding();

            return m;
        }
    }
}