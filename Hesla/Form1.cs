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

        private void button2_Click(object sender, EventArgs e)
        {
            string jmeno = textBox3.Text;
            string password = textBox4.Text;
            foreach(Users u in DataHandler.seznamUzivatelu)
            {
                if(u.userName.Equals(jmeno) && u.password.Equals(password))
                {
                    DataHandler.currentUser = u;
                    Form2 form = new Form2();
                    
                    form.Show();
                    
                }
                
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists(DataHandler.xmlSoubor))
                {
                    using (FileStream fs = new FileStream(DataHandler.xmlSoubor, FileMode.Open))
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(List<Users>));
                        DataHandler.seznamUzivatelu = (List<Users>)serializer.Deserialize(fs);
                    }
                }
            }
            catch
            {
                MessageBox.Show("Chyba při načítání kontaktů ze souboru");
            }
           
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            FormRegistrace form3 = new FormRegistrace();
            form3.Show();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            FormRegistrace reg = new FormRegistrace();
            reg.Show();
        }
    }
}
