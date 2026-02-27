using Ordenes.Objetos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Ordenes.Servicios
{
    public class Repositorio
    {
        public List<Vehiculo> ObtenerVehiculos(int id)
        {
            List<Vehiculo> vehiculos = new List<Vehiculo>();

            using (SqlConnection connection = new SQL().ObtenerConexion())
            {
                try
                {
                    connection.Open();
                    //System.Diagnostics.Debug.WriteLine("Estado de conexión: " + connection.State);

                    SqlCommand cmd = new SqlCommand("get_vehiculos", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idCliente", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        Vehiculo vehiculo;

                        while (reader.Read())
                        {
                            vehiculo = new Vehiculo(
                                Convert.ToInt32(reader["NumeroDeSerie"]),
                                Convert.ToInt32(reader["idCliente"]),
                                reader["Placa"].ToString(),
                                reader["Marca"].ToString(),
                                reader["Modelo"].ToString(),
                                Convert.ToInt32(reader["Año"]),
                                reader["Color"].ToString(),
                                Convert.ToInt32(reader["Kilometraje"]),
                                reader["Tipo"].ToString(),
                                Convert.ToInt32(reader["Antiguedad"])
                            );
                            vehiculos.Add(vehiculo);
                        }
                    }

                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                    return null;
                }
            }

            return vehiculos;
        }

        public Vehiculo ObtenerVehiculo(int NumeroDeSerie)
        {
            Vehiculo vehiculo = null;

            using (SqlConnection connection = new SQL().ObtenerConexion())
            {
                try
                {
                    connection.Open();
                    //System.Diagnostics.Debug.WriteLine("Estado de conexión: " + connection.State);

                    SqlCommand cmd = new SqlCommand("get_vehiculo", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@id", NumeroDeSerie);


                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            vehiculo = new Vehiculo(
                                Convert.ToInt32(reader["NumeroDeSerie"]),
                                Convert.ToInt32(reader["idCliente"]),
                                reader["Placa"].ToString(),
                                reader["Marca"].ToString(),
                                reader["Modelo"].ToString(),
                                Convert.ToInt32(reader["Año"]),
                                reader["Color"].ToString(),
                                Convert.ToInt32(reader["Kilometraje"]),
                                reader["Tipo"].ToString(),
                                Convert.ToInt32(reader["Antiguedad"])
                            );
                        }
                    }

                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                    return null;
                }
            }

            return vehiculo;
        }

        public List<Cliente> ObtenerClientes()
        {
            List<Cliente> clientes = new List<Cliente>();

            using (SqlConnection connection = new SQL().ObtenerConexion())
            {
                try
                {
                    connection.Open();
                    //System.Diagnostics.Debug.WriteLine("Estado de conexión: " + connection.State);

                    SqlCommand cmd = new SqlCommand("get_clientes", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        Cliente cliente;

                        //System.Diagnostics.Debug.WriteLine("Ya va a leer");

                        while (reader.Read())
                        {
                            cliente = new Cliente(
                                Convert.ToInt32(reader["idCliente"]),
                                reader["RFC"].ToString(),
                                reader["Nombre"].ToString(),
                                reader["Direccion"].ToString(),
                                reader["Telefono1"].ToString(),
                                reader["Telefono2"].ToString(), 
                                reader["Telefono3"].ToString(), 
                                reader["Correo"].ToString(),
                                Convert.ToDateTime(reader["FechaRegistro"])
                            );
                            //System.Diagnostics.Debug.WriteLine("Leyó un Cliente: " + vehiculo.Placa);

                            clientes.Add(cliente);

                        }

                        //System.Diagnostics.Debug.WriteLine("Clientes leidos!");

                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine("excepcion en cliente"+ex.Message);
                    return null;
                }
            }

            return clientes;
        }

        public Cliente ObtenerCliente(int idCliente)
        {
            Cliente cliente = null;

            using (SqlConnection connection = new SQL().ObtenerConexion())
            {
                try
                {
                    connection.Open();
                    //System.Diagnostics.Debug.WriteLine("Estado de conexión: " + connection.State);

                    SqlCommand cmd = new SqlCommand("get_cliente", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", idCliente);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        //System.Diagnostics.Debug.WriteLine("Ya va a leer");

                        if (reader.Read())
                        {
                            cliente = new Cliente(
                                Convert.ToInt32(reader["idCliente"]),
                                reader["RFC"].ToString(),
                                reader["Nombre"].ToString(),
                                reader["Direccion"].ToString(),
                                reader["Telefono1"].ToString(),
                                reader["Telefono2"].ToString(),
                                reader["Telefono3"].ToString(),
                                reader["Correo"].ToString(),
                                Convert.ToDateTime(reader["FechaRegistro"])
                            );
                            //System.Diagnostics.Debug.WriteLine("Leyó un Cliente: " + vehiculo.Placa);
                        }
                        //System.Diagnostics.Debug.WriteLine("Clientes leidos!");
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }
            }

            return cliente;
        }

        public List<Servicio> ObtenerServicios()
        {
            List<Servicio> servicios = new List<Servicio>();

            using (SqlConnection connection = new SQL().ObtenerConexion())
            {
                try
                {
                    connection.Open();
                    //System.Diagnostics.Debug.WriteLine("Estado de conexión: " + connection.State);

                    SqlCommand cmd = new SqlCommand("get_servicios", connection);
                    cmd.CommandType = CommandType.StoredProcedure;


                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        Servicio servicio;

                        //System.Diagnostics.Debug.WriteLine("Ya va a leer");

                        while (reader.Read())
                        {
                            servicio = new Servicio(
                                Convert.ToInt32(reader["idServicio"]),
                                reader["NombreServicio"].ToString(),
                                reader["Descripcion"].ToString(),
                                Convert.ToDecimal(reader["Costo"]),
                                Convert.ToDecimal(reader["TiempoEstimado"])
                            );
                            //System.Diagnostics.Debug.WriteLine("Leyó un Servicio: " + vehiculo.Placa);

                            servicios.Add(servicio);

                        }

                        //System.Diagnostics.Debug.WriteLine("Servicios leidos!");

                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }
            }

            return servicios;
        }

        public Servicio ObtenerServicio(int idServicio)
        {
            Servicio servicio = null;
            using (SqlConnection connection = new SQL().ObtenerConexion())
            {
                try
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand("get_servicio", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", idServicio);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            servicio = new Servicio(
                                Convert.ToInt32(reader["idServicio"]),
                                reader["NombreServicio"].ToString(),
                                reader["Descripcion"].ToString(),
                                Convert.ToDecimal(reader["Costo"]),
                                Convert.ToDecimal(reader["TiempoEstimado"])
                            );
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }
            }
            return servicio;
        }

        public bool GuardarOrden(int numeroDeSerie, DateTime entregaEstimada, decimal costoTotal, List<Servicio> servicios)
        {
            using (SqlConnection con = new SQL().ObtenerConexion())
            {
                con.Open();

                SqlTransaction transaccion = con.BeginTransaction();

                try
                {
                    SqlCommand cmdCabecera = new SqlCommand("sp_InsertarOrden", con, transaccion);
                    cmdCabecera.CommandType = CommandType.StoredProcedure;
                    cmdCabecera.Parameters.AddWithValue("@fechaEntrega", entregaEstimada);
                    cmdCabecera.Parameters.AddWithValue("@numSerie", numeroDeSerie);
                    cmdCabecera.Parameters.AddWithValue("@costoTotal", costoTotal);

                    int folioGenerado = Convert.ToInt32(cmdCabecera.ExecuteScalar());

                    foreach (var serv in servicios)
                    {
                        SqlCommand cmdDetalle = new SqlCommand("sp_InsertarDetalleOrden", con, transaccion);
                        cmdDetalle.CommandType = CommandType.StoredProcedure;
                        cmdDetalle.Parameters.AddWithValue("@idFolio", folioGenerado);
                        cmdDetalle.Parameters.AddWithValue("@idServicio", serv.idServicio);
                        cmdDetalle.Parameters.AddWithValue("@precio", serv.Costo);

                        cmdDetalle.ExecuteNonQuery();
                    }

                    transaccion.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    transaccion.Rollback();
                    System.Diagnostics.Debug.WriteLine("Error en transacción: " + ex.Message);
                    return false;
                }
            }
        }
    }
}