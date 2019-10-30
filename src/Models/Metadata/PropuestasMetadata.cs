using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class PropuestasMetadata
    {
        [Required(ErrorMessage = "Debe ponerle un nombre a la propuesta")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Debe poner una descripción")]
        public string Descripcion { get; set; }
        [Required(ErrorMessage = "Debe poner una fecha de finalización")]
        [CustomRangoFecha(ErrorMessage = "Debe ser una fecha superior a la actual")]
        public DateTime FechaFin { get; set; }
        [Required(ErrorMessage = "Debe poner un teléfono de contacto")]
        public string TelefonoContacto { get; set; }
        [Required(ErrorMessage = "Debe elegir una de las opciones")]
        public int TipoDonacion { get; set; }
        [Required(ErrorMessage = "Debe cargar una foto")]
        //[RegularExpression(@"^.*\.(jpg|JPG|jpeg|JPEG|png|PNG)$", ErrorMessage = "Debe ser de extensión .jpg, .jpeg o .png")]
        public string Foto { get; set; }
    }
}
