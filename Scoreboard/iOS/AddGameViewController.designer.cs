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
    [Register ("AddGameViewController")]
    partial class AddGameViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIBarButtonItem barButtonCancel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIBarButtonItem barButtonSave { get; set; }

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
        UIKit.UILabel lblNameTeam1Player1 { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblNameTeam1Player2 { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblNameTeam2Player1 { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblNameTeam2Player2 { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblRankTeam1Player1 { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblRankTeam1Player2 { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblRankTeam2Player1 { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblRankTeam2Player2 { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView team1Player1View { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView team1Player2View { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView team2Player1View { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView team2Player2View { get; set; }

        [Action ("BarButtonCancel_Activated:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void BarButtonCancel_Activated (UIKit.UIBarButtonItem sender);

        [Action ("BarButtonSave_Activated:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void BarButtonSave_Activated (UIKit.UIBarButtonItem sender);

        void ReleaseDesignerOutlets ()
        {
            if (barButtonCancel != null) {
                barButtonCancel.Dispose ();
                barButtonCancel = null;
            }

            if (barButtonSave != null) {
                barButtonSave.Dispose ();
                barButtonSave = null;
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

            if (lblNameTeam1Player1 != null) {
                lblNameTeam1Player1.Dispose ();
                lblNameTeam1Player1 = null;
            }

            if (lblNameTeam1Player2 != null) {
                lblNameTeam1Player2.Dispose ();
                lblNameTeam1Player2 = null;
            }

            if (lblNameTeam2Player1 != null) {
                lblNameTeam2Player1.Dispose ();
                lblNameTeam2Player1 = null;
            }

            if (lblNameTeam2Player2 != null) {
                lblNameTeam2Player2.Dispose ();
                lblNameTeam2Player2 = null;
            }

            if (lblRankTeam1Player1 != null) {
                lblRankTeam1Player1.Dispose ();
                lblRankTeam1Player1 = null;
            }

            if (lblRankTeam1Player2 != null) {
                lblRankTeam1Player2.Dispose ();
                lblRankTeam1Player2 = null;
            }

            if (lblRankTeam2Player1 != null) {
                lblRankTeam2Player1.Dispose ();
                lblRankTeam2Player1 = null;
            }

            if (lblRankTeam2Player2 != null) {
                lblRankTeam2Player2.Dispose ();
                lblRankTeam2Player2 = null;
            }

            if (team1Player1View != null) {
                team1Player1View.Dispose ();
                team1Player1View = null;
            }

            if (team1Player2View != null) {
                team1Player2View.Dispose ();
                team1Player2View = null;
            }

            if (team2Player1View != null) {
                team2Player1View.Dispose ();
                team2Player1View = null;
            }

            if (team2Player2View != null) {
                team2Player2View.Dispose ();
                team2Player2View = null;
            }
        }
    }
}