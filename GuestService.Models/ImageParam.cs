using System;
namespace GuestService.Models
{
	public class ImageParam
	{
		public int? w
		{
			get;
			set;
		}
		public int? Width
		{
			get
			{
				return this.w;
			}
		}
		public int? h
		{
			get;
			set;
		}
		public int? Height
		{
			get
			{
				return this.h;
			}
		}
		public string m
		{
			get;
			set;
		}
		public string Mode
		{
			get
			{
				return this.m;
			}
		}
		public bool? sd
		{
			get;
			set;
		}
		public bool? ShowDefault
		{
			get
			{
				return this.sd;
			}
		}
		public void ApplyFormat(ImageFormatter formatter)
		{
			if (this.Width.HasValue)
			{
				formatter.Width = this.Width.Value;
			}
			if (this.Height.HasValue)
			{
				formatter.Height = this.Height.Value;
			}
			if (this.Mode == "fill")
			{
				formatter.Mode = ImageFormatter.AjustMode.fillRegion;
			}
			else
			{
				if (this.Mode == "fit")
				{
					formatter.Mode = ImageFormatter.AjustMode.fitRegion;
				}
				else
				{
					if (this.Mode == "original")
					{
						formatter.Mode = ImageFormatter.AjustMode.original;
					}
				}
			}
		}
	}
}
