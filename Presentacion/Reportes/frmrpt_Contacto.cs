using Microsoft.Reporting.WinForms;
using ProcesoCRUD.Datos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProcesoCRUD.Presentacion.Reportes
{
    public partial class frmrpt_Contacto : Form
    {
        public frmrpt_Contacto()
        {
            InitializeComponent();
        }


        #region "Mis Metodos"
        private void Listado()
        {
            try
            {
                D_Contactos Datos = new D_Contactos();
                string cTexto = txt_01.Text;
                DataTable miTabla= new DataTable();
                miTabla = Datos.Listado_co(cTexto);
                ReportDataSource fuente = new ReportDataSource("DataSet1", miTabla);
                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.LocalReport.DataSources.Add(fuente);
                reportViewer1.LocalReport.ReportEmbeddedResource = "ProcesoCRUD.Presentacion.Reportes.Rpt_Contactos.rdlc";
                reportViewer1.LocalReport.Refresh();
                reportViewer1.Refresh();
                reportViewer1.RefreshReport();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion
        private void frmrpt_Contacto_Load(object sender, EventArgs e)
        {
            this.Listado();
            //this.reportViewer1.RefreshReport();
        }
    }
}
