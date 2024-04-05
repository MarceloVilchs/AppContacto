using Npgsql;
using ProcesoCRUD.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ProcesoCRUD.Datos
{
    internal class D_Contactos
    {
        //metodo para obtener un listado desde la base de datos
        public DataTable Listado_ca()
        {
            NpgsqlDataReader Resultado; //se declara para almacenar el resultado de la columna
            DataTable Tabla=new DataTable(); //se crea para almacenar los resultados
            NpgsqlConnection SqlCon = new NpgsqlConnection(); //se declara un NpgsqlConnection para la conexion con la base de datos
            try
            {
                // Obtiene una instancia de la conexión a la base de datos
                SqlCon = Conexion.getInstancia().CrearConexion();
                // Se crea un comando sql para obtener los datos requeridos
                NpgsqlCommand Comando = 
                    new NpgsqlCommand("SELECT descripcion_ca, codigo_ca from tb_cargos where activo=true;", SqlCon);

                Comando.CommandType = CommandType.Text;
                SqlCon.Open(); // Abre la conexion a la bbdd
                Resultado = Comando.ExecuteReader(); // Ejecuta la consulta y almacena el resultado
                Tabla.Load(Resultado);  // Carga los resultados en el DataTable
                return Tabla;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // Cierra la conexión a la base de datos en caso de estar abierta
                if (SqlCon.State==ConnectionState.Open) SqlCon.Close();
            }
        }

        public DataTable Listado_co(string cTexto)
        {
            NpgsqlDataReader Resultado; //se declara para almacenar el resultado de la columna
            DataTable Tabla = new DataTable(); //se crea para almacenar los resultados
            NpgsqlConnection SqlCon = new NpgsqlConnection(); //se declara un NpgsqlConnection para la conexion con la base de datos
            try
            {
                // Obtiene una instancia de la conexión a la base de datos
                SqlCon = Conexion.getInstancia().CrearConexion();
                // Se crea un comando sql para obtener los datos requeridos
                NpgsqlCommand Comando =
                    new NpgsqlCommand("select (func_listado_co('"+cTexto+"')).*", SqlCon);

                Comando.CommandType = CommandType.Text;
                SqlCon.Open(); // Abre la conexion a la bbdd
                Resultado = Comando.ExecuteReader(); // Ejecuta la consulta y almacena el resultado
                Tabla.Load(Resultado);  // Carga los resultados en el DataTable
                return Tabla;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // Cierra la conexión a la base de datos en caso de estar abierta
                if (SqlCon.State == ConnectionState.Open) SqlCon.Close();
            }
        }

        public string Guardar_co(int nOpcion, E_Contactos oPro)
        {
            string Rpta = "";
            string SQL = "";
            string SentenciaSQL01 = "insert into tb_contactos(nombre_co, nromovil_co, correo_co, fechanac_co, codigo_ca)" +
                                  " values('" + oPro.Nombre_co+ "','"+oPro.Nromovil_co+"','"+oPro.Correo_co+ "','"+oPro.Fechanac_co+ "','"+oPro.Codigo_ca+"')";
            string SentenciaSQL02 = "update tb_contactos set nombre_co='" + oPro.Nombre_co + "'," +
                                    " nromovil_co='" + oPro.Nromovil_co + "'," +
                                    " correo_co = '" + oPro.Correo_co + "'," +
                                    " fechanac_co='" + oPro.Fechanac_co + "'," +
                                    " codigo_ca='" + oPro.Codigo_ca + "'" +
                                    " where codigo_co='" + oPro.Codigo_co + "'";
            SQL = nOpcion ==1 ? SentenciaSQL01 : SentenciaSQL02;

            NpgsqlConnection SqlCon = new NpgsqlConnection(); //se declara un NpgsqlConnection para la conexion con la base de datos
            try
            {
                // Obtiene una instancia de la conexión a la base de datos
                SqlCon = Conexion.getInstancia().CrearConexion();
                NpgsqlCommand Comando = new NpgsqlCommand(SQL, SqlCon);
                Comando.CommandType = CommandType.Text;
                SqlCon.Open();
                Rpta = Comando.ExecuteNonQuery() >= 1 ? "OK" : "No se pudo registrar la información" ;
            }
            catch (Exception ex)
            {

                Rpta=ex.Message;
            }
            finally
            {
                if(SqlCon.State == ConnectionState.Open) SqlCon.Close ();
            }
            return Rpta;
        }

        public string Eliminar_co(int nCodigo_co)
        {
            string Rpta = "";

            string SQL = "update tb_contactos set activo=false where codigo_co='"+nCodigo_co+"'";

            NpgsqlConnection SqlCon = new NpgsqlConnection(); //se declara un NpgsqlConnection para la conexion con la base de datos
            try
            {
                // Obtiene una instancia de la conexión a la base de datos
                SqlCon = Conexion.getInstancia().CrearConexion();
                NpgsqlCommand Comando = new NpgsqlCommand(SQL, SqlCon);
                Comando.CommandType = CommandType.Text;
                SqlCon.Open();
                Rpta = Comando.ExecuteNonQuery() >= 1 ? "OK" : "No se pudo eliminar la informacion";
            }
            catch (Exception ex)
            {

                Rpta = ex.Message;
            }
            finally
            {
                if (SqlCon.State == ConnectionState.Open) SqlCon.Close();
            }
            return Rpta;
        }
    }
}
