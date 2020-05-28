using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace Parcial_02
{
    public partial class NormUserWF : Form
    {
        private readonly string usuarioAct = "";

        public NormUserWF(string usuarioAct)
        {
            this.usuarioAct = usuarioAct;
            InitializeComponent();
        }

        private void button6_Click(object sender, EventArgs e) //nueva orden
        {
            try
            {
                var query2 = ConnectionDB.ExecuteQuery("select idproduct from product " +
                                                       $"WHERE name ='{comboBox7.Text}'");
                var query3 = query2.Rows[0];
                var query4 = Convert.ToInt32(query3[0].ToString());

                var add = ConnectionDB.ExecuteQuery("select idaddress from address " +
                                                    $"WHERE address ='{comboBox2.Text}'");
                var addre = add.Rows[0];
                var address = Convert.ToInt32(addre[0].ToString());


                var query = "";
                query = "INSERT INTO APPORDER(createDate, idProduct, idAddress)" +
                        $" VALUES('{DateTime.UtcNow.ToString("d")}','{query4}','{address}')";
                ConnectionDB.ExecuteNonQuery(query);
                MessageBox.Show("Se ha hecho el pedido");
            }
            catch (Exception exception)
            {
                MessageBox.Show("No se ha hecho el pedido");
                throw;
            }
        }

        private void button4_Click(object sender, EventArgs e) //Nueva direccion
        {
            if (textBox3.Text.Equals(""))
                MessageBox.Show("ingrese los datos requeridos.");
            else
                try
                {
                    var query2 = ConnectionDB.ExecuteQuery("select iduser from appuser " +
                                                           $"WHERE username ='{usuarioAct}'");
                    var query3 = query2.Rows[0];
                    var query4 = Convert.ToInt32(query3[0].ToString());
                    var query = "";
                    query = "INSERT INTO ADDRESS(iduser,address)" +
                            $"values('{query4}','{textBox3.Text}')";
                    ConnectionDB.ExecuteNonQuery(query);
                    MessageBox.Show("se ha guardado la nueva ubicacion.");
                }
                catch (Exception exception)
                {
                    MessageBox.Show("No se ha registrado la ubicacion");
                    throw;
                }
        }


        private void button3_Click(object sender, EventArgs e) //actualizar la ubicacion
        {
            if (textBox2.Text.Equals(""))
                MessageBox.Show("Ingrese los datos requeridos.");
            else
                try
                {
                    var add = ConnectionDB.ExecuteQuery("select idaddress from address " +
                                                        $"WHERE address ='{comboBox4.Text}'");
                    var addre = add.Rows[0];
                    var address = Convert.ToInt32(addre[0].ToString());

                    var query = "";
                    query =
                        $"UPDATE ADDRESS SET address = '{textBox2.Text}' WHERE idAddress = '{address}'";
                    ConnectionDB.ExecuteNonQuery(query);
                    MessageBox.Show("Se ha actualizado la direccion");
                }
                catch (Exception exception)
                {
                    MessageBox.Show("No se ha actualizado la direccion.");
                    throw;
                }
        }

        private void button7_Click(object sender, EventArgs e) // Eliminar Ubicacion
        {
            try
            {
                var add = ConnectionDB.ExecuteQuery("select idaddress from address " +
                                                    $"WHERE address ='{comboBox5.Text}'");
                var addre = add.Rows[0];
                var address = Convert.ToInt32(addre[0].ToString());
                var query = "";
                query = $"DELETE FROM ADDRESS WHERE idAddress = '{address}'";
                ConnectionDB.ExecuteNonQuery(query);
                MessageBox.Show("Se ha eliminado la direccion");
            }
            catch (Exception exception)
            {
                MessageBox.Show("No se ha eliminado la direccion, Error");
                throw;
            }
        }


        private void button8_Click(object sender, EventArgs e) //ver Direccion
        {
            var add = ConnectionDB.ExecuteQuery("select iduser from appuser " +
                                                                $"WHERE username ='{usuarioAct}'");
                            var addre = add.Rows[0];
                            var address = Convert.ToInt32(addre[0].ToString());
            var Query = $"SELECT ad.idAddress, ad.address FROM ADDRESS ad WHERE idUser = '{address}'";
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

        private void button11_Click(object sender, EventArgs e) // Ver Pedidos
        {
            var add = ConnectionDB.ExecuteQuery("select iduser from appuser " +
                                                                            $"WHERE username ='{usuarioAct}'");
            var addre = add.Rows[0];
            var address = Convert.ToInt32(addre[0].ToString());
        
            var Query = "SELECT ao.idOrder, ao.createDate, pr.name, au.fullname, ad.address " +
                         "FROM APPORDER ao, ADDRESS ad, PRODUCT pr, APPUSER au " +
                         "WHERE ao.idProduct = pr.idProduct " +
                         "AND ao.idAddress = ad.idAddress " +
                         "AND ad.idUser = au.idUser " +
                         $"AND au.idUser = '{address}'";
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


        private void button5_Click(object sender, EventArgs e) //Actualizar contraseña
        {
            if (textBox5.Text.Equals(""))
                MessageBox.Show("Ingrese la nueva contraseña por favor");
            else
            {
                var query2 = ConnectionDB.ExecuteQuery("select iduser from appuser " +
                                                       $"WHERE username ='{usuarioAct}'");
                var query3 = query2.Rows[0];
                var query4 = Convert.ToInt32(query3[0].ToString());

                var NonQuery = $"UPDATE appuser SET password= '{textBox5.Text}' WHERE iduser= '{query4}'";
                try
                {
                    ConnectionDB.ExecuteNonQuery(NonQuery);
                    MessageBox.Show("Contraseña Actualizada");
                }
                catch (Exception exception)
                {
                    MessageBox.Show("Usuario no eliminado");
                }
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                var ord = ConnectionDB.ExecuteQuery("select idorder from apporder " +
                                                    $"WHERE idorder ='{comboBox3.Text}'");
                var orde = ord.Rows[0];
                var order = Convert.ToInt32(orde[0].ToString());
                var query = "";
                query = $"DELETE FROM apporder WHERE idorder = '{order}'";
                ConnectionDB.ExecuteNonQuery(query);
                MessageBox.Show("se ha eliminado el pedido");
            }
            catch (Exception exception)
            {
                MessageBox.Show("No se ha eliminado el pedido, Error");
                throw;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Hide();
            var nextWFs = new Form1();
            nextWFs.Show();
        }

        private void NormUserWF_Load(object sender, EventArgs e)
        {
            //direccion      

            var direccion = ConnectionDB.ExecuteQuery("SELECT address FROM address");
            var direccioncombos = new List<string>();

            foreach (DataRow row in direccion.Rows) direccioncombos.Add(row[0].ToString());
            comboBox2.DataSource = direccioncombos;
            comboBox4.DataSource = direccioncombos;
            comboBox5.DataSource = direccioncombos;

            //productos
            var productos = ConnectionDB.ExecuteQuery("SELECT name FROM product");
            var productosCombo = new List<string>();

            foreach (DataRow row in productos.Rows) productosCombo.Add(row[0].ToString());
            comboBox7.DataSource = productosCombo;
         
            
            
            var query2 = ConnectionDB.ExecuteQuery("select iduser from appuser " +
                                                                   $"WHERE username ='{usuarioAct}'");
                            var query3 = query2.Rows[0];
                            var query4 = Convert.ToInt32(query3[0].ToString());
                            
                            
            var pedidos = ConnectionDB.ExecuteQuery("SELECT ao.idOrder, ao.createDate, pr.name, au.fullname, ad.address " +
                                                       "FROM APPORDER ao, ADDRESS ad, PRODUCT pr, APPUSER au " +
                                                       "WHERE ao.idProduct = pr.idProduct " +
                                                       "AND ao.idAddress = ad.idAddress " +
                                                       "AND ad.idUser = au.idUser " +
                                                       $"AND au.idUser = '{query4}'");
                        var pedidosCombo = new List<string>();
            
                        foreach (DataRow row in pedidos.Rows) pedidosCombo.Add(row[0].ToString());
                       
                        comboBox3.DataSource = pedidosCombo;
            
        }
    }
}