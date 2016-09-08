using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Scoreboard;
using Android.Graphics.Drawables;

namespace Scoreboard.Droid
{
	[Activity (Label = "Scoreboard.Droid", MainLauncher = true)]
	public class MainActivity : Activity
	{
        static String[] FRUITS = new String[] { "Apple", "Avocado", "Banana",
			"Blueberry", "Coconut", "Durian", "Guava", "Kiwifruit",
			"Jackfruit", "Mango", "Olive", "Pear", "Sugar-apple" };
        EditText userInput;

        protected async override void OnCreate(Bundle bundle)
        {
            MyClass clas = new MyClass();
            UserCall call = new UserCall();
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Our code will go here
            //EditText text = FindViewById<EditText>(Resource.Id.text);
            User u = await call.getUser();
            //text.Text = u.name;

            ListView games = FindViewById<ListView>(Resource.Id.games);
            ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Resource.Layout.listAdapter, FRUITS);
            // Assign adapter to ListView
            games.Adapter = adapter;

            games.ItemClick += listView_ItemClick;


            Button gamesBtn = FindViewById<Button>(Resource.Id.button1);
            Button leaderboardBtn = FindViewById<Button>(Resource.Id.button2);
            Button profileBtn = FindViewById<Button>(Resource.Id.button3);

            gamesBtn.SetBackgroundResource(Resource.Color.green);
            gamesBtn.Click += (object sender, EventArgs e) =>
            {
                leaderboardBtn.SetBackgroundResource(Resource.Color.white);
                gamesBtn.SetBackgroundResource(Resource.Color.green);
                profileBtn.SetBackgroundResource(Resource.Color.white);
                games.Visibility = ViewStates.Visible;
            };

            
            leaderboardBtn.Click += (object sender, EventArgs e) =>
            {
                leaderboardBtn.SetBackgroundResource(Resource.Color.green);
                gamesBtn.SetBackgroundResource(Resource.Color.white);
                profileBtn.SetBackgroundResource(Resource.Color.white);
                games.Visibility = ViewStates.Invisible;
            };

            profileBtn.Click += (object sender, EventArgs e) =>
            {
                profileBtn.SetBackgroundResource(Resource.Color.green);
                gamesBtn.SetBackgroundResource(Resource.Color.white);
                leaderboardBtn.SetBackgroundResource(Resource.Color.white);
                games.Visibility = ViewStates.Invisible;
            };

            LayoutInflater li = LayoutInflater.From(this);
            View promptsView = li.Inflate(Resource.Layout.dialog, null);

            AlertDialog.Builder alertDialogBuilder = new AlertDialog.Builder(this);

            // set prompts.xml to alertdialog builder
            alertDialogBuilder.SetView(promptsView);
            alertDialogBuilder.SetTitle("Username");

            userInput = (EditText)promptsView.FindViewById(Resource.Id.dialogText);

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
            string fruit = FRUITS[e.Position];

            //Make a toast with the item name just to show it was clicked
            Toast.MakeText(this, fruit + " Clicked!", ToastLength.Short).Show();
        }

        void HandleTouchUpInside(object sender, EventArgs ea)
        {
            Toast.MakeText(this, userInput.Text + " Clicked!", ToastLength.Short).Show();
            User user = new User();
            user.name = userInput.Text;

            UserCall call = new UserCall();
            call.addUser(user);
        }
    }
}


