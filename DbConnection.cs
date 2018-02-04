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
using SQLite;
using SQLitePCL;
using System.IO;
namespace project3
{
    /// <summary>
    /// This class implements all the db operations
    /// creates a db on the device
    /// performs insert and retrieve operation
    /// </summary>
    class DbConnection
    {
        //set the path and name of the database on the device
        static private string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Temperatures.db3");
        
        /// <summary>
        /// This function returns the db path location to the classes who want
        /// to perform db operations
        /// </summary>
        /// <returns>string path of the database</returns>
        public string getDbPath() {
            return dbPath;
        }

        /// <summary>
        /// This function inserts the data into to the db
        /// </summary>
        /// <param name="temp">the current temperature</param>
        /// <param name="time">current time at which the temperature was recorded</param>
        public void insertData(string temp, string time) {
            var db = new SQLiteConnection(dbPath);
            db.CreateTable<TemperatureDataClass>();
            db.Insert(new TemperatureDataClass(temp, time));
        }

        /// <summary>
        /// This function retieves the data from the data base
        /// </summary>
        /// <returns>List<string> it returns a list of temperatures and their
        /// corresponding time</returns>
        public List<string> retrieveData()
        {
            var db = new SQLiteConnection(dbPath);
            var table = db.Table<TemperatureDataClass>();
            List<string> tableContent = new List<string>();
            if(table!=null)
                foreach (var item in table) {
                    tableContent.Add(item.time + " " + item.temperature);
                }
            return tableContent;
        }
    }
}