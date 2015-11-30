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
    [Activity(Label = "ViewPostDetail")]
    public class ViewPostDetail : Activity
    {
        BaseAdapter adapt;
        ListView listview;
        IList<string> persistentData, repliesList, postID, query;
        List<string> nameDate = new List<string>();
        List<string> content = new List<string>();
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            persistentData = Intent.GetStringArrayListExtra("persistent data");
            repliesList = Intent.GetStringArrayListExtra("replies list");
            postID = Intent.GetStringArrayListExtra("post id");
            query = Intent.GetStringArrayListExtra("query text");
            SetContentView(Resource.Layout.ViewPostDetail);
            TextView postText = FindViewById<TextView>(Resource.Id.PostText);
            Button addReply = FindViewById<Button>(Resource.Id.addReplyButton);
            postText.Text = (query[0]);
            string[] myArray;
            for (int i = 1; i < repliesList.Count; i++)
            {
                myArray = repliesList[i].Split(',');
                string date = myArray[2].Split(' ').First();
                nameDate.Add(myArray[0] + ", " + date);
                content.Add(myArray[1]);
            }
            addReply.Click += AddReply_Click;
            adapt = new ViewApptsAdapter(this, content, nameDate);
            listview = FindViewById<ListView>(Resource.Id.postDetailView);
            listview.Adapter = adapt;
        }

        private void AddReply_Click(object sender, EventArgs e)
        {
            Intent myIntent = new Intent(this, typeof(AddReply));
            myIntent.PutStringArrayListExtra("persistent data", persistentData);
            myIntent.PutStringArrayListExtra("post id", postID);
            myIntent.PutStringArrayListExtra("query text", query);
            StartActivity(myIntent);
        }
    }
}