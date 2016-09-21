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
		//List<Game> Games
		//{
		//	get { return games; }
		//	set
		//	{
		//		games = value;
		//		System.Diagnostics.Debug.WriteLine("Set datasource");
		//		gameTableView.Source = new TableViewSource(value, this);
		//		gameTableView.ReloadData();
		//	}
		//}
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
			var newGames = await GameCall.GetAllGames();
			if (games == null)
			{
				setupDatasource(newGames);
			}
			else if (games.Count != newGames.Count)
			{
				setupDatasource(newGames);
			}
			games = newGames;

		}

		public async void setupDatasource(List<Game> newGames)
		{
			
			var dict = new Dictionary<Game, Dictionary<int, UIImage>>();

			foreach (var game in newGames)
			{
				var imageTeam1Player1 = await IOSImageUtil.FromUrl(game.team1.player1.imageUrl);
				var imageTeam1Player2 = await IOSImageUtil.FromUrl(game.team1.player2.imageUrl);
				var imageTeam2Player1 = await IOSImageUtil.FromUrl(game.team2.player1.imageUrl);
				var imageTeam2Player2 = await IOSImageUtil.FromUrl(game.team2.player2.imageUrl);

				Dictionary<int, UIImage> images = new Dictionary<int, UIImage>();
				images.Add(1, imageTeam1Player1);
				images.Add(2, imageTeam1Player2);
				images.Add(3, imageTeam2Player1);
				images.Add(4, imageTeam2Player2);

				dict.Add(game, images);
				System.Diagnostics.Debug.WriteLine("Set datasource");
				gameTableView.Source = new TableViewSource(newGames, dict, this);
				gameTableView.ReloadData();
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
				gameDetailController.team1Player1Image = imgTeam1Player1;
				gameDetailController.team1Player2Image = imgTeam1Player2;
				gameDetailController.team2Player1Image = imgTeam2Player1;
				gameDetailController.team2Player2Image = imgTeam2Player2;
			}
		}
	}


}