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
using Microcharts;
using Microcharts.Droid;
using SkiaSharp;

namespace project3
{
    /// <summary>
    /// This activity deals with the events of GraphAppLayout
    /// </summary>
    [Activity(Label = "GraphApp")]
    public class GraphApp : Activity
    {
        DbConnection dbConnection;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.GraphAppLayout);

            Button time = FindViewById<Button>(Resource.Id.time);
            Button history = FindViewById<Button>(Resource.Id.history);
            time.Click += goToTime;
            history.Click += goToHistory;

            //it fetches all the past records to be plotted
            dbConnection = new DbConnection();
            var values = dbConnection.retrieveData();

            Entry[] entries = new Entry[values.Count];

            //this creates entries to be plotted on the linear graph
            for(int i=0; i<entries.Length; i++)
            {
                var splitData = values[i].Split(' ');               
                entries[i] = new Entry(float.Parse(splitData[2]))
                {
                    Label = splitData[0],
                    ValueLabel = splitData[2]
                };
            }
            //create a line chart of the entries created
            var chart = new LineChart() { Entries = entries};
            
            //assign the chart to the chart view in the graph page
            var chartView = FindViewById<ChartView>(Resource.Id.chartView);
            chartView.Chart = chart;
        }

        /// <summary>
        /// This function helps in navigating to the history page
        /// </summary>
        /// <param name="sender">sender of the event</param>
        /// <param name="e">any args of the sender</param>
        private void goToHistory(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(History));
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