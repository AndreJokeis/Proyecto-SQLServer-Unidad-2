using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ordenes.Objetos
{
    public class Servicio
    {
        public int idServicio { get; set; }
        public string NombreServicio { get; set; }
        public string Descripcion { get; set; }
        public decimal Costo { get; set; }
        public decimal TiempoEstimado { get; set; }

        public Servicio(int idServicio, string NombreServicio, string Descripcion, decimal Costo, decimal TiempoEstimado)
        {
            this.idServicio = idServicio;
            this.NombreServicio = NombreServicio;
            this.Descripcion = Descripcion;
            this.Costo = Costo;
            this.TiempoEstimado = TiempoEstimado;
        }

        public Servicio(string NombreServicio, string Descripcion, decimal Costo, decimal TiempoEstimado)
        {
            this.NombreServicio = NombreServicio;
            this.Descripcion = Descripcion;
            this.Costo = Costo;
            this.TiempoEstimado = TiempoEstimado;
        }
    }
}