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
    public partial class AgregarInventario : Form
    {
        Thread Proceso_;
        MySqlConnection conn;
        MySqlCommand cmd;
        string myConnectionString;

        public AgregarInventario()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        private void AgregarBtn_Click(object sender, EventArgs e)
        {
            Proceso_ = new Thread(Go);
            Proceso_.Start();
        }

        private void Go()
        {
            myConnectionString = "server=localhost;userid=root;password=;database=engy";
            try
            {
                conn = new MySqlConnection(myConnectionString);
                conn.Open();
                string sql = "INSERT INTO inventory(Producto, Stock, Marca, Modelo, Precio) VALUES(@Producto, @Stock, @Marca, @Modelo, @Precio)";
                cmd = new MySqlCommand(sql, conn);
                cmd.CommandText = sql;

                cmd.Parameters.AddWithValue("@Producto", txtProducto.Text);
                cmd.Parameters.AddWithValue("@Stock", txtStock.Text);
                cmd.Parameters.AddWithValue("@Marca", txtMarca.Text);
                cmd.Parameters.AddWithValue("@Modelo", txtModelo.Text);
                cmd.Parameters.AddWithValue("@Precio", txtPrecio.Text);

                cmd.Prepare();
                cmd.ExecuteNonQuery();
                
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
