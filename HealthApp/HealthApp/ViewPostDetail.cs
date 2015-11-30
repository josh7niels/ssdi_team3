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
        IList<string> persistentData, repliesList, postID;
        List<string> nameDate = new List<string>();
        List<string> content = new List<string>();
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            persistentData = Intent.GetStringArrayListExtra("persistent data");
            repliesList = Intent.GetStringArrayListExtra("replies list");
            postID = Intent.GetStringArrayListExtra("post id");
            SetContentView(Resource.Layout.ViewPostDetail);
            Button addReply = FindViewById<Button>(Resource.Id.addReplyButton);
            string[] myArray;
            for (int i = 1; i < repliesList.Count; i++)
            {
                myArray = repliesList[i].Split(',');
                nameDate.Add(myArray[0] + ", " + myArray[2]);
                content.Add(myArray[1]);
            }
            addReply.Click += AddReply_Click;
            adapt = new ViewApptsAdapter(this, content, nameDate);
            listview = FindViewById<ListView>(Resource.Id.postDetailView);
            listview.Adapter = adapt;
            listview.ItemClick += Listview_ItemClick;
        }

        private void AddReply_Click(object sender, EventArgs e)
        {
            Intent myIntent = new Intent(this, typeof(AddReply));
            myIntent.PutStringArrayListExtra("persistent data", persistentData);
            myIntent.PutStringArrayListExtra("post id", postID);
            StartActivity(myIntent);
        }

        private void Listview_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}