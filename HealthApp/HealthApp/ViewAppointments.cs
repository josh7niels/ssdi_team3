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
    [Activity(Label = "ViewAppointments")]
    public class ViewAppointments : ListActivity
    {
        ArrayAdapter adapter;
        IList<string> apptList, persistentData;
        List<string> dateCause = new List<string>();
        List<string> docTime = new List<string>();
        List<string> apptID = new List<string>();
        List<string> date = new List<string>();
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            apptList = Intent.GetStringArrayListExtra("appointments list");
            persistentData = Intent.GetStringArrayListExtra("persistent data");
            string[] myArray;
            for(int i=1; i<apptList.Count; i++)
            {
                myArray = apptList[i].Split(',');
                date.Add(myArray[2]);
                apptID.Add(myArray[0]);
                dateCause.Add(myArray[2] + ", " + myArray[4]);
                docTime.Add(myArray[1] + ", " + myArray[3]);
            }
            adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, dateCause);
            ListView.Adapter = adapter;
        }
        protected override void OnListItemClick(ListView l, View v, int position, long id)
        {
            bool delete = false;
            base.OnListItemClick(l, v, position, id);
            if (DateTime.Now.Date < DateTime.Parse(date[position]))
            {
                string alertMessage = "Are you sure you want to delete this appointment?";
                DeleteConfirmAlert(alertMessage, position);
            }
            else
            {
                string alertMessage = "This appointment has already occured and cannot be deleted!";
                CannotDeleteAlert(alertMessage);
            }
            if(delete)
            {
                Core myCore = new Core(persistentData);
                List<string> myList = new List<string>();
                List<string> recieved = new List<string>();
                myList.Add("03");
                myList.Add(apptID[position]);
                recieved = myCore.messageHandler(myList);
                deleteAppointmentEntry(position);
                if (recieved[1] == "1")
                    DeletedAlert("The appointment you selected has been deleted");
            }       
        }
        private void deleteAppointmentEntry(int index)
        {
            date.RemoveAt(index);
            dateCause.RemoveAt(index);
            docTime.RemoveAt(index);
            apptID.RemoveAt(index);
            adapter.NotifyDataSetChanged();
            DeletedAlert("The appointment you selected has been deleted");
            //ListView.RemoveViewAt(index);
        }
        private void DeleteConfirmAlert(string failMessage, int position)
        {
            //build incorrect username/password alert and display when called
            AlertDialog.Builder alert = new AlertDialog.Builder(this);
            alert.SetTitle("Confirm Deletion");
            alert.SetMessage(failMessage);
            alert.SetNegativeButton("No", (senderAlert, args) =>
             {
                 //do nothing, return to view
             });
            alert.SetPositiveButton("Yes", (senderAlert, args) =>
            {
                //delete appointment from database, return to view
                Core myCore = new Core(persistentData);
                List<string> myList = new List<string>();
                List<string> recieved = new List<string>();
                myList.Add("03");
                myList.Add(apptID[position]);
                recieved = myCore.messageHandler(myList);
                deleteAppointmentEntry(position);
            });
            RunOnUiThread(() =>
            {
                alert.Show();
            });
        }
        private void CannotDeleteAlert(string alertMessage)
        {
            AlertDialog.Builder alert = new AlertDialog.Builder(this);
            alert.SetTitle("Delete not allowed");
            alert.SetMessage(alertMessage);
            alert.SetPositiveButton("Ok", (senderAlert, args) =>
            {
                //do nothing, return to view
            });
            RunOnUiThread(() =>
            {
                alert.Show();
            });
        }
        private void DeletedAlert(string message)
        {
            AlertDialog.Builder alert = new AlertDialog.Builder(this);
            alert.SetTitle("Success!");
            alert.SetMessage(message);
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
    }
}