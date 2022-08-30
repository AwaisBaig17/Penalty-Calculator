using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace finalProjectAPI.Models
{
    public class Country
    {
        public int countryID { get; set; }
        public string countryName { get; set; }
        public string countryCurrency { get; set;}
        public string currencyCode { get; set; }
        public string countryWeekend { get; set; }
        public int countryTax { get; set; }
        public int countryPenaltyRate { get; set; }

    }
}