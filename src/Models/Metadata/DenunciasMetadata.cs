using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class DenunciasMetadata
    {
        [Required(ErrorMessage = "Debe ingresar un motivo")]
        public int IdMotivo { get; set; }
        [Required(ErrorMessage = "Debe ingresar un comentario")]
        [MaxLength(300,ErrorMessage = "El campo no debe superar los 300 caracteres")]
        public string Comentarios { get; set; }
    }
}
