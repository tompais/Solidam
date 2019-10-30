using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class CustomRangoFecha : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime fecha = (DateTime)value;

            if (fecha.Date <= DateTime.Today)
                return new ValidationResult("Debe ser una fecha superior a la actual");

            return ValidationResult.Success;
        }
    }
}
