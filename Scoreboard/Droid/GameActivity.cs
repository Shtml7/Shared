using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Widget;
using Newtonsoft.Json;
using Scoreboard.domain;
using Scoreboard.Droid;
using System;
using System.Net;

[Activity(Label = "Scoreboard.Droid")]
public class GameActivity : Activity
{
    Bitmap team1Player1;
    Bitmap team1Player2;
    Bitmap team2Player1;
    Bitmap team2Player2;
    protected override void OnCreate(Bundle bundle)
    {
        base.OnCreate(bundle);

        // Set our view from the "main" layout resource
        SetContentView(Resource.Layout.Game);
        
        string jsonMyObject = Intent.GetStringExtra("game");
        
        Game game = JsonConvert.DeserializeObject<Game>(jsonMyObject);
        
        Toast.MakeText(this, game.id + " Clicked!", ToastLength.Short).Show();

        team1Player1 = GetImageBitmapFromUrl(game.team1.player1.imageUrl);
        team1Player2 = GetImageBitmapFromUrl(game.team1.player2.imageUrl);
        team2Player1 = GetImageBitmapFromUrl(game.team2.player1.imageUrl);
        team2Player2 = GetImageBitmapFromUrl(game.team2.player2.imageUrl);

        FindViewById<TextView>(Resource.Id.gameTextView).Text = game.team1.score + ":" + game.team2.score;
        FindViewById<ImageView>(Resource.Id.gameImageView1).SetImageBitmap(team1Player1);
        FindViewById<ImageView>(Resource.Id.gameImageView2).SetImageBitmap(team1Player2);
        FindViewById<ImageView>(Resource.Id.gameImageView3).SetImageBitmap(team2Player1);
        FindViewById<ImageView>(Resource.Id.gameImageView4).SetImageBitmap(team2Player2);

        FindViewById<ImageView>(Resource.Id.team1Player1Image).SetImageBitmap(team1Player1);
        FindViewById<TextView>(Resource.Id.team1Player1Text).Text = game.team1.player1.username;

        FindViewById<ImageView>(Resource.Id.team1Player2Image).SetImageBitmap(team1Player2);
        FindViewById<TextView>(Resource.Id.team1Player2Text).Text = game.team1.player2.username;

        FindViewById<ImageView>(Resource.Id.team2Player1Image).SetImageBitmap(team2Player1);
        FindViewById<TextView>(Resource.Id.team2Player1Text).Text = game.team2.player1.username;

        FindViewById<ImageView>(Resource.Id.team2Player2Image).SetImageBitmap(team2Player2);
        FindViewById<TextView>(Resource.Id.team2Player2Text).Text = game.team2.player2.username;
        Toast.MakeText(this, "Test", ToastLength.Short).Show();

        ImageView livestream = FindViewById<ImageView>(Resource.Id.liveStreamImg);
        livestream.Click += (object sender, EventArgs e) =>
        {
            var activity = new Intent(this, typeof(LiveStreamActivity));
            StartActivity(activity);
        };
        livestream.SetImageResource(Resource.Mipmap.Play);

        Button editGameBtn = FindViewById<Button>(Resource.Id.editGameBtn);
        editGameBtn.Click += (object sender, EventArgs e) =>
        {
            var activity = new Intent(this, typeof(EditGameActivity));
            activity.PutExtra("game", jsonMyObject);
            StartActivity(activity);
        };
    }

    private Bitmap GetImageBitmapFromUrl(string url)
    {
        Bitmap imageBitmap = null;

        using (var webClient = new WebClient())
        {
            var imageBytes = webClient.DownloadData(url);
            if (imageBytes != null && imageBytes.Length > 0)
            {
                imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
            }
        }

        return imageBitmap;
    }
}