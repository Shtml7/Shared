using System;
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
	}
}

