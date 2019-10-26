using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class DonacionesInsumosMetadata
    {
        [Required(ErrorMessage = "Debe ingresar una cantidad")]
        [CustomValidation(typeof(DonacionesInsumosMetadata),"ValidarCantidadObjetos")]
        public int Cantidad { get; set; }

        public static ValidationResult ValidarCantidadObjetos(object value, ValidationContext context)
        {
            return ValidationResult.Success;
        }
    }
}
