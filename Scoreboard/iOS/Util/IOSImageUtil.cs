using System;
using System.Net.Http;
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

		public static UIImage FromUrl(string uri)
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

		//public async static Task<UIImage> FromUrl2(string url)
		//{
		//	try
		//	{

		//		using (var client = new HttpClient())
		//		{
		//			using (var response = await client.GetAsync(url))
		//			{
		//				response.EnsureSuccessStatusCode();
		//				//var byteArray = response.Content.ReadAsInputStreamAsync();
		//				//UIImage image = new UIImage(byteArray.as);
		//			}
		//		}
		//	}
		//	catch (Exception ex)
		//	{
		//		System.Diagnostics.Debug.WriteLine("EXCEPTION WHILE LOADING IMAGE FROM URL: " + ex.Message);
		//		return new UIImage("noImage.png");
		//	}


		//}

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

