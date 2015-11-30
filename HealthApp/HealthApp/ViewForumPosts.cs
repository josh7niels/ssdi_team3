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
    [Activity(Label = "ViewForumPosts")]
    public class ViewForumPosts : ListActivity
    {
        ArrayAdapter adapter;
        IList<string> postsList, persistentData;
        List<string> contentDate = new List<string>();
        List<string> postID = new List<string>();
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            postsList = Intent.GetStringArrayListExtra("posts list");
            persistentData = Intent.GetStringArrayListExtra("persistent data");
            string[] myArray;
            for (int i = 1; i < postsList.Count; i++)
            {
                myArray = postsList[i].Split(',');
                contentDate.Add(myArray[1] + ", " + myArray[2]);
                postID.Add(myArray[0]);
            }

            adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, contentDate);
            ListView.Adapter = adapter;
        }
        protected override void OnListItemClick(ListView l, View v, int position, long id)
        {
            base.OnListItemClick(l, v, position, id);
            List<string> recieved = new List<string>();
            List<string> data = new List<string>();
            List<string> postID = new List<string>();
            Core myCore = new Core(persistentData);
            data.Add("09");
            data.Add(postID[position]);
            recieved = myCore.messageHandler(data);
            postID.Add(postID[position]);
            Intent myIntent = new Intent(this, typeof(ViewPostDetail));
            myIntent.PutStringArrayListExtra("replies list", recieved);
            myIntent.PutStringArrayListExtra("persistent data", persistentData);
            myIntent.PutStringArrayListExtra("post id", postID);
            StartActivity(myIntent);
        }
    }
}