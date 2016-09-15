using System;
using System.Collections.Generic;
using Scoreboard.communicator;
using Scoreboard.domain;
using UIKit;

namespace Scoreboard.iOS
{
    public partial class GameDetailViewController : UIViewController
    {
		public Game game;
		public bool isOwnerOfTheGame;

		private List<UIImageView> imageViews;
		private UIBarButtonItem barButtonEdit;

        public GameDetailViewController (IntPtr handle) : base (handle)
        {
		}

		public async override void ViewDidLoad()
		{
			barButtonEdit = new UIBarButtonItem("Edit", UIBarButtonItemStyle.Plain, (sender, e) => editScore());
			if (isOwnerOfTheGame && game.isActive)
			{
				this.NavigationItem.SetRightBarButtonItem(barButtonEdit, true);
			}


			lblTeam1Score.Text = game.team1.score.ToString();
			lblTeam2Score.Text = game.team2.score.ToString();


			lblTeam2Player1.Text = game.team2.player1.username;
			lblTeam2Player2.Text = game.team2.player2.username;
			lblTeam1Player2.Text = game.team1.player2.username;
			lblTeam1Player1.Text = game.team1.player1.username;

			var team1Player1Image = await IOSImageUtil.FromUrl(game.team1.player1.imageUrl);
			var team1Player2Image = await IOSImageUtil.FromUrl(game.team1.player2.imageUrl);
			var team2Player1Image = await IOSImageUtil.FromUrl(game.team2.player1.imageUrl);
			var team2Player2Image = await IOSImageUtil.FromUrl(game.team2.player2.imageUrl);


			imageViews = new List<UIImageView>();
			imageViews.Add(imgTeam1Player1);
			imageViews.Add(imgTeam1Player2);
			imageViews.Add(imgTeam2Player1);
			imageViews.Add(imgTeam2Player2);
			imageViews.Add(imgTeam1Player1Detail);
			imageViews.Add(imgTeam1Player2Detail);
			imageViews.Add(imgTeam2Player1Detail);
			imageViews.Add(imgTeam2Player2Detail);
			
			imgTeam1Player1.Image = team1Player1Image;
			imgTeam1Player2.Image = team1Player2Image;
			imgTeam2Player1.Image = team2Player1Image;
			imgTeam2Player2.Image = team2Player2Image;

			imgTeam1Player1Detail.Image = team1Player1Image;
			imgTeam1Player2Detail.Image = team1Player2Image;
			imgTeam2Player1Detail.Image = team2Player1Image;
			imgTeam2Player2Detail.Image = team2Player2Image;
			System.Diagnostics.Debug.WriteLine("Height: " + scrollView.Bounds.Height + ", Width: " + scrollView.Bounds.Width);
			System.Diagnostics.Debug.WriteLine("Content Height: " + scrollView.ContentSize.Height + ", Content Width: " + scrollView.ContentSize.Width);

			foreach(var imageView in imageViews)
			{
				IOSImageUtil.makeRoundImageView(imageView);
			}
		}

		public override void ViewWillLayoutSubviews()
		{
			base.ViewWillLayoutSubviews();
			scrollView.ContentSize = new CoreGraphics.CGSize(View.Bounds.Width, View.Bounds.Height);
		}

		public void setScore(int scoreTeam1, int scoreTeam2)
		{
			lblTeam1Score.Text = scoreTeam1.ToString();
			lblTeam2Score.Text = scoreTeam2.ToString();
			if (!game.isActive)
			{
				barButtonEdit.Enabled = false;
			}
		}

		public void editScore()
		{
			PerformSegue("editScoreSegue", this); 
		}

		public override void PrepareForSegue(UIStoryboardSegue segue, Foundation.NSObject sender)
		{
			base.PrepareForSegue(segue, sender);
			if (segue.Identifier == "editScoreSegue")
			{
				var destinationViewController = (EditScoreViewController)segue.DestinationViewController;
				destinationViewController.parent = this;
				destinationViewController.currentGame = game;
			}
		}

	}
}