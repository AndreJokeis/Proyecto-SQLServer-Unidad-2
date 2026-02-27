using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using static System.Net.Mime.MediaTypeNames;

namespace Ordenes.Servicios
{
    public class SQL
    {
        public SqlConnection ObtenerConexion()
        {
            return new SqlConnection(ConfigurationManager.ConnectionStrings["stringConnection"].ConnectionString);
        }
    }
}