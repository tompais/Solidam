//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class DonacionHorasTrabajo
    {
        public int IdDonacionHorasTrabajo { get; set; }
        public int IdPropuestaDonacionHorasTrabajo { get; set; }
        public int IdUsuario { get; set; }
        public int Cantidad { get; set; }
    
        public virtual PropuestaDonacionHorasTrabajo PropuestaDonacionHorasTrabajo { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}