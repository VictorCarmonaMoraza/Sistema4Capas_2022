using Sistema.Negocio;
using System;
using System.Windows.Forms;

namespace Sistema.Presentacion
{
    public partial class FrmCategoria : Form
    {
        private string NombreAnt;

        public FrmCategoria()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Carga el DatagridView con los datos de la capa negocio
        /// </summary>
        private void Listar()
        {
            try
            {
                //Cargamos el datagridview con los datos obtenidos de la capa negocio
                DgvListado.DataSource = NCategoria.Listar();
                //Llamamos al metodo formato
                this.Formato();
                //Limpiamos los controles y ocultamos el boton Actualizar
                this.Limpiar();
                //Mostramos el total de resgitros obtenidos desde BBDD
                LblTotal.Text = "Total registros: " + Convert.ToString(DgvListado.Rows.Count);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        /// <summary>
        /// Metod para establecer un formato al DataGridView
        /// </summary>
        private void Formato()
        {
            //Primera columna por eso el indice cero
            DgvListado.Columns[0].Visible = false;
            //Ocultamos la segunda columna que es el id
            DgvListado.Columns[1].Visible = false;
            //Indicamos un ancho a las columnas
            //Columnna con el indice 2 es el nombre
            DgvListado.Columns[2].Width = 150;
            //Columnna con el indice 3 es la descripcion
            DgvListado.Columns[3].Width = 300;
            //Modifico elñ texto de la cabecera porque desde BBDD viene sin tilde descripcion asi que la modifico aqui
            DgvListado.Columns[3].HeaderText = "Descripción";
            //Columnna con el indice 4 es el estado
            DgvListado.Columns[4].Width = 100;
        }

        /// <summary>
        /// Obteiene el elemento a buscar
        /// </summary>
        private void Buscar()
        {
            try
            {
                //Cargamos el datagridview con los datos obtenidos de la capa negocio
                DgvListado.DataSource = NCategoria.Buscar(TxtBuscar.Text);
                //Llamamos al metodo formato
                this.Formato();
                //Mostramos el total de resgitros obtenidos desde BBDD
                LblTotal.Text = "Total registros: " + Convert.ToString(DgvListado.Rows.Count);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        /// <summary>
        /// Inicializa el Formulario de Categoria(FrmCategoria)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmCategoria_Load(object sender, EventArgs e)
        {
            this.Listar();
        }

        //private void DgvListado_CellContentClick(object sender, DataGridViewCellEventArgs e)
        //{

        //}

        /// <summary>
        /// Busca el elemento introducido 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            this.Buscar();
        }

        private void Limpiar()
        {
            //Dejamos la caja de texto de buscar vacia
            TxtBuscar.Clear();
            //Dejamos la caja de texto de Nombre vacia
            TxtNombre.Clear();
            //Dejamos la caja de texto de Id vacia
            TxtId.Clear();
            //Dejamos la caja de texto de Descripcion vacia
            TxtDescripcion.Clear();
            //Hacemos visible el boton Insertar
            BtnInsertar.Visible = true;
            //Ocultamos el boton Actualizar
            BtnActualizar.Visible = false;
            //Limpiamos la validacion de la caja de texto
            ErrorIcono.Clear();

            //La columna seleccionar estara visible
            DgvListado.Columns[0].Visible = false;
            //El boton Activar tambien estara visible
            BtnActivar.Visible = false;
            //el boton Desactivar tambien estara visible
            BtnDesactivar.Visible = false;
            //El boton Eliminar estara visible
            BtnEliminar.Visible = false;
            ChkSeleccionar.Checked = false;
        }

        private void BtnInsertar_Click(object sender, EventArgs e)
        {
            try
            {
                string Rpta = "";
                //Validamos si esta vacio la caja de texto TxtNombre
                if(TxtNombre.Text == string.Empty)
                {
                    //Llamamos a n uestro metodo parta mostrar el mensaje de error
                    this.MensajeError("Falta ingresar algunos datos, seran remarcados.");
                    //
                    ErrorIcono.SetError(TxtNombre, "Ingrese un nombre.");
                }
                else
                {
                    //Llamamos a la capa de negocio al metodo insertar pasandole el Nombre y la descripcion
                    //.Trim elimina los espacio en blanco por delante y por detras
                    Rpta = NCategoria.Insertar(TxtNombre.Text.Trim(),TxtDescripcion.Text.Trim());
                    //validamos que la respuesta de la insercion es OK
                    if (Rpta.Equals("OK"))
                    {
                        this.MensajeOK("Se inserto de forma correcta el registro");
                        //Dejamos despues de insertar todas las cajas de texto en blanco
                        this.Limpiar();
                        //Llamamos al metodo Listar
                        this.Listar();
                    }
                    else //Si respuesta no es un OK es que tenemos un error
                    {
                        this.MensajeError(Rpta);
                    }
                }
            }
            catch (Exception ex)
            {

                //Mostramos la excepcion en un MessageBox
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        /// <summary>
        /// Muestra el mensaje de error en un MessageBox
        /// </summary>
        /// <param name="Mensaje">Mensjae con el error correspodiente</param>
        private void MensajeError(string Mensaje)
        {
            //Mostramos el mensaje de error, Mensaje sera el mensaje del error, nombre de la cebecera 
            //de la caja de texto, el mensaje del boton sera de OK y sera con un icono de error
            MessageBox.Show(Mensaje, "Sistema de ventas",MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Muestra el mensaje de ok en un MessageBox
        /// </summary>
        /// <param name="Mensaje">Mensjae con la informacion correspodiente</param>
        private void MensajeOK(string Mensaje)
        {
            //Mostramos el mensaje de error, Mensaje sera el mensaje del error, nombre de la cebecera 
            //de la caja de texto, el mensaje del boton sera de OK y sera con un icono de informacion
            MessageBox.Show(Mensaje, "Sistema de ventas", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Cuando hacemos click sobre el boton Cancelar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            //Limpiamos los controles
            this.Limpiar();
            //Seleccionamos el indice cero es decir que visualice el listado, el mantenimiento es el indice 1
            TabGeneral.SelectedIndex = 0;
        }

        private void DgvListado_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            try
            {
                //Limpiamos los campos
                this.Limpiar();
                //Cambiamos para que el boton Actualizar sea Visible
                BtnActualizar.Visible = true;
                //Ocultamos boton de Insertar
                BtnInsertar.Visible = false;
                //Obtenemos el valor de la celda ID, debemos pasarlo a String porque lo que nos devuelve es un String
                TxtId.Text = Convert.ToString(DgvListado.CurrentRow.Cells["ID"].Value);
                //Obtenemos el valor de la celda Nombre, debemos pasarlo a String porque lo que nos devuelve es un String
                NombreAnt = Convert.ToString(DgvListado.CurrentRow.Cells["Nombre"].Value);
                //Obtenemos el valor de la celda Nombre, debemos pasarlo a String porque lo que nos devuelve es un String
                TxtNombre.Text = Convert.ToString(DgvListado.CurrentRow.Cells["Nombre"].Value);
                //Obtenemos el valor de la celda Descripcion, debemos pasarlo a String porque lo que nos devuelve es un String
                TxtDescripcion.Text = Convert.ToString(DgvListado.CurrentRow.Cells["Descripcion"].Value);
                //Cuando çhacemos doble click sobre el elemento que se nos abra la pestaña de Mantenimiento con los datos del
                //registro seleccionado para poder editarlo
                TabGeneral.SelectedIndex = 1;
            }
            catch (Exception )
            {
                MessageBox.Show("Seleccione desde la celda nombre.");
            }
            
        }

        private void BtnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                string Rpta = "";
                //Validamos si esta vacio la caja de texto TxtNombre y el Id porque si esta vacio es que es un elemento nuevo
                if (TxtNombre.Text == string.Empty || TxtId.Text == String.Empty)
                {
                    //Llamamos a n uestro metodo parta mostrar el mensaje de error
                    this.MensajeError("Falta ingresar algunos datos, seran remarcados.");
                    ErrorIcono.SetError(TxtNombre, "Ingrese un nombre.");
                }
                else
                {
                    //Llamamos a la capa de negocio al metodo insertar pasandole el Nombre y la descripcion
                    //.Trim elimina los espacio en blanco por delante y por detras
                    Rpta = NCategoria.Actualizar(Convert.ToInt32(TxtId.Text),this.NombreAnt, TxtNombre.Text.Trim(), TxtDescripcion.Text.Trim());
                    //validamos que la respuesta de la insercion es OK
                    if (Rpta.Equals("OK"))
                    {
                        this.MensajeOK("Se actualizo de forma correcta el registro");
                        //Dejamos despues de insertar todas las cajas de texto en blanco
                        this.Limpiar();
                        //Llamamos al metodo Listar
                        this.Listar();
                    }
                    else //Si respuesta no es un OK es que tenemos un error
                    {
                        this.MensajeError(Rpta);
                    }
                }
            }
            catch (Exception ex)
            {

                //Mostramos la excepcion en un MessageBox
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void ChkSeleccionar_CheckedChanged(object sender, EventArgs e)
        {
            //Si el checkBox Seleccionar esta marcado
            if (ChkSeleccionar.Checked)
            {
                //La columna seleccionar estara visible
                DgvListado.Columns[0].Visible = true;
                //El boton Activar tambien estara visible
                BtnActivar.Visible = true;
                //el boton Desactivar tambien estara visible
                BtnDesactivar.Visible = true;
                //El boton Eliminar estara visible
                BtnEliminar.Visible = true;
            }
            else
            {
                //La columna seleccionar estara visible
                DgvListado.Columns[0].Visible = false;
                //El boton Activar tambien estara visible
                BtnActivar.Visible = false;
                //el boton Desactivar tambien estara visible
                BtnDesactivar.Visible = false;
                //El boton Eliminar estara visible
                BtnEliminar.Visible = false;
            }
        }

        private void DgvListado_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //Si he seleccionado una celda de la columna seleccionar
            if (e.ColumnIndex == DgvListado.Columns["Seleccionar"].Index)
            {
                //El objeto ChkEliminar le saignamos la celda seleccionada
                DataGridViewCheckBoxCell ChkEliminar = (DataGridViewCheckBoxCell) DgvListado.Rows[e.RowIndex].Cells["Seleccionar"];
                //
                ChkEliminar.Value = !Convert.ToBoolean(ChkEliminar.Value);
            }
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult Opcion;
                //Mostramos un cuadro de dialogo con dos botones, uno de OK y otro de Cancel
                Opcion =MessageBox.Show("Realmente deseas eliminar el/los registro(s)?","Sistema de ventas", MessageBoxButtons.OKCancel,MessageBoxIcon.Question);
                //si hemos seleccionado la opcion de OK
                if (Opcion==DialogResult.OK)
                {
                    int Codigo;
                    string Rpta = "";
                    //Recorremos todo el listado de elementos seleccionados
                    foreach (DataGridViewRow row in DgvListado.Rows)
                    {
                        //Si el elemento esta seleccionado
                        if (Convert.ToBoolean(row.Cells[0].Value))
                        {
                            //Obtenemos el codigo que en realidad es el Id del elemento a eliminar
                            Codigo = Convert.ToInt32(row.Cells[1].Value);
                            //Devolvemos la respuesta de la eliminacion
                            Rpta = NCategoria.Eliminar(Codigo);

                            //
                            if (Rpta.Equals("OK"))
                            {
                                //LLamamos al mensaje OK
                                this.MensajeOK("Se elimino el registro: " + Convert.ToString(row.Cells[2].Value ));
                            }
                            else
                            {
                                this.MensajeError(Rpta);
                            }
                        }
                    }
                    this.Listar();
                }

            }
            catch (Exception ex )
            {
                MessageBox.Show(ex.Message+ex.StackTrace);
            }
        }
    }
}
