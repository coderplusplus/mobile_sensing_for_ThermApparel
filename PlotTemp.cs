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
    [Activity(Label = "PlotTemp")]
    public class PlotTemp : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.GraphAppLayout);
            // Create your application here
            Button time = FindViewById<Button>(Resource.Id.time);
            Button history = FindViewById<Button>(Resource.Id.history);

            time.Click += goToTime;
            history.Click += goToHistory;

        }
        private void goToHistory(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(History));

            StartActivity(intent);
        }

        private void goToTime(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(MainActivity));

            StartActivity(intent);
        }
    }
}
