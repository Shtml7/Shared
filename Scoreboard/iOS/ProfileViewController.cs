using Foundation;
using System;
using UIKit;
using Scoreboard.communicator;
using System.Collections.Generic;
using Scoreboard.domain;

namespace Scoreboard.iOS
{
	/**
	 * TODO: ViewController for showing the profile of the current user
	*/ 
    public partial class ProfileViewController : UIViewController
    {
        public ProfileViewController (IntPtr handle) : base (handle)
        {
        }

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
		}
	}
}