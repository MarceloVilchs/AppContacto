using ProcesoCRUD.Datos;
using ProcesoCRUD.Entidades;
using ProcesoCRUD.Presentacion.Reportes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProcesoCRUD.Presentacion
{
    public partial class FrmContactos : Form
    {
        public FrmContactos()
        {
            InitializeComponent();
        }

        #region "Mis Variables"
        int vCodigo_co = 0;
        int vCodigo_ca = 0;
        int nEstadoguarda = 0;
        #endregion

        #region "Mis Metodos"
        //Limpia los campos a rellenar por el usuario
        private void LimpiarTexto()
        {
            txtNombre_co.Clear();
            txtNroMovil_co.Clear();
            txtCorreo_co.Clear();
        }

        //Activamos las cajas de texto
        private void EstadoTexto(bool lEstado)
        {
            txtNombre_co.Enabled = lEstado;
            txtNroMovil_co.Enabled= lEstado;
            txtCorreo_co.Enabled = lEstado;
            dpFechaNac_co.Enabled = lEstado;
            cmbCargos_co.Enabled = lEstado;
        }

        //Ponemos visibilidad a los botones cancelar y guardar
        private void EstadoBotonesprocesos(bool lEstado)
        {
            btnCancelar.Visible = lEstado;
            btnGuardar.Visible = lEstado;
        }

        //Se inhabilitan los botones principales -nuevo,actualizar,eliminar,reporte,salir-
        private void EstadoBotonesprincipales(bool lEstado)
        {
            btnNuevo.Enabled = lEstado;
            btnActualizar.Enabled = lEstado;    
            btnSalir.Enabled = lEstado; 
            btnReporte.Enabled = lEstado;   
            btnEliminar.Enabled = lEstado;
        }

        private void Listado_ca()
        {
            try
            {
                D_Contactos Datos = new D_Contactos();
                //lo que seleccione de la variable cargos_co
                cmbCargos_co.DataSource = Datos.Listado_ca();
                //lo importante es esta info
                cmbCargos_co.ValueMember = "codigo_ca";
                cmbCargos_co.DisplayMember = "descripcion_ca";
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        //esto sirve para darle el tamaño y nombre a las columnas que se trajeron del dgvListado
        private void Formato_co()
        {
            dgvListado.Columns[0].Width = 100;
            dgvListado.Columns[0].HeaderText = "CÓDIGO";
            dgvListado.Columns[1].Width = 130;
            dgvListado.Columns[1].HeaderText = "NOMBRE";
            dgvListado.Columns[2].Width = 100;
            dgvListado.Columns[2].HeaderText = "N° MÓVIL";
            dgvListado.Columns[3].Width = 150;
            dgvListado.Columns[3].HeaderText = "CORREO";
            dgvListado.Columns[4].Width = 110;
            dgvListado.Columns[4].HeaderText = "FECHA NAC.";
            dgvListado.Columns[5].Width = 150;
            dgvListado.Columns[5].HeaderText = "CARGO";
        }

        private void Listado_co(string cTexto)
        {
            try
            {
                D_Contactos Datos = new D_Contactos();
                dgvListado.DataSource = Datos.Listado_co(cTexto);
                this.Formato_co();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void Seleccionar_co()
        {
            if (string.IsNullOrEmpty(Convert.ToString(dgvListado.CurrentRow.Cells["codigo"].Value)))
            {
                MessageBox.Show("Seleccione un registro valido",
                                "Aviso del sistema",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);
            }
            else //si tiene dato seleccionar en tablilla
            {
                vCodigo_co = Convert.ToInt32(dgvListado.CurrentRow.Cells["codigo"].Value);
                txtNombre_co.Text = Convert.ToString(dgvListado.CurrentRow.Cells["nombre"].Value);
                txtNroMovil_co.Text = Convert.ToString(dgvListado.CurrentRow.Cells["nromovil"].Value);
                txtCorreo_co.Text = Convert.ToString(dgvListado.CurrentRow.Cells["correo"].Value);
                dpFechaNac_co.Text = Convert.ToString(dgvListado.CurrentRow.Cells["fechanac"].Value);
                cmbCargos_co.Text = Convert.ToString(dgvListado.CurrentRow.Cells["cargo"].Value);
            }
        }
        #endregion


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pnlTareas_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnReporte_Click(object sender, EventArgs e)
        {
            frmrpt_Contacto oRpt_01 = new frmrpt_Contacto();
            oRpt_01.txt_01.Text = txtBuscar.Text;
            oRpt_01.ShowDialog();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvListado.Rows.Count>0)
            {
                string Rpta = "";
                D_Contactos Datos = new D_Contactos();
                vCodigo_co = Convert.ToInt32(dgvListado.CurrentRow.Cells["codigo"].Value);
                Rpta = Datos.Eliminar_co(vCodigo_co);
                if (Rpta.Equals("OK"))
                {
                    vCodigo_co = 0;
                    this.Listado_co("%");
                    MessageBox.Show("Registro Eliminado",
                                    "Aviso del Sistema",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation);
                }
                else
                {
                    MessageBox.Show( Rpta,
                                    "Aviso del Sistema",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }

            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            nEstadoguarda = 2; //Actualizar el registro
            this.LimpiarTexto();
            this.EstadoTexto(true);
            this.EstadoBotonesprocesos(true);
            this.EstadoBotonesprincipales(false);
            //recibe enfoque del cursor
            txtNombre_co.Focus();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            nEstadoguarda = 1; //Este escenario es para un nuevo registro
            this.LimpiarTexto();
            this.EstadoTexto(true);
            this.EstadoBotonesprocesos(true);
            this.EstadoBotonesprincipales(false);
            //recibe enfoque del cursor
            txtNombre_co.Focus();

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            nEstadoguarda = 0; //Sin actividad
            this.LimpiarTexto();
            this.EstadoTexto(false);
            this.EstadoBotonesprocesos(false);
            this.EstadoBotonesprincipales(true);
        }

        private void FrmContactos_Load(object sender, EventArgs e)
        {
            this.Listado_ca();
            this.Listado_co("%");
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (txtNombre_co.Text==string.Empty ||
                cmbCargos_co.Text==string.Empty)//Validamos si la informacion esta vacia
            {
                MessageBox.Show("Faltan los datos requeridos (*)",
                                "Aviso del Sistema",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);
            }
            else //todo conforme para el proceso guardado
            {
                string Rpta = "";
                //seleccionar variable de tabla cargos y convertirla en int32
                vCodigo_ca = Convert.ToInt32(cmbCargos_co.SelectedValue);

                E_Contactos oPro = new E_Contactos();
                oPro.Codigo_co = vCodigo_co;
                //se llaman a las casillas del programa para recolectar info
                oPro.Nombre_co = txtNombre_co.Text;
                oPro.Nromovil_co = txtNroMovil_co.Text;   
                oPro.Correo_co = txtCorreo_co.Text;
                oPro.Fechanac_co = dpFechaNac_co.Text;
                oPro.Codigo_ca = vCodigo_ca;

                D_Contactos Datos = new D_Contactos();
                Rpta = Datos.Guardar_co(nEstadoguarda, oPro);
                if (Rpta.Equals("OK"))
                {
                    this.LimpiarTexto();
                    this.EstadoTexto(false);
                    this.EstadoBotonesprocesos(false);
                    this.EstadoBotonesprincipales(true);
                    this.Listado_co("%");
                    MessageBox.Show("Los datos han sido guardados correctamente",
                                    "Aviso del sistema",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(Rpta,
                                    "Aviso del sistema",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void dgvListado_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            this.Seleccionar_co();  
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            this.Listado_co(txtBuscar.Text);    
        }

        private void cmbCargos_co_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
