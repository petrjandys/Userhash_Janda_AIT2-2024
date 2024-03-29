﻿using System;
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

namespace Hesla
{
    
    public partial class FormRegistrace : Form
    {
        private bool firstRegistraton = false;
        public FormRegistrace()
        {
            InitializeComponent();           
        }

        private void Registerbtn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrEmpty(textBox2.Text))
            {
                if (DataHandler.seznamUzivatelu.Any(user => user.userName == textBox1.Text))
                {
                    MessageBox.Show("Uživatel pod tímto jménem již existuje", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    string hashedPassword = Users.HashPassword(textBox2.Text);
                    Users newUser = new Users
                    {
                        userName = textBox1.Text,
                        password = hashedPassword,
                        isAdmin = firstRegistraton
                    };

                    DataHandler.seznamUzivatelu.Add(newUser);
                    Users.SaveToDatabase();  // Uložíme nového uživatele do databáze
                    MessageBox.Show("Uživatel přidán", "Úspěch", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Zadejte údaje", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void FormRegistrace_Load(object sender, EventArgs e)
        {
            if (DataHandler.seznamUzivatelu == null || DataHandler.seznamUzivatelu.Count == 0)
            {
                firstRegistraton = true;
                MessageBox.Show("Prvni ucet bude zaregistrovany jako administratorsky", "Defaultni ucet", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                
            }
        }
    }
}
