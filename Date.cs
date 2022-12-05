using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace p1.Models
{
    public class Date
    {
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
       // DateTime sd = DateTime.ParseExact(DateTime.Now.ToString(), "yyyy-MM-dd", CultureInfo.InvariantCulture);
        //DateTime ed = DateTime.ParseExact(DateTime.Now.ToString(), "yyyy-MM-dd", CultureInfo.InvariantCulture);
         public DateTime sd { get; set; }


        public DateTime ed { get; set; }
    }
}
