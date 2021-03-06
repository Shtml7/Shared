﻿using Android.App;
using Android.Graphics;
using Android.Net;
using Android.Views;
using Android.Widget;
using Scoreboard;
using Scoreboard.domain;
using Scoreboard.Droid;
using System.Collections.Generic;
using System.Net;

public class UserAdapter : BaseAdapter<User>
{
    List<User> users;
    Activity context;
    public UserAdapter(Activity context, List<User> users) : base()
    {
        this.context = context;
        this.users = users;
    }

    /**
    * get a item id from a item
    */
    public override long GetItemId(int position)
    {
        return position;
    }

    /**
     * get a user from the position
     */
    public override User this[int position]
    {
        get { return users[position]; }
    }

    /**
     * Count the users
     */
    public override int Count
    {
        get { return users.Count; }
    }

    /**
     * Create a view with the users
     */
    public override View GetView(int position, View convertView, ViewGroup parent)
    {
        View view = convertView; // re-use an existing view, if one is available
        if (view == null) // otherwise create a new one
            view = context.LayoutInflater.Inflate(Resource.Layout.UserAdapter, null);
        view.FindViewById<TextView>(Resource.Id.userRow).Text = users[position].username;
        return view;
    }
}