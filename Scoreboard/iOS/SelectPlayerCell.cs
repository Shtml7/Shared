using Foundation;
using System;
using UIKit;

namespace Scoreboard.iOS
{
	/**
	 * Custom cell for displaying other users
	*/
    public partial class SelectPlayerCell : UITableViewCell
    {
        public UIImageView ImgProfile
		{
			get
			{
				return imgProfile;
			}

			set
			{
				this.imgProfile = value;
			}
		}

		public UILabel LblPlayerUsername
		{
			get
			{
				return lblPlayerUsername;
			}

			set
			{
				this.lblPlayerUsername = value;
			}
		}

		public UILabel LblPlayerRank
		{
			get
			{
				return lblPlayerRank;
			}

			set
			{
				this.lblPlayerRank = value;
			}
		}


		public SelectPlayerCell(IntPtr handle) : base(handle)
		{

		}

		public SelectPlayerCell() { }
    }
}