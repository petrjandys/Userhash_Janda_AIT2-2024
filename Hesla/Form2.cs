using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hesla
{
    public partial class Form2 : Form
    {
        private string xmlSoubor = DataHandler.xmlSoubor;
        private Users currentUser = DataHandler.currentUser;
        private List<Users> seznamUzivatelu = DataHandler.seznamUzivatelu;
        public Form2()
        {
            InitializeComponent();
        }      

        private void Form2_Load(object sender, EventArgs e)
        {
            welcomeLabel.Text = $"Vítejte, {currentUser.userName}";
            if (DataHandler.currentUser.isAdmin == true)
            {
                listBox1.Items.Clear();
                foreach (Users user in DataHandler.seznamUzivatelu)
                {
                    listBox1.Items.Add($"Uzivatel: {user.userName} (admin: {user.isAdmin})");
                }
            }           
            if (!currentUser.isAdmin)
            {
                label1.Text = currentUser.userName;
                listBox1.Visible = false;
                panel1.Location = new Point(50, 50);
            }
        }

        private void ChangePassBtn_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(ChangePassTextbox.Text))
            {
                if (DataHandler.currentUser.isAdmin)
                {
                    if (listBox1.SelectedIndex >= 0)
                    {
                        Users selectedUser = DataHandler.seznamUzivatelu[listBox1.SelectedIndex];
                        selectedUser.password = Users.HashPassword(ChangePassTextbox.Text);
                        Users.SaveXML();
                        MessageBox.Show($"Uzivateli {selectedUser.userName} bylo zmeneno heslo", "Uspech", MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
                    }
                    else
                    {
                        MessageBox.Show("Vyber uzivatele v seznamu.", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    DataHandler.currentUser.password = Users.HashPassword(ChangePassTextbox.Text);
                    Users.SaveXML();
                    MessageBox.Show("Vase heslo by uspesne zmeneno", "Uspech", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
            else { 
                MessageBox.Show("Zadej heslo", "Chyba", MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
            ChangePassTextbox.Text = "";
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >= 0)
            {
                label1.Text =  DataHandler.seznamUzivatelu[listBox1.SelectedIndex].userName;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();  
            Form1 form1 = new Form1();
            form1.Show();
        }
    }
}
