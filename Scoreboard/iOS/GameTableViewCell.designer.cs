// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace Scoreboard.iOS
{
    [Register ("GameTableViewCell")]
    partial class GameTableViewCell
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView imgIsOwner { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView imgTeam1Player1 { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView imgTeam1Player2 { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView imgTeam2Player1 { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView imgTeam2Player2 { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblTeam1Score { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblTeam2Score { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (imgIsOwner != null) {
                imgIsOwner.Dispose ();
                imgIsOwner = null;
            }

            if (imgTeam1Player1 != null) {
                imgTeam1Player1.Dispose ();
                imgTeam1Player1 = null;
            }

            if (imgTeam1Player2 != null) {
                imgTeam1Player2.Dispose ();
                imgTeam1Player2 = null;
            }

            if (imgTeam2Player1 != null) {
                imgTeam2Player1.Dispose ();
                imgTeam2Player1 = null;
            }

            if (imgTeam2Player2 != null) {
                imgTeam2Player2.Dispose ();
                imgTeam2Player2 = null;
            }

            if (lblTeam1Score != null) {
                lblTeam1Score.Dispose ();
                lblTeam1Score = null;
            }

            if (lblTeam2Score != null) {
                lblTeam2Score.Dispose ();
                lblTeam2Score = null;
            }
        }
    }
}