using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcesoCRUD.Datos
{
    public class Conexion
    {
        //definicion de variables
        private string Basededatos;
        private string Servidor;
        private string Puerto;
        private string Usuario;
        private string Clave;
        private static Conexion Con = null;

        //carga de informacion
        public Conexion()
        {
            this.Basededatos = "bd_crud";
            this.Servidor = "localhost";
            this.Puerto = "5432";
            this.Usuario = "user_CRUD";
            this.Clave = "123";

        }

        //metodos para la conexion sql
        public NpgsqlConnection CrearConexion()
        {
            NpgsqlConnection Cadena = new NpgsqlConnection();
            try
            {
                //info de la base de datos pgsql
                Cadena.ConnectionString = "Server=" + this.Servidor +
                                          ";Port=" + this.Puerto +
                                          ";User id=" + this.Usuario +
                                          ";Password=" + this.Clave +
                                          ";Database=" + this.Basededatos;
            }
            catch (Exception ex)
            {
                Cadena = null;
                throw ex;
            }
            return Cadena;
        }

        //validar si hay una conexion abierta o si se encuentra en uso
        public static Conexion getInstancia()
        {
            if(Con == null)
            {
                Con = new Conexion();
                
            }
            return Con;
        }
    }
}
