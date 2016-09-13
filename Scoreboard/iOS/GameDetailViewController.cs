using System;
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

        public GameDetailViewController (IntPtr handle) : base (handle)
        {
		}

		public override void ViewDidLoad()
		{
			imgTeam1Player1.Image = team1Player1Image;
			imgTeam1Player2.Image = team1Player2Image;
			imgTeam2Player1.Image = team2Player1Image;
			imgTeam2Player2.Image = team2Player2Image;

			System.Diagnostics.Debug.WriteLine("Height: " + scrollView.Bounds.Height + ", Width: " + scrollView.Bounds.Width);
;
			System.Diagnostics.Debug.WriteLine("Content Height: " + scrollView.ContentSize.Height + ", Content Width: " + scrollView.ContentSize.Width);
		}

		public override void ViewWillLayoutSubviews()
		{
			base.ViewWillLayoutSubviews();
			scrollView.ContentSize = new CoreGraphics.CGSize(View.Bounds.Width, View.Bounds.Height);
		}

	}
}