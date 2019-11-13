using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Models
{
    public class DonacionesMonetariasMetadata : BaseMetadata
    {
        [Required(ErrorMessage = "Debe ingresar una cantidad")]
        [CustomValidation(typeof(DonacionesMonetariasMetadata), "ValidarDineroDonado")]
        [Range(1,int.MaxValue,ErrorMessage = "El valor mínimo debe ser 1")]
        public decimal Dinero { get; set; }

        [Required(ErrorMessage = "Debe ingresar su comprobante de transferencia")]
        public HttpPostedFileBase File { get; set; }

        public static ValidationResult ValidarDineroDonado(object value, ValidationContext context)
        {
            var donacion = context.ObjectInstance as DonacionesMonetarias;

            var donaciones = Db.DonacionesMonetarias
                .Where(dm => dm.IdPropuestaDonacionMonetaria == donacion.IdPropuestaDonacionMonetaria).ToList();
            var obtenido = donaciones.Count == 0 ? 0 : donaciones.Sum(d => d.Dinero);
            var objetivo = Db.PropuestasDonacionesMonetarias
                .FirstOrDefault(p => p.IdPropuestaDonacionMonetaria == donacion.IdPropuestaDonacionMonetaria)?.Dinero;
            var restante = objetivo - obtenido;
            return donacion?.Dinero > restante ? new ValidationResult("No se puede donar esa cantidad de dinero") : ValidationResult.Success;
        }
    }
}
