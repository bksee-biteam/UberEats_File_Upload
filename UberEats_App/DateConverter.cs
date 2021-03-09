using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UberEats_Upload
{
    public static class DateConverter
    {




        public static string  getDateFromIntegerAndSaveToString (int date_value)
        {
            return getDateFromInteger(date_value).ToString("yyyy-MM-dd");
        }




        private static DateTime getDateFromInteger(int date_value)
        {
            int d = date_value % 100;
            int m = (date_value / 100) % 100;
            int y = date_value / 10000;



            return new DateTime(y, m, d);

        }

        private static DateTime getDateFromInteger(long date_value)
        {
            return getDateFromInteger((int)(date_value / 1000000));
        }




        public static string formatStringToValidDate(string date_value)
        {
            return getDateFromIntegerAndSaveToString(int.Parse(date_value));
        }



        

        public static string getDateFromIntegerAndSaveToString(Int64 date)
        {
                return getDateFromIntegerAndSaveToString((int)(date / 1000000));
        }

        public static string getDateTimeFromIntegerAndSaveToString(Int64 dateTime_value)
        {

            int timePart =  (int) (dateTime_value % 1000000) ;

            DateTime datePart = getDateFromInteger(dateTime_value);

            int hour = timePart / 10000;
            int minute = (timePart / 100) % 100;
            int second = timePart % 100;



            TimeSpan timeSpanPart = new TimeSpan(hour, minute, second);

            DateTime dt = datePart + timeSpanPart;


            return dt.ToString();

        }


        
        public static string formatStringToValidTime(string time_value)
        {
            string time = time_value.Substring(0, 2);

            for (int i = 2; i <= 4;) 
            {

                time += ":" + time_value.Substring(i, 2);

                i += 2;

            }


            return time;
        }
 


    }
}
