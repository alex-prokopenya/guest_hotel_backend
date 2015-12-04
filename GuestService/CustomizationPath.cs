using System;
using System.IO;
namespace GuestService
{
	public static class CustomizationPath
	{
		public static string ViewsFolder
		{
			get
			{
				return "~/Customization/Views";
			}
		}
		public static string AgreementsFolder
		{
			get
			{
				return "~/Customization/Agreements";
			}
		}
		public static string AdoptionFolder
		{
			get
			{
				return "~/Customization/Adoption";
			}
		}
		private static string Combine(string a, string b, string c = null, string d = null, string e = null, string f = null)
		{
			if (a == null)
			{
				throw new System.ArgumentNullException("a");
			}
			CustomizationPath.ValidatePathPart(a);
			string path = a;
			if (b != null)
			{
				CustomizationPath.ValidatePathPart(b);
				path = System.IO.Path.Combine(path, b);
				if (c != null)
				{
					CustomizationPath.ValidatePathPart(c);
					path = System.IO.Path.Combine(path, c);
					if (d != null)
					{
						CustomizationPath.ValidatePathPart(d);
						path = System.IO.Path.Combine(path, d);
						if (e != null)
						{
							CustomizationPath.ValidatePathPart(e);
							path = System.IO.Path.Combine(path, e);
							if (f != null)
							{
								CustomizationPath.ValidatePathPart(f);
								path = System.IO.Path.Combine(path, f);
							}
						}
					}
				}
			}
			return path;
		}
		private static void ValidatePathPart(string name)
		{
			if (name == null)
			{
				throw new System.ArgumentNullException("name");
			}
			if (name.IndexOf(System.IO.Path.PathSeparator) >= 0)
			{
				throw new System.ArgumentException("path part contains invalid characters", "name");
			}
		}
	}
}
