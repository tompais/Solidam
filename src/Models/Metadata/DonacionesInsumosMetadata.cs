using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class DonacionesInsumosMetadata : BaseMetadata
    {
        [Required(ErrorMessage = "Debe ingresar una cantidad")]
        [CustomValidation(typeof(DonacionesInsumosMetadata),"ValidarCantidadObjetos")]
        [Range(1,int.MaxValue,ErrorMessage="El valor mínimo de elementos es 1")]
        public int Cantidad { get; set; }

        public static ValidationResult ValidarCantidadObjetos(object value, ValidationContext context)
        {
            var donacion = context.ObjectInstance as DonacionesInsumos;
            var objetivo = Db.PropuestasDonacionesInsumos
                .FirstOrDefault(p => p.IdPropuestaDonacionInsumo == donacion.IdPropuestaDonacionInsumo)?.Cantidad;
            var donacionesObtenidas = Db.DonacionesInsumos
                .Where(d => d.IdPropuestaDonacionInsumo == donacion.IdPropuestaDonacionInsumo).ToList();
            var obtenido = 0;
            if(donacionesObtenidas.Count != 0) obtenido = donacionesObtenidas.Sum(d => d.Cantidad);
            var restante = objetivo - obtenido;
            return donacion?.Cantidad > restante
                ? new ValidationResult("No se puede donar esa cantidad de elementos")
                : ValidationResult.Success;
        }
    }
}
