using System;
using System.Collections.Generic;

namespace Repository_UnitOfWork.Models
{
    public partial class Usuario:BaseEntity
    {
        public string Contrasenia { get; set; } = null!;
        public Rol Rol { get; set; }

        public virtual Cliente IdNavigation { get; set; } = null!;
    }
    public enum Rol
    {
        Admin,
        general
    }
}
