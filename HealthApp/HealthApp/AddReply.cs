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
        IList<string> persistentData, postID, query;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.AddReply);
            persistentData = Intent.GetStringArrayListExtra("persistent data");
            postID = Intent.GetStringArrayListExtra("post id");
            query = Intent.GetStringArrayListExtra("query text");
            TextView replyText = FindViewById<TextView>(Resource.Id.replyText);
            string a = query[0];
            replyText.Text = ("Add Reply: " + query[0]);
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
            data.Add(persistentData[1]);
            data.Add(replyContent.Text);
            data.Add(DateTime.Now.ToString("yyyy-MM-dd"));
            data.Add(DateTime.Now.ToString("HH:mm:ss"));
            if (replyContent.Text == null || replyContent.Text == "please enter your response...")
                replyErrorMessage("Query cannot be blank");
            else
            {
                recieved = myCore.messageHandler(data);
                if (recieved[1] == "1")
                    replyQueryMessage("Your reply has been posted");
                else
                    replyErrorMessage("There was an error in your request. Please try again.");
            }

        }
        private void replyQueryMessage(string message)
        {
            AlertDialog.Builder alert = new AlertDialog.Builder(this);
            alert.SetTitle(message);
            alert.SetPositiveButton("Ok", (senderAlert, args) =>
            {
                Intent intent = new Intent(this, typeof(ForumActivity));
                intent.PutStringArrayListExtra("persistent data", persistentData);
                StartActivity(intent);
            });
            RunOnUiThread(() =>
            {
                alert.Show();
            });
        }
        private void replyErrorMessage(string message)
        {
            AlertDialog.Builder alert = new AlertDialog.Builder(this);
            alert.SetTitle(message);
            alert.SetPositiveButton("Ok", (senderAlert, args) =>
            {
            });
            RunOnUiThread(() =>
            {
                alert.Show();
            });
        }
    }
}