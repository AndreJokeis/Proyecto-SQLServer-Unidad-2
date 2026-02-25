using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ordenes.Objetos
{
    public class Cliente
    {
        public int idCliente { get; set; }
        public string RFC { get; set; }
        public string Nombre { get; set; }
        public string Direccion {  set; get; }
        public string Telefono1 { set; get; }
        public string Telefono2 { set; get; }
        public string Telefono3 { set; get; }
        public string Correo { set; get; }
        public DateTime FechaRegistro { set; get; }

        public Cliente(int idCliente, string RFC, string Nombre, string Direccion, string Telefono1, string Telefono2, string Telefono3, string Correo, DateTime FechaRegistro)
        {
            this.idCliente = idCliente;
            this.RFC = RFC;
            this.Nombre = Nombre;
            this.Direccion = Direccion;
            this.Telefono1 = Telefono1;
            this.Telefono2 = Telefono2;
            this.Telefono3 = Telefono3;
            this.Correo = Correo;
            this.FechaRegistro = FechaRegistro;
        }
    }
}