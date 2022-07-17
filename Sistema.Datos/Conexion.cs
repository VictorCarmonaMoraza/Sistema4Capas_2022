using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Datos
{
    public class Conexion
    {
        //Propiedades
        //Nombre de la BBDD a la que nos vamos a conectar
        private string Base;
        //Nombre del servidor donde esta alojada la base de datos
        private string Servidor;
        //Usuario con el que nos conectamos a la BBDD
        private string Usuario;
        //Clave o password con la cual accedemos a base de datos
        private string Clave;
        //Con esto le decimos si utilizamos con la autenticacion de windows o la de sql server
        //Seguridad a TRUE significa con seguridad de Windows y a FALSE con seguridad de SQL Server
        private bool Seguridad;
        //Objeto que instancia a la clase Conexion
        private static Conexion Con = null;

        //Constructor privado porque esta clase no puede ser instanciada desde otra clase
        private Conexion()
        {
            this.Base = "dbsistema";
            this.Servidor = "MSI\\SQLEXPRESS";
            this.Usuario = "sa";
            this.Clave = "1234";
            this.Seguridad = true;
        }

        /// <summary>
        /// MMetodo para crear la conexion 
        /// </summary>
        /// <returns></returns>
        public SqlConnection CrearConexion()
        {
            SqlConnection Cadena = new SqlConnection();
            try
            {
                Cadena.ConnectionString = "Server=" + this.Servidor + "; Database=" + this.Base + ";";
                if (this.Seguridad)
                {
                    Cadena.ConnectionString=Cadena.ConnectionString + "Integrated Security = SSPI";
                }
                else
                {
                    Cadena.ConnectionString = Cadena.ConnectionString + "User Id="+this.Usuario+"; Password="+this.Clave;
                }
            }
            catch (Exception ex)
            {
                Cadena = null;
                throw ex;
                
            }
            return Cadena;
        }

        /// <summary>
        /// Creamos la instancia de la conexion sino esta creada
        /// </summary>
        /// <returns></returns>
        public static Conexion getInstancia()
        {
            //Verificamos si tenemos una instancia de esta clase
            if (Con==null) 
            {
                Con=new Conexion();
            }
            return Con;
        }
    }
}
