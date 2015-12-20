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
using Planning_Tool.Masterdata;
using Planning_Tool.Exceptions;
using Planning_Tool.Production;
using System.Data.SQLite;
	
namespace Planning_Tool
{
    public partial class Main : Form
    {
        private SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter();
        private BindingSource ppOverviewBinding = new BindingSource();

        public Main()
        {
            InitializeComponent();
            DatabaseManager manager = new DatabaseManager();
            dataGridView1.DataSource = ppOverviewBinding;
            int count = 0;
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

        /// <summary>
        /// Läd die Daten als DataAdapter aus der Datenbank
        /// </summary>
        /// <param name="selectCommand"></param>
        private void getData(string selectCommand)
        {
            string connectionString = "Data Source=database.db";
            try
            {

                dataAdapter = new SQLiteDataAdapter(selectCommand, connectionString);

                SQLiteCommandBuilder commandBuilder = new SQLiteCommandBuilder(dataAdapter);

                DataTable table = new DataTable();
                table.Locale = System.Globalization.CultureInfo.InvariantCulture;
                dataAdapter.Fill(table);

                if (selectCommand.Contains(typeof(ProductionPlan).Name))
                {
                    ppOverviewBinding.DataSource = table;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Füllt alle DataGridFelder neu
        /// </summary>
        public void fillFields()
        {
            fillProductionPlan();
        }

        /// <summary>
        /// Füllt das DataGrid für die Produktionsübersicht
        /// </summary>
        private void fillProductionPlan()
        {
            getData("select productionplan, designation, safetystock, stock, waitlist, inwork, production from  " + typeof(ProductionPlan).Name);
        }

       
        /// <summary>
        /// Öffnet die Auswahl für den Pfad zur XML
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        //Pad anzeigen, wenn es sich um eine Datei handelt
        private void xml_path_input_textbox_DragEnter(object sender,
        System.Windows.Forms.DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void xml_path_input_textbox_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
        {
            int i;
            String s;

            // Position für die Anzeige des Pfades.
            i = xml_path_input_textbox.SelectionStart;
            s = xml_path_input_textbox.Text.Substring(i);
            xml_path_input_textbox.Text = xml_path_input_textbox.Text.Substring(0, i);

            // Pfad anzeigen.
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string file in files) xml_path_input_textbox.Text = file + s;
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

        /// <summary>
        /// Speichert die Eingaben für die Forcast in die Forecasts
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            finally
            {
                    fillFields();
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
