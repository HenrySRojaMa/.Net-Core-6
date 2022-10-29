using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Persona_CRUD.Models
{
    public class Conexion
    {
        public SqlConnection Link { get; set; }

        public Conexion(string ConnectionString)
        {
            Link = new SqlConnection(ConnectionString);
        }


    }
}
