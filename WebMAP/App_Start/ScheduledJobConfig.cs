using Service;
using Service.RestApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Web;

namespace WebMAP
{
    public class ScheduledJobConfig
    {
        private static Timer timer = null;
        public static void Start()
        {
            timer = new Timer((new TimeSpan(0, 1, 0)).TotalMilliseconds);
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        private static void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                timer.Stop();
                //tratiement ...
                ApiFactory.AddArchiveTerm(DateTime.Now);
                System.Diagnostics.Debug.WriteLine("Passage dans timer elapsed...");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("exception timer elapsed:." + ex.Message);
            }
            finally
            {
                timer.Start();
            }
        }

        public static void Stop()
        {
            if (timer != null)
            {
                timer.Elapsed -= Timer_Elapsed;
                timer.Stop();
                timer = null;
            }
        }
    }
}