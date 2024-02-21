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
        
        private void AddUserBtn_Click(object sender, EventArgs e)
        {
            FormRegistrace formReg = new FormRegistrace();
            formReg.Show();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            if(DataHandler.currentUser.isAdmin == true)
            {
                listBox1.Items.Clear();
                foreach (Users user in DataHandler.seznamUzivatelu)
                {
                    listBox1.Items.Add($"Uzivatel: {user.userName} admin: {user.isAdmin}");
                }             
            }
            else
            {
                listBox1.Items.Add($"Jste prihlasen jako bezny uzivatel: {DataHandler.currentUser.userName}");
            }
        }
    }
}
