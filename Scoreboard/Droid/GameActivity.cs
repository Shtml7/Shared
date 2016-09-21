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
    string jsonMyObject;

    protected override void OnCreate(Bundle bundle)
    {
        base.OnCreate(bundle);

        // Set our view from the "main" layout resource
        SetContentView(Resource.Layout.Game);

        //Get the game which is send from the other view
        jsonMyObject = Intent.GetStringExtra("game");

        //Deserialize String to Game
        Game game = JsonConvert.DeserializeObject<Game>(jsonMyObject);
        
        Toast.MakeText(this, game.id + " Clicked!", ToastLength.Short).Show();

        //Get the images from the users
        team1Player1 = GetImageBitmapFromUrl(game.team1.player1.imageUrl);
        team1Player2 = GetImageBitmapFromUrl(game.team1.player2.imageUrl);
        team2Player1 = GetImageBitmapFromUrl(game.team2.player1.imageUrl);
        team2Player2 = GetImageBitmapFromUrl(game.team2.player2.imageUrl);

        //Set the images from the users
        FindViewById<TextView>(Resource.Id.gameTextView).Text = game.team1.score + ":" + game.team2.score;
        FindViewById<ImageView>(Resource.Id.gameImageView1).SetImageBitmap(team1Player1);
        FindViewById<ImageView>(Resource.Id.gameImageView2).SetImageBitmap(team1Player2);
        FindViewById<ImageView>(Resource.Id.gameImageView3).SetImageBitmap(team2Player1);
        FindViewById<ImageView>(Resource.Id.gameImageView4).SetImageBitmap(team2Player2);

        //Set items for team 1 player 1
        FindViewById<ImageView>(Resource.Id.team1Player1Image).SetImageBitmap(team1Player1);
        FindViewById<TextView>(Resource.Id.team1Player1Text).Text = game.team1.player1.username;

        //Set items for team 1 player 2
        FindViewById<ImageView>(Resource.Id.team1Player2Image).SetImageBitmap(team1Player2);
        FindViewById<TextView>(Resource.Id.team1Player2Text).Text = game.team1.player2.username;

        //Set items for team 2 player 1
        FindViewById<ImageView>(Resource.Id.team2Player1Image).SetImageBitmap(team2Player1);
        FindViewById<TextView>(Resource.Id.team2Player1Text).Text = game.team2.player1.username;

        //Set items for team 2 player 2
        FindViewById<ImageView>(Resource.Id.team2Player2Image).SetImageBitmap(team2Player2);
        FindViewById<TextView>(Resource.Id.team2Player2Text).Text = game.team2.player2.username;

        //Add a clickhandler to the livestream image
        ImageView livestream = FindViewById<ImageView>(Resource.Id.liveStreamImg);
        livestream.Click += (object sender, EventArgs e) =>
        {
            //Open livestream activity
            var activity = new Intent(this, typeof(LiveStreamActivity));
            StartActivity(activity);
        };
        livestream.SetImageResource(Resource.Mipmap.Play);

        //Add a clickhandler to edit a game
        Button editGameBtn = FindViewById<Button>(Resource.Id.editGameBtn);
        editGameBtn.Click += (object sender, EventArgs e) =>
        {
            //Open edit game activity
            var activity = new Intent(this, typeof(EditGameActivity));
            activity.PutExtra("game", jsonMyObject);
            StartActivity(activity);
        };
    }

    /**
     * Creates a bitmap from a url
     */
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