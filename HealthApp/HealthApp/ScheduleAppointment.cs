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
    [Activity(Label = "ScheduleAppointment")]
    public class ScheduleAppointment : Activity
    {
        IList<string> persistentData, doctorList;
        List<string> recieved, dateList, timeList;
        Spinner doctorSpinner, dateSpinner, timeSpinner;
        Button schedApptButton;
        string selectedDoctor, selectedDate, selectedTime;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.ScheduleAppointment);
            persistentData = Intent.GetStringArrayListExtra("persistent data");
            doctorList = Intent.GetStringArrayListExtra("doctor list");
            doctorSpinner = FindViewById<Spinner>(Resource.Id.doctorSelector);
            dateSpinner = FindViewById<Spinner>(Resource.Id.dateSelector);
            timeSpinner = FindViewById<Spinner>(Resource.Id.timeSelector);
            schedApptButton = FindViewById<Button>(Resource.Id.schedButton);
            doctorList.RemoveAt(0);
            //doctor
            doctorSpinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(doctor_ItemSelected);
            ArrayAdapter<string> doctorAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, doctorList);
            doctorAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            doctorSpinner.Adapter = doctorAdapter;
            //date
            dateSpinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(date_ItemSelected);
            //time
            timeSpinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(time_ItemSelected);
            schedApptButton.Click += SchedApptButton_Click;
        }

        private void SchedApptButton_Click(object sender, EventArgs e)
        {
            recieved = new List<string>();
            List<string> data = new List<string>();
            Core myCore = new Core(persistentData);
            data.Add("07");
            data.Add(selectedDoctor);
            data.Add(persistentData[1]);
            data.Add(selectedDate);
            data.Add(selectedTime);
            recieved = myCore.messageHandler(data);
            if (recieved[1] == "1")
                scheduleMessage("Appointment has been scheduled");
            else
                scheduleMessage("There was an error in scheduling please try again");
        }
        private void scheduleMessage(string message)
        {
            AlertDialog.Builder alert = new AlertDialog.Builder(this);
            alert.SetTitle(message);
            alert.SetPositiveButton("Ok", (senderAlert, args) =>
            {
                Intent intent = new Intent(this, typeof(ProfileActivity));
                intent.PutStringArrayListExtra("persistent data", persistentData);
                StartActivity(intent);
            });
            RunOnUiThread(() =>
            {
                alert.Show();
            });
        }
        private void dateHelper()
        {
            //doctorSpinner.Visibility = ViewStates.Invisible;
            ArrayAdapter<string> dateAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, recieved);
            dateAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerItem);
            dateSpinner.Adapter = dateAdapter;
        }
        private void timeHelper()
        {
            //dateSpinner.Visibility = ViewStates.Invisible;
            ArrayAdapter<string> timeAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, timeList);
            timeAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerItem);
            timeSpinner.Adapter = timeAdapter;
        }
        private void doctor_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            selectedDoctor = doctorList[e.Position];
            recieved = new List<string>();
            List<string> data = new List<string>();
            Core myCore = new Core(persistentData);
            data.Add("05");
            data.Add(selectedDoctor);
            recieved = myCore.messageHandler(data);
            recieved.RemoveAt(0);
            dateList = recieved;
            dateSpinner.Visibility = ViewStates.Visible;
            dateHelper();
        }
        private void date_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            selectedDate = dateList[e.Position];
            recieved = new List<string>();
            List<string> data = new List<string>();
            Core myCore = new Core(persistentData);
            data.Add("06");
            data.Add(selectedDoctor);
            data.Add(selectedDate);
            recieved = myCore.messageHandler(data);
            recieved.RemoveAt(0);
            timeList = recieved;
            timeSpinner.Visibility = ViewStates.Visible;
            timeHelper();
        }
        private void time_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            selectedTime = timeList[e.Position];
            schedApptButton.Visibility = ViewStates.Visible;
            schedApptButton.Enabled = true;
        }
    }
}