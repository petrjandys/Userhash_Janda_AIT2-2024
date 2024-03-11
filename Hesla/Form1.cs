using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Security.Cryptography;

namespace Hesla
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 form = new Form2();
            form.Show();
        }
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            string jmeno = textBox3.Text;
            string password = textBox4.Text;
            bool successLogin = false;
            if (!string.IsNullOrEmpty(textBox3.Text) && !string.IsNullOrEmpty(textBox4.Text))
            {

                foreach (Users u in DataHandler.seznamUzivatelu)
                {
                    if (u.userName.Equals(jmeno) && u.password.Equals(Users.HashPassword(password)))
                    {            
                        DataHandler.currentUser = u;
                        Form2 form = new Form2();
                        form.FormClosed += Form1_FormClosed;
                        successLogin = true;
                        form.Show();
                        this.Hide();
                        
                    }
                }
            }
                if (!successLogin)
                {
                    MessageBox.Show("Spatne prihlasovaci udaje", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                successLogin = false;
                textBox4.Text = "";            
        }
        
            private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                DataHandler.InitializeDatabase();
                DataHandler.LoadFromDatabase();
            }
            catch
            {
                MessageBox.Show("Chyba při načítání uživatelů z databáze", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            FormRegistrace reg = new FormRegistrace();
            reg.Show();
        }
    }
}
