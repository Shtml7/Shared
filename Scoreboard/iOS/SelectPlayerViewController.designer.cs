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
    [Register ("SelectPlayerViewController")]
    partial class SelectPlayerViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIBarButtonItem barButtonCancel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITableView playersTableView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UISearchBar searchBar { get; set; }

        [Action ("BarButtonCancel_Activated:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void BarButtonCancel_Activated (UIKit.UIBarButtonItem sender);

        void ReleaseDesignerOutlets ()
        {
            if (barButtonCancel != null) {
                barButtonCancel.Dispose ();
                barButtonCancel = null;
            }

            if (playersTableView != null) {
                playersTableView.Dispose ();
                playersTableView = null;
            }

            if (searchBar != null) {
                searchBar.Dispose ();
                searchBar = null;
            }
        }
    }
}