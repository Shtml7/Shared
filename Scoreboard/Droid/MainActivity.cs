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

            // Our code will go here
            //EditText text = FindViewById<EditText>(Resource.Id.text);
			User u = await UserCall.getUser();
            games = new List<Game>();
            games = await GameCall.GetAllGames();

            ListView listViewGames = FindViewById<ListView>(Resource.Id.games);
            
            // Assign adapter to ListView
            listViewGames.Adapter = new GameAdapter(this, games);

            listViewGames.ItemClick += listView_ItemClick;


            Button gamesBtn = FindViewById<Button>(Resource.Id.button1);
            Button leaderboardBtn = FindViewById<Button>(Resource.Id.button2);
            Button profileBtn = FindViewById<Button>(Resource.Id.button3);

            gamesBtn.SetBackgroundResource(Resource.Color.green);
            gamesBtn.Click += (object sender, EventArgs e) =>
            {
                leaderboardBtn.SetBackgroundResource(Resource.Color.white);
                gamesBtn.SetBackgroundResource(Resource.Color.green);
                profileBtn.SetBackgroundResource(Resource.Color.white);
                listViewGames.Visibility = ViewStates.Visible;
            };

            
            leaderboardBtn.Click += (object sender, EventArgs e) =>
            {
                leaderboardBtn.SetBackgroundResource(Resource.Color.green);
                gamesBtn.SetBackgroundResource(Resource.Color.white);
                profileBtn.SetBackgroundResource(Resource.Color.white);
                listViewGames.Visibility = ViewStates.Invisible;
            };

            profileBtn.Click += (object sender, EventArgs e) =>
            {
                profileBtn.SetBackgroundResource(Resource.Color.green);
                gamesBtn.SetBackgroundResource(Resource.Color.white);
                leaderboardBtn.SetBackgroundResource(Resource.Color.white);
                listViewGames.Visibility = ViewStates.Invisible;
            };

            LayoutInflater li = LayoutInflater.From(this);
            View promptsView = li.Inflate(Resource.Layout.dialog, null);

            AlertDialog.Builder alertDialogBuilder = new AlertDialog.Builder(this);

            // set prompts.xml to alertdialog builder
            alertDialogBuilder.SetView(promptsView);
            alertDialogBuilder.SetTitle("Username");

            userInput = (EditText)promptsView.FindViewById(Resource.Id.dialogText);
            imageView = (ImageView)promptsView.FindViewById(Resource.Id.imageView);
            imageBtn = (Button)promptsView.FindViewById(Resource.Id.imageBtn);

            imageBtn.Click += delegate {
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

        private void listView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            //Get our item from the list adapter
            Game game = games[e.Position];

            //Make a toast with the item name just to show it was clicked
            Toast.MakeText(this, game.id + " Clicked!", ToastLength.Short).Show();
        }

        void HandleTouchUpInside(object sender, EventArgs ea)
        {
            if (userInput.Text != "")
            {
                User user = new User();
                user.name = userInput.Text;
                byte[] image = ReadFully(imageStream);
                UserCall.UploadImage(image, "jpg", user);
            }
        }

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


