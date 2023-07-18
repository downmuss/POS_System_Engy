using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_Engy
{
    public partial class Inventario : Form
    {
        Thread Proceso_;
        MySqlConnection conn;
        MySqlCommand cmd;
        MySqlDataReader reader;
        string myConnectionString;

        public Inventario()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;

        }

        private void AgregarProductoBtn_Click(object sender, EventArgs e)
        {
            Proceso_ = new Thread(AggProducto);
            Proceso_.Start();
        }

        private void AggProducto()
        {
            AgregarInventario _NewForm = new AgregarInventario();
            _NewForm.ShowDialog();

        }

        private void ListarProductosBtn_Click(object sender, EventArgs e)
        {
            Proceso_ = new Thread(ListarProducto);
            Proceso_.Start();
        }

        private void ListarProducto()
        {
            dtgvProductos.Rows.Clear();
            myConnectionString = "server=localhost;userid=root;password=;database=engy";
            try
            {
                conn = new MySqlConnection(myConnectionString);
                conn.Open();
                string sql = "SELECT * FROM inventory";
                cmd = new MySqlCommand(sql, conn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string _ID = reader.GetString("ID");
                    string _Producto = reader.GetString("Producto");
                    string _Stock = reader.GetString("Stock");
                    string _Marca = reader.GetString("Marca");
                    string _Modelo = reader.GetString("Modelo");
                    string _Precio = reader.GetString("Precio");
                    string _Product_Updated = reader.GetString("Product_Updated");
                    int n = dtgvProductos.Rows.Add();
                    dtgvProductos.Rows[n].Cells[0].Value = _ID;
                    dtgvProductos.Rows[n].Cells[1].Value = _Producto;
                    dtgvProductos.Rows[n].Cells[2].Value = _Stock;
                    dtgvProductos.Rows[n].Cells[3].Value = _Marca;
                    dtgvProductos.Rows[n].Cells[4].Value = _Modelo;
                    dtgvProductos.Rows[n].Cells[5].Value = _Precio;
                    dtgvProductos.Rows[n].Cells[6].Value = _Product_Updated;
                }

                Console.WriteLine("Row Inserted!");
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }

    }
}
