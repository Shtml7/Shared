using System;
using Foundation;
using Plugin.Media;
using Plugin.Media.Abstractions;
using UIKit;

namespace Scoreboard.iOS
{
	public partial class ViewController : UIViewController
	{
		int count = 1;
		UIImagePickerController imagePicker = new UIImagePickerController();

		public ViewController(IntPtr handle) : base(handle)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			// Perform any additional setup after loading the view, typically from a nib.
			Button.AccessibilityIdentifier = "myButton";
			Button.TouchUpInside += delegate
			{
				//var title = string.Format("{0} clicks!", count++);
				//Button.SetTitle(title, UIControlState.Normal);

				imagePicker.SourceType = UIImagePickerControllerSourceType.PhotoLibrary;
				imagePicker.MediaTypes = UIImagePickerController.AvailableMediaTypes(UIImagePickerControllerSourceType.PhotoLibrary);

				imagePicker.FinishedPickingMedia += Handle_FinishedPickingMedia;
				imagePicker.Canceled += Handle_Canceled;

				PresentModalViewController(imagePicker, true);

			};
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.		
		}

		public async void getUser()
		{
			UserCall calls = new UserCall();
			await calls.getUser();
		}

		protected void Handle_FinishedPickingMedia(object sender, UIImagePickerMediaPickedEventArgs e)
		{
			// determine what was selected, video or image
			bool isImage = false;
			switch (e.Info[UIImagePickerController.MediaType].ToString())
			{
				case "public.image":
					Console.WriteLine("Image selected");
					isImage = true;
					break;
				case "public.video":
					Console.WriteLine("Video selected");
					break;
			}

			// get common info (shared between images and video)
			NSUrl referenceURL = e.Info[new NSString("UIImagePickerControllerReferenceUrl")] as NSUrl;
			if (referenceURL != null)
				Console.WriteLine("Url:" + referenceURL.ToString());

			// if it was an image, get the other image info
			if (isImage)
			{
				// get the original image
				UIImage originalImage = e.Info[UIImagePickerController.OriginalImage] as UIImage;
				if (originalImage != null)
				{
					// do something with the image
					Console.WriteLine("got the original image");
					imageView.Image = originalImage; // display

					Byte[] myByteArray;
					User user = new User();
					user.name = "ericderegter";
					using (NSData imageData = originalImage.AsPNG())
					{
						myByteArray = new Byte[imageData.Length];
						System.Runtime.InteropServices.Marshal.Copy(imageData.Bytes, myByteArray, 0, Convert.ToInt32(imageData.Length));
						//Do something with the byte array
						System.Diagnostics.Debug.WriteLine("Making a byte array");

					}
					UserCall call = new UserCall();
					call.UploadImage(myByteArray, "png", user);
				}
			}
			else { // if it's a video
				   // get video url
				NSUrl mediaURL = e.Info[UIImagePickerController.MediaURL] as NSUrl;
				if (mediaURL != null)
				{
					Console.WriteLine(mediaURL.ToString());
				}
			}
			// dismiss the picker
			imagePicker.DismissModalViewController(true);
		}

		private void Handle_Canceled(object sender, EventArgs e)
		{
			imagePicker.DismissModalViewController(true);
		}
	}
}
