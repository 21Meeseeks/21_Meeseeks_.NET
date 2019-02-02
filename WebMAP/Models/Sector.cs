using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebMAP.Models
{
    public class Sector
    {
        public string sector { get; set; }
        public int size { get; set; }

        public Sector(string sector, int size = 1)
        {
            this.sector = sector;
            this.size = size;
        }


        public Sector()
        {
        }

        public static implicit operator Service.ProjectStatService.Sector(Sector s)
        {
            Service.ProjectStatService.Sector sec = new Service.ProjectStatService.Sector();
            sec.sector = s.sector;
            sec.size = s.size;
            return sec;
        }
    }
}