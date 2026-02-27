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

                lblSubtotal.Text = 0.ToString("C");
                lblIVA.Text = 0.ToString("C");
                lblTotal.Text = 0.ToString("C");

                txtFolio.Text = "AUTOMÁTICO";

                txtFechaRegistro.Text = DateTime.Now.ToString("yyyy-MM-dd");

                txtFechaEntrega.Text = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
                txtFechaEntrega.Attributes["min"] = DateTime.Now.ToString("yyyy-MM-dd");
            }
        }

        private void CargarVehiculos(int idCliente)
        {
            ddlVehiculo.DataSource = repositorio.ObtenerVehiculos(idCliente);
            //System.Diagnostics.Debug.WriteLine("Vehiculos obtenidos para cliente " + idCliente + ": " + ddlVehiculo.DataSource);    
            ddlVehiculo.DataTextField = "Modelo";
            ddlVehiculo.DataValueField = "NumeroDeSerie";
            ddlVehiculo.DataBind();

            ddlVehiculo.Items.Insert(0, new ListItem("Seleccione un vehiculo", "0"));
        }

        private void CargarClientes()
        {
            ddlCliente.DataSource = repositorio.ObtenerClientes();
            //System.Diagnostics.Debug.WriteLine("Clientes obtenidos: " + ddlCliente.DataSource);
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
            TableCell cellCantidad = new TableCell();

            cellID.Text = servicio.idServicio.ToString();
            row.Cells.Add(cellID);
            cellNombre.Text = servicio.NombreServicio;
            row.Cells.Add(cellNombre);
            cellDescripcion.Text = servicio.Descripcion;
            row.Cells.Add(cellDescripcion);
            cellCantidad.Text = servicio.Cantidad.ToString();
            row.Cells.Add(cellCantidad);
            cellCosto.Text = servicio.Costo.ToString("C");
            row.Cells.Add(cellCosto);
            cellTiempoEstimado.Text = servicio.TiempoEstimado.ToString() + " horas";
            row.Cells.Add(cellTiempoEstimado);
            tblServicios.Rows.Add(row);
        }

        private bool isPresentInTable(int idServicio)
        {
            List<Servicio> servicios = Session["ServiciosLista"] as List<Servicio> ?? new List<Servicio>();
            return servicios.Any(
                s => s.idServicio == idServicio
            );
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
            txtPrecio.Text = 0.ToString("C");
            tblServicios.Rows.Clear();
            LimpiarTotales();
        }

        private void LimpiarTotales()
        {
            lblSubtotal.Text = 0.ToString("C");
            lblIVA.Text = 0.ToString("C");
            lblTotal.Text = 0.ToString("C");
        }

        public void showNotification(string mensaje, string tipo = "success")
        {
            // Usamos un bloque {} para crear un nuevo ámbito (scope) y evitar conflictos
            // O simplemente usamos 'var' en lugar de 'const'
            string script = $@"
            {{
                const ToastNotif = Swal.mixin({{
                  toast: true,
                  position: 'top-end', // O la posición que prefieras
                  showConfirmButton: false,
                  timer: 3000,
                  timerProgressBar: true,
                  showClass: {{ popup: 'animate__animated animate__fadeInRight' }},
                  hideClass: {{ popup: 'animate__animated animate__fadeOutRight' }}
                }});

                ToastNotif.fire({{
                  icon: '{tipo}',
                  title: '{mensaje}'
                }});
            }}";

            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
        }

        protected void ddlCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idCliente = Convert.ToInt32(ddlCliente.SelectedValue);

            if (new Func<bool>(() =>
            {
                List<Servicio> servicios = Session["ServiciosLista"] as List<Servicio> ?? new List<Servicio>();
                return servicios.Count > 0;
            })())
                CargarTabla();

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

            if (id > 0){
                Servicio servicio = repositorio.ObtenerServicio(id);

                txtID.Text = servicio.idServicio.ToString();
                txtDescripcion.Text = servicio.Descripcion;
                txtPrecio.Text = servicio.Costo.ToString("C");
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

        protected void ddlVehiculo_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(ddlVehiculo.SelectedValue);

            if (new Func<bool>(() =>
            {
                List<Servicio> servicios = Session["ServiciosLista"] as List<Servicio> ?? new List<Servicio>();
                return servicios.Count > 0;
            })())
                CargarTabla();

            if (id > 0)
            {
                txtNumeroDeSerie.Text = id.ToString();
            }
            else
            {
                txtNumeroDeSerie.Text = string.Empty;
            }
        }

        protected void btnAgregarServicio_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(ddlServicio.SelectedValue);

            if (id > 0)
            {
                if (string.IsNullOrEmpty(txtCantidad.Text))
                    showNotification("Ingrese una cantidad válida",
                        "info");
                else if (Convert.ToInt32(txtCantidad.Text) > 0)
                {
                    Servicio servicio = repositorio.ObtenerServicio(id);
                    servicio.Cantidad = Convert.ToInt32(txtCantidad.Text);
                    servicio.Costo *=  Convert.ToDecimal(txtCantidad.Text);

                    if (!isPresentInTable(servicio.idServicio))
                    {
                        List<Servicio> listaTemporal = Session["ServiciosLista"] as List<Servicio> ?? new List<Servicio>();
                        listaTemporal.Add(servicio);

                        Session["ServiciosLista"] = listaTemporal;

                        CargarTabla();
                    }
                    else
                    {
                        List<Servicio> servicios = Session["ServiciosLista"] as List<Servicio> ?? new List<Servicio>();
                        foreach(var s in servicios)
                        {
                            if (s.idServicio == servicio.idServicio)
                            {
                                s.Cantidad += servicio.Cantidad;
                                s.Costo += servicio.Costo;

                                Session["ServiciosLista"] = servicios;
                                CargarTabla();

                                break;
                            }
                        }
                    }

                    decimal subtotal = Session["Subtotal"] != null ? (decimal) Session["Subtotal"] : 0m;
                    subtotal += (servicio.Costo * Convert.ToDecimal(txtCantidad.Text));
                    Session["Subtotal"] = subtotal;
                    
                    lblSubtotal.Text = subtotal.ToString("C");

                    decimal iva = subtotal * 0.16m;
                    lblIVA.Text = iva.ToString("C");

                    decimal total = subtotal + iva;
                    lblTotal.Text = total.ToString("C");

                    ddlServicio.SelectedValue = 0.ToString();
                    txtID.Text = string.Empty;
                    txtDescripcion.Text = string.Empty;
                    txtCantidad.Text = string.Empty;
                    txtPrecio.Text = string.Empty;
                } else
                {
                    showNotification("La cantidad debe ser mayor a 0",
                        "info");
                }
            } else
            {
                if (new Func<bool>(() =>
                {
                    List<Servicio> servicios = Session["ServiciosLista"] as List<Servicio> ?? new List<Servicio>();
                    return servicios.Count > 0;
                })())
                    CargarTabla();

                showNotification("Seleccione un servicio para agregar",
                    "info");
            }
        }

        protected void btnGuardarOrden_Click(object sender, EventArgs e)
        {
            List<Servicio> servicios = Session["ServiciosLista"] as List<Servicio> ?? new List<Servicio>();

            if (servicios.Count > 0 && ddlCliente.SelectedIndex > 0 && ddlVehiculo.SelectedIndex > 0)
            {
                int idCliente = Convert.ToInt32(ddlCliente.SelectedValue);
                int NumeroDeSerie = Convert.ToInt32(txtNumeroDeSerie.Text);
                decimal total = Session["Subtotal"] != null ? (decimal)Session["Subtotal"] : 0m;
                total += (total*0.16m);

                //(int numeroDeSerie, DateTime entregaEstimada, decimal costoTotal, List<Servicio> servicios)
                if (repositorio.GuardarOrden(NumeroDeSerie, Convert.ToDateTime(txtFechaEntrega.Text), total, servicios))
                {
                    LimpiarFormulario();
                    showNotification("Orden guardada exitosamente.");
                }
                else
                {
                    showNotification("Ocurrió un error al guardar la orden.\nIntente nuevamente.",
                        "error");
                }
            }
            else
            {
                showNotification("Complete todos los campos y agregue al menos un servicio para guardar la orden.",
                    "info");
                CargarTabla();
            }
        }

    }
}