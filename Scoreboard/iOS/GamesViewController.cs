using Foundation;
using System;
using UIKit;

namespace Scoreboard.iOS
{
    public partial class GamesViewController : UIViewController
    {
        public GamesViewController (IntPtr handle) : base (handle)
        {
        }

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			//check if user is a first time user
			var plist = NSUserDefaults.StandardUserDefaults;
			var username = plist.StringForKey("username");
			System.Diagnostics.Debug.WriteLine("Username: " + username);
			if (username == null)
			{
				showRegisterViewController();
			}
				
		}

		partial void UIButton65_TouchUpInside(UIButton sender)
		{
			
		}

		public void showRegisterViewController()
		{
			UIStoryboard storyboard = UIStoryboard.FromName("Main", null);
			RegisterModalViewController viewController = storyboard.InstantiateViewController("registerModalViewController") as RegisterModalViewController;
			PresentViewController(viewController, true, null);
		}
	}
}