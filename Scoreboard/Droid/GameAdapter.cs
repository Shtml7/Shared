using Android.App;
using Android.Graphics;
using Android.Net;
using Android.Views;
using Android.Widget;
using Scoreboard;
using Scoreboard.domain;
using Scoreboard.Droid;
using System.Collections.Generic;
using System.Net;

public class GameAdapter : BaseAdapter<Game>
{
    List<Game> games;
    Activity context;
    public GameAdapter(Activity context, List<Game> games) : base()
    {
        this.context = context;
        this.games = games;
    }

    /**
     * get a item id from a item
     */
    public override long GetItemId(int position)
    {
        return position;
    }

    /**
     * get a game from the position
     */
    public override Game this[int position]
    {
        get { return games[position]; }
    }

    /**
     * Count the games
     */
    public override int Count
    {
        get { return games.Count; }
    }

    /**
     * Create a view with the games
     */
    public override View GetView(int position, View convertView, ViewGroup parent)
    {
        View view = convertView; // re-use an existing view, if one is available
        if (view == null) // otherwise create a new one
            view = context.LayoutInflater.Inflate(Resource.Layout.listAdapter, null);
        view.FindViewById<TextView>(Resource.Id.rowTextView).Text = games[position].team1.score + ":" + games[position].team2.score;
        view.FindViewById<ImageView>(Resource.Id.rowImageView1).SetImageBitmap(GetImageBitmapFromUrl(games[position].team1.player1.imageUrl));
        view.FindViewById<ImageView>(Resource.Id.rowImageView2).SetImageBitmap(GetImageBitmapFromUrl(games[position].team1.player2.imageUrl));
        view.FindViewById<ImageView>(Resource.Id.rowImageView3).SetImageBitmap(GetImageBitmapFromUrl(games[position].team2.player1.imageUrl));
        view.FindViewById<ImageView>(Resource.Id.rowImageView4).SetImageBitmap(GetImageBitmapFromUrl(games[position].team2.player2.imageUrl));
        return view;
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