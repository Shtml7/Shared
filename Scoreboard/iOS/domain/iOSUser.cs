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
	}
}
