using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Planning_Tool.XML;
	
namespace Planning_Tool
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

       
        private void startButton_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
           OpenFileDialog xml_path = new OpenFileDialog();
           xml_path.DefaultExt = ".xml";
           xml_path.Filter = "XML-Datei (.xml)|*.xml";

         if( xml_path.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                xml_path_input_textbox.Text = xml_path.FileName;
            }
        }

        private void xml_path_input_textbox_TextChanged(object sender, EventArgs e)
        {

        }

        private void xml_importieren_button_Click(object sender, EventArgs e)
        {
           
                try
                {
                    XML_Manager.read(xml_path_input_textbox.Text);
                    MessageBox.Show("Import erfolgreich!");
                    xml_path_input_textbox.Text = "";
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                    
           

           
        }
    }
}
