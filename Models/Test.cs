using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace finalProjectAPI.Models
{
    public class Test
    {
        public DateTime checkIn { get; set; }
        public DateTime checkOut { get; set; }
        public int countryId { get; set; }

        public Test(DateTime start, DateTime end, int id)
        {
            this.checkIn = start;
            this.checkOut = end;
            this.countryId = id;
        }
    }
}