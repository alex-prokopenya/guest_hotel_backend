using Sm.System.Mvc.Language;
using System;
using System.IO;
using System.Web.Mvc;
namespace GuestService.Controllers.Html
{
	public class CustomizationController : BaseController
	{
		[ChildActionOnly, OutputCache(Duration = 3600, VaryByCustom = "language")]
		public ActionResult PageSuperHeader()
		{
			string content = this.LoadCustomContent("PageSuperHeader.html");
			ActionResult result;
			if (content != null)
			{
				result = new ContentResult
				{
					Content = content
				};
			}
			else
			{
				result = base.View();
			}
			return result;
		}
		[ChildActionOnly, OutputCache(Duration = 3600, VaryByCustom = "language")]
		public ActionResult PageLogo()
		{
			string content = this.LoadCustomContent("PageLogo.html");
			ActionResult result;
			if (content != null)
			{
				result = new ContentResult
				{
					Content = content
				};
			}
			else
			{
				result = base.View();
			}
			return result;
		}
		[ChildActionOnly, OutputCache(Duration = 3600, VaryByCustom = "language")]
		public ActionResult PageNavigation()
		{
			string content = this.LoadCustomContent("PageNavigation.html");
			ActionResult result;
			if (content != null)
			{
				result = new ContentResult
				{
					Content = content
				};
			}
			else
			{
				result = base.View();
			}
			return result;
		}
		[ChildActionOnly, OutputCache(Duration = 3600, VaryByCustom = "language")]
		public ActionResult PageSubHeader()
		{
			string content = this.LoadCustomContent("PageSubHeader.html");
			ActionResult result;
			if (content != null)
			{
				result = new ContentResult
				{
					Content = content
				};
			}
			else
			{
				result = base.View();
			}
			return result;
		}
		[ChildActionOnly, OutputCache(Duration = 3600, VaryByCustom = "language")]
		public ActionResult PageFooter()
		{
			string content = this.LoadCustomContent("PageFooter.html");
			ActionResult result;
			if (content != null)
			{
				result = new ContentResult
				{
					Content = content
				};
			}
			else
			{
				result = base.View();
			}
			return result;
		}
		private string LoadCustomContent(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new System.ArgumentNullException("name");
			}
			string fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(name);
			string fileNameExtension = System.IO.Path.GetExtension(name);
			string adoptionFoldersServerPath = base.Server.MapPath(CustomizationPath.AdoptionFolder);
			string file = System.IO.Path.Combine(adoptionFoldersServerPath, string.Format("{0}.{1}{2}", fileNameWithoutExtension, UrlLanguage.CurrentLanguage, fileNameExtension));
			string result;
			if (System.IO.File.Exists(file))
			{
				result = System.IO.File.ReadAllText(file);
			}
			else
			{
				file = System.IO.Path.Combine(adoptionFoldersServerPath, name);
				if (System.IO.File.Exists(file))
				{
					result = System.IO.File.ReadAllText(file);
				}
				else
				{
					result = null;
				}
			}
			return result;
		}
	}
}
