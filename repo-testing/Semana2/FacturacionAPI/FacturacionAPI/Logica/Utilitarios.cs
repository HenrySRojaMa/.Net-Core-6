using FacturacionAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FacturacionAPI.Logica
{
    public class Utilitarios
    {
        public static bool validaFactura(Factura f)
        {
            bool flag = false;

            if (f.CodigoCliente<=0)
            {
                flag = true;
            }
            if (f.Total<=0)
            {
                flag = true;
            }
            if (f.Descuento < 0)
            {
                flag = true;
            }
            if (f.IVA<=0)
            {
                flag = true;
            }
            if (f.TotalaPagar <= 0)
            {
                flag = true;
            }
            return flag;
        }
    }
}