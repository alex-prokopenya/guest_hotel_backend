using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Razor;
using RazorEngine;
using System.IO;
using System.Reflection;
using System.Configuration;


namespace GuestService.Notifications
{
    class TemplateParser
    {
        public static EmailMessage ParseMessage(string templateName, string language, string type, object model)
        {
            var templatePath = Path.Combine(GetAssemblyDirectory(), "Views", "Shared", "Templates", type, language, string.Format("{0}.cshtml", templateName));
            var subjectPath = Path.Combine(GetAssemblyDirectory(), "Views", "Shared", "Templates", type, language, string.Format("{0}_subj.cshtml", templateName));

            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["templates_folder"]))
            {
                templatePath = Path.Combine(ConfigurationManager.AppSettings["templates_folder"], type, language, string.Format("{0}.cshtml", templateName));
                subjectPath = Path.Combine(ConfigurationManager.AppSettings["templates_folder"], type, language, string.Format("{0}_subj.cshtml", templateName));
            }
            
            var content = templatePath.ReadTemplateContent();
            var subjectContent = subjectPath.ReadTemplateContent();

            string subject = Razor.Parse(subjectContent, model);
            string body = Razor.Parse(content, model);

            return new EmailMessage() {
                Body = body,
                Subject = subject
            };
        }

        public static string ParseSms(string templateName, string language, object model)
        {
            var templatePath = Path.Combine(GetAssemblyDirectory(), "Views", "Shared", "Templates", "Sms", language, string.Format("{0}.cshtml", templateName));

            if(!string.IsNullOrEmpty( ConfigurationManager.AppSettings["templates_folder"]))
                templatePath = Path.Combine(ConfigurationManager.AppSettings["templates_folder"], "Sms", language, string.Format("{0}.cshtml", templateName));

            var content = templatePath.ReadTemplateContent();

            return Razor.Parse(content, model);
        }

        public static string GetAssemblyDirectory()
        {
            var codeBase = Assembly.GetExecutingAssembly().CodeBase;
            var uri = new UriBuilder(codeBase);
            var dir = new DirectoryInfo(Uri.UnescapeDataString(uri.Path));
            return dir.Parent.Parent.FullName;
        }
    }

    public static class TemplateExtensions
    {
        public static string ReadTemplateContent(this string path)
        {
            string content;
            using (var reader = new StreamReader(path, new System.Text.UTF8Encoding()))
            {
                content = reader.ReadToEnd();
            }

            return content;
        }
    }
}
