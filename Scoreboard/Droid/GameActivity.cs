using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Widget;
using Newtonsoft.Json;
using Scoreboard.domain;
using Scoreboard.Droid;
using System.Net;

[Activity(Label = "Scoreboard.Droid")]
public class GameActivity : Activity
{
    protected override void OnCreate(Bundle bundle)
    {
        base.OnCreate(bundle);

        // Set our view from the "main" layout resource
        SetContentView(Resource.Layout.Game);
        
        string jsonMyObject = Intent.GetStringExtra("game");
        
        Game game = JsonConvert.DeserializeObject<Game>(jsonMyObject);
        
        Toast.MakeText(this, game.id + " Clicked!", ToastLength.Short).Show();

        FindViewById<TextView>(Resource.Id.gameTextView).Text = game.team1.score + ":" + game.team2.score;
        FindViewById<ImageView>(Resource.Id.gameImageView1).SetImageBitmap(GetImageBitmapFromUrl(game.team1.player1.imageUrl));
        FindViewById<ImageView>(Resource.Id.gameImageView2).SetImageBitmap(GetImageBitmapFromUrl(game.team1.player2.imageUrl));
        FindViewById<ImageView>(Resource.Id.gameImageView3).SetImageBitmap(GetImageBitmapFromUrl(game.team2.player1.imageUrl));
        FindViewById<ImageView>(Resource.Id.gameImageView4).SetImageBitmap(GetImageBitmapFromUrl(game.team2.player2.imageUrl));

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