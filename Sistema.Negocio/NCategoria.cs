using Sistema.Datos;
using Sistema.Entidades;
using System.Data;

namespace Sistema.Negocio
{
    public class NCategoria
    {
        /// <summary>
        /// Obtenemos el listado de categorias
        /// </summary>
        /// <returns>Devuelve el listado de categorias</returns>
        public static  DataTable Listar()
        {
            //Instanciamos la capa Datos
            DCategoria Datos = new DCategoria();
            //retornamos lo que nos envie el metodo Listar de la Capa Datos
            return Datos.Listar();
        }

        /// <summary>
        /// Busca un elemento por su Id
        /// </summary>
        /// <param name="Valor">Valor del elemento a buscar</param>
        /// <returns>Retorna una tabla con los elementos encontrados</returns>
        public static  DataTable Buscar(string Valor)
        {
            //Instanciamos la capa Datos
            DCategoria Datos = new DCategoria();
            //retornamos lo que nos envie el metodo Buscar de la Capa Datos
            return Datos.Buscar(Valor);
        }

        /// <summary>
        /// Inserta un elemento en la BBDD
        /// </summary>
        /// <param name="Nombre">Nombre del elemento a insertar</param>
        /// <param name="Descripcion">Descripcion del elemento a insertar</param>
        public static string Insertar(string Nombre, string Descripcion)
        {
            //Instanciamos la capa Datos
            DCategoria Datos = new DCategoria();
            //verificamos si la categoria existe
            string Existe = Datos.Existe(Nombre);
            if (Existe.Equals("1"))
            {
                return "La categoria ya existe";
            }
            else
            {
                //Creamos una instancia a la clase Categoria
                Categoria categoria = new Categoria();
                //Añadimos los valores de las propiedades del objeto Categoria
                categoria.Nombre = Nombre;
                categoria.Descripcion = Descripcion;
                //retornamos lo que nos envie el metodo Buscar de la Capa Datos
                return Datos.Insertar(categoria);
            }
        }

        /// <summary>
        /// Actualiza un elemento por su id
        /// </summary>
        /// <param name="Id">Id del elemento a actualizar</param>
        /// <param name="Nombre">Nombre nuevo del elemento a actualizar</param>
        /// <param name="Descripcion">Descripcion nueva del elemento a actualiza</param>
        /// <returns></returns>
        public static string Actualizar(int Id, string Nombre,string Descripcion)
        {
            //Instanciamos la capa Datos
            DCategoria Datos = new DCategoria();
            //verificamos si la categoria existe
            string Existe = Datos.Existe(Nombre);
            if (Existe.Equals("1"))
            {
                return "La categoria ya existe";
            }
            else
            {
                //Creamos una instancia a la clase Categoria
                Categoria categoria = new Categoria();
                //Añadimos los valores de las propiedades del objeto Categoria
                categoria.IdCategoria = Id;
                categoria.Nombre = Nombre;
                categoria.Descripcion = Descripcion;
                //retornamos lo que nos envie el metodo Buscar de la Capa Datos
                return Datos.Actualizar(categoria);
            }
            
        }

        /// <summary>
        /// Eliminamos un elemento por su Id
        /// </summary>
        /// <param name="Id">Id del elemento a eliminar</param>
        /// <returns></returns>
        public static string Eliminar(int Id)
        {
            //Instanciamos la capa Datos
            DCategoria Datos = new DCategoria();
            //Eliminamos el elemento por su Id
            return Datos.Eliminar(Id);
        }

        /// <summary>
        /// Activamos un elemento por su Id
        /// </summary>
        /// <param name="Id">Id del elemento a Activar </param>
        /// <returns></returns>
        public static string Activar(int Id)
        {
            //Instanciamos la capa Datos
            DCategoria Datos = new DCategoria();
            //Activamos el elemento por su Id
            return Datos.Activar(Id);
        }

        /// <summary>
        /// Desactivamos un elemento por su Id
        /// </summary>
        /// <param name="Id">Id del elemento a Desactivar</param>
        /// <returns></returns>
        public static string Desactivar(int Id)
        {
            //Instanciamos la capa Datos
            DCategoria Datos = new DCategoria();
            //Activamos el elemento por su Id
            return Datos.Desactivar(Id);
        }
    }
}
