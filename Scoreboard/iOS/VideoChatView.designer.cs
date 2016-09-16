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
    [Register ("VideoChatView")]
    partial class VideoChatView
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIBarButtonItem HangupButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView PublisherView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel StatusLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView SubscriberView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIBarButtonItem SwitchButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIToolbar ToolBar { get; set; }

        [Action ("HangupButton_Activated:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void HangupButton_Activated (UIKit.UIBarButtonItem sender);

        void ReleaseDesignerOutlets ()
        {
            if (HangupButton != null) {
                HangupButton.Dispose ();
                HangupButton = null;
            }

            if (PublisherView != null) {
                PublisherView.Dispose ();
                PublisherView = null;
            }

            if (StatusLabel != null) {
                StatusLabel.Dispose ();
                StatusLabel = null;
            }

            if (SubscriberView != null) {
                SubscriberView.Dispose ();
                SubscriberView = null;
            }

            if (SwitchButton != null) {
                SwitchButton.Dispose ();
                SwitchButton = null;
            }

            if (ToolBar != null) {
                ToolBar.Dispose ();
                ToolBar = null;
            }
        }
    }
}