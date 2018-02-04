using Android.App;
using Android.Widget;
using Android.OS;
using System;
using Android.Content;
using Android.Bluetooth;
using Android.Bluetooth.LE;
using Plugin.BLE;
using Plugin.BLE.Abstractions;
using System.Collections.Generic;
using Plugin.BLE.Android;
using Android.Runtime;
using Android;
using System.Threading.Tasks;
using Android.Locations;
using Android.Util;

namespace project3
{
    /// <summary>
    /// This is the entry point of the program
    /// It handles all the events of the time remaining(main) layout
    /// </summary>
    [Activity(Label = "project3", MainLauncher = true)]
    public class MainActivity : Activity
    {
        private List<Object> deviceList = new List<Object>();
        public static TextView deviceName;
        public static TextView currentTemperature;
        public static TextView batteryLife;
        public static TextView timeRemaining;

        /// <summary>
        /// This function is called each time a page is revisited
        /// </summary>
        /// <param name="savedInstanceState"></param>
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);

            Button history = FindViewById<Button>(Resource.Id.history);
            Button graph = FindViewById<Button>(Resource.Id.graph);
            deviceName = FindViewById<TextView>(Resource.Id.deviceName);
            currentTemperature = FindViewById<TextView>(Resource.Id.currentTemperature);
            batteryLife = FindViewById<TextView>(Resource.Id.batteryLife);
            timeRemaining = FindViewById<TextView>(Resource.Id.timeRemaining);
            history.Click += goToHistory;
            graph.Click += goToGraph;

            //requires location permission
            String permission = "android.permission.ACCESS_COARSE_LOCATION";
            var res = Android.App.Application.Context.CheckCallingOrSelfPermission(permission);
            if (!(res == Android.Content.PM.Permission.Granted))
            {
                Android.App.AlertDialog.Builder dialog = new AlertDialog.Builder(this);
                AlertDialog alert = dialog.Create();
                alert.SetTitle("Title");
                alert.SetMessage("Please enable the location for this App on your phone, and run again.");
                alert.SetButton("OK", (c, ev) =>
                {
                    alert.Dismiss();
                    this.FinishAffinity();
                });
                alert.SetButton2("CANCEL", (c, ev) => { });
                alert.Show();
            }
            //start the bluetooth connection
            Task task= Task.Run(() => { startConnectingBluetooth(); });      
        }
            
        /// <summary>
        /// This function switches the bluetooth on if it is not on
        /// It then uses the BLE scanner to scan the bluetooth active devices in
        /// the surrounding, and receives the data in the callback function provided
        /// </summary>
        public void startConnectingBluetooth()
        {
            BluetoothAdapter adapter = BluetoothAdapter.DefaultAdapter;
            var scanner = adapter.BluetoothLeScanner;
            if (adapter != null && !adapter.IsEnabled)
            {
                adapter.Enable();
            }
           
            var callback = new project3.CallBackClass();
            ScanSettings settings = new ScanSettings.Builder()
                .SetScanMode(Android.Bluetooth.LE.ScanMode.LowLatency)
                .SetReportDelay(0)
                .Build();
            scanner.StartScan(new List<ScanFilter>(), settings, callback);
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
        /// This function helps in navigating to the history page
        /// </summary>
        /// <param name="sender">sender of the event</param>
        /// <param name="e">any args of the sender</param>
        private void goToHistory(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(History));
            StartActivity(intent);
        }
    }
}