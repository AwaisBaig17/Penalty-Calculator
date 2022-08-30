using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace finalProjectAPI.Models
{
    public class Penalty
    {
       public int businessDays {get; set;}
       public string penaltyAmount { get; set; }

       public Penalty()
       {

       }
       public Penalty(int days, string amount)
       {
           this.businessDays = days;
           this.penaltyAmount = amount;
       }
    }
}