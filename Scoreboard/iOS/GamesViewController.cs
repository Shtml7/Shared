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
		private List<Game> games;
		List<Game> Games
		{
			get { return games; }
			set
			{
				games = value;
				System.Diagnostics.Debug.WriteLine("Set datasource");
				gameTableView.Source = new TableViewSource(value, this);
				gameTableView.ReloadData();
			}
		}
		public Game selectedGame;
		NSUserDefaults plist = NSUserDefaults.StandardUserDefaults;

		public UIImage imgTeam1Player1;
		public UIImage imgTeam1Player2;
		public UIImage imgTeam2Player1;
		public UIImage imgTeam2Player2;

        public GamesViewController (IntPtr handle) : base (handle) {}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();


			var username = plist.StringForKey("username");
			System.Diagnostics.Debug.WriteLine("Username: " + username);
			if (username == null)
			{
				showRegisterViewController();
			}
		}

		public async override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);
			if (Games == null)
			{
				Games = await GameCall.GetAllGames();
			}
			else
			{
				List<Game> newGames = await GameCall.GetAllGames();
				if (Games.Count != newGames.Count)
				{
					Games = newGames;
				}
			}

		}

		public void showRegisterViewController()
		{
			PerformSegue("registerSegue", this);
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
				var id = plist.IntForKey("userId");
				gameDetailController.isOwnerOfTheGame = selectedGame.owner.id == id;
			}
		}
	}


}