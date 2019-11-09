using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class DonacionesHorasTrabajoMetadata : BaseMetadata
    {
        [Required(ErrorMessage = "Debe ingresar la cantidad de horas")]
        [CustomValidation(typeof(DonacionesHorasTrabajoMetadata),"ValidarCantidadHoras")]
        [Range(1, int.MaxValue, ErrorMessage = "El valor mínimo debe ser 1")]
        public int Cantidad { get; set; }

        public static ValidationResult ValidarCantidadHoras(object value, ValidationContext context)
        {
            var donacion = context.ObjectInstance as DonacionesHorasTrabajo;
            var obtenido  = Db.DonacionesHorasTrabajo
                .Where(d => d.IdPropuestaDonacionHorasTrabajo == donacion.IdPropuestaDonacionHorasTrabajo).ToList().Sum(d=> d.Cantidad);


            var objetivo = Db.PropuestasDonacionesHorasTrabajo.FirstOrDefault(p =>
                p.IdPropuestaDonacionHorasTrabajo == donacion.IdPropuestaDonacionHorasTrabajo)?.CantidadHoras;
            var restante = objetivo - obtenido;
            return donacion?.Cantidad > restante
                ? new ValidationResult("No se puede ingresar esa cantidad de horas")
                : ValidationResult.Success;

        }
    }
}
