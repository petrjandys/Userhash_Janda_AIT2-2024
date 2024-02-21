using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Hesla
{
    [Serializable]
    public class Users
    {

        public string userName { get; set; }
        public string password { get; set; }
        public bool isAdmin { get; set; }

        public static void SaveXML()
        {            
            try
            {
                using (FileStream fs = new FileStream(DataHandler.xmlSoubor, FileMode.Create))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(List<Users>));
                    serializer.Serialize(fs, DataHandler.seznamUzivatelu);

                }
                MessageBox.Show("Uzivatel pridan");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Chyba při ukládání uzivatele: " + ex.Message);
            }
        }
    }
}
