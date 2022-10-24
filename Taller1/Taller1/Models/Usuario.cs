using System;
using System.Collections.Generic;

namespace Taller1.Models
{
    public partial class Usuario
    {
        public string Codigo { get; set; } = null!;
        public string Contrasenia { get; set; } = null!;

        public virtual Cliente CodigoNavigation { get; set; } = null!;
    }
}
