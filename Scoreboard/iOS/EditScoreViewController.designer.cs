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
    [Register ("EditScoreViewController")]
    partial class EditScoreViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIBarButtonItem barButtonCancel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblWarning { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField tfTeam1 { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField tfTeam2 { get; set; }

        [Action ("BarButtonCancel_Activated:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void BarButtonCancel_Activated (UIKit.UIBarButtonItem sender);

        [Action ("UIBarButtonItem1770_Activated:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void UIBarButtonItem1770_Activated (UIKit.UIBarButtonItem sender);

        void ReleaseDesignerOutlets ()
        {
            if (barButtonCancel != null) {
                barButtonCancel.Dispose ();
                barButtonCancel = null;
            }

            if (lblWarning != null) {
                lblWarning.Dispose ();
                lblWarning = null;
            }

            if (tfTeam1 != null) {
                tfTeam1.Dispose ();
                tfTeam1 = null;
            }

            if (tfTeam2 != null) {
                tfTeam2.Dispose ();
                tfTeam2 = null;
            }
        }
    }
}