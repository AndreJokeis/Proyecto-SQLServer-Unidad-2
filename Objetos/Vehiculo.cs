using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ordenes.Objetos
{
    public class Vehiculo
    {
        public int NumeroDeSerie { get; set; }
        public int idCliente { get; set; }
        public string Placa { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int Año { get; set; }
        public string Color { get; set; }
        public int Kilometraje { get; set; }
        public string Tipo { get; set; }
        public int Antiguedad { get; set; }

        public Vehiculo(int NumeroDeSerie, int idCliente, string Placa, string Marca, string Modelo, int Año, string Color, int Kilometraje, string Tipo, int Antiguedad)
        {
            this.NumeroDeSerie = NumeroDeSerie;
            this.idCliente = idCliente;
            this.Placa = Placa;
            this.Marca = Marca;
            this.Modelo = Modelo;
            this.Año = Año;
            this.Color = Color;
            this.Kilometraje = Kilometraje;
            this.Tipo = Tipo;
            this.Antiguedad = Antiguedad;
        }
        public Vehiculo(int idCliente, string Placa, string Marca, string Modelo, int Año, string Color, int Kilometraje, string Tipo, int Antiguedad)
        {
            this.idCliente = idCliente;
            this.Placa = Placa;
            this.Marca = Marca;
            this.Modelo = Modelo;
            this.Año = Año;
            this.Color = Color;
            this.Kilometraje = Kilometraje;
            this.Tipo = Tipo;
            this.Antiguedad = Antiguedad;
        }
    }
}