using Foundation;
using System;
using UIKit;

namespace Scoreboard.iOS
{
	public partial class RegisterModalViewController : UIViewController
	{
		UIImagePickerController imagePicker;
		String imageUrl = "";
		nfloat oldConstraint;

		public RegisterModalViewController(IntPtr handle) : base(handle)
		{
		}

		public RegisterModalViewController() { }

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			imagePicker = new UIImagePickerController();
			imagePicker.FinishedPickingMedia += Handle_FinishedPickingMedia;
			imagePicker.Canceled += Handle_Canceled;
			imagePicker.SourceType = UIImagePickerControllerSourceType.PhotoLibrary;
			imagePicker.MediaTypes = UIImagePickerController.AvailableMediaTypes(UIImagePickerControllerSourceType.PhotoLibrary);

			UITapGestureRecognizer tapGesture = new UITapGestureRecognizer(TapProfileImageView);
			profileImage.AddGestureRecognizer(tapGesture);

			NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.WillHideNotification, OnKeyboardHideNotification);
			NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.WillShowNotification, OnKeyboardShowNotification);

			tfUsername.ShouldReturn += (tfUsername) =>
			{
				((UITextField)tfUsername).ResignFirstResponder();
				return true;
			};

			try
			{
				double min = Math.Min(profileImage.Bounds.Width, profileImage.Bounds.Height);
				profileImage.Layer.CornerRadius = (float)(min / 2.0);
				profileImage.Layer.MasksToBounds = false;
				profileImage.ClipsToBounds = true;
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine("Unable to create circle image: " + ex);
			}
		}

		partial void Button_TouchUpInside(UIButton sender)
		{
			if (tfUsername.Text.Trim().Length == 0)
			{
				lblWarning.Text = "Enter a your name first";
			}
			else {
				var byteArray = IOSImageUtil.CompressImage(profileImage.Image);
				User user = new User();
				user.username = tfUsername.Text;
				UserCall.UploadImage(byteArray, "jpg", user);
				var plist = NSUserDefaults.StandardUserDefaults;
				plist.SetString(tfUsername.Text, "username");
				plist.SetString(imageUrl, "imageUrl");
				DismissViewController(true, null);
			}

		}

		private void TapProfileImageView()
		{
			System.Diagnostics.Debug.WriteLine("Did tap imageview");

			try
			{
				PresentViewController(imagePicker, true, null);
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine("EXCEPTION: " + ex.Message);
			}

		}

		protected void Handle_FinishedPickingMedia(object sender, UIImagePickerMediaPickedEventArgs e)
		{
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

			NSUrl referenceURL = e.Info[new NSString("UIImagePickerControllerReferenceURL")] as NSUrl;
			Console.WriteLine("URL: " + referenceURL);
			imageUrl = referenceURL.ToString();

			if (isImage)
			{
				UIImage originalImage = e.Info[UIImagePickerController.OriginalImage] as UIImage;
				if (originalImage != null)
				{
					Console.WriteLine("got the original image");
					profileImage.Image = originalImage;
					lblWarning.Text = "";
				}
			}
			else
			{
				NSUrl mediaURL = e.Info[UIImagePickerController.MediaURL] as NSUrl;
				if (mediaURL != null)
				{
					Console.WriteLine(mediaURL);
					lblWarning.Text = "Only images are allowed.";
				}
			}
			imagePicker.DismissModalViewController(true);
		}

		private void Handle_Canceled(object sender, EventArgs e)
		{
			imagePicker.DismissModalViewController(true);
			lblWarning.Text = "";
		}

		private void OnKeyboardShowNotification(NSNotification notification)
		{
			oldConstraint = topImageConstraint.Constant;
			System.Diagnostics.Debug.WriteLine("Show me the keyboard");
			System.Diagnostics.Debug.WriteLine("Screen height:" + UIScreen.MainScreen.Bounds.Height);
			if (UIScreen.MainScreen.Bounds.Height <= 960/2)
			{
				topImageConstraint.Constant = 20;
				animateLayout();
			}	
		}

		private void OnKeyboardHideNotification(NSNotification notification)
		{
			System.Diagnostics.Debug.WriteLine("Hide the keyboard");
			if (UIScreen.MainScreen.Bounds.Height/2 <= 960/2)
			{
				topImageConstraint.Constant = oldConstraint;
				animateLayout();
			}

		}

		private void animateLayout()
		{
			UIView.Animate(1.0, () =>
			{
				this.View.LayoutIfNeeded();
			});
		}
	}
}