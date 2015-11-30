using System;
using System.IO;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;

namespace HealthApp
{
    [Activity(Label = "HealthApp", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        string ip = "10.38.43.245";
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            //declare buttom variable and call button handler when clicked
            Button loginButton = FindViewById<Button>(Resource.Id.loginButton);
            loginButton.Click += Login_Click;
        }
        private void Login_Click(object sender, EventArgs e)
        {
            //declare variables for getting text from UI
            EditText usernameText = FindViewById<EditText>(Resource.Id.usernameText);
            EditText passwordText = FindViewById<EditText>(Resource.Id.passwordText);
            //EditText ipText = FindViewById<EditText>(Resource.Id.ipText);
            List<string> persistentData = new List<string>();
            persistentData.Add(ip);
            persistentData.Add(usernameText.Text);
            /*if (usernameText.Text == null)
                LoginFailedAlert("Username cannot be blank");
            if (usernameText.Text == null)
                LoginFailedAlert("Password cannot be blank");*/
            Core myCore = new Core(persistentData);
            List<string> recieved = new List<string>();
            List<string> data = new List<string>();
            data.Add("01");
            data.Add(usernameText.Text);
            data.Add(passwordText.Text);
            recieved = myCore.messageHandler(data);
            persistentData.Add(recieved[2]);
            //create new intent to start profile activity
            Intent myIntent = new Intent(this, typeof(ProfileActivity));
            myIntent.PutStringArrayListExtra("persistent data", persistentData);
            //Start profile activity is correct login data otherwise display incorrect username/password alert
            if (recieved[1] == "1")
            {
                StartActivity(myIntent);
            }
            else
            {
                LoginFailedAlert("Incorrect username or password. Please try again");
            }
        }
        private void LoginFailedAlert(string failMessage)
        {
            //build incorrect username/password alert and display when called
            AlertDialog.Builder alert = new AlertDialog.Builder(this);
            alert.SetTitle(failMessage);
            alert.SetPositiveButton("Ok", (senderAlert, args) =>
            {
                OnCreate(null);
            });
            RunOnUiThread(() =>
            {
                alert.Show();
            });
        }
    }
}

