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

namespace Scoreboard.Droid
{
	[Activity (Label = "Scoreboard.Droid", MainLauncher = true)]
	public class MainActivity : Activity
	{
        static String[] FRUITS = new String[] { "Apple", "Avocado", "Banana",
			"Blueberry", "Coconut", "Durian", "Guava", "Kiwifruit",
			"Jackfruit", "Mango", "Olive", "Pear", "Sugar-apple" };
        EditText userInput;
        ImageView imageView;
        Button imageBtn;
        Stream imageStream;

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
            string fruit = FRUITS[e.Position];

            //Make a toast with the item name just to show it was clicked
            Toast.MakeText(this, fruit + " Clicked!", ToastLength.Short).Show();
        }

        void HandleTouchUpInside(object sender, EventArgs ea)
        {
            User user = new User();
            user.name = userInput.Text;
            UserCall call = new UserCall();
            byte[] image = ReadFully(imageStream);
            call.UploadImage(image, "png", user);
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


