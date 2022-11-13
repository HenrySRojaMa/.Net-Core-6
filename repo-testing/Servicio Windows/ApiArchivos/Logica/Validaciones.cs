using System;
using System.Collections.Generic;
using System.Text;
using Data;
namespace Logica
{
    class Validaciones
    {
        public static bool validaCliente(Cliente c )
        {
            bool flag = false;

            if (string.IsNullOrEmpty(c.Cedula))
            {
                flag = true;
            }
            if (string.IsNullOrEmpty(c.Nombre))
            {
                flag = true;
            }
            if (string.IsNullOrEmpty(c.Apellido))
            {
                flag = true;
            }
            if (c.Edad <= 0)
            {
                flag = true;
            }
            return flag;
        }
    }
}
