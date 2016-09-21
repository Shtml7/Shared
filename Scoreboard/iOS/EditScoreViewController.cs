using Foundation;
using System;
using UIKit;
using Scoreboard.domain;
using Scoreboard.communicator;

namespace Scoreboard.iOS
{
	/**
	 * ViewController for editing the score 
	 */
    public partial class EditScoreViewController : UIViewController
    {
		public GameDetailViewController parent;
		public Game currentGame;

        public EditScoreViewController (IntPtr handle) : base (handle)
        {
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			tfTeam1.Text = currentGame.team1.score.ToString();
			tfTeam2.Text = currentGame.team2.score.ToString();
		}

		partial void BarButtonCancel_Activated(UIBarButtonItem sender)
		{
			DismissViewController(true, null);
		}

		//Checks if the score is valid and saves/updates the score
		partial void UIBarButtonItem1770_Activated(UIBarButtonItem sender)
		{
			try
			{
				int scoreTeam1 = int.Parse(tfTeam1.Text);
				int scoreTeam2 = int.Parse(tfTeam2.Text);

				if (scoreTeam1 > 10 || scoreTeam2 > 10)
				{
					throw new Exception();
				}
				updateScore(scoreTeam1, scoreTeam2);
				parent.setScore(scoreTeam1, scoreTeam2);
				DismissViewController(true, null);

			}
			catch (Exception ex)
			{
				lblWarning.Text = "Enter only numbers less than 10";
				System.Diagnostics.Debug.WriteLine("Could not parse score, or the score was greater than 10, EX:" + ex.Message);
			}
		}

		//Makes a call to update the score on the server
		public async void updateScore(int scoreTeam1, int scoreTeam2)
		{
			currentGame.team1.score = scoreTeam1;
			currentGame.team2.score = scoreTeam2;
			if (scoreTeam1 == 10 || scoreTeam2 == 10)
			{
				currentGame.isActive = false;
			}
			await GameCall.updateGame(currentGame);
		}
	}
}