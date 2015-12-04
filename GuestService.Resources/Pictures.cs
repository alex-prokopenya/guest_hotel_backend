using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;
namespace GuestService.Resources
{
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0"), System.Diagnostics.DebuggerNonUserCode, System.Runtime.CompilerServices.CompilerGenerated]
	public class Pictures
	{
		private static System.Resources.ResourceManager resourceMan;
		private static System.Globalization.CultureInfo resourceCulture;
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static System.Resources.ResourceManager ResourceManager
		{
			get
			{
				if (object.ReferenceEquals(Pictures.resourceMan, null))
				{
					System.Resources.ResourceManager temp = new System.Resources.ResourceManager("GuestService.Resources.Pictures", typeof(Pictures).Assembly);
					Pictures.resourceMan = temp;
				}
				return Pictures.resourceMan;
			}
		}
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static System.Globalization.CultureInfo Culture
		{
			get
			{
				return Pictures.resourceCulture;
			}
			set
			{
				Pictures.resourceCulture = value;
			}
		}
		public static Bitmap GuideNoPhoto
		{
			get
			{

                return Properties.Resources._78777;
            }
		}
		public static Bitmap nophoto
		{
			get
			{

                return Properties.Resources._78777;
            }
		}
		internal Pictures()
		{
		}
	}
}
