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
using Android.Graphics;
using System.Net;
using Scoreboard.communicator;

namespace Scoreboard.Droid
{
    [Activity(Label = "NewGameActivity")]
    public class NewGameActivity : Activity
    {
        List<User> users;
        int clicked = 0;
        Game game;

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.NewGame);

            // Get all the users from the server
            users = await UserCall.getUsers();

            //Set click to imageView to set user
            ImageView newTeam1Player1Image = FindViewById<ImageView>(Resource.Id.newTeam1Player1Image);
            newTeam1Player1Image.SetImageResource(Resource.Mipmap.noImage);
            newTeam1Player1Image.Click += (object sender, EventArgs e) =>
            {
                clicked = 1;
                createAlert();
            };

            //Set click to imageView to set user
            ImageView newTeam1Player2Image = FindViewById<ImageView>(Resource.Id.newTeam1Player2Image);
            newTeam1Player2Image.SetImageResource(Resource.Mipmap.noImage);
            newTeam1Player2Image.Click += (object sender, EventArgs e) =>
            {
                clicked = 2;
                createAlert();
            };

            //Set click to imageView to set user
            ImageView newTeam2Player1Image = FindViewById<ImageView>(Resource.Id.newTeam2Player1Image);
            newTeam2Player1Image.SetImageResource(Resource.Mipmap.noImage);
            newTeam2Player1Image.Click += (object sender, EventArgs e) =>
            {
                clicked = 3;
                createAlert();
            };

            //Set click to imageView to set user
            ImageView newTeam2Player2Image = FindViewById<ImageView>(Resource.Id.newTeam2Player2Image);
            newTeam2Player2Image.SetImageResource(Resource.Mipmap.noImage);
            newTeam2Player2Image.Click += (object sender, EventArgs e) =>
            {
                clicked = 4;
                createAlert();
            };

            //Create a new game
            game = new Game();
            game.team1 = new Team();
            game.team2 = new Team();


            Button createGame = FindViewById<Button>(Resource.Id.createGameBtn);
            createGame.Click += async (object sender, EventArgs e) =>
            {
                if (game.team1.player1 != null && game.team2.player1 != null)
                {
                    //Create a new game
                    await GameCall.createGame(game);
                    //Open main activity
                    var activity = new Intent(this, typeof(MainActivity));
                    StartActivity(activity);
                }
            };
         }

        /**
         * Create Alert with all users
         */
        public void createAlert()
        {
            LayoutInflater li = LayoutInflater.From(this);
            View promptsView = li.Inflate(Resource.Layout.users, null);
            AlertDialog.Builder alertDialogBuilder = new AlertDialog.Builder(this);

            // set prompts.xml to alertdialog builder
            alertDialogBuilder.SetView(promptsView);
            alertDialogBuilder.SetTitle("Select User");

            ListView listView = (ListView)promptsView.FindViewById(Resource.Id.users);
            listView.Adapter = new UserAdapter(this, users);

            //Set click handler
            listView.ItemClick += listView_ItemClick;
            
            // create alert dialog
            AlertDialog alertDialog = alertDialogBuilder.Create();

            // show it
            alertDialog.Show();
        }

        /**
         * Create clickhandler to click a user
         */
        private void listView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            //Get our item from the list adapter
            User user = users[e.Position];

            //Make a toast with the item name just to show it was clicked
            Toast.MakeText(this, user.username + " Clicked!", ToastLength.Short).Show();

            //Switch on which user is clicked
            switch (clicked)
            {
                case 1:
                    game.team1.player1 = user;
                    FindViewById<ImageView>(Resource.Id.newTeam1Player1Image).SetImageBitmap(GetImageBitmapFromUrl(user.imageUrl));
                    FindViewById<TextView>(Resource.Id.newTeam1Player1Text).Text = user.username;
                    break;
                case 2:
                    game.team1.player2 = user;
                    FindViewById<ImageView>(Resource.Id.newTeam1Player2Image).SetImageBitmap(GetImageBitmapFromUrl(user.imageUrl));
                    FindViewById<TextView>(Resource.Id.newTeam1Player2Text).Text = user.username;
                    break;
                case 3:
                    game.team2.player1 = user;
                    FindViewById<ImageView>(Resource.Id.newTeam2Player1Image).SetImageBitmap(GetImageBitmapFromUrl(user.imageUrl));
                    FindViewById<TextView>(Resource.Id.newTeam2Player1Text).Text = user.username;
                    break;
                case 4:
                    game.team2.player2 = user;
                    FindViewById<ImageView>(Resource.Id.newTeam2Player2Image).SetImageBitmap(GetImageBitmapFromUrl(user.imageUrl));
                    FindViewById<TextView>(Resource.Id.newTeam2Player2Text).Text = user.username;
                    break;
                default:
                    break;
            }
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

}