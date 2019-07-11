﻿using ERP.Lib.AppForms;
using ERP.Lib.Validations;
using ERP.Repositories;
using ERP.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ERP.Forms.Proveedores
{
    public partial class frmEdicion : FormBase
    {
        public frmEdicion()
        {
            InitializeComponent();

            this.Text = "Nuevo Proveedor";
            txtRazonSocial.Select();
            _validator = new FormValidations(this, errorProvider1);
            CargarProvincias();
            CargarTiposDocumento();
            InicializarDomicilio();
            ckEstado.Checked = true;
        }

        private void InicializarDomicilio()
        {
            Action<ComboBox, string> seleccionar = (cb, s) => {
                for (int i = 0; i < cb.Items.Count; i++)
                {
                    if (cb.GetItemText(cb.Items[i]).Contains(s))
                    {
                        cb.SelectedIndex = i;
                        break;
                    }
                }
            };
            seleccionar(cbProvincia, "Santiago");
            CargarDepartamentos(IdProvincia);
            //seleccionar(cbDepartamento, "Capital");
            //CargarLocalidades(IdDepartamento);
            //seleccionar(cbLocalidad, "Capital");
            //cbBarrio.SelectedIndex = -1;
        }

        public frmEdicion(Models.Proveedores proveedor) : this()
        {
            this.Text = "Edición del Proveedor";
            txtRazonSocial.Text = proveedor.RazonSocial;
            cbTipoDoc.SelectedIndex = proveedor.IdTipoDocumento - 1;
            txtNroDocumento.Text = proveedor.NroDocumento.ToString();
            dtpFechaNac.Text = proveedor.FechaNacimiento.ToString();
            txtEmail.Text = proveedor.EMail;
            txtTelefono.Text = proveedor.Telefono;
            txtDireccion.Text = proveedor.Direccion;
            CargarDomicilio(proveedor.IdDomicilio);
            ckEstado.Checked = proveedor.Estado == 1;
        }

        private void CargarDomicilio(int? idDomicilio)
        {
            var d = ProveedoresRepository.ObtenerDomicilio(idDomicilio);

            if (d == null)
            {
                cbProvincia.SelectedIndex = -1;
                cbDepartamento.SelectedIndex = -1;
                cbLocalidad.SelectedIndex = -1;
                cbBarrio.SelectedIndex = -1;
            }
            else
            {
                cbProvincia.SelectedValue = d.IdProvincia;

                CargarDepartamentos(d.IdProvincia);
                cbDepartamento.SelectedValue = d.IdDepartamento;

                CargarLocalidades(d.IdDepartamento);
                cbLocalidad.SelectedValue = d.IdLocalidad;

                CargarBarrios(d.IdLocalidad);
                cbBarrio.SelectedValue = d.IdBarrio;
            }
        }


        private void CargarProvincias()
        {
            var p = ProvinciasRepository.ObtenerProvincias();
            cbProvincia.DataSource = p;
            cbProvincia.DisplayMember = "Nombre";
            cbProvincia.ValueMember = "Id";
            //if (p.Any())
            //{
            //    //cbProvincia.SelectedIndex = 0;
            //    CargarDepartamentos(IdProvincia);
            //}
        }

        private void CargarDepartamentos(int idProvincia)
        {
            var d = DepartamentosRepository.ObtenerDepartamentosPorProvincia(idProvincia);
            cbDepartamento.DataSource = d;
            cbDepartamento.DisplayMember = "Nombre";
            cbDepartamento.ValueMember = "Id";
            if (d.Any()) cbDepartamento.SelectedIndex = 0;
            CargarLocalidades(IdDepartamento);
        }

        private void CargarLocalidades(int idDepartamento)
        {
            var l = LocalidadesRepository.ObtenerLocalidadesPorDepartamento(idDepartamento);
            cbLocalidad.DataSource = l;
            cbLocalidad.DisplayMember = "Nombre";
            cbLocalidad.ValueMember = "Id";
            if (l.Any()) cbLocalidad.SelectedIndex = 0;
            CargarBarrios(IdLocalidad);
        }

        private void CargarBarrios(int idLocalidad)
        {
            var b = BarriosRepository.ObtenerBarriosPorLocalidad(idLocalidad);
            cbBarrio.DataSource = b;
            cbBarrio.DisplayMember = "Nombre";
            cbBarrio.ValueMember = "Id";
            if (b.Any()) cbBarrio.SelectedIndex = 0;
        }

        private void CargarTiposDocumento()
        {
            var td = TiposDocumentoRepository.ObtenerTiposDocumento();
            cbTipoDoc.DataSource = td;
            cbTipoDoc.DisplayMember = "Descripcion";
            cbTipoDoc.ValueMember = "Id";
            if (td.Any()) cbTipoDoc.SelectedIndex = 0;
        }

        private bool ValidarDatos()
        {
            return
            //_validator.Validar(txtRazonSocial, !String.IsNullOrEmpty(RazonSocial), "No puede estar vacío") &&                
            //_validator.Validar(txtNroDocumento, NroDocumento > 0, "No puede ser menor o igual que cero");
            _validator.ValidarMaterial(!String.IsNullOrEmpty(RazonSocial), "No puede estar vacío") &&
            _validator.ValidarMaterial(NroDocumento > 0, "No puede ser menor o igual que cero");
        }

        public string RazonSocial
        {
            get
            {
                return txtRazonSocial.Text.Trim();
            }
        }

        public decimal NroDocumento
        {
            get
            {
                //return txtNroDocumento.DecValue;
                return Convert.ToDecimal(txtNroDocumento.Text.Trim());
            }
        }

        public byte Estado
        {
            get
            {
                return (byte)(ckEstado.Checked ? 1 : 0);
            }
        }

        public DateTime FechaNacimiento
        {
            get
            {
                return dtpFechaNac.Value;
            }
        }

        public string Email
        {
            get
            {
                return txtEmail.Text.Trim();
            }
        }

        public string Dirección
        {
            get
            {
                return txtDireccion.Text.Trim();
            }
        }

        public string Teléfono
        {
            get
            {
                return txtTelefono.Text.Trim();
            }
        }

        public int IdTipoDocumento
        {
            get
            {
                return Convert.ToInt32(cbTipoDoc.SelectedValue);
            }
        }

        public Domicilios Domicilio
        {
            get
            {
                if (IdProvincia == 0 || IdDepartamento == 0 || IdLocalidad == 0 || IdBarrio == 0)
                {
                    return null;
                }
                var dom = new Domicilios();
                dom.IdProvincia = IdProvincia;
                dom.IdDepartamento = IdDepartamento;
                dom.IdLocalidad = IdLocalidad;
                dom.IdBarrio = IdBarrio;
                return dom;
            }
        }

        public int IdProvincia
        {
            get
            {
                return Convert.ToInt32(cbProvincia.SelectedValue);
            }
        }

        public int IdDepartamento
        {
            get
            {
                return Convert.ToInt32(cbDepartamento.SelectedValue);
            }
        }

        public int IdLocalidad
        {
            get
            {
                return Convert.ToInt32(cbLocalidad.SelectedValue);
            }
        }

        public int IdBarrio
        {
            get
            {
                return Convert.ToInt32(cbBarrio.SelectedValue);
            }
        }

        private void cbProvincia_SelectionChangeCommitted(object sender, EventArgs e)
        {
            CargarDepartamentos(IdProvincia);
        }

        private void cbDepartamento_SelectionChangeCommitted(object sender, EventArgs e)
        {
            CargarLocalidades(IdDepartamento);
        }

        private void cbLocalidad_SelectionChangeCommitted(object sender, EventArgs e)
        {
            CargarBarrios(IdLocalidad);
        }

        private void btnDepartamentos_Click(object sender, EventArgs e)
        {
            using (var f = new frmInputQuery("Nuevo departamento", "Nuevo departamento de " + cbProvincia.Text + ":"))
            {
                if (f.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        DepartamentosRepository.Insertar(IdProvincia, f.Descripción.Trim());
                        CargarDepartamentos(IdProvincia);
                    }
                    catch (Exception ex)
                    {
                        ShowError("Error al intentar grabar los datos: \n" + ex.Message);
                    }
                }
            }
        }

        private void btnLocalidad_Click(object sender, EventArgs e)
        {
            using (var f = new frmInputQuery("Nueva localidad", "Nueva localidad de " + cbDepartamento.Text + ":"))
            {
                if (f.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        LocalidadesRepository.Insertar(IdDepartamento, f.Descripción.Trim());
                        CargarLocalidades(IdDepartamento);
                    }
                    catch (Exception ex)
                    {
                        ShowError("Error al intentar grabar los datos: \n" + ex.Message);
                    }
                }
            }
        }

        private void btnBarrio_Click(object sender, EventArgs e)
        {
            var s = String.Format("Nuevo barrio de {0}, {1}:", cbLocalidad.Text, cbDepartamento.Text);
            using (var f = new frmInputQuery("Nuevo barrio", s))
            {
                if (f.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        BarriosRepository.Insertar(IdLocalidad, f.Descripción.Trim());
                        CargarBarrios(IdLocalidad);
                    }
                    catch (Exception ex)
                    {
                        ShowError("Error al intentar grabar los datos: \n" + ex.Message);
                    }
                }
            }
        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.None;
            if (this.ValidarDatos())
            {
                DialogResult = DialogResult.OK;
            }
        }

        private void materialRaisedButton2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
