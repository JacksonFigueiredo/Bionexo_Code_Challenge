using DispatchingModule.Data;
using DispatchingModule.Model;
using DispatchingModule.Model.TO;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Timers;

namespace DispatchingModule.Business
{
    public class DispatchingService
    {
        public void Start()
        {
            var runningtimer = new Timer();
            runningtimer.Interval = 15000;
            runningtimer.Elapsed += new System.Timers.ElapsedEventHandler(timer1_Tick);
            runningtimer.Enabled = true;
            Console.WriteLine("Bionexo Data Gathering and Transformation Started");
        }
        private void timer1_Tick(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine("Preparing to store data in the Database 1 in 15 seconds.");
            StoreDataOnBase(); // Collecting actual information and saving in first database.        
            FowardDataToTheApi(GetDataAndManipulate()); // Picking data on the first database, manipulating and then sending to the endpoint.
        }

        public void Stop()
        {
            Console.WriteLine("Service Has Been Stopped");
        }

        // This Method Will save computer name and local datetime on the 1ST database.
        // This guy will be running on every 15 seconds.
        public static void StoreDataOnBase()
        {
            using (var db = new SqlContext())
            {
                Capture capture = new Capture();
                capture.ComputerName = System.Environment.MachineName.ToString();
                capture.CaptureDate = DateTime.Now;
                db.Add<Capture>(capture);
                db.SaveChanges();
            }
        }

        private async void FowardDataToTheApi(IList<Capture> capture)
        {
            /* Async Method to foward the JSON over API, but first we need to make the entity to be normalized 
             * Selecting only fields that json are expecting, after this we keep sending the data.
             * */
            try
            {
                CaptureTO captureprocessed = new CaptureTO();
                foreach (Capture capturados in capture)
                {
                    captureprocessed.CaptureDate = capturados.CaptureDate;
                    captureprocessed.ComputerName = capturados.ComputerName;
                }

                string endPoint = "http://localhost:54144/api/Computer/v1";

                using (var httpClient = new HttpClient())
                {
                    var serializedObject = JsonConvert.SerializeObject(captureprocessed);
                    var content = new StringContent(serializedObject, Encoding.UTF8, "application/json");
                    await httpClient.PostAsync(endPoint, content);
                }
            }
            catch (Exception exp)
            {
                Console.WriteLine("Something Went Wrong : " + exp.InnerException.ToString());
            }
        }

        public IList<Capture> GetDataAndManipulate()
        {
            IList<Capture> datalist = new List<Capture>(); // using an Ilist to save and foward the data.

            using (SqlContext db = new SqlContext())
            {
                datalist = db.Capture.Select(c => new Capture() { CaptureDate = c.CaptureDate, ComputerName = c.ComputerName }).ToList();
            }
            return datalist;
        }

    }
}
