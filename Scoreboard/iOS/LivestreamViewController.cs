using Foundation;
using System;
using UIKit;

namespace Scoreboard.iOS
{
    public partial class LivestreamViewController : UIViewController
    {
		VideoChatView _videoChatView;

		// *** Fill the following variables using your own Project info from the Dashboard  ***
		// *** https://dashboard.tokbox.com/projects  
		const string _apiKey = "45686252";
		const string _sessionId = @"1_MX40NTY4NjI1Mn5-MTQ3Mzg2NzM5OTAwM35ORHlGeG5nQ2EzcnNpcWRxaUNEQ296bVh-fg";
		const string _token = @"T1==cGFydG5lcl9pZD00NTY4NjI1MiZzaWc9M2U0MjNmOGRiNzc5OGNhNGNmZDNiMmE0Mjk1YzU2ZjU2ZmExMWMwNTpzZXNzaW9uX2lkPTFfTVg0ME5UWTROakkxTW41LU1UUTNNemcyTnpNNU9UQXdNMzVPUkhsR2VHNW5RMkV6Y25OcGNXUnhhVU5FUTI5NmJWaC1mZyZjcmVhdGVfdGltZT0xNDc0MDIwMTY3Jm5vbmNlPTAuMjU3NzE2MjQxNTI1NDg2MSZyb2xlPXB1Ymxpc2hlciZleHBpcmVfdGltZT0xNDc2NjEyMTY3";


        public LivestreamViewController (IntPtr handle) : base (handle)
        {
        }

		public LivestreamViewController() { }

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			// Perform any additional setup after loading the view, typically from a nib.

			// Configure the Video Chat View
			_videoChatView = new VideoChatView()
			{
				Frame = View.Frame,
				ApiKey = _apiKey,
				SessionId = _sessionId,
				Token = _token,
				SubscribeToSelf = false
			};

			// Add The View
			View.AddSubview(_videoChatView);

			// Subscribe to Events
			_videoChatView.OnHangup += (sender, e) =>
				{
					System.Diagnostics.Debug.WriteLine("OnHangup: User tapped the hangup button.");
					this.DismissViewController(true, null);
				};

			_videoChatView.OnError += (sender, e) =>
				{
					System.Diagnostics.Debug.WriteLine(e.Message);

					this.ShowAlert(e.Message);
				};

			// Connect to Session
			_videoChatView.Connect();
		}

		private void ShowAlert(string message)
		{
			var alert = new UIAlertView("Alert", message, null, "Ok", null);

			alert.Show();
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.		
		}
    }
}