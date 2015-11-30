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
    [Activity(Label = "AddReply")]
    public class AddReply : Activity
    {
        IList<string> persistentData, postID;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            persistentData = Intent.GetStringArrayListExtra("persistent data");
            postID = Intent.GetStringArrayListExtra("post id");
            SetContentView(Resource.Layout.AddReply);
            Button submitReply = FindViewById<Button>(Resource.Id.addReplyButton);

            submitReply.Click += SubmitReply_Click;
            // Create your application here
        }

        private void SubmitReply_Click(object sender, EventArgs e)
        {
            EditText replyContent = FindViewById<EditText>(Resource.Id.replyContentText);
            Core myCore = new Core(persistentData);
            List<string> recieved = new List<string>();
            List<string> data = new List<string>();
            data.Add("11");
            data.Add(postID[0]);
            data.Add(replyContent.Text);
            recieved = myCore.messageHandler(data);
            Intent myIntent = new Intent(this, typeof(ForumActivity));
            myIntent.PutStringArrayListExtra("persistent data", persistentData);
            StartActivity(myIntent);
        }
    }
}