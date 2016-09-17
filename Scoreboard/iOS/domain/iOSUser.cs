using System;
using UIKit;

namespace Scoreboard.iOS
{
	public class iOSUser : User
	{
		public UIImage image { get; set; }
		
		public iOSUser(UIImage image, User user) : base()
		{
			this.image = image;
			this.username = user.username;
			this.id = user.id;
			this.imageUrl = user.imageUrl;
			this.wins = user.wins;
			this.losses = user.losses;
		}

		public User toUser(iOSUser iOSUser)
		{
			User user = new User();
			user.id = iOSUser.id;
			user.username = iOSUser.username;
			user.imageUrl = iOSUser.imageUrl;
			user.wins = iOSUser.wins;
			user.losses = iOSUser.losses;
			return user;
		}
	}
}
