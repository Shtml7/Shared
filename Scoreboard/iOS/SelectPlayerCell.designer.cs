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
    [Register ("SelectPlayerCell")]
    partial class SelectPlayerCell
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView imgProfile { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblPlayerRank { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblPlayerUsername { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (imgProfile != null) {
                imgProfile.Dispose ();
                imgProfile = null;
            }

            if (lblPlayerRank != null) {
                lblPlayerRank.Dispose ();
                lblPlayerRank = null;
            }

            if (lblPlayerUsername != null) {
                lblPlayerUsername.Dispose ();
                lblPlayerUsername = null;
            }
        }
    }
}