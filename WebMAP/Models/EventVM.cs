using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebMAP.Models
{
    public class EventVM
    {
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public DateTime start { get; set; }

        public DateTime end { get; set; }
        public string className { get; set; }

        public bool allDay { get; set; }

        public EventVM()
        {

        }

        public EventVM(DayoffVM dayoff)
        {

            this.id = dayoff.idLeave;
            this.title = dayoff.leaveType.name;
            this.description = dayoff.leaveType.description;
            this.start = dayoff.startDate.Value;
            this.end = dayoff.endDate.Value;
            this.allDay = true;
            switch (dayoff.leaveType.name)
            {
                default:
                    className = "m-fc-event--primary";
                    break;
            }
        }

        
    }
}