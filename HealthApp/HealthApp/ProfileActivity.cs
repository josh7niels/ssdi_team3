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
    [Activity(Label = "ProfileActivity")]
    public class ProfileActivity : Activity
    {
        IList<string> persistentData;
        protected override void OnCreate(Bundle bundle)
        {

            base.OnCreate(bundle);
            persistentData = Intent.GetStringArrayListExtra("persistent data");
            SetContentView(Resource.Layout.ProfilePage);
            TextView welcome = FindViewById<TextView>(Resource.Id.welcomeView);
            Button schedAppt = FindViewById<Button>(Resource.Id.schedAppt);
            Button viewAppt = FindViewById<Button>(Resource.Id.viewAppt);
            welcome.Text = ("Welcome " + persistentData[2].Split(' ').First());
            // Create your application here
            schedAppt.Click += SchedAppt_Click;
            viewAppt.Click += ViewAppt_Click;
        }

        private void ViewAppt_Click(object sender, EventArgs e)
        {
            List<string> recieved = new List<string>();
            List<string> data = new List<string>();
            Core myCore = new Core(persistentData);
            data.Add("02");
            data.Add(persistentData[1]);
            recieved = myCore.messageHandler(data);
            Intent myIntent = new Intent(this, typeof(ViewAppointments));
            myIntent.PutStringArrayListExtra("appointments list", recieved);
            myIntent.PutStringArrayListExtra("persistent data", persistentData);
            StartActivity(myIntent);
        }

        private void SchedAppt_Click(object sender, EventArgs e)
        {
            List<string> recieved = new List<string>();
            List<string> data = new List<string>();
            Core myCore = new Core(persistentData);
            data.Add("04");
            recieved = myCore.messageHandler(data);
            Intent myIntent = new Intent(this, typeof(ScheduleAppointment));
            myIntent.PutStringArrayListExtra("doctor list", recieved);
            myIntent.PutStringArrayListExtra("persistent data", persistentData);
            StartActivity(myIntent);
        }
    }
}