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

namespace project3
{
    /// <summary>
    /// This class is responsible for displaying the past records
    /// in a listview
    /// </summary>
    [Activity(Label = "History")]
    public class History : Activity
    {
        DbConnection dbConnection = new DbConnection();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.history);
            
            Button time = FindViewById<Button>(Resource.Id.time);
            Button graph = FindViewById<Button>(Resource.Id.graph);
            time.Click += goToTime;
            graph.Click += goToGraph;
            //get the list of temperatures from the database
            getTempList();
        }

        /// <summary>
        /// This fetches the past temperture records from the database
        /// and assigns it to the list view in the history layout
        /// </summary>
        private void getTempList()
        {
            ListView listView1 = FindViewById<ListView>(Resource.Id.tempList);
            ArrayAdapter<string> arrayAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleExpandableListItem1, dbConnection.retrieveData());
            listView1.Adapter = arrayAdapter;
        }

        /// <summary>
        /// This function helps in navigating to the graph page
        /// </summary>
        /// <param name="sender">sender of the event</param>
        /// <param name="e">any args of the sender</param>
        private void goToGraph(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(GraphApp));
            StartActivity(intent);
        }

        /// <summary>
        /// This function helps in navigating to the timeremaining page
        /// </summary>
        /// <param name="sender">sender of the event</param>
        /// <param name="e">any args of the sender</param>
        private void goToTime(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(MainActivity));
            StartActivity(intent);
        }
    }
}