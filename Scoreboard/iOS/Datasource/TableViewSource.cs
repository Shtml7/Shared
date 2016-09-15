using System;
using System.Collections.Generic;
using Foundation;
using UIKit;
using Scoreboard.domain;
using System.Threading.Tasks;

namespace Scoreboard.iOS
{
	public class TableViewSource : UITableViewSource
	{
		List<Game> games;
		GamesViewController owner;
		string cellIdentifier = "Cell";

		public TableViewSource(List<Game> games, GamesViewController owner)
		{
			this.games = games;
			this.owner = owner;
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

			var ImgTeam1Player1 = IOSImageUtil.OldFromUrl(game.team1.player1.imageUrl);
			var ImgTeam1Player2 = IOSImageUtil.OldFromUrl(game.team1.player2.imageUrl);
			var ImgTeam2Player1 = IOSImageUtil.OldFromUrl(game.team2.player1.imageUrl);
			var ImgTeam2Player2 = IOSImageUtil.OldFromUrl(game.team2.player2.imageUrl);

			cell.ImgTeam1Player1.Image = ImgTeam1Player1;
			cell.ImgTeam1Player2.Image = ImgTeam1Player2;
			cell.ImgTeam2Player1.Image = ImgTeam2Player1;
			cell.ImgTeam2Player2.Image = ImgTeam2Player2;

			IOSImageUtil.makeRoundImageView(cell.ImgTeam1Player1);
			IOSImageUtil.makeRoundImageView(cell.ImgTeam1Player2);
			IOSImageUtil.makeRoundImageView(cell.ImgTeam2Player1);
			IOSImageUtil.makeRoundImageView(cell.ImgTeam2Player2);

			owner.imgTeam1Player1 = ImgTeam1Player1;
			owner.imgTeam1Player2 = ImgTeam1Player2;
			owner.imgTeam2Player1 = ImgTeam2Player1;
			owner.imgTeam2Player2 = ImgTeam2Player2;

			return cell;
		}

		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{ 
			System.Diagnostics.Debug.WriteLine("Did select row: " + games[indexPath.Row].id);
			owner.selectedGame = games[indexPath.Row];
			owner.goToDetailViewController();
		}

	}
}

