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
		List<User> users
		{
			set
			{
				System.Diagnostics.Debug.WriteLine("Set datasource");
				playersTableView.Source = new SelectPlayerDatasource(value, this);
				playersTableView.ReloadData();
			}
		}


        public SelectPlayerViewController (IntPtr handle) : base (handle)
        {
        }

		public async override void ViewDidLoad()
		{
			base.ViewDidLoad();

			users = await UserCall.getUsers();


		}

		partial void BarButtonCancel_Activated(UIBarButtonItem sender)
		{
			this.DismissViewController(true, null);
		}

		public void didSelectPlayer(User user, UIImage image)
		{
			parent.didSelectPlayer(thisPlayer, user, image);
		}
	}
}