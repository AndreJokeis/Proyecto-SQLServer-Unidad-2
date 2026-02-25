<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="Ordenes.Formularios.Inicio" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Taller André - Nueva Orden</title>
    
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet">
    <link href="../Content/EstilosTaller.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />

        <div class="container py-5">
            <div class="card card-orden">
                <div class="card-header-taller">
                    <h2 class="mb-0">Registro de Orden de Servicio</h2>
                </div>

                <div class="card-body p-4">
                    <asp:UpdatePanel ID="upPrincipal" runat="server">
                        <ContentTemplate>

                            <div class="section-title">1. Información de la Orden</div>
                            <div class="row g-3 mb-4">
                                <div class="col-md-4">
                                    <label class="form-label fw-bold">Folio:</label>
                                    <asp:TextBox ID="txtFolio" runat="server" CssClass="form-control input-taller" placeholder="Automático" Enabled="False"></asp:TextBox>
                                </div>
                                <div class="col-md-4">
                                    <label class="form-label fw-bold">Fecha de Registro:</label>
                                    <asp:TextBox ID="txtFechaRegistro" runat="server" TextMode="Date" CssClass="form-control input-taller" Enabled="False"></asp:TextBox>
                                </div>
                                <div class="col-md-4">
                                    <label class="form-label fw-bold text-primary">Fecha de Entrega:</label>
                                    <asp:TextBox ID="txtFechaEntrega" runat="server" TextMode="Date" CssClass="form-control input-taller border-primary"></asp:TextBox>
                                </div>
                            </div>

                            <div class="section-title">2. Cliente y Vehículo</div>
                            <div class="row g-3 mb-4">
                                <div class="col-md-6 border-end">
                                    <label class="form-label">Seleccionar Cliente:</label>
                                    <asp:DropDownList ID="ddlCliente" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCliente_SelectedIndexChanged" CssClass="form-select mb-2"></asp:DropDownList>
                                    <div class="row g-2">
                                        <div class="col-6">
                                            <asp:TextBox ID="txtRFC" runat="server" placeholder="RFC" CssClass="form-control form-control-sm" Enabled="False"></asp:TextBox>
                                        </div>
                                        <div class="col-6">
                                            <asp:TextBox ID="txtNombre" runat="server" placeholder="Nombre" CssClass="form-control form-control-sm" Enabled="False"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <label class="form-label">Seleccionar Vehículo:</label>
                                    <asp:DropDownList ID="ddlVehiculo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlVehiculo_SelectedIndexChanged" CssClass="form-select mb-2"></asp:DropDownList>
                                    <asp:TextBox ID="txtNumeroDeSerie" runat="server" placeholder="No. de Serie" CssClass="form-control form-control-sm" Enabled="False"></asp:TextBox>
                                </div>
                            </div>

                            <div class="section-title">3. Detalle de Servicios</div>
                            <div class="row g-2 align-items-end mb-4 p-3 bg-light rounded border">
                                <div class="col-md-4">
                                    <label class="form-label">Servicio:</label>
                                    <asp:DropDownList ID="ddlServicio" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlServicio_SelectedIndexChanged" CssClass="form-select"></asp:DropDownList>
                                </div>
                                <div class="col-md-1">
                                    <label class="form-label">ID:</label>
                                    <asp:TextBox ID="txtID" runat="server" CssClass="form-control" Enabled="False"></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    <label class="form-label">Descripción:</label>
                                    <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" Enabled="False"></asp:TextBox>
                                </div>
                                <div class="col-md-1">
                                    <label class="form-label">Cant.:</label>
                                    <asp:TextBox ID="txtCantidad" runat="server" TextMode="Number" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-1">
                                    <label class="form-label">Precio:</label>
                                    <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control" Enabled="False"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <asp:Button ID="btnAgregarServicio" runat="server" Text="Agregar" OnClick="btnAgregarServicio_Click" CssClass="btn btn-success w-100 fw-bold" />
                                </div>
                            </div>

                            <div class="table-responsive shadow-sm rounded">
                                <asp:Table ID="tblServicios" runat="server" CssClass="table table-hover table-striped mb-0">
                                    <asp:TableHeaderRow CssClass="table-dark">
                                        <asp:TableHeaderCell>ID</asp:TableHeaderCell>
                                        <asp:TableHeaderCell>Servicio</asp:TableHeaderCell>
                                        <asp:TableHeaderCell>Descripción</asp:TableHeaderCell>
                                        <asp:TableHeaderCell>Costo</asp:TableHeaderCell>
                                        <asp:TableHeaderCell>Tiempo</asp:TableHeaderCell>
                                    </asp:TableHeaderRow>
                                </asp:Table>
                            </div>

                            <div class="row mt-5 justify-content-end">
                                <div class="col-md-4">
                                    <div class="total-box shadow-sm">
                                        <div class="d-flex justify-content-between mb-2">
                                            <span class="text-muted">Subtotal:</span>
                                            <asp:Label ID="lblSubtotal" runat="server" Text="0.00" CssClass="fw-bold"></asp:Label>
                                        </div>
                                        <div class="d-flex justify-content-between mb-2">
                                            <span class="text-muted">IVA (16%):</span>
                                            <asp:Label ID="lblIVA" runat="server" Text="0.00" CssClass="fw-bold"></asp:Label>
                                        </div>
                                        <div class="d-flex justify-content-between border-top pt-3 mb-4">
                                            <span class="h5 mb-0">TOTAL:</span>
                                            <asp:Label ID="lblTotal" runat="server" Text="$ 0.00" CssClass="h4 text-success fw-bold mb-0"></asp:Label>
                                        </div>
                                        <asp:Button ID="btnGuardarOrden" runat="server" Text="GUARDAR" OnClick="btnGuardarOrden_Click" CssClass="btn btn-primary btn-lg w-100 shadow" />
                                    </div>
                                </div>
                            </div>

                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </form>
    
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>