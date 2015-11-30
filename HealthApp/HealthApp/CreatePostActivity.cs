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
    [Activity(Label = "CreatePostActivity")]
    public class CreatePostActivity : Activity
    {
        IList<string> persistentData;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            persistentData = Intent.GetStringArrayListExtra("persistent data");
            SetContentView(Resource.Layout.AddPost);
            Button addPost = FindViewById<Button>(Resource.Id.addPostButton);

            addPost.Click += AddPost_Click;
            // Create your application here
        }

        private void AddPost_Click(object sender, EventArgs e)
        {
            EditText postContent = FindViewById<EditText>(Resource.Id.postContentText);
            Core myCore = new Core(persistentData);
            List<string> recieved = new List<string>();
            List<string> data = new List<string>();
            data.Add("10");
            data.Add(persistentData[1]);
            data.Add(postContent.Text);
            data.Add(DateTime.Now.ToString("yyyy-MM-dd"));
            data.Add(DateTime.Now.ToString("HH:mm:ss"));
            if (postContent.Text == null || postContent.Text == "Type your query...")
                postErrorMessage("Query cannot be blank");
            else
            recieved = myCore.messageHandler(data);
            if (recieved[1] == "1")
                postQueryMessage("Your question has been posted");
            else
            {
                recieved = myCore.messageHandler(data);
                if (recieved[1] == "1")
                    postQueryMessage("Your question has been posted");
                else
                    postErrorMessage("There was an error in your request. Please try again.");
            }
        }
        private void postQueryMessage(string message)
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
        private void postErrorMessage(string message)
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