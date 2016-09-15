using System;
using System.Collections.Generic;
using Scoreboard.domain;
using UIKit;

namespace Scoreboard.iOS
{
    public partial class GameDetailViewController : UIViewController
    {
		public Game game;
		public UIImage team1Player1Image;
		public UIImage team1Player2Image;
		public UIImage team2Player1Image;
		public UIImage team2Player2Image;

		private List<UIImageView> imageViews;

        public GameDetailViewController (IntPtr handle) : base (handle)
        {
		}

		public override void ViewDidLoad()
		{
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

			lblTeam2Player1.Text = game.team2.player1.username;
			lblTeam2Player2.Text = game.team2.player2.username;
			lblTeam1Player2.Text = game.team1.player2.username;
			lblTeam1Player1.Text = game.team1.player1.username;

			lblTeam1Score.Text = game.team1.score.ToString();
			lblTeam2Score.Text = game.team2.score.ToString();

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

	}
}