using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class CustomRangoFecha : RangeAttribute
    {
        public CustomRangoFecha() : base(typeof(DateTime), DateTime.Today.AddDays(1).ToString()
            , DateTime.Today.AddYears(200).ToString())
        { }
    }
}
