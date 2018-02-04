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
    /// This class defines the table structure of the database
    /// </summary>
    class TemperatureDataClass
    {
        public string temperature { get; set; }
        public string time { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public TemperatureDataClass() { }

        /// <summary>
        /// Parameterized Constructor makes an instance with
        /// temperature and time values
        /// </summary>
        /// <param name="temperature">recorded temperature</param>
        /// <param name="time">recorded time</param>
        public TemperatureDataClass(string temperature, string time) {
            this.temperature = temperature;
            this.time = time;
        }

        /// <summary>
        /// This function overrides the ToString function of the object of this class
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return time+ " " + temperature;
        }
    }
}