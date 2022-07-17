using Sistema.Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema.Presentacion
{
    public partial class FrmCategoria : Form
    {
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
            DgvListado.Columns[3].Width = 400;
            //Modifico elñ texto de la cabecera porque desde BBDD viene sin tilde descripcion asi que la modifico aqui
            DgvListado.Columns[4].HeaderText = "Descripción";
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
    }
}
