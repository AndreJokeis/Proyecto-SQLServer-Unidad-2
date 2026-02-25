using Ordenes.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;

using Ordenes.Objetos;

namespace Ordenes.Formularios
{
    public partial class Inicio : System.Web.UI.Page
    {
        Repositorio repositorio = new Repositorio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarClientes();
                CargarServicios();

                txtFolio.Text = "AUTOMÁTICO";

                txtFechaRegistro.Text = DateTime.Now.ToString("yyyy-MM-dd");

                txtFechaEntrega.Text = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
                txtFechaEntrega.Attributes["min"] = DateTime.Now.ToString("yyyy-MM-dd");
            }
        }

        private void CargarVehiculos(int idCliente)
        {
            ddlVehiculo.DataSource = repositorio.ObtenerVehiculos(idCliente);
            ddlVehiculo.DataTextField = "Modelo";
            ddlVehiculo.DataValueField = "NumeroDeSerie";
            ddlVehiculo.DataBind();

            ddlVehiculo.Items.Insert(0, new ListItem("Seleccione un vehiculo", "0"));
        }

        private void CargarClientes()
        {
            ddlCliente.DataSource = repositorio.ObtenerClientes();
            ddlCliente.DataTextField = "Nombre";
            ddlCliente.DataValueField = "idCliente";
            ddlCliente.DataBind();

            ddlCliente.Items.Insert(0, new ListItem("Seleccione un cliente", "0"));
        }

        private void CargarServicios()
        {
            ddlServicio.DataSource = repositorio.ObtenerServicios();
            ddlServicio.DataTextField = "NombreServicio";
            ddlServicio.DataValueField = "idServicio";
            ddlServicio.DataBind();

            ddlServicio.Items.Insert(0, new ListItem("Seleccione un servicio", "0"));

        }

        public void CargarTabla()
        {
            List<Servicio> servicios = Session["ServiciosLista"] as List<Servicio> ?? new List<Servicio>();
            foreach (var servicio in servicios)
            {
                AgregarServicio(servicio);
            }
        }

        private void AgregarServicio(Servicio servicio)
        {
            TableRow row = new TableRow();
            TableCell cellID = new TableCell();
            TableCell cellNombre = new TableCell();
            TableCell cellDescripcion = new TableCell();
            TableCell cellCosto = new TableCell();
            TableCell cellTiempoEstimado = new TableCell();

            cellID.Text = servicio.idServicio.ToString();
            row.Cells.Add(cellID);
            cellNombre.Text = servicio.NombreServicio;
            row.Cells.Add(cellNombre);
            cellDescripcion.Text = servicio.Descripcion;
            row.Cells.Add(cellDescripcion);
            cellCosto.Text = servicio.Costo.ToString("C");
            row.Cells.Add(cellCosto);
            cellTiempoEstimado.Text = servicio.TiempoEstimado.ToString() + " horas";
            row.Cells.Add(cellTiempoEstimado);
            tblServicios.Rows.Add(row);
        }

        private void showMessage(string message)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('{message}');", true);
        }

        private void LimpiarFormulario()
        {
            ddlCliente.SelectedIndex = 0;
            ddlVehiculo.Items.Clear();
            ddlServicio.SelectedIndex = 0;
            txtRFC.Text = string.Empty;
            txtNombre.Text = string.Empty;
            txtNumeroDeSerie.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
            txtPrecio.Text = string.Empty;
            tblServicios.Rows.Clear();
        }

        protected void btnGuardarOrden_Click(object sender, EventArgs e)
        {
            List<Servicio> servicios = Session["ServiciosLista"] as List<Servicio> ?? new List<Servicio>();
            if (servicios.Count > 0 && ddlCliente.SelectedIndex > 0 && ddlVehiculo.SelectedIndex > 0)
            {
                int idCliente = Convert.ToInt32(ddlCliente.SelectedValue);
                int NumeroDeSerie = Convert.ToInt32(txtNumeroDeSerie.Text);
                decimal total = Convert.ToDecimal(lblTotal.Text);

                //(int numeroDeSerie, DateTime entregaEstimada, decimal costoTotal, List<Servicio> servicios)
                if (repositorio.GuardarOrden(NumeroDeSerie, Convert.ToDateTime(txtFechaEntrega.Text), total, servicios))
                {
                    LimpiarFormulario();
                    showMessage("Orden guardada exitosamente.");
                }
                else
                {
                    showMessage("Ocurrió un error al guardar la orden. Intente nuevamente.");
                }
            }
            else
            {
                showMessage("Complete todos los campos y agregue al menos un servicio para guardar la orden.");
            }
        }

        protected void ddlCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idCliente = Convert.ToInt32(ddlCliente.SelectedValue);

            if (idCliente > 0)
            {
                Cliente cliente = repositorio.ObtenerCliente(idCliente);

                txtRFC.Text = cliente.RFC;
                txtNombre.Text = cliente.Nombre;

                ddlVehiculo.Items.Clear();
                txtNumeroDeSerie.Text = string.Empty;

                CargarVehiculos(idCliente);
            }
            else
            {
                txtRFC.Text = string.Empty;
                txtNombre.Text = string.Empty;
                ddlVehiculo.Items.Clear();
                txtNumeroDeSerie.Text = string.Empty;

                Session["Cliente"] = null;
            }
        }

        protected void ddlServicio_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(ddlServicio.SelectedValue);

            if(id > 0){
                Servicio servicio = repositorio.ObtenerServicio(id);

                txtID.Text = servicio.idServicio.ToString();
                txtDescripcion.Text = servicio.Descripcion;
                txtPrecio.Text = servicio.Costo.ToString();
                txtCantidad.Text = "1";

                CargarTabla();
            }
            else
            {
                txtID.Text = string.Empty;
                txtDescripcion.Text = string.Empty;
                txtPrecio.Text = string.Empty;
            }
        }

        protected void btnAgregarServicio_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(ddlServicio.SelectedValue);

            if (id > 0)
            {
                if(string.IsNullOrEmpty(txtCantidad.Text))
                    showMessage("Ingrese una cantidad válida");
                else if (Convert.ToInt32(txtCantidad.Text) > 0)
                {
                    List<Servicio> listaTemporal = Session["ServiciosLista"] as List<Servicio> ?? new List<Servicio>();
                    Servicio servicio = repositorio.ObtenerServicio(id);

                    listaTemporal.Add(servicio);

                    Session["ServiciosLista"] = listaTemporal;

                    CargarTabla();

                    decimal subtotal = Convert.ToDecimal(lblSubtotal.Text) * Convert.ToInt32(txtCantidad.Text);
                    subtotal += servicio.Costo;
                    lblSubtotal.Text = subtotal.ToString();

                    decimal iva = subtotal * 0.16m;
                    lblIVA.Text = iva.ToString();

                    decimal total = subtotal + iva;
                    lblTotal.Text = total.ToString();

                    ddlServicio.SelectedValue = 0.ToString();
                    txtID.Text = string.Empty;
                    txtDescripcion.Text = string.Empty;
                    txtCantidad.Text = string.Empty;
                    txtPrecio.Text = string.Empty;
                } else
                {
                    showMessage("La cantidad debe ser mayor a 0");
                }
            } else
            {
                showMessage("Seleccione un servicio para agregar");
            }
        }

        protected void ddlVehiculo_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(ddlVehiculo.SelectedValue);

            if (id > 0)
            {
                txtNumeroDeSerie.Text = id.ToString();
            } else
            {
                txtNumeroDeSerie.Text = string.Empty;
            }
        }
    }
}