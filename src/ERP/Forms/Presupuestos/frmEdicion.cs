﻿using ERP.Lib.AppForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ERP.Repositories;
using ERP.Models;
using ERP.Reports.DataSet;
using ERP.Reports.Designs;

namespace ERP.Forms.Presupuestos
{
    public partial class frmEdicion : FormBase 
    {
        decimal _subTotal;
        int _filaArticulo;
        DataTable _empresa;
        DataTable _cliente;
        DataTable _comprobante;

        public frmEdicion()
        {
            InitializeComponent();
            this.Text = "Nuevo Presupuesto";
            CargarClientes();
            CargarVendedor();
            rbCodigo.Checked = true;
            CargarProductosCodBarra();
            cbLista.SelectedIndex = 0;           
            
        }
        
        private void CargarProductosCodBarra()
        {
            var a = ArticulosRepository.ObtenerArticulos();
            cbArticulos.DataSource = a;
            cbArticulos.DisplayMember = "CodBarra";
            cbArticulos.ValueMember = "Id";
            if (a.Any()) cbClientes.SelectedIndex = 0;
        }

        private void CargarProductosDescripcion()
        {
            var a = ArticulosRepository.ObtenerArticulos();
            cbArticulos.DataSource = a;
            cbArticulos.DisplayMember = "Descripcion";
            cbArticulos.ValueMember = "Id";
            if (a.Any()) cbClientes.SelectedIndex = 0;
        }

        private void CargarVendedor()
        {
            var usuario = UsuariosRepository.ObtenerUsuarioPorId(Lib.Configuration.IdUsuarioConectado);
            lVendedor.Text = "Vendedor " + usuario.Id + " - " + usuario.NombreCompleto;
        }

        private void CargarClientes()
        {
            var c = ClientesRepository.ObtenerClientes();
            cbClientes.DataSource = c;
            cbClientes.DisplayMember = "RazonSocial";
            cbClientes.ValueMember = "Id";
            if (c.Any()) cbClientes.SelectedIndex = 0;
        }

        public int IdCliente
        {
            get
            {
                return Convert.ToInt32(cbClientes.SelectedValue);
            }
        }

        private void rbCodigo_CheckedChanged(object sender, EventArgs e)
        {
            CargarProductosCodBarra();
        }

        private void rbDescripcion_CheckedChanged(object sender, EventArgs e)
        {
            CargarProductosDescripcion();
        }

        private void cbClientes_SelectionChangeCommitted(object sender, EventArgs e)
        {
            var c = ClientesRepository.ObtenerClientePorId(IdCliente);
            txtDireccion.Text = c.Direccion;
            txtDocumento.Text = TiposDocumentoRepository.TiposDocumentoPorId(c.IdTipoDocumento).Descripcion +
                "  " + c.NroDocumento.ToString().Trim();
        }

