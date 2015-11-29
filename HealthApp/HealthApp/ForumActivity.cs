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

namespace HealthApp
{
    [Activity(Label = "ForumActivity")]
    public class ForumActivity : Activity
    {
        IList<string> persistentData;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            persistentData = Intent.GetStringArrayListExtra("persistent data");
            SetContentView(Resource.Layout.ProfilePage);
            TextView welcome = FindViewById<TextView>(Resource.Id.forumView);
            Button newPost = FindViewById<Button>(Resource.Id.newPost);
            Button viewPosts = FindViewById<Button>(Resource.Id.viewPosts);
            newPost.Click += NewPost_Click;
            viewPosts.Click += ViewPosts_Click;
        }

        private void ViewPosts_Click(object sender, EventArgs e)
        {
            List<string> recieved = new List<string>();
            List<string> data = new List<string>();
            Core myCore = new Core(persistentData);
            data.Add("08");
            recieved = myCore.messageHandler(data);
            Intent myIntent = new Intent(this, typeof(ViewForumPosts));
            myIntent.PutStringArrayListExtra("posts list", recieved);
            myIntent.PutStringArrayListExtra("persistent data", persistentData);
            StartActivity(myIntent);
        }

        private void NewPost_Click(object sender, EventArgs e)
        {
            Intent myIntent = new Intent(this, typeof(CreatePostActivity));
            myIntent.PutStringArrayListExtra("persistent data", persistentData);
            StartActivity(myIntent);
        }
    }
}