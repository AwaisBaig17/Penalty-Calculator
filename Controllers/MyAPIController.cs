using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using finalProjectAPI.Models;
using finalProjectAPI.Data_Layer;

namespace finalProjectAPI.Controllers
{
    public class MyAPIController : ApiController
    {
        //This function gets the list of countries...
        [Route("GetCountries")]
        public List<Country> Get()
        {
            DataProvider myData = new DataProvider();
            var data = myData.GetCountryList();
            return data;
        }
        [Route("Calculate")]
        [HttpPost]
        //This function returns the object of penalty having the information of business days and penalty amout;
        public Penalty CalculatePenalty([FromBody]Test obj)
        {
            DataProvider myData = new DataProvider();
            var checkin = obj.checkIn;
            var checkout = obj.checkOut;
            var countryId = obj.countryId;
            var data = myData.CalculateAmount(checkin, checkout, countryId);
            return data;
        }
    }
}
