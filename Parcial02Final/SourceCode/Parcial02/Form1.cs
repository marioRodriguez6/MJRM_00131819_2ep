using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace Parcial_02
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                var password = ConnectionDB.ExecuteQuery("SELECT password From appuser " +
                                                         $"WHERE username = '{comboBox1.SelectedItem}'");
                var password2 = password.Rows[0];
                var password3 = password2[0].ToString();


                if (textBox1.Text.Equals(password3))
                {
                    var ad = ConnectionDB.ExecuteQuery("SELECT usertype FROM appuser " +
                                                       $"WHERE username = '{comboBox1.SelectedItem}'");
                    var adm = ad.Rows[0];
                    var admin = Convert.ToBoolean(adm[0].ToString());

                    if (admin)
                    {
                        Hide();
                        var nextWF = new AdminWF();
                        nextWF.Show();
                    }
                    else
                    {
                        Hide();
                        var nextWF = new NormUserWF(comboBox1.SelectedItem.ToString());
                        nextWF.Show();
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("Error");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var users = ConnectionDB.ExecuteQuery("SELECT username FROM appuser");
            var usersCombo = new List<string>();

            foreach (DataRow row in users.Rows) usersCombo.Add(row[0].ToString());
            comboBox1.DataSource = usersCombo;
        }
    }
}