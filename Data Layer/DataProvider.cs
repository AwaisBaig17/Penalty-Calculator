using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Data.SqlClient;
using finalProjectAPI.Models;

namespace finalProjectAPI.Data_Layer
{

    public class DataProvider
    {
        List<string> weekDays = new List<string>(){"Monday","Tuesday","Wednesday","Thursday","Friday","Saturday","Sunday"};
        string connectionString = "";

        public DataProvider(){
             connectionString = WebConfigurationManager.ConnectionStrings["MyDB4099"].ConnectionString;
        }

        //Function for getting the list of countries in order to populate the dropdown
        public List<Country> GetCountryList()
        {
            List<Country> myCountry = new List<Country>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("Select *from countryTable", con);

                con.Open();

                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    Country country = new Country();
                    country.countryID = Convert.ToInt32(sdr["countryID"]);
                    country.countryName = sdr["countryName"].ToString();
                    country.countryCurrency = sdr["countryCurrency"].ToString();
                    country.currencyCode = sdr["currencyCode"].ToString();
                    country.countryWeekend = sdr["countryWeekend"].ToString();
                    country.countryTax = Convert.ToInt32(sdr["countryTax"]);
                    country.countryPenaltyRate = Convert.ToInt32(sdr["countryPenaltyRate"]);
                    myCountry.Add(country);
                }
                con.Close();
            }
            return myCountry;
        }
        
        //Function for calculating the penalty amount returning an object of penalty including business days and amount of penalty;
        public Penalty CalculateAmount(DateTime initialDate, DateTime finalDate, int countryId)
        {
            Country myCountry = this.GetCountry(countryId);
            List<DateTime> myHolidays = this.GetHolidays(countryId);
            List<string> myWeekends = this.GetWeekened(weekDays, myCountry.countryWeekend);
            List<DateTime> dummyList = new List<DateTime>();
            double penaltyAmount = 0;
            double totalPenalty = 0;
            double taxtAmount = 0;
            int penaltyDays = 0;
            string penaltyString = "";
            int k = 0;
            int count=0;


            DateTime startDate = initialDate.Date;
            DateTime endDate = finalDate.Date;
            if (startDate > endDate)
            {
                throw new ArgumentException("Incorrect last day" + endDate);
            }

            TimeSpan span = endDate - startDate;
            int businessDays = span.Days + 1;

            //In the below loop I am comparing the dates converted into Days of Week with the List of weekends to remove them from businees days; 
            DateTime day = startDate;
            while (day <= endDate)
            {
                for (int i = 0; i < myWeekends.Count; i++)
                {
                    if ((day.DayOfWeek).ToString() == myWeekends[i])
                    {
                        businessDays = businessDays - 1;
                    }
                }
                day = day.AddDays(1);
            }

            //This Loop basically iterates the span from start date till end date to confirm any holiday present in that span or not;
            for (int j = 0; j < myHolidays.Count; j++)
            {
                if (startDate.CompareTo(myHolidays[j].Date) <= 0 && endDate.CompareTo(myHolidays[j].Date) >= 0)
                {
                    dummyList.Add(myHolidays[j]);
                    businessDays = businessDays - 1;
                }
            }

            // While this loop checks that if holiday occurs on weekend so it should add the subtracted business day from the above loop
            while (k < dummyList.Count)
            {
                string holiday = dummyList[k].Date.DayOfWeek.ToString();
                while (count < myWeekends.Count)
                {
                    if (holiday == myWeekends[count])
                    {
                        businessDays = businessDays + 1;
                    }
                    count++;
                }
                k++;
            }

                if (businessDays > 10)
                {
                    penaltyDays = businessDays - 10;
                    penaltyAmount = (penaltyDays) * (myCountry.countryPenaltyRate);
                    taxtAmount = penaltyAmount * ((myCountry.countryTax / 100));
                    totalPenalty = penaltyAmount + taxtAmount;
                    penaltyString = totalPenalty.ToString() + myCountry.currencyCode;

                    Penalty myPenalty = new Penalty(businessDays, penaltyString);
                    return myPenalty;
                }
                else
                {
                    penaltyDays = 0;
                    penaltyAmount = (penaltyDays) * (myCountry.countryPenaltyRate);
                    taxtAmount = penaltyAmount * ((myCountry.countryTax / 100));
                    totalPenalty = penaltyAmount + taxtAmount;
                    penaltyString = totalPenalty.ToString() + myCountry.currencyCode;

                    Penalty myPenalty = new Penalty(businessDays, penaltyString);
                    return myPenalty;
                }
            
        }

        //Function for returning the object of Country on the basis of id;
        public Country GetCountry(int countryId)
        {
            Country getCountry = new Country();
            string query = "Select *from countryTable where countryID=" + countryId;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, con);

                con.Open();

                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    getCountry.countryID = Convert.ToInt32(sdr["countryID"]);
                    getCountry.countryName = sdr["countryName"].ToString();
                    getCountry.countryCurrency = sdr["countryCurrency"].ToString();
                    getCountry.currencyCode = sdr["currencyCode"].ToString();
                    getCountry.countryWeekend = sdr["countryWeekend"].ToString();
                    getCountry.countryTax = Convert.ToInt32(sdr["countryTax"]);
                    getCountry.countryPenaltyRate = Convert.ToInt32(sdr["countryPenaltyRate"]);
                }
                con.Close();
            }
            return getCountry;
        }
        //Function for returning the holidays list for a specific country;
        public List<DateTime> GetHolidays(int countryId)
        {
            List<DateTime> holidayList = new List<DateTime>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string myQuery = "select cast(holidayDate AS datetime) as holidayDate from holidayTable where countryID=" + countryId;
                SqlCommand cmd = new SqlCommand(myQuery, con);
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    DateTime myHolidays = Convert.ToDateTime(sdr["holidayDate"]);
                    holidayList.Add(myHolidays);
                }
                con.Close();
            }
            return holidayList;
        }
        //Function for returning the weekends by using the specific country weekend information and returning a list of string;
        public List<string> GetWeekened(List<string> weekDays, string weekend)
        {
            List<string> myDays = new List<string> (weekDays) ;
            for (int i = 0; i < weekDays.Count; i++)
            {
                if(!(weekend[i]=='1'))
                {
                    myDays.Remove(weekDays[i]);
                }
            }
            weekDays = myDays;
            return weekDays;
        }
    }
}