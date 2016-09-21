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
        string jsonMyObject;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.EditGame);

            //Get the game which is send from the other view
            jsonMyObject = Intent.GetStringExtra("game");

            //Deserialize String to Game
            Game game = JsonConvert.DeserializeObject<Game>(jsonMyObject);

            //Set the score of the teams
            FindViewById<EditText>(Resource.Id.scoreTeam1).Text = game.team1.score + "";
            FindViewById<EditText>(Resource.Id.scoreTeam2).Text = game.team2.score + "";

            //Set clickhandler on save button
            Button saveEditGameBtn = FindViewById<Button>(Resource.Id.saveScoreBtn);
            saveEditGameBtn.Click += async (object sender, EventArgs e) =>
            {
                try
                {
                    //Parse the string score to int
                    game.team1.score = int.Parse(FindViewById<EditText>(Resource.Id.scoreTeam1).Text);
                    game.team2.score = int.Parse(FindViewById<EditText>(Resource.Id.scoreTeam2).Text);

                    //Update the game with te new score
                    await GameCall.updateGame(game);

                    //Redirect to GameActivity with the new Game
                    var activity = new Intent(this, typeof(GameActivity));
                    activity.PutExtra("game", JsonConvert.SerializeObject(game));
                    StartActivity(activity);
                }
                catch (Exception ex)
                {
                    Toast.MakeText(this, ex.Message, ToastLength.Short).Show();
                }
        };
        }
    }
}