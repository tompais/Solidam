﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class UsuariosMetadata
    {
        [Required]
        [StringLength(50)]
        public string Email { get; set; }
    }
}
