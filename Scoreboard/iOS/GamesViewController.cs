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
				gameTableView.Source = new TableViewSource(value, this);
				gameTableView.ReloadData();
			}
		}
		public Game selectedGame;
		UIImagePickerController imagePicker;

		public UIImage imgTeam1Player1;
		public UIImage imgTeam1Player2;
		public UIImage imgTeam2Player1;
		public UIImage imgTeam2Player2;

        public GamesViewController (IntPtr handle) : base (handle) {}

		public async override void ViewDidLoad()
		{
			base.ViewDidLoad();

			var plist = NSUserDefaults.StandardUserDefaults;
			var username = plist.StringForKey("username");
			System.Diagnostics.Debug.WriteLine("Username: " + username);
			if (username == null)
			{
				showRegisterViewController();
			}

			games = await GameCall.GetAllGames();
		}

		public void showRegisterViewController()
		{
			UIStoryboard storyboard = UIStoryboard.FromName("Main", null);
			RegisterModalViewController viewController = storyboard.InstantiateViewController("registerModalViewController") as RegisterModalViewController;

			PresentViewController(viewController, true, null);
		}

		public void goToDetailViewController()
		{
			PerformSegue("gameDetailSegue", this);
		}

		public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
		{
			base.PrepareForSegue(segue, sender);
			if (segue.Identifier == "gameDetailSegue" && selectedGame != null)
			{
				var gameDetailController = (GameDetailViewController)segue.DestinationViewController;
				gameDetailController.game = this.selectedGame;
				gameDetailController.team1Player1Image = imgTeam1Player1;
				gameDetailController.team1Player2Image = imgTeam1Player2;
				gameDetailController.team2Player1Image = imgTeam2Player1;
				gameDetailController.team2Player2Image = imgTeam2Player2;
			}
		}
	}


}