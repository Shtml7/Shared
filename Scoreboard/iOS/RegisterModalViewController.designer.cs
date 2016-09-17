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
    [Register ("RegisterModalViewController")]
    partial class RegisterModalViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton button { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblWarning { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView profileImage { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField tfUsername { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.NSLayoutConstraint topImageConstraint { get; set; }

        [Action ("Button_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void Button_TouchUpInside (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (button != null) {
                button.Dispose ();
                button = null;
            }

            if (lblWarning != null) {
                lblWarning.Dispose ();
                lblWarning = null;
            }

            if (profileImage != null) {
                profileImage.Dispose ();
                profileImage = null;
            }

            if (tfUsername != null) {
                tfUsername.Dispose ();
                tfUsername = null;
            }

            if (topImageConstraint != null) {
                topImageConstraint.Dispose ();
                topImageConstraint = null;
            }
        }
    }
}