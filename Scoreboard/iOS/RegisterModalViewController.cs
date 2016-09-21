using Foundation;
using System;
using UIKit;

namespace Scoreboard.iOS
{
	/**
	 * ViewController for registering as a new user
	*/ 
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

			//Create a new imagepicker
			imagePicker = new UIImagePickerController();
			imagePicker.FinishedPickingMedia += Handle_FinishedPickingMedia;
			imagePicker.Canceled += Handle_Canceled;
			imagePicker.SourceType = UIImagePickerControllerSourceType.PhotoLibrary;
			imagePicker.MediaTypes = UIImagePickerController.AvailableMediaTypes(UIImagePickerControllerSourceType.PhotoLibrary);

			//Adds a tapgesture to the imageview
			UITapGestureRecognizer tapGesture = new UITapGestureRecognizer(TapProfileImageView);
			profileImage.AddGestureRecognizer(tapGesture);

			//Dismiss keyboard when return is pressed
			tfUsername.ShouldReturn += (tfUsername) =>
			{
				((UITextField)tfUsername).ResignFirstResponder();
				return true;
			};

			IOSImageUtil.makeRoundImageView(profileImage);
		}

		//Saves the new user if everything is correctly filled in
		partial void Button_TouchUpInside(UIButton sender)
		{
			if (tfUsername.Text.Trim().Length == 0)
			{
				lblWarning.Text = "Enter a your name first";
			}
			else 
			{
				handleRegisterUser();
			}
		}

		//Prepares and performs an api call to register the new user
		public async void handleRegisterUser()
		{
			var byteArray = IOSImageUtil.CompressImage(profileImage.Image);
			User user = new User();
			user.username = tfUsername.Text;
			var userId = await UserCall.createUser(byteArray, "jpg", user);
			if (userId == -1)
			{
				lblWarning.Text = "Something went wrong, try again";
			}
			else
			{
				var plist = NSUserDefaults.StandardUserDefaults;
				plist.SetString(tfUsername.Text, "username");
				plist.SetString(imageUrl, "imageUrl");
				plist.SetInt(userId, "userId");
				DismissViewController(true, null);
			}
		}

		//Handler when the imageview is tapped
		private void TapProfileImageView()
		{
			PresentViewController(imagePicker, true, null);
		}

		//Handler for when the user picked an image from the gallery
		protected void Handle_FinishedPickingMedia(object sender, UIImagePickerMediaPickedEventArgs e)
		{
			//Check if the selected file is an image or a video
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

			//If it is an image, select it.
			//If not then display a message
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

			//Dismiss when done
			imagePicker.DismissModalViewController(true);
		}

		//Handles the cancel event
		private void Handle_Canceled(object sender, EventArgs e)
		{
			imagePicker.DismissModalViewController(true);
			lblWarning.Text = "";
		}
	}
}