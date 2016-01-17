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
using Planning_Tool.Time;
using Planning_Tool.Data;
	
namespace Planning_Tool
{
    public partial class Main : Form
    {
        private SQLiteDataAdapter ppStockAdapter = new SQLiteDataAdapter();
        private SQLiteDataAdapter ppDirektSaleAdapter = new SQLiteDataAdapter();
        private SQLiteDataAdapter ovProductionPlanAdapter = new SQLiteDataAdapter();
        private SQLiteDataAdapter ovOrderingPosAdapter = new SQLiteDataAdapter();
        private SQLiteDataAdapter ovDirektSaleAdapter = new SQLiteDataAdapter();
        private SQLiteDataAdapter ovCapacityPlanAdapter = new SQLiteDataAdapter();
        
        private BindingSource ppStockBinding = new BindingSource();
        private BindingSource ppDirektSaleBinding = new BindingSource();
        private BindingSource ovProductionPlanBinding = new BindingSource();
        private BindingSource ovOrderingPosBinding = new BindingSource();
        private BindingSource ovDirektSaleBinding = new BindingSource();
        private BindingSource ovCapacityPlanBinding = new BindingSource();
        private Loading loading;

        public Main()
        {
            InitializeComponent();
            DatabaseManager manager = new DatabaseManager();
            pDirektSale.DataSource = ppDirektSaleBinding;
            pStock.DataSource = ppStockBinding;
            ovOrderingPosView.DataSource = ovOrderingPosBinding;
            ovProductionPlanView.DataSource = ovProductionPlanBinding;
            ovCapacityPlanView.DataSource = ovCapacityPlanBinding;
            ovDirektSaleView.DataSource = ovDirektSaleBinding;
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
        /// Läd die Daten als DataAdapter aus der Datenbank
        /// </summary>
        /// <param name="selectCommand"></param>
        private void getDataProductionPlan(string selectCommand)
        {
            string connectionString = "Data Source=database.db";
            try
            {
                ovProductionPlanAdapter = new SQLiteDataAdapter(selectCommand, connectionString);
                SQLiteCommandBuilder commandBuilder = new SQLiteCommandBuilder(ovProductionPlanAdapter);

                DataTable table = new DataTable();
                table.Locale = System.Globalization.CultureInfo.InvariantCulture;
                ovProductionPlanAdapter.Fill(table);
                ovProductionPlanBinding.DataSource = table;
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
        private void getDataOrderingPos(string selectCommand)
        {
            string connectionString = "Data Source=database.db";
            try
            {
                ovOrderingPosAdapter = new SQLiteDataAdapter(selectCommand, connectionString);
                SQLiteCommandBuilder commandBuilder = new SQLiteCommandBuilder(ovOrderingPosAdapter);

                DataTable table = new DataTable();
                table.Locale = System.Globalization.CultureInfo.InvariantCulture;
                ovOrderingPosAdapter.Fill(table);
                ovOrderingPosBinding.DataSource = table;
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
        private void getDataCapacityPlan(string selectCommand)
        {
            string connectionString = "Data Source=database.db";
            try
            {
                ovCapacityPlanAdapter = new SQLiteDataAdapter(selectCommand, connectionString);
                SQLiteCommandBuilder commandBuilder = new SQLiteCommandBuilder(ovCapacityPlanAdapter);

                DataTable table = new DataTable();
                table.Locale = System.Globalization.CultureInfo.InvariantCulture;
                ovCapacityPlanAdapter.Fill(table);
                ovCapacityPlanBinding.DataSource = table;
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
        private void getDataDirektSale(string selectCommand)
        {
            string connectionString = "Data Source=database.db";
            try
            {
                ovDirektSaleAdapter = new SQLiteDataAdapter(selectCommand, connectionString);
                SQLiteCommandBuilder commandBuilder = new SQLiteCommandBuilder(ovDirektSaleAdapter);

                DataTable table = new DataTable();
                table.Locale = System.Globalization.CultureInfo.InvariantCulture;
                ovDirektSaleAdapter.Fill(table);
                ovDirektSaleBinding.DataSource = table;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Füllt alle Übersichts views
        /// </summary>
        public void fillOvFields()
        {
            fillovProductionPlan();
            fillovOrderingPos();
            fillovCapacityPlan();
            fillovDirektSale();
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

        private void updateOvFields()
        {
            try
            {
                updateovCapacityPlan();
                updateovDirektSale();
                updateovOrderingPos();
                updateovProductionPlan();
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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

        private void fillovProductionPlan()
        {
            string select = "SELECT productionplan, amount, dependence FROM ProductionPlan ORDER BY ProductionPlan";
            getDataProductionPlan(select);
        }

        private void updateovProductionPlan()
        {
            ovProductionPlanAdapter.Update((DataTable)ovProductionPlanBinding.DataSource);
        }

        private void fillovOrderingPos()
        {
            string select = "SELECT ordering, orderingpos,amount,isexpress FROM OrderingPos"
                          + " WHERE ordering = \"" + Period.getCurrentPeriod() + "\"";
            getDataOrderingPos(select);
        }

        private void updateovOrderingPos()
        {
            ovOrderingPosAdapter.Update((DataTable)ovOrderingPosBinding.DataSource);
        }

        private void fillovDirektSale()
        {
            string select = "SELECT * FROM DirektSale";
            getDataDirektSale(select);
        }

        private void updateovDirektSale()
        {
            ovDirektSaleAdapter.Update((DataTable)ovDirektSaleBinding.DataSource);
        }

        private void fillovCapacityPlan()
        {
            string select = "SELECT CapacityPlan, neededCompleteTime, OverTime, Shift FROM CapacityPlan";
            getDataCapacityPlan(select);
        }

        private void updateovCapacityPlan()
        {
            ovCapacityPlanAdapter.Update((DataTable)ovCapacityPlanBinding.DataSource);
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

        private void xml_export_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog path = new SaveFileDialog();
                if (path.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    updateOvFields();
                    xmlExportieren(path.FileName);
                }
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
                cleanAllForXMLimport();
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
        /// Importiert die XML
        /// </summary>
        private void xmlExportieren(string path)
        {
            try
            {
                XML_Manager.write(path);
            }
            catch (Exception ex)
            {
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
                cleanAllForPlanning();
                Forecast.saveForecasts(Convert.ToInt32(A1_P0.Value), Convert.ToInt32(A1_P1.Value), Convert.ToInt32(A1_P2.Value), Convert.ToInt32(A1_P3.Value),
                                            Convert.ToInt32(A2_P0.Value), Convert.ToInt32(A2_P1.Value), Convert.ToInt32(A2_P2.Value), Convert.ToInt32(A2_P3.Value),
                                            Convert.ToInt32(A3_P0.Value), Convert.ToInt32(A3_P1.Value), Convert.ToInt32(A3_P2.Value), Convert.ToInt32(A3_P3.Value));
                updateFields();
                ProductionUtil.startPlanning();
                this.Invoke((Action)closeLoding);
                this.Invoke((Action)fillOvFields);
            }
            catch (Exception ex)
            {
                this.Invoke((Action)closeLoding);
                this.Invoke((Action)fillOvFields);
                MessageBox.Show(ex.Message);
            }
        }

        private void cleanAllForPlanning()
        {
            DatabaseManager manager = new DatabaseManager();
            manager.delete("Forecast",null);
            manager.delete("OrderBom",null);
            manager.delete("OrderBomPos", null);
            manager.delete("PurchasePlan", null);
            manager.delete("ProductionPlan", null);
            manager.delete("CapacityPlan",null);
            manager.delete("CapacityPlanPos", null);
            manager.delete("Ordering", " Where Ordering = \"" + Period.getCurrentPeriod() + "\"");
            manager.delete("OrderingPos", " Where Ordering = \"" + Period.getCurrentPeriod() + "\"");
            manager.delete("WaitingListPlan", null);
        }

        private void cleanAllForXMLimport()
        {
            DatabaseManager manager = new DatabaseManager();
            manager.delete("Stock", null);
            manager.delete("Workplace", null);
            manager.delete("WorkplacePos", null);
            manager.delete("Period", null);
            manager.delete("Ordering", null);
            manager.delete("OrderingPos", null);
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

        private void tab_direktverkäufe_Click(object sender, EventArgs e)
        {

        }

        private void pStock_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ovOrderingPosView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ovDirektSaleView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
