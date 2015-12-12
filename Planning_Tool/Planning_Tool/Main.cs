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
using Planning_Tool.Data;
using Planning_Tool.Forecasts;
	
namespace Planning_Tool
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            DatabaseManager manager = new DatabaseManager();
            try
            {
                manager.initialize();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                manager.release();
            }
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

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void saveForecastButton_Click(object sender, EventArgs e)
        {
            try
            {
                Forecast.saveForecasts(Convert.ToInt32(A1_P0.Value), Convert.ToInt32(A1_P1.Value), Convert.ToInt32(A1_P2.Value), Convert.ToInt32(A1_P3.Value),
                                        Convert.ToInt32(A2_P0.Value), Convert.ToInt32(A2_P1.Value), Convert.ToInt32(A2_P2.Value), Convert.ToInt32(A2_P3.Value),
                                        Convert.ToInt32(A3_P0.Value), Convert.ToInt32(A3_P1.Value), Convert.ToInt32(A3_P2.Value), Convert.ToInt32(A3_P3.Value));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void label63_Click(object sender, EventArgs e)
        {

        }

        private void label160_Click(object sender, EventArgs e)
        {

        }
    }
}
