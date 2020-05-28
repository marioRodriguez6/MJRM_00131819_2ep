using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace Parcial_02
{
    public partial class AdminWF : Form
    {
        public AdminWF()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e) //crear usuario
        {
            if (textBox9.Text.Equals("") || textBox1.Text.Equals("") || textBox5.Text.Equals(""))
                MessageBox.Show("Por favor inserta los datos necesarios");
            else
                try
                {
                    var Query = "";
                    if (radioButton1.Checked)
                        Query = "INSERT " +
                                "INTO " +
                                "appuser(fullname,username,password,userType) " +
                                $"VALUES('{textBox9.Text}','{textBox1.Text}','{textBox5.Text}',true)";
                    else
                        Query = "INSERT " +
                                "INTO " +
                                $"appuser(fullname,username,password,userType) VALUES('{textBox9.Text}'," +
                                $"'{textBox1.Text}','{textBox5.Text}',false)";

                    ConnectionDB.ExecuteNonQuery(Query);

                    MessageBox.Show("Usuario insertado con exito");
                }
                catch (Exception exception)
                {
                    MessageBox.Show("Usuario no ingresado");
                }
        }

        private void button5_Click(object sender, EventArgs e) //eliminar usuario
        {
            var query2 = ConnectionDB.ExecuteQuery("select iduser from appuser " +
                                                   $"WHERE username ='{comboBox3.Text}'");
            var query3 = query2.Rows[0];
            var query4 = Convert.ToInt32(query3[0].ToString());

            var NonQuery = $"DELETE FROM appuser WHERE iduser = '{query4}'";
            try
            {
                ConnectionDB.ExecuteNonQuery(NonQuery);
                MessageBox.Show("Usuario eliminado");
            }
            catch (Exception exception)
            {
                MessageBox.Show("Usuario no eliminado");
            }
        }

        private void button1_Click(object sender, EventArgs e) //Rload ver usuario
        {
            var Query = "SELECT * FROM appuser";
            var dt = ConnectionDB.ExecuteQuery(Query);
            try
            {
                dataGridView1.DataSource = dt;
            }
            catch (Exception exception)
            {
                MessageBox.Show("Error");
            }
        }


        private void button7_Click(object sender, EventArgs e) //Agregar Productos
        {
            if (textBox3.Text.Equals(""))
                MessageBox.Show("Digite los datos pedidos !");
            else
                try
                {
                    var query2 = ConnectionDB.ExecuteQuery("select idbusiness from business " +
                                                           $"WHERE name ='{comboBox1.Text}'");
                    var query3 = query2.Rows[0];
                    var query4 = Convert.ToInt32(query3[0].ToString());
                    var query = "";
                    query = "INSERT INTO product(idBusiness,name)" +
                            $"VALUES ('{query4}', '{textBox3.Text}')";

                    ConnectionDB.ExecuteNonQuery(query);
                    MessageBox.Show("Producto Agregado");
                }
                catch (Exception exception)
                {
                    MessageBox.Show("No se ha agregado");
                    throw;
                }
        }


        private void button9_Click(object sender, EventArgs e) //Eliminar Producto
        {
            var query2 = ConnectionDB.ExecuteQuery("select idproduct from product " +
                                                   $"WHERE name ='{comboBox2.Text}'");
            var query3 = query2.Rows[0];
            var query4 = Convert.ToInt32(query3[0].ToString());

            var NonQuery = $"DELETE FROM PRODUCT WHERE idProduct = '{query4}'";
            try
            {
                ConnectionDB.ExecuteNonQuery(NonQuery);
                MessageBox.Show("Producto eliminado");
            }
            catch (Exception exception)
            {
                MessageBox.Show("Producto no eliminado");
            }
        }

        private void button2_Click(object sender, EventArgs e) //Agregar Negocio
        {
            if (textBox10.Text.Equals("") || textBox2.Text.Equals(""))
                MessageBox.Show("Digite los datos pedidos !");
            else
                try
                {
                    var query = "";
                    query = "INSERT INTO business(name, description)" +
                            $"VALUES ('{textBox10.Text}', '{textBox2.Text}')";

                    ConnectionDB.ExecuteNonQuery(query);
                    MessageBox.Show("Negocio Agregado");
                }
                catch (Exception exception)
                {
                    MessageBox.Show("No se ha agregado");
                    throw;
                }
        }

        private void button8_Click(object sender, EventArgs e) //eliminar negocios
        {
            var query2 = ConnectionDB.ExecuteQuery("select idbusiness from business " +
                                                   $"WHERE name ='{comboBox6.Text}'");
            var query3 = query2.Rows[0];
            var query4 = Convert.ToInt32(query3[0].ToString());

            var NonQuery = $"DELETE FROM BUSINESS WHERE idBusiness = '{query4}'";
            try
            {
                ConnectionDB.ExecuteNonQuery(NonQuery);
                MessageBox.Show("Negocio eliminado");
            }
            catch (Exception exception)
            {
                MessageBox.Show("Negocio no eliminado");
            }
        }


        private void button4_Click(object sender, EventArgs e) //RldNegocio
        {
            var Query = "SELECT * FROM business";
            var dt = ConnectionDB.ExecuteQuery(Query);
            try
            {
                dataGridView2.DataSource = dt;
            }
            catch (Exception exception)
            {
                MessageBox.Show("Error");
            }
        }

        private void button10_Click(object sender, EventArgs e) //Historial pedidos
        {
            var Query = "SELECT ao.idOrder, ao.createDate, pr.name, au.fullname, ad.address " +
                         "FROM APPORDER ao, ADDRESS ad, PRODUCT pr, APPUSER au " +
                         "WHERE ao.idProduct = pr.idProduct " +
                         "AND ao.idAddress = ad.idAddress " +
                         "AND ad.idUser = au.idUser";
                         
            var dt = ConnectionDB.ExecuteQuery(Query);
            try
            {
                dataGridView3.DataSource = dt;
            }
            catch (Exception exception)
            {
                MessageBox.Show("Error");
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Hide();
            var nextWFs = new Form1();
            nextWFs.Show();
        }

        private void AdminWF_Load(object sender, EventArgs e)
        {
            //usuarios
            var users = ConnectionDB.ExecuteQuery("SELECT username FROM appuser");
            var usersCombo = new List<string>();

            foreach (DataRow row in users.Rows) usersCombo.Add(row[0].ToString());
            comboBox3.DataSource = usersCombo;

            //negocios      

            var negocios = ConnectionDB.ExecuteQuery("SELECT name FROM business");
            var negocioscombo = new List<string>();

            foreach (DataRow row in negocios.Rows) negocioscombo.Add(row[0].ToString());
            comboBox1.DataSource = negocioscombo;
            comboBox6.DataSource = negocioscombo;

            //productos
            var productos = ConnectionDB.ExecuteQuery("SELECT name FROM product");
            var productosCombo = new List<string>();

            foreach (DataRow row in productos.Rows) productosCombo.Add(row[0].ToString());
            comboBox2.DataSource = productosCombo;
        }
    }
}