        private void cbArticulos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) btnAgregar.PerformClick();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            AgregarArticulo(Convert.ToInt32(cbArticulos.SelectedValue));  //enviamos id Artículo
        }

        private void AgregarArticulo(int idarticulo)
        {

            var art = ArticulosRepository.ObtenerArticulosPorId(idarticulo);
            
            if (buscarArticuloEnDetalle(idarticulo))
            {
                modificarCantidadDetalles(_filaArticulo);
            }
            else
            {
                decimal precio = ObtenerPrecioPorLista(Convert.ToInt16(cbLista.Text), art.Id);
                dgvDetalles.Rows.Add(art.Id, art.CodBarra, art.Descripcion, 1, precio, precio);
                modificarCantidadDetalles(dgvDetalles.Rows.Count-1);
                calcularImportes();
            }
            
        }

        private bool buscarArticuloEnDetalle(int idarticulo)
        {
            bool articuloEnDetalle = false;
            for (int i = 0; i <= dgvDetalles.Rows.Count - 1; i++)
            {
                if (Convert.ToInt32(dgvDetalles.Rows[i].Cells[0].Value) == idarticulo)
                {
                    _filaArticulo = i;
                    i = dgvDetalles.Rows.Count;
                    articuloEnDetalle = true;
                }
            }
            return articuloEnDetalle;
        }

        private void modificarCantidadDetalles(int i)
        {
            dgvDetalles.CurrentCell = dgvDetalles.Rows[i].Cells[3];
            dgvDetalles.BeginEdit(true);
        }

        private decimal ObtenerPrecioPorLista(short lista, int idarticulo)
        {
            decimal precio = -1;
            var articulo = ArticulosRepository.ObtenerArticulosPorId(idarticulo);
            if (lista == 1 && articulo.PrecioL1 > 0) precio = Convert.ToDecimal(articulo.PrecioL1);
            if (lista == 2 && articulo.PrecioL2 > 0) precio = Convert.ToDecimal(articulo.PrecioL2);
            if (lista == 3 && articulo.PrecioL3 > 0) precio = Convert.ToDecimal(articulo.PrecioL3);
            return precio;
        }

        private void dgvDetalles_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //calcularImportes();
                cbArticulos.Focus();
            }

        }

        private void calcularImportes()
        {
            _subTotal = 0;

            for (int i = 0; i <= Convert.ToInt32(dgvDetalles.Rows.Count - 1); i++)
            {
                dgvDetalles.Rows[i].Cells[5].Value =
                    Convert.ToDecimal(dgvDetalles.Rows[i].Cells[3].Value) *
                    Convert.ToDecimal(dgvDetalles.Rows[i].Cells[4].Value);
                
                _subTotal = _subTotal + Convert.ToDecimal(dgvDetalles.Rows[i].Cells[5].Value);

            }
            txtsubtotal.Text = Convert.ToString(_subTotal);

            if (nudDescuento.Value > 0)
            {
                txtDescuentoPesos.Text = 
                    Convert.ToString(Math.Round((Convert.ToDecimal(txtsubtotal.Text) * 
                    Convert.ToDecimal(nudDescuento.Text)) / 100, 2));
                txtTotal.Text = 
                    Convert.ToString(Math.Round(Convert.ToDecimal(txtsubtotal.Text)-
                    (Convert.ToDecimal(txtsubtotal.Text) * Convert.ToDecimal(nudDescuento.Text))/100, 2));
            }else
            {
                txtDescuentoPesos.Text = "0.00";
                txtTotal.Text = txtsubtotal.Text;
            }
            
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dgvDetalles_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            //COntrola ingreso de valores decimales
            DataGridViewTextBoxEditingControl dText = (DataGridViewTextBoxEditingControl)e.Control;

            dText.KeyPress -= new KeyPressEventHandler(dText_KeyPress);
            dText.KeyPress += new KeyPressEventHandler(dText_KeyPress);
        }
        void dText_KeyPress(object sender, KeyPressEventArgs e)
        {
           ingresaDecimal(sender,e);
        }

        private void ingresaDecimal(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsSeparator(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
            if (e.KeyChar == ',')
            {
                if (((TextBox)sender).Text.Contains(","))
                    e.Handled = true;
                else
                    e.Handled = false;
            }
        }

            
        private void nudDescuento_ValueChanged(object sender, EventArgs e)
        {
            calcularImportes();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.None;
            if (this.ValidarDatos())
            {
                DialogResult = DialogResult.OK;
            }
        }

        private bool ValidarDatos()
        {
            return true;
        }
                
        public DateTime Fecha
        {
            get
            {
                return Convert.ToDateTime(dtpFecha.Value);
            }
        }

        public int DiasValidez
        {
            get
            {
                return Convert.ToInt16(nDias.Value);
            }
        }

        public Decimal ImporteB
        {
            get
            {
                return Convert.ToDecimal(txtsubtotal.Text);
            }
        }

        public Decimal Descuento
        {
            get
            {
                return Convert.ToDecimal(txtDescuentoPesos.Text);
            }
        }

        public Decimal DescPorc
        {
            get
            {
                return Convert.ToDecimal(nudDescuento.Value);
            }
        }

        public Decimal ImporteTotal
        {
            get
            {
                return Convert.ToDecimal(txtTotal.Text);
            }
        }

        public int PrecioLista
        {
            get
            {
                return Convert.ToInt16(cbLista.Text);
            }
        }

        public byte Estado
        {
            get
            {
                return 1; //Revisar
            }
        }

        public int IdUsuario
        {
            get
            {
                //var usuario = UsuariosRepository.ObtenerUsuarioPorId(Lib.Configuration.IdUsuarioConectado);
                //return  usuario.Id;
                return Lib.Configuration.IdUsuarioConectado;
            }
            
        }

        private void cbClientes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var c = ClientesRepository.ObtenerClientePorId(IdCliente);
                txtDireccion.Text = c.Direccion;
                txtDocumento.Text = TiposDocumentoRepository.TiposDocumentoPorId(c.IdTipoDocumento).Descripcion +
                    "  " + c.NroDocumento.ToString().Trim();
            }
        }

        private void dgvDetalles_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            calcularImportes();
        }

        private void dgvDetalles_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            calcularImportes();
        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            using (var dt = ObtenerDatos())
            {
                if (dt.Rows.Count > 0)
                {
                    MostrarReporte(dt);
                }
                else
                {
                    ShowError("No hay ningún registro que coincida con su consulta.");
                }
            }
        }

        private void MostrarReporte(DataTable detalles)
        {
            using (var reporte = new Presupuesto())
            {
                reporte.Database.Tables["Detalles"].SetDataSource(detalles);
                reporte.Database.Tables["Cliente"].SetDataSource(_cliente);
                reporte.Database.Tables["Comprobante"].SetDataSource(_comprobante);
                reporte.Database.Tables["Empresa"].SetDataSource(_empresa);
                using (var f = new frmReporte(reporte)) f.ShowDialog();
            }
        }

        private DataTable ObtenerDatos()
        {
            CargaDatosEmpresa();
            cargaDatosCliente();
            cargaDatosComprobante();
            var detalles = CargarDetalles();
            return detalles;
        }

        private void cargaDatosComprobante()
        {
            var tabla = new dsImpresiones.ComprobanteDataTable();
            string comprobante = "Presupuesto";
            string numero = "1";
            string fecha = "01/01/2019";
            string lista = "Lista 1";
            string validez = "10 días";
            string subTotal = "10,00";
            string descuento = "0,00";
            string Total = "10,00";
            tabla.AddComprobanteRow(comprobante, numero, fecha, lista, validez, subTotal, descuento, Total);
            _comprobante = tabla;
        }

        private void cargaDatosCliente()
        {
            var tabla = new dsImpresiones.ClienteDataTable();
            string razonSocial = "Razón Social";
            string documento = "Documento";
            string dirección = "Dirección";
            tabla.AddClienteRow(razonSocial, documento, dirección);
            _cliente = tabla;
        }

        private DataTable CargarDetalles()
        {
            var tabla = new dsImpresiones.DetallesDataTable(); //new dsImpresiones.AlumnoMorosoDataTable();
            for (int i = 0; i <= Convert.ToInt32(dgvDetalles.Rows.Count - 1); i++)
            {
                string id = Convert.ToString(dgvDetalles.Rows[i].Cells[0].Value);
                string codBarra = Convert.ToString(dgvDetalles.Rows[i].Cells[1].Value);
                string descripcion = Convert.ToString(dgvDetalles.Rows[i].Cells[2].Value);
                string cantidad = Convert.ToString(dgvDetalles.Rows[i].Cells[3].Value);
                string precio = Convert.ToString(dgvDetalles.Rows[i].Cells[4].Value);
                string importe = Convert.ToString(dgvDetalles.Rows[i].Cells[5].Value);

                tabla.AddDetallesRow(id, codBarra, descripcion, cantidad, precio, importe);
            }
            return tabla;
        }

        private void CargaDatosEmpresa()
        {
            var tabla = new dsImpresiones.EmpresaDataTable();
            string nombreFantasía = "Nombre Fantasía";
            string descripcion = "Descripción";
            string razonSocial = "Razón Social";
            string documento = "Documento";
            string dirección = "Dirección";
            string telefono = "Teléfono";
            tabla.AddEmpresaRow(nombreFantasía, descripcion, razonSocial, documento, dirección, telefono);
            _empresa = tabla;
        }
    }
}
