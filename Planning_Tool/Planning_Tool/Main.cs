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
	
namespace Planning_Tool
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            DatabaseManager manager = new DatabaseManager();
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

        public void fillFields()
        {
            fillProductionPlan();
        }

        private void fillProductionPlan()
        {
            ProductionPlan pp1 = null;
            List<ProductionPlan> pp1Pos = null;
            ProductionPlan pp2 = null;
            ProductionPlan pp3 = null;
            
            pp1 = ProductionPlanFactory.search(typeof(ProductionPlan), "1") as ProductionPlan;
            if (pp1 != null)
            {
                //P1
                tb_pp_p1_vw.Text = pp1.sellwich.ToString();
                tb_pp_p1_sb.Text = pp1.safetyStock.ToString();
                tb_pp_p1_ws.Text = pp1.waitList.ToString();
                tb_pp_p1_iB.Text = pp1.inWork.ToString();
                tb_pp_p1_lb.Text = pp1.stock.ToString();
                tb_pp_p1_pr.Text = pp1.production.ToString();
            }

            pp1 = ProductionPlanFactory.search(typeof(ProductionPlan), "16") as ProductionPlan;
            if (pp1 != null)
            {
                //P1
                tb_pp_16P1_vw.Text = pp1.sellwich.ToString();
                tb_pp_16P1_sb.Text = pp1.safetyStock.ToString();
                tb_pp_16P1_ws.Text = pp1.waitList.ToString();
                tb_pp_16P1_iB.Text = pp1.inWork.ToString();
                tb_pp_16P1_lb.Text = pp1.stock.ToString();
                tb_pp_16P1_pr.Text = pp1.production.ToString();
            }

            pp1 = ProductionPlanFactory.search(typeof(ProductionPlan), "51") as ProductionPlan;
            if (pp1 != null)
            {
                //P1
                tb_pp_51_vw.Text = pp1.sellwich.ToString();
                tb_pp_51_sb.Text = pp1.safetyStock.ToString();
                tb_pp_51_ws.Text = pp1.waitList.ToString();
                tb_pp_51_iB.Text = pp1.inWork.ToString();
                tb_pp_51_lb.Text = pp1.stock.ToString();
                tb_pp_51_pr.Text = pp1.production.ToString();
            }

            pp1 = ProductionPlanFactory.search(typeof(ProductionPlan), "26") as ProductionPlan;
            if (pp1 != null)
            {
                //P1
                tb_pp_26P1_vw.Text = pp1.sellwich.ToString();
                tb_pp_26P1_sb.Text = pp1.safetyStock.ToString();
                tb_pp_26P1_ws.Text = pp1.waitList.ToString();
                tb_pp_26P1_iB.Text = pp1.inWork.ToString();
                tb_pp_26P1_lb.Text = pp1.stock.ToString();
                tb_pp_26P1_pr.Text = pp1.production.ToString();
            }

            pp1 = ProductionPlanFactory.search(typeof(ProductionPlan), "17") as ProductionPlan;
            if (pp1 != null)
            {
                //P1
                tb_pp_17P1_vw.Text = pp1.sellwich.ToString();
                tb_pp_17P1_sb.Text = pp1.safetyStock.ToString();
                tb_pp_17P1_ws.Text = pp1.waitList.ToString();
                tb_pp_17P1_iB.Text = pp1.inWork.ToString();
                tb_pp_17P1_lb.Text = pp1.stock.ToString();
                tb_pp_17P1_pr.Text = pp1.production.ToString();
            }

            pp1 = ProductionPlanFactory.search(typeof(ProductionPlan), "50") as ProductionPlan;
            if (pp1 != null)
            {
                //P1
                tb_pp_50_vw.Text = pp1.sellwich.ToString();
                tb_pp_50_sb.Text = pp1.safetyStock.ToString();
                tb_pp_50_ws.Text = pp1.waitList.ToString();
                tb_pp_50_iB.Text = pp1.inWork.ToString();
                tb_pp_50_lb.Text = pp1.stock.ToString();
                tb_pp_50_pr.Text = pp1.production.ToString();
            }

            pp1 = ProductionPlanFactory.search(typeof(ProductionPlan), "4") as ProductionPlan;
            if (pp1 != null)
            {
                //P1
                tb_pp_4_vw.Text = pp1.sellwich.ToString();
                tb_pp_4_sb.Text = pp1.safetyStock.ToString();
                tb_pp_4_ws.Text = pp1.waitList.ToString();
                tb_pp_4_iB.Text = pp1.inWork.ToString();
                tb_pp_4_lb.Text = pp1.stock.ToString();
                tb_pp_4_pr.Text = pp1.production.ToString();
            }

            pp1 = ProductionPlanFactory.search(typeof(ProductionPlan), "10") as ProductionPlan;
            if (pp1 != null)
            {
                //P1
                tb_pp_10_vw.Text = pp1.sellwich.ToString();
                tb_pp_10_sb.Text = pp1.safetyStock.ToString();
                tb_pp_10_ws.Text = pp1.waitList.ToString();
                tb_pp_10_iB.Text = pp1.inWork.ToString();
                tb_pp_10_lb.Text = pp1.stock.ToString();
                tb_pp_10_pr.Text = pp1.production.ToString();
            }

            pp1 = ProductionPlanFactory.search(typeof(ProductionPlan), "49") as ProductionPlan;
            if (pp1 != null)
            {
                //P1
                tb_pp_49_vw.Text = pp1.sellwich.ToString();
                tb_pp_49_sb.Text = pp1.safetyStock.ToString();
                tb_pp_49_ws.Text = pp1.waitList.ToString();
                tb_pp_49_iB.Text = pp1.inWork.ToString();
                tb_pp_49_lb.Text = pp1.stock.ToString();
                tb_pp_49_pr.Text = pp1.production.ToString();
            }

            pp1 = ProductionPlanFactory.search(typeof(ProductionPlan), "7") as ProductionPlan;
            if (pp1 != null)
            {
                //P1
                tb_pp_7_vw.Text = pp1.sellwich.ToString();
                tb_pp_7_sb.Text = pp1.safetyStock.ToString();
                tb_pp_7_ws.Text = pp1.waitList.ToString();
                tb_pp_7_iB.Text = pp1.inWork.ToString();
                tb_pp_7_lb.Text = pp1.stock.ToString();
                tb_pp_7_pr.Text = pp1.production.ToString();
            }

            pp1 = ProductionPlanFactory.search(typeof(ProductionPlan), "13") as ProductionPlan;
            if (pp1 != null)
            {
                //P1
                tb_pp_13_vw.Text = pp1.sellwich.ToString();
                tb_pp_13_sb.Text = pp1.safetyStock.ToString();
                tb_pp_13_ws.Text = pp1.waitList.ToString();
                tb_pp_13_iB.Text = pp1.inWork.ToString();
                tb_pp_13_lb.Text = pp1.stock.ToString();
                tb_pp_13_pr.Text = pp1.production.ToString();
            }

            pp1 = ProductionPlanFactory.search(typeof(ProductionPlan), "18") as ProductionPlan;
            if (pp1 != null)
            {
                //P1
                tb_pp_18_vw.Text = pp1.sellwich.ToString();
                tb_pp_18_sb.Text = pp1.safetyStock.ToString();
                tb_pp_18_ws.Text = pp1.waitList.ToString();
                tb_pp_18_iB.Text = pp1.inWork.ToString();
                tb_pp_18_lb.Text = pp1.stock.ToString();
                tb_pp_18_pr.Text = pp1.production.ToString();
            }
            //P2

            //P3
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
