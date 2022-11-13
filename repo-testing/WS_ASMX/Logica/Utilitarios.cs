using System;
using WS_ASMX.Models;

namespace WS_ASMX.Logica
{
    public class Utilitarios
    {
        public static bool validaCliente(ClsCliente objCliente)
        {

            bool flag = false;

            if (string.IsNullOrEmpty(objCliente.Nombre))
            {
                flag = true;
            }

            if (string.IsNullOrEmpty(objCliente.Apellido))
            {
                flag = true;
            }
            if (string.IsNullOrEmpty(objCliente.Identificacion))
            {
                flag = true;
            }
            if (objCliente.Edad <= 0)
            {
                flag = true;
            }
            if (string.IsNullOrEmpty(objCliente.Nombre))
            {
                flag = true;
            }
            if (objCliente.EstadoCivil <= 0)
            {
                flag = true;
            }
            else
            {
                string valor = objCliente.EstadoCivil.ToString();
                try
                {
                    int valorTemp = Convert.ToInt32(valor);
                    flag = true;
                }
                catch (Exception ex)
                {
                    string estadoCivil = objCliente.EstadoCivil.ToString();
                    if (string.IsNullOrWhiteSpace(estadoCivil))
                    {
                        flag = true;
                    }
                }
            }
            return flag;
        }

    }
}