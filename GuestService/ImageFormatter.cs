using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
namespace GuestService
{
	public class ImageFormatter
	{
		public enum AjustMode
		{
			fillRegion,
			fitRegion,
			original
		}
		private Image image;
		public int Width
		{
			get;
			set;
		}
		public int Height
		{
			get;
			set;
		}
		public ImageFormat Format
		{
			get;
			set;
		}
		public ImageFormatter.AjustMode Mode
		{
			get;
			set;
		}
		public Color Background
		{
			get;
			set;
		}
		public string MediaType
		{
			get
			{
				string result;
				if (this.Format == ImageFormat.Png)
				{
					result = "image/png";
				}
				else
				{
					if (this.Format != ImageFormat.Jpeg)
					{
						throw new System.Exception(string.Format("unsupported image format: {0}", this.Format.ToString()));
					}
					result = "image/jpeg";
				}
				return result;
			}
		}
		public ImageFormatter(Image image) : this(image, null)
		{
		}
		public ImageFormatter(Image image, Image defaultImage)
		{
			this.Format = ImageFormat.Png;
			this.Mode = ImageFormatter.AjustMode.fillRegion;
			this.Background = Color.Transparent;
			this.image = ((image != null) ? image : defaultImage);
			if (this.image != null)
			{
				this.Width = this.image.Width;
				this.Height = this.image.Height;
			}
			else
			{
				this.Width = (this.Height = 0);
			}
		}
		public System.IO.Stream CreateStream()
		{
			System.IO.Stream result;
			if (this.image != null)
			{
				System.IO.MemoryStream stream = new System.IO.MemoryStream();
				if (this.Mode == ImageFormatter.AjustMode.original || (this.image.Width == this.Width && this.image.Height == this.Height))
				{
					this.image.Save(stream, this.Format);
				}
				else
				{
					using (Bitmap dstimage = new Bitmap(this.Width, this.Height))
					{
						using (Graphics gr = Graphics.FromImage(dstimage))
						{
							gr.InterpolationMode = InterpolationMode.HighQualityBicubic;
							float sizeRatio = 1f;
							if (this.Mode == ImageFormatter.AjustMode.fillRegion)
							{
								sizeRatio = System.Math.Max((float)this.Width / (float)this.image.Width, (float)this.Height / (float)this.image.Height);
							}
							else
							{
								if (this.Mode == ImageFormatter.AjustMode.fitRegion)
								{
									gr.FillRectangle(new SolidBrush(this.Background), new Rectangle(new Point(0, 0), dstimage.Size));
									sizeRatio = System.Math.Min((float)this.Width / (float)this.image.Width, (float)this.Height / (float)this.image.Height);
								}
							}
							int resultWidth = System.Convert.ToInt32((float)this.image.Width * sizeRatio);
							int resultHeight = System.Convert.ToInt32((float)this.image.Height * sizeRatio);
							gr.DrawImage(this.image, new Rectangle((this.Width - resultWidth) / 2 - 1, (this.Height - resultHeight) / 2 - 1, resultWidth + 2, resultHeight + 2), -1, -1, this.image.Width + 2, this.image.Height + 2, GraphicsUnit.Pixel);
						}
						dstimage.Save(stream, this.Format);
					}
				}
				stream.Position = 0L;
				result = stream;
			}
			else
			{
				result = null;
			}
			return result;
		}
	}
}
