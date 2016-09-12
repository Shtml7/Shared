using Android.App;
using Android.Graphics;
using Android.Net;
using Android.Views;
using Android.Widget;
using Scoreboard;
using Scoreboard.domain;
using Scoreboard.Droid;
using System.Collections.Generic;

public class GameAdapter : BaseAdapter<Game>
{
    List<Game> games;
    Activity context;
    public GameAdapter(Activity context, List<Game> games) : base()
    {
        this.context = context;
        this.games = games;
    }
    public override long GetItemId(int position)
    {
        return position;
    }
    public override Game this[int position]
    {
        get { return games[position]; }
    }
    public override int Count
    {
        get { return games.Count; }
    }
    public override View GetView(int position, View convertView, ViewGroup parent)
    {
        View view = convertView; // re-use an existing view, if one is available
        if (view == null) // otherwise create a new one
            view = context.LayoutInflater.Inflate(Resource.Layout.listAdapter, null);
        view.FindViewById<TextView>(Resource.Id.rowTextView).Text = games[position].team1.score + ":" + games[position].team2.score;
        Uri imagePlayer1 = Uri.Parse(games[position].team1.player1.imageUrl);
        Uri imagePlayer2 = Uri.Parse(games[position].team1.player2.imageUrl);
        Uri imagePlayer3 = Uri.Parse(games[position].team2.player1.imageUrl);
        Uri imagePlayer4 = Uri.Parse(games[position].team2.player2.imageUrl);
        view.FindViewById<ImageView>(Resource.Id.rowImageView1).SetImageURI(imagePlayer1);
        view.FindViewById<ImageView>(Resource.Id.rowImageView2).SetImageURI(imagePlayer2);
        view.FindViewById<ImageView>(Resource.Id.rowImageView3).SetImageURI(imagePlayer3);
        view.FindViewById<ImageView>(Resource.Id.rowImageView4).SetImageURI(imagePlayer4);
        return view;
    }

   
}