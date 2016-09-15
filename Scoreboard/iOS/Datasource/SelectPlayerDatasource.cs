using System;
using System.Collections.Generic;
using Foundation;
using UIKit;

namespace Scoreboard.iOS
{
	public class SelectPlayerDatasource: UITableViewSource
	{
		String cellIdentifier = "PlayerCell";

		List<User> users;
		SelectPlayerViewController owner;
		Dictionary<User, UIImage> userDict;


		public SelectPlayerDatasource(List<User> users, SelectPlayerViewController owner)
		{
			this.users = users;
			this.owner = owner;
			userDict = new Dictionary<User, UIImage>();
		}

		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			SelectPlayerCell cell = tableView.DequeueReusableCell(cellIdentifier) as SelectPlayerCell;
			User user = users[indexPath.Row];
			if (cell == null)
			{
				cell = new SelectPlayerCell();
			}

			UIImage image;
			if (userDict.ContainsKey(user))
			{
				image = userDict[user];
			}
			else 
			{
				image = IOSImageUtil.FromUrl(user.imageUrl);
			}

			cell.ImageView.Image = image;
			cell.LblPlayerUsername.Text = user.username;
			cell.LblPlayerRank.Text = "Wins: " + user.wins + "  Losses " + user.losses;

			return cell;
		}

		public override nint RowsInSection(UITableView tableview, nint section)
		{
			if (users == null)
			{
				return 0;
			}

			return users.Count;
		}

		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			base.RowSelected(tableView, indexPath);
			User selectedUser = users[indexPath.Row];
			owner.didSelectPlayer(selectedUser, userDict[selectedUser]);

		}
	}
}
