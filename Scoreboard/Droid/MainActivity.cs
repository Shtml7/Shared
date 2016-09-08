using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Scoreboard;

namespace Scoreboard.Droid
{
	[Activity (Label = "Scoreboard.Droid", MainLauncher = true)]
	public class MainActivity : Activity
	{
        protected async override void OnCreate(Bundle bundle)
        {
            MyClass clas = new MyClass();
            UserCall call = new UserCall();
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Our code will go here
            EditText text = FindViewById<EditText>(Resource.Id.text);
            User u = await call.getUser();
            text.Text = u.name;
        }
    }
}


