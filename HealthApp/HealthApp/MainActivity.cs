using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace HealthApp
{
    [Activity(Label = "HealthApp", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        int count = 1;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            EditText usernameText = FindViewById<EditText>(Resource.Id.usernameText);
            EditText passwordText = FindViewById<EditText>(Resource.Id.passwordText);
            Button loginButton = FindViewById<Button>(Resource.Id.loginButton);

            loginButton.Click += (object sender, EventArgs e) =>
            {
                bool valid;
                Core myCore = new Core();
                valid = myCore.loginHandler(usernameText.Text, passwordText.Text);
                Intent myIntent = new Intent(this, typeof(ProfileActivity));
                if (valid)
                    StartActivity(myIntent);
                else
                {
                    string alertMessage = "Incorrect username or password. Please try again";
                    LoginFailed(alertMessage);
                }
                    

            };

        }
        private void LoginFailed(string failMessage)
        {
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

