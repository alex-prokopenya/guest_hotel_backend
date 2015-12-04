using System;
using System.IO;
namespace GuestService.Code
{
	public class ImageCacheItem
	{
		public byte[] Data
		{
			get;
			set;
		}
		public string MediaType
		{
			get;
			set;
		}
		public static ImageCacheItem Create(System.IO.Stream stream, string mediaType)
		{
			ImageCacheItem result = new ImageCacheItem();
			if (stream != null)
			{
				result.Data = new byte[stream.Length];
				stream.Read(result.Data, 0, result.Data.Length);
				stream.Position = 0L;
			}
			result.MediaType = mediaType;
			return result;
		}
		public System.IO.Stream CraeteStream()
		{
			return (this.Data != null) ? new System.IO.MemoryStream(this.Data) : null;
		}
	}
}
