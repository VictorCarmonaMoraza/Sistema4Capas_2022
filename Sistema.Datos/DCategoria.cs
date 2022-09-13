using Sistema.Entidades;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Sistema.Datos
{
    public class DCategoria
    {

        /// <summary>
        /// Obtiene todos los elementos de la tabla categoria
        /// </summary>
        /// <returns></returns>
        public DataTable Listar()
        {
            //Proporciona una forma de leer una secuencia de filas de una BBDD
            SqlDataReader Resultado;
            //Representa una tabla en memoria
            DataTable Tabla =new DataTable();
            //Variable para establecer la conexion a BBDD
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon = Conexion.getInstancia().CrearConexion();
                //Representa a una instruccion Trans de SQl o aun procedimiento
                SqlCommand Comando = new SqlCommand("categoria_listar",SqlCon);
                //Le decimos que estamos haciendo referencia a un procedimiento almacenado
                Comando.CommandType = CommandType.StoredProcedure;
                //Abrimos la conexion
                SqlCon.Open();
                //Almacenamos en Resultado la ejecucion del Comando
                Resultado = Comando.ExecuteReader();
                //Cargamos la tabla con el resultado
                Tabla.Load(Resultado);
                return Tabla;
            }
            catch (Exception ex )
            {

                throw ex;
            }
            finally
            {
                //Codigo que siempre se ejecuta
                //Si la conexion esta abierta la cerramos, es una condicion en una sola linea
                if (SqlCon.State == ConnectionState.Open) SqlCon.Close();
            }
        }

        /// <summary>
        /// Obtiene un elemento
        /// </summary>
        /// <param name="Valor">Valor que le paseremos en la busqueda</param>
        /// <returns></returns>
        public DataTable Buscar(string Valor)
        {
            //Proporciona una forma de leer una secuencia de filas de una BBDD
            SqlDataReader Resultado;
            //Representa una tabla en memoria
            DataTable Tabla = new DataTable();
            //Variable para establecer la conexion a BBDD
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon = Conexion.getInstancia().CrearConexion();
                //Representa a una instruccion Trans de SQl o aun procedimiento
                SqlCommand Comando = new SqlCommand("categoria_buscar", SqlCon);
                //Le decimos que estamos haciendo referencia a un procedimiento almacenado
                Comando.CommandType = CommandType.StoredProcedure;
                //Añadimos el parametro en la busqueda
                Comando.Parameters.Add("@Valor",SqlDbType.VarChar).Value=Valor;
                //Abrimos la conexion
                SqlCon.Open();
                //Almacenamos en Resultado la ejecucion del Comando
                Resultado = Comando.ExecuteReader();
                //Cargamos la tabla con el resultado
                Tabla.Load(Resultado);
                return Tabla;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                //Codigo que siempre se ejecuta
                //Si la conexion esta abierta la cerramos, es una condicion en una sola linea
                if (SqlCon.State == ConnectionState.Open) SqlCon.Close();
            }
        }

        /// <summary>
        /// Conmprueba si una categora existe en funcion de su valor
        /// </summary>
        /// <param name="Valor">valor de la categoria</param>
        /// <returns></returns>
        public string Existe(string Valor)
        {
            string Rpta = "";
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon = Conexion.getInstancia().CrearConexion();
                SqlCommand Comando = new SqlCommand("categoria_existe", SqlCon);
                //Le decimos que vamos a ejecutar un procedimiento almacenado
                Comando.CommandType = CommandType.StoredProcedure;
                //
                Comando.Parameters.Add("@valor", SqlDbType.VarChar).Value = Valor;
                //Parametro de salida
                SqlParameter ParExiste = new SqlParameter();
                //Enlace de paraemtro de salida, existe del procedimiento almacenado
                //Le indico que el parametro existe es de tipo entero
                ParExiste.SqlDbType = SqlDbType.Int;
                //Le decimos que es un parametro de salida
                ParExiste.Direction = ParameterDirection.Output;
                //Agrego el parametro al comando
                Comando.Parameters.Add(ParExiste);
                ParExiste.ParameterName = "@existe";
                //Abrimos la conexion
                SqlCon.Open();
                //Ejecutamos el comando
                Comando.ExecuteNonQuery();
                //Convertimos el parametro en string, ya que respuesta es de tipo String
                Rpta = Convert.ToString(ParExiste.Value);
            }
            catch (Exception ex)
            {
                //Respondemos el mensaje de la excepcion
                Rpta = ex.Message;
            }
            finally
            {
                //Codigo que siempre se ejecuta
                //Si la conexion esta abierta la cerramos, es una condicion en una sola linea
                if (SqlCon.State == ConnectionState.Open) SqlCon.Close();
            }
            return Rpta;
        }

        /// <summary>
        /// Insertamos una categoria en BBDD
        /// </summary>
        /// <param name="Obj">Objeto categoria que insertamos</param>
        /// <returns></returns>
        public string Insertar(Categoria Obj)
        {
            string Rpta = "";
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon = Conexion.getInstancia().CrearConexion();
                SqlCommand Comando = new SqlCommand("categoria_insertar", SqlCon);
                //Le decimos que vamos a ejecutar un procedimiento almacenado
                Comando.CommandType = CommandType.StoredProcedure;
                //
                Comando.Parameters.Add("@nombre", SqlDbType.VarChar).Value = Obj.Nombre;
                Comando.Parameters.Add("@descripcion", SqlDbType.VarChar).Value = Obj.Descripcion;
                //Abrimos la conexion
                SqlCon.Open();
                //Si todo correcto se nos manda un ok en caso contrario es un error
                Rpta = Comando.ExecuteNonQuery() == 1 ? "OK" : "No se pudo ingresar el registro";
            }
            catch (Exception ex)
            {
                //Respondemos el mensaje de la excepcion
                Rpta=ex.Message;
            }
            finally
            {
                //Codigo que siempre se ejecuta
                //Si la conexion esta abierta la cerramos, es una condicion en una sola linea
                if (SqlCon.State == ConnectionState.Open) SqlCon.Close();
            }
            return Rpta;
        }


        /// <summary>
        /// Actualizamos una categoria por su id
        /// </summary>
        /// <param name="Obj">objeto a actualizar</param>
        /// <returns></returns>
        public string Actualizar(Categoria Obj)
        {
            string Rpta = "";
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon = Conexion.getInstancia().CrearConexion();
                SqlCommand Comando = new SqlCommand("categoria_actualizar", SqlCon);
                //Le decimos que vamos a ejecutar un procedimiento almacenado
                Comando.CommandType = CommandType.StoredProcedure;
                //
                Comando.Parameters.Add("@idcategoria", SqlDbType.Int).Value = Obj.IdCategoria;
                Comando.Parameters.Add("@nombre", SqlDbType.VarChar).Value = Obj.Nombre;
                Comando.Parameters.Add("@descripcion", SqlDbType.VarChar).Value = Obj.Descripcion;
                
                //Abrimos la conexion
                SqlCon.Open();
                //Si todo correcto se nos manda un ok en caso contrario es un error
                Rpta = Comando.ExecuteNonQuery() == 1 ? "OK" : "No se pudo actualizar el registro";
            }
            catch (Exception ex)
            {
                //Respondemos el mensaje de la excepcion
                Rpta = ex.Message;
            }
            finally
            {
                //Codigo que siempre se ejecuta
                //Si la conexion esta abierta la cerramos, es una condicion en una sola linea
                if (SqlCon.State == ConnectionState.Open) SqlCon.Close();
            }
            return Rpta;
        }

        /// <summary>
        /// Elimina una categoria por su Id 
        /// </summary>
        /// <param name="Id">Id de la categoria a eliminar</param>
        /// <returns></returns>
        public string Eliminar(int Id)
        {
            string Rpta = "";
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon = Conexion.getInstancia().CrearConexion();
                SqlCommand Comando = new SqlCommand("categoria_eliminar", SqlCon);
                //Le decimos que vamos a ejecutar un procedimiento almacenado
                Comando.CommandType = CommandType.StoredProcedure;
                //
                Comando.Parameters.Add("@idcategoria", SqlDbType.Int).Value = Id;
                //Abrimos la conexion
                SqlCon.Open();
                //Si todo correcto se nos manda un ok en caso contrario es un error
                Rpta = Comando.ExecuteNonQuery() == 1 ? "OK" : "No se pudo eliminar el registro";
            }
            catch (Exception ex)
            {
                //Respondemos el mensaje de la excepcion
                Rpta = ex.Message;
            }
            finally
            {
                //Codigo que siempre se ejecuta
                //Si la conexion esta abierta la cerramos, es una condicion en una sola linea
                if (SqlCon.State == ConnectionState.Open) SqlCon.Close();
            }
            return Rpta;
        }

        public string Activar(int Id)
        {
            string Rpta = "";
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon = Conexion.getInstancia().CrearConexion();
                SqlCommand Comando = new SqlCommand("categoria_activar", SqlCon);
                //Le decimos que vamos a ejecutar un procedimiento almacenado
                Comando.CommandType = CommandType.StoredProcedure;
                //
                Comando.Parameters.Add("@idcategoria", SqlDbType.Int).Value = Id;
                //Abrimos la conexion
                SqlCon.Open();
                //Si todo correcto se nos manda un ok en caso contrario es un error
                Rpta = Comando.ExecuteNonQuery() == 1 ? "OK" : "No se pudo activar el registro";
            }
            catch (Exception ex)
            {
                //Respondemos el mensaje de la excepcion
                Rpta = ex.Message;
            }
            finally
            {
                //Codigo que siempre se ejecuta
                //Si la conexion esta abierta la cerramos, es una condicion en una sola linea
                if (SqlCon.State == ConnectionState.Open) SqlCon.Close();
            }
            return Rpta;
        }

        public string Desactivar(int Id)
        {
            string Rpta = "";
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon = Conexion.getInstancia().CrearConexion();
                SqlCommand Comando = new SqlCommand("categoria_activar", SqlCon);
                //Le decimos que vamos a ejecutar un procedimiento almacenado
                Comando.CommandType = CommandType.StoredProcedure;
                //
                Comando.Parameters.Add("@idcategoria", SqlDbType.Int).Value = Id;
                //Abrimos la conexion
                SqlCon.Open();
                //Si todo correcto se nos manda un ok en caso contrario es un error
                Rpta = Comando.ExecuteNonQuery() == 1 ? "OK" : "No se pudo desactivarmarche el registro";
            }
            catch (Exception ex)
            {
                //Respondemos el mensaje de la excepcion
                Rpta = ex.Message;
            }
            finally
            {
                //Codigo que siempre se ejecuta
                //Si la conexion esta abierta la cerramos, es una condicion en una sola linea
                if (SqlCon.State == ConnectionState.Open) SqlCon.Close();
            }
            return Rpta;
        }
    }
}
