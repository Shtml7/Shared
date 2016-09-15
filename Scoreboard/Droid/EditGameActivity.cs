using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Scoreboard.domain;
using Newtonsoft.Json;
using Scoreboard.communicator;

namespace Scoreboard.Droid
{
    [Activity(Label = "EditGameActivity")]
    public class EditGameActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Game);

            string jsonMyObject = Intent.GetStringExtra("game");

            Game game = JsonConvert.DeserializeObject<Game>(jsonMyObject);

            FindViewById<EditText>(Resource.Id.scoreTeam1).Text = game.team1.score + "";
            FindViewById<EditText>(Resource.Id.scoreTeam2).Text = game.team2.score + "";

            Button saveEditGameBtn = FindViewById<Button>(Resource.Id.saveScoreBtn);
            saveEditGameBtn.Click += async (object sender, EventArgs e) =>
            {
                await GameCall.updateGame(game);
                var activity = new Intent(this, typeof(GameActivity));
                activity.PutExtra("game", jsonMyObject);
                StartActivity(activity);
            };
        }
    }
}