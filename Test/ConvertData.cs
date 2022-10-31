using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public interface ConvertDate
    {
        string convertDate(DateTime dateTime);
       
    }

    public class Data : ConvertDate
    {
        public string convertDate(DateTime dateTime)
        {
            return dateTime.Year.ToString() + "-" + dateTime.Month.ToString() + "-" + dateTime.Day.ToString() + " " + dateTime.Hour.ToString() + ":" + dateTime.Minute.ToString();
        }
    }

}
