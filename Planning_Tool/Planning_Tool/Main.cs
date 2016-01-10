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
using System.Threading;
	
namespace Planning_Tool
{
    public partial class Main : Form
    {
        private SQLiteDataAdapter ppStockAdapter = new SQLiteDataAdapter();
        private SQLiteDataAdapter ppDirektSaleAdapter = new SQLiteDataAdapter();
        private BindingSource ppStockBinding = new BindingSource();
        private BindingSource ppDirektSaleBinding = new BindingSource();
        private Loading loading;

        public Main()
        {
            InitializeComponent();
            DatabaseManager manager = new DatabaseManager();
            pDirektSale.DataSource = ppDirektSaleBinding;
            pStock.DataSource = ppStockBinding; 
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
        private void getDataStock(string selectCommand)
        {
            string connectionString = "Data Source=database.db";
            try
            {
                ppStockAdapter = new SQLiteDataAdapter(selectCommand, connectionString);
                SQLiteCommandBuilder commandBuilder = new SQLiteCommandBuilder(ppStockAdapter);

                DataTable table = new DataTable();
                table.Locale = System.Globalization.CultureInfo.InvariantCulture;
                ppStockAdapter.Fill(table);
                ppStockBinding.DataSource = table;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Läd die Daten als DataAdapter aus der Datenbank
        /// </summary>
        /// <param name="selectCommand"></param>
        private void getDataDirekt(string selectCommand)
        {
            string connectionString = "Data Source=database.db";
            try
            {
                ppDirektSaleAdapter = new SQLiteDataAdapter(selectCommand, connectionString);
                SQLiteCommandBuilder commandBuilder = new SQLiteCommandBuilder(ppDirektSaleAdapter);

                DataTable table = new DataTable();
                table.Locale = System.Globalization.CultureInfo.InvariantCulture;
                ppDirektSaleAdapter.Fill(table);
                ppDirektSaleBinding.DataSource = table;
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
            fillppDirektSaleBinding();
            fillppStockBinding();
        }

        /// <summary>
        /// Speichert die änderungen in den views
        /// </summary>
        private void updateFields()
        {
            updateppStockBinding();
            updateppDirektSaleBinding();
        }

        /// <summary>
        /// Füllt das DataGrid für die Produktionsübersicht
        /// </summary>
        private void fillppDirektSaleBinding()
        {
            string select = "select * from  " + typeof(DirektSale).Name;
            getDataDirekt(select);
        }

        private void updateppDirektSaleBinding()
        {
            ppDirektSaleAdapter.Update((DataTable)ppDirektSaleBinding.DataSource);
        }

        /// <summary>
        /// Füllt das DataGrid für die Produktionsübersicht
        /// </summary>
        private void fillppStockBinding()
        {
            string select = "select stock, designation, use, amount, safetystock from  " + typeof(Stock).Name
                          + " WHERE safetystock > 0 ORDER BY STOCK";
            getDataStock(select);
        }

        private void updateppStockBinding()
        {
            ppStockAdapter.Update((DataTable)ppStockBinding.DataSource);
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
                    loading = new Loading("XML wird Importiert");
                    loading.Show();
                    new Thread(xmlImportieren).Start();
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }  
        }

        /// <summary>
        /// Importiert die XML
        /// </summary>
        private void xmlImportieren()
        {
            try
            {
                XML_Manager.read(xml_path_input_textbox.Text);
                this.Invoke((Action)closeLoding);
                this.Invoke((Action)cleanXMLPath);
                this.Invoke((Action)fillFields);
            }
            catch(Exception ex)
            {
                this.Invoke((Action)closeLoding);

                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Leert den pfad
        /// </summary>
        public void cleanXMLPath()
        {
            xml_path_input_textbox.Text = null;
        }

        /// <summary>
        /// Schließt das Ladefenster
        /// </summary>
        private void closeLoding()
        {
            if (loading != null)
            {
                loading.Close();
                loading = null;
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
                loading = new Loading("Produktion wird geplant");
                loading.Show();
                new Thread(saveForecast).Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void saveForecast()
        {
            bool success = false;
            try
            {
                Forecast.saveForecasts(Convert.ToInt32(A1_P0.Value), Convert.ToInt32(A1_P1.Value), Convert.ToInt32(A1_P2.Value), Convert.ToInt32(A1_P3.Value),
                                            Convert.ToInt32(A2_P0.Value), Convert.ToInt32(A2_P1.Value), Convert.ToInt32(A2_P2.Value), Convert.ToInt32(A2_P3.Value),
                                            Convert.ToInt32(A3_P0.Value), Convert.ToInt32(A3_P1.Value), Convert.ToInt32(A3_P2.Value), Convert.ToInt32(A3_P3.Value));
                updateFields();
                startPlaning();
                this.Invoke((Action)closeLoding);
            }
            catch (Exception ex)
            {
                this.Invoke((Action)closeLoding);
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Führt die Produktionsplanung durch
        /// </summary>
        private void startPlaning()
        {
            try
            {
                ProductionUtil.startPlanning();
            }
            catch(Exception ex)
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

        private void dataGridView10_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void save_saftyStock_Click(object sender, EventArgs e)
        {
        }

        private void dataGridView6_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label236_Click(object sender, EventArgs e)
        {

        }
    }
}
