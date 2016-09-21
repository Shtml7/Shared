using Foundation;
using System;
using UIKit;
using Scoreboard.domain;
using Scoreboard.communicator;

namespace Scoreboard.iOS
{
	/**
	 * ViewController for adding a new game
	 */ 
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

			//Make the imageviews round
			IOSImageUtil.makeRoundImageView(imgTeam1Player1);
			IOSImageUtil.makeRoundImageView(imgTeam1Player2);
			IOSImageUtil.makeRoundImageView(imgTeam2Player1);
			IOSImageUtil.makeRoundImageView(imgTeam2Player2);

			//Create and add guesture recognizers for the views
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

		//Saves the new game and makes the call to the server
		public async void saveAndCreateGame()
		{
			if (team1Player1 != null && team1Player2 != null && team2Player1 != null && team2Player2 != null)
			{
				//Save the new game and dismiss
				Team team1 = new Team();
				team1.player1 = team1Player1;
				team1.player2 = team1Player2;

				Team team2 = new Team();
				team2.player1 = team2Player1;
				team2.player2 = team2Player2;

				Game game = new Game();
				game.team1 = team1;
				game.team2 = team2;

				//Get the current user and add it as the owner
				var plist = NSUserDefaults.StandardUserDefaults;
				var userId = (int)plist.IntForKey("userId");
				User owner = await UserCall.getUserWithid(userId);
				if (owner != null)
				{
					game.owner = owner;
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

		//When the view of player 1 from team 1 is selected
		public void FirstPlayer()
		{
			playerToSelect = NrPlayer.First;
			GotoSelectPlayer();
		}

		//When the view of player 2 from team 1 is selected
		public void SecondPlayer()
		{
			playerToSelect = NrPlayer.Second;
			GotoSelectPlayer();
		}

		//When the view of player 1 from team 2 is selected
		public void ThirdPlayer()
		{
			playerToSelect = NrPlayer.Third;
			GotoSelectPlayer();
		}

		//When the view of player 2 from team 2 is selected
		public void FourthPlayer()
		{
			playerToSelect = NrPlayer.Fourth;
			GotoSelectPlayer();
		}

		public void GotoSelectPlayer()
		{
			PerformSegue("SelectPlayerSegue", this);
		}

		//Prepares for next viewcontroller. Sets the to be selected player
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

		//Called when a player is selected
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

		//Helper method for easily showing a AlertViewController
		public void showAlertController(String title, String message)
		{
			var alertController = UIAlertController.Create(title, message, UIAlertControllerStyle.Alert);
			alertController.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, alert => System.Diagnostics.Debug.Write("OK was selected")));
			this.PresentViewController(alertController, true, null);
		}
	}

	public enum NrPlayer { First, Second, Third, Fourth };
}