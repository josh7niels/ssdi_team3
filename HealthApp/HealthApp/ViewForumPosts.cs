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
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            postsList = Intent.GetStringArrayListExtra("appointments list");
            persistentData = Intent.GetStringArrayListExtra("persistent data");
            // Create your application here

            adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, postsList);
            ListView.Adapter = adapter;
        }
        protected override void OnListItemClick(ListView l, View v, int position, long id)
        {
            base.OnListItemClick(l, v, position, id);
        }
    }
}