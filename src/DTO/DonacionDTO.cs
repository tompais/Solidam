using System;

namespace DTO
{
    public class DonacionDTO
    {
        public string FechaDonacion { get; set; } = string.Empty;
        public string Nombre { get; set; }
        public string TipoDonacion { get; set; }
        public string Estado { get; set; }
        public string TotalRecaudado { get; set; }
        public string MiDonacion { get; set; }
        public string Propuesta { get; set; }
    }
}