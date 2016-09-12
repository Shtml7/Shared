using Foundation;
using System;
using UIKit;
using Scoreboard.communicator;
using System.Collections.Generic;
using Scoreboard.domain;

namespace Scoreboard.iOS
{
	public partial class GamesViewController : UIViewController
	{
		List<Game> games
		{
			set
			{
				System.Diagnostics.Debug.WriteLine("Set datasource");
				gameTableView.Source = new TableViewSource(value);
				gameTableView.ReloadData();
			}
		}

        public GamesViewController (IntPtr handle) : base (handle) {}

		public async override void ViewDidLoad()
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

			games = await GameCall.GetAllGames();
			//gameTableView.Source = new TableViewSource(games);


				
		}

		public void showRegisterViewController()
		{
			UIStoryboard storyboard = UIStoryboard.FromName("Main", null);
			RegisterModalViewController viewController = storyboard.InstantiateViewController("registerModalViewController") as RegisterModalViewController;
			PresentViewController(viewController, true, null);
		}
	}


}