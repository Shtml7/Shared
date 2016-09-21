using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Scoreboard;
using Android.Graphics.Drawables;
using Java.IO;
using Android.Graphics;
using System.IO;
using Scoreboard.domain;
using Scoreboard.communicator;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Scoreboard.Droid
{
	[Activity (Label = "Scoreboard.Droid", MainLauncher = true)]
	public class MainActivity : Activity
	{
        List<Game> games;
        EditText userInput;
        ImageView imageView;
        Button imageBtn;
        Stream imageStream;

        protected async override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // get all the games
            games = new List<Game>();
            games = await GameCall.GetAllGames();

            ListView listViewGames = FindViewById<ListView>(Resource.Id.games);

            // Assign adapter to ListView
            listViewGames.Adapter = new GameAdapter(this, games);

            listViewGames.ItemClick += listView_ItemClick;

            //Get all the buttons
            Button gamesBtn = FindViewById<Button>(Resource.Id.button1);
            Button leaderboardBtn = FindViewById<Button>(Resource.Id.button2);
            Button profileBtn = FindViewById<Button>(Resource.Id.button3);
            Button newGameBtn = FindViewById<Button>(Resource.Id.newGameBtn);

            //Set clickhandler to button
            gamesBtn.SetBackgroundResource(Resource.Color.green);
            gamesBtn.Click += (object sender, EventArgs e) =>
            {
                //Select the games tab
                //Set the colors
                leaderboardBtn.SetBackgroundResource(Resource.Color.white);
                gamesBtn.SetBackgroundResource(Resource.Color.green);
                profileBtn.SetBackgroundResource(Resource.Color.white);
                listViewGames.Visibility = ViewStates.Visible;
            };

            //Set clickhandler to button
            leaderboardBtn.Click += (object sender, EventArgs e) =>
            {
                //Select the leaderboard tab
                //Set the colors
                leaderboardBtn.SetBackgroundResource(Resource.Color.green);
                gamesBtn.SetBackgroundResource(Resource.Color.white);
                profileBtn.SetBackgroundResource(Resource.Color.white);
                listViewGames.Visibility = ViewStates.Invisible;
            };

            //Set clickhandler to button
            profileBtn.Click += (object sender, EventArgs e) =>
            {
                //Select the profile tab
                //Set the colors
                profileBtn.SetBackgroundResource(Resource.Color.green);
                gamesBtn.SetBackgroundResource(Resource.Color.white);
                leaderboardBtn.SetBackgroundResource(Resource.Color.white);
                listViewGames.Visibility = ViewStates.Invisible;
            };

            //Set clickhandler to button
            newGameBtn.Click += (object sender, EventArgs e) =>
            {
                //Open the new game activity
                var activity = new Intent(this, typeof(NewGameActivity));
                StartActivity(activity);
            };

            LayoutInflater li = LayoutInflater.From(this);
            View promptsView = li.Inflate(Resource.Layout.dialog, null);

            //Create a Dialog to create a new user
            AlertDialog.Builder alertDialogBuilder = new AlertDialog.Builder(this);

            // set prompts.xml to alertdialog builder
            alertDialogBuilder.SetView(promptsView);
            alertDialogBuilder.SetTitle("Username");

            //Get the view items for the dialog
            userInput = (EditText)promptsView.FindViewById(Resource.Id.dialogText);
            imageView = (ImageView)promptsView.FindViewById(Resource.Id.imageView);
            imageBtn = (Button)promptsView.FindViewById(Resource.Id.imageBtn);

            //Add clickhandler to open the gallery
            imageBtn.Click += delegate
            {
                Intent = new Intent();
                Intent.SetType("image/*");
                Intent.SetAction(Intent.ActionGetContent);
                StartActivityForResult(Intent.CreateChooser(Intent, "Select Picture"), 1000);
            };

            // set dialog message
            alertDialogBuilder
                .SetCancelable(false)
                .SetPositiveButton("OK", HandleTouchUpInside);

            // create alert dialog
            AlertDialog alertDialog = alertDialogBuilder.Create();

            // show it
            alertDialog.Show();
        }
        
        /**
         * Create a item click handler to activate when clicked on a game
         */
        private void listView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            //Get our item from the list adapter
            Game game = games[e.Position];

            //Make a toast with the item name just to show it was clicked
            Toast.MakeText(this, game.id + " Clicked!", ToastLength.Short).Show();

            //Open game detail activity
            var activity2 = new Intent(this, typeof(GameActivity));
            activity2.PutExtra("game", JsonConvert.SerializeObject(game));
            StartActivity(activity2);
        }

        /**
         * Create a new user
         * Send post to server
         */
        async void HandleTouchUpInside(object sender, EventArgs ea)
        {
            if (userInput.Text != "")
            {
                User user = new User();
                user.username = userInput.Text;
                byte[] image = ReadFully(imageStream);
                await UserCall.createUser(image, "jpg", user);
            }
        }

        /**
         * Get the image which is selected in the gallery
         */
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            if ((requestCode == 1000) && (resultCode == Result.Ok) && (data != null))
            {
                Android.Net.Uri imageUri = data.Data;
                imageStream = ContentResolver.OpenInputStream(imageUri);
                imageView.SetImageURI(imageUri);
            }
        }
        
        /**
         * Get byte array from a image
         */
        public static byte[] ReadFully(Stream input)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }
        }
    }
}


