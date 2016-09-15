using Foundation;
using System;
using UIKit;
using System.Collections.Generic;

namespace Scoreboard.iOS
{
    public partial class SelectPlayerViewController : UIViewController
    {
		public AddGameViewController parent;
		public NrPlayer thisPlayer;
		List<iOSUser> users;


        public SelectPlayerViewController (IntPtr handle) : base (handle)
        {
        }

		public async override void ViewDidLoad()
		{
			base.ViewDidLoad();
			users = new List<iOSUser>();

			var userList = await UserCall.getUsers();
			if (userList == null)
			{
				var alertController = UIAlertController.Create("Warning", "Could not connect to server, please try again.", UIAlertControllerStyle.Alert);
				alertController.AddAction(UIAlertAction.Create("Try Again", UIAlertActionStyle.Default, alert => ViewDidLoad()));
				alertController.AddAction(UIAlertAction.Create("No thanks", UIAlertActionStyle.Default, alert => System.Diagnostics.Debug.Write("No thanks was selected")));
				this.PresentViewController(alertController, true, null);
			}
			else {
				System.Diagnostics.Debug.WriteLine("NR OF USERS IN LIST: " + userList.Count);
				foreach (var user in userList)
				{
					UIImage image = await IOSImageUtil.FromUrl(user.imageUrl);
					var iosUser = new iOSUser(image, user);
					users.Add(iosUser);
					System.Diagnostics.Debug.WriteLine("NR OF USERS IN LIST: " + users.Count);
					playersTableView.Source = new SelectPlayerDatasource(users, this);
					playersTableView.ReloadData();
				}
			}
		}

		partial void BarButtonCancel_Activated(UIBarButtonItem sender)
		{
			this.DismissViewController(true, null);
		}

		public void didSelectPlayer(User user, UIImage image)
		{
			parent.didSelectPlayer(thisPlayer, user, image);
			this.DismissViewController(true, null);
		}
	}
}