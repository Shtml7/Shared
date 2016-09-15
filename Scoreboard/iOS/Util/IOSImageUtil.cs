using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Foundation;
using UIKit;

namespace Scoreboard.iOS
{
	public static class IOSImageUtil
	{

		public static byte[] CompressImage(UIImage image)
		{
			using (NSData imageData = image.AsJPEG((float)0.5))
			{
				var byteArray = new Byte[imageData.Length];
				System.Runtime.InteropServices.Marshal.Copy(imageData.Bytes, byteArray, 0, Convert.ToInt32(imageData.Length));
				//Do something with the byte array
				System.Diagnostics.Debug.WriteLine("Making a byte array");
				return byteArray;
			}
		}

		public static UIImage OldFromUrl(string uri)
		{
			using (var url = new NSUrl(uri))
			{
				System.Diagnostics.Debug.WriteLine("Going to download image from url: " + url);
				using (var data = NSData.FromUrl(url))
				{
					return UIImage.LoadFromData(data);
				}
			}
		}

		public static async Task<UIImage> FromUrl(string imageUrl)
		{
			var httpClient = new HttpClient();
			try
			{
				System.Diagnostics.Debug.WriteLine("Going to download image from url: " + imageUrl);
				Task<byte[]> contentsTask = httpClient.GetByteArrayAsync(imageUrl);
				var contents = await contentsTask;

				return UIImage.LoadFromData(NSData.FromArray(contents));
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine("EXCEPTION: " + ex.Message);
				return new UIImage("noImage.png");
			}

		}

		public static void makeRoundImageView(UIImageView imageView)
		{
			try
			{
				double min = Math.Min(imageView.Bounds.Width, imageView.Bounds.Height);
				imageView.Layer.CornerRadius = (float)(min / 2.0);
				imageView.Layer.MasksToBounds = false;
				imageView.ClipsToBounds = true;
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine("Unable to create circle image: " + ex);
			}
		}
	}
}

