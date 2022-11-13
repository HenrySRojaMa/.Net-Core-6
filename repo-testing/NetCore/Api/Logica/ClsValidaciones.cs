using Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logica
{
    class ClsValidaciones
    {

        public static bool validarProducto(ClsProducto Producto)
        {

            bool flag = false;

            if (Producto.Codigo <= 0)
            {
                flag = true;
            }
            else
            {
                string id = Producto.Codigo.ToString();
                if (string.IsNullOrWhiteSpace(id))
                {
                    flag = true;
                }
            }

            if (string.IsNullOrEmpty(Producto.Nombre))
            {
                flag = true;
            }

            if (string.IsNullOrEmpty(Producto.Descripcion))
            {
                flag = true;
            }

            if (Producto.Precio <= 0)
            {
                flag = true;
            }
            else
            {
                string valor = Producto.Precio.ToString();
                if (string.IsNullOrWhiteSpace(valor))
                {
                    flag = true;
                }
            }

            if (Producto.Stock <= 0)
            {
                flag = true;
            }

            return flag;
        }

    }
}
