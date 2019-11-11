using System;
using Models;

namespace DTO
{
    public class DenunciaDTO
    {
        public int IdDenuncia { get; set; }
        public string Propuesta { get; set; }
        public string Motivo { get; set; }
        public string Comentarios { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}