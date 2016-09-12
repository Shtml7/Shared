using System;
using System.Collections.Generic;
using Foundation;
using UIKit;
using Scoreboard.domain;

namespace Scoreboard.iOS
{
	public class TableViewSource : UITableViewSource
	{
		List<Game> games;
		string cellIdentifier = "Cell";

		public TableViewSource(List<Game> games)
		{
			this.games = games;
		}

		public override nint RowsInSection(UITableView tableview, nint section)
		{
			return games.Count;
		}

		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			GameTableViewCell cell = tableView.DequeueReusableCell(cellIdentifier) as GameTableViewCell;
			Game game = games[indexPath.Row];

			if (cell == null)
			{
				cell = new GameTableViewCell();
			}

			cell.LblTeam1Score.Text = game.team1.score.ToString();
			cell.LblTeam2Score.Text = game.team2.score.ToString();


			cell.ImgTeam1Player1.Image = IOSImageUtil.FromUrl(game.team1.player1.imageUrl);
			cell.ImgTeam1Player2.Image = IOSImageUtil.FromUrl(game.team1.player2.imageUrl);
			cell.ImgTeam2Player1.Image = IOSImageUtil.FromUrl(game.team2.player1.imageUrl);
			cell.ImgTeam2Player2.Image = IOSImageUtil.FromUrl(game.team2.player2.imageUrl);

			makeRoundImageView(cell.ImgTeam1Player1);
			makeRoundImageView(cell.ImgTeam1Player2);
			makeRoundImageView(cell.ImgTeam2Player1);
			makeRoundImageView(cell.ImgTeam2Player2);

			return cell;
		}

		private void makeRoundImageView(UIImageView imageView)
		{
			try
			{
				double min = Math.Min(imageView.Bounds.Width, imageView.Bounds.Height);
				imageView.Layer.CornerRadius = (float)(min / 2.0);
				imageView.Layer.MasksToBounds = false;
				imageView.ClipsToBounds = true;
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine("Unable to create circle image: " + ex);
			}
		}


	}
}

