using Foundation;
using System;
using UIKit;
using Scoreboard.domain;
using Scoreboard.communicator;

namespace Scoreboard.iOS
{
    public partial class AddGameViewController : UIViewController
    {
		public User team1Player1;
		public User team1Player2;
		public User team2Player1;
		public User team2Player2;

		NrPlayer playerToSelect = NrPlayer.First;

        public AddGameViewController (IntPtr handle) : base (handle)
        {
        }

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			IOSImageUtil.makeRoundImageView(imgTeam1Player1);
			IOSImageUtil.makeRoundImageView(imgTeam1Player2);
			IOSImageUtil.makeRoundImageView(imgTeam2Player1);
			IOSImageUtil.makeRoundImageView(imgTeam2Player2);

			var firstGesture = new UITapGestureRecognizer(FirstPlayer);
			var secondGesture = new UITapGestureRecognizer(SecondPlayer);
			var thirdGesture = new UITapGestureRecognizer(ThirdPlayer);
			var fourthGesture = new UITapGestureRecognizer(FourthPlayer);
			team1Player1View.AddGestureRecognizer(firstGesture);
			team1Player2View.AddGestureRecognizer(secondGesture);
			team2Player1View.AddGestureRecognizer(thirdGesture);
			team2Player2View.AddGestureRecognizer(fourthGesture);
		}

		partial void BarButtonSave_Activated(UIBarButtonItem sender)
		{
			saveAndCreateGame();
		}


		public async void saveAndCreateGame()
		{
			if (team1Player1 != null && team1Player2 != null && team2Player1 != null && team2Player2 != null)
			{
				//Save and dismis
				Team team1 = new Team();
				team1.player1 = team1Player1;
				team1.player2 = team1Player2;

				Team team2 = new Team();
				team2.player1 = team2Player1;
				team2.player2 = team2Player2;

				Game game = new Game();
				game.team1 = team1;
				game.team2 = team2;

				var plist = NSUserDefaults.StandardUserDefaults;
				var userId = (int)plist.IntForKey("userId");
				User owner = await UserCall.getUserWithid(userId);
				if (owner != null)
				{
					Game createdGame = await GameCall.createGame(game);
					if (createdGame.id != 0 || createdGame.id != -1)
					{
						DismissViewController(true, null);
					}
					else
					{
						showAlertController("Error", "Unable to create the game");
					}
				}
			}
			else
			{
				showAlertController("Warning", "Please select all four players");
			}
		}

		partial void BarButtonCancel_Activated(UIBarButtonItem sender)
		{
			DismissViewController(true, null);
		}

		public void FirstPlayer()
		{
			playerToSelect = NrPlayer.First;
			GotoSelectPlayer();
		}

		public void SecondPlayer()
		{
			playerToSelect = NrPlayer.Second;
			GotoSelectPlayer();
		}

		public void ThirdPlayer()
		{
			playerToSelect = NrPlayer.Third;
			GotoSelectPlayer();
		}

		public void FourthPlayer()
		{
			playerToSelect = NrPlayer.Fourth;
			GotoSelectPlayer();
		}

		public void GotoSelectPlayer()
		{
			PerformSegue("SelectPlayerSegue", this);
		}

		public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
		{
			base.PrepareForSegue(segue, sender);
			if (segue.Identifier == "SelectPlayerSegue")
			{
				var destionation = (SelectPlayerViewController)segue.DestinationViewController;
				destionation.parent = this;
				destionation.thisPlayer = playerToSelect;
			}
		}

		public void didSelectPlayer(NrPlayer whichPlayer, User user, UIImage image)
		{
			switch (whichPlayer)
			{
				case NrPlayer.First:
					team1Player1 = user;
					imgTeam1Player1.Image = image;
					lblNameTeam1Player1.Text = user.username;
					lblRankTeam1Player1.Text = "Wins: " + user.wins + " Loses: " + user.losses;
					break;
				case NrPlayer.Second:
					team1Player2 = user;
					imgTeam1Player2.Image = image;
					lblNameTeam1Player2.Text = user.username;
					lblRankTeam1Player2.Text = "Wins: " + user.wins + " Loses: " + user.losses;
				break;
				case NrPlayer.Third:
					team2Player1 = user;
					imgTeam2Player1.Image = image;
					lblNameTeam2Player1.Text = user.username;
					lblRankTeam2Player1.Text = "Wins: " + user.wins + " Loses: " + user.losses;
				break;
				case NrPlayer.Fourth:
					team2Player2 = user;
					imgTeam2Player2.Image = image;
					lblNameTeam2Player2.Text = user.username;
					lblRankTeam2Player2.Text = "Wins: " + user.wins + " Loses: " + user.losses;
				break;
			}
		}

		public void showAlertController(String title, String message)
		{
			var alertController = UIAlertController.Create(title, message, UIAlertControllerStyle.Alert);
			alertController.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, alert => System.Diagnostics.Debug.Write("OK was selected")));
			this.PresentViewController(alertController, true, null);
		}
	}

	public enum NrPlayer { First, Second, Third, Fourth };
}