using Foundation;
using System;
using UIKit;

namespace Scoreboard.iOS
{
    public partial class GameTableViewCell : UITableViewCell
    {

		public UILabel LblTeam1Score
		{
			get
			{
				return lblTeam1Score;
			}

			set
			{
				this.lblTeam1Score = value;
			}
		}

		public UILabel LblTeam2Score
		{
			get
			{
				return lblTeam2Score;
			}

			set
			{
				this.lblTeam2Score = value;
			}
		}

		public UIImageView ImgTeam1Player1
		{
			get
			{
				return imgTeam1Player1;
			}

			set
			{
				this.imgTeam1Player1 = value;
			}
		}

		public UIImageView ImgTeam1Player2
		{
			get
			{
				return imgTeam1Player2;
			}

			set
			{
				this.imgTeam1Player2 = value;
			}
		}

		public UIImageView ImgTeam2Player1
		{
			get
			{
				return imgTeam2Player1;
			}

			set
			{
				this.imgTeam2Player1 = value;
			}
		}

		public UIImageView ImgTeam2Player2
		{
			get
			{
				return imgTeam2Player2;
			}

			set
			{
				this.imgTeam2Player2 = value;
			}
		}

		public UIImageView ImgIsOwner
		{
			get
			{
				return imgIsOwner;
			}

			set
			{
				this.imgIsOwner = value;
			}
		}

		public GameTableViewCell(IntPtr handle) : base(handle)
        {
        }

		public GameTableViewCell(){
		}

    }
}