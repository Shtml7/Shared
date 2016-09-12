using Android.App;
using Android.Views;
using Android.Widget;
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
        view.FindViewById<TextView>(Resource.Id.rowTextView).Text = games[position].id + "#";
        return view;
    }
}