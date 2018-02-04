using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Bluetooth;
using Android.Bluetooth.LE;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace project3
{
    /// <summary>
    /// This is the callback class 
    /// that receives the data from the surrounding bluetooth active devices
    /// </summary>
    class CallBackClass : ScanCallback
    {
        public CallBackClass() { }

        public override void OnScanResult(ScanCallbackType callbackType, ScanResult result)
        {
            DbConnection db = new DbConnection();
            BluetoothDevice device = result.Device;
            Console.WriteLine(result);
            //fetches the data from the sensor
            var data = result.ScanRecord.ServiceData;
            foreach (var item in data)
            {
                var bytes = item.Value;
                //this would hold the temperatures
                if (bytes.Length == 4)
                {
                    var myShort = (short)(bytes[1] << 8 | bytes[0]);
                    float temp = (float)(myShort / 100.0);
                    float temp_f = (float)(temp * 9.0 / 5 + 32);
                    db.insertData(temp_f.ToString(), DateTime.Now.ToString("h:mm:ss tt"));
                    MainActivity.deviceName.Text = "Device Name: "+ result.ScanRecord.DeviceName;
                    MainActivity.currentTemperature.Text = "Current Temperature: "+ temp_f.ToString();
                }
                if (bytes.Length == 1) {
                    Random rnd = new Random();
                    var tempValue = System.Convert.ToInt32(bytes[0]);
                    if (tempValue > 40)
                    {
                        int hoursLeft = rnd.Next(1, 5);
                        MainActivity.timeRemaining.Text = "Time Remaining: " + System.DateTime.Now.AddHours(hoursLeft);
                    }
                    else
                    {
                        int hoursLeft = rnd.Next(5, 12);
                        MainActivity.timeRemaining.Text = "Time Remaining: " + System.DateTime.Now.AddHours(hoursLeft);
                    }
                    MainActivity.batteryLife.Text = "Battery Life: "+tempValue.ToString();
                }
            }
        }
    }
}