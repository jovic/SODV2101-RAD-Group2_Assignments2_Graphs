using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace DrawGraphs
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            btn_save.Enabled = false;
            btn_delete.Enabled = false;
        }

        private void LoadChart()
        {


            /*try
            {
                // Create an array of Valuedata using the existing TextBoxes
                var values = new PieData[]
                {
                    new PieData("X", int.Parse(txtX.Text)),
                    new PieData("Y", int.Parse(txtY.Text)),
                    new PieData("Z", int.Parse(txtZ.Text))
                };

                // Create and display the piechart
                *//*var piechart = new Piechart();
                piechart.AddData(values);
                var chart = piechart.GetChart();
                chart.Dock = DockStyle.Right;
                chart.Height = 200;
                chart.Width = 450;*//*

                // Remove only the previous chart if it exists
                var existingChart = this.Controls.OfType<Chart>().FirstOrDefault();
                if (existingChart != null)
                {
                    this.Controls.Remove(existingChart);
                }
                // Add the new chart and retain other controls
                this.Controls.Add(chart);
            }
            catch (FormatException)
            {
                MessageBox.Show("Please enter valid values.");
            }*/

        }

        private void btnCreateChart_Click(object sender, EventArgs e)
        {
            if(dgv_chartValues.Rows.Count -1 <= 0 )
                MessageBox.Show("No information to display. Please try again.","Warning",MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                ch_piechart.Series.Clear();
                Series series = new Series
                {
                    Name = "PointsSeries",
                    IsVisibleInLegend = true,
                    ChartType = SeriesChartType.Pie
                };

                ch_piechart.Series.Add(series);

                foreach (DataGridViewRow row in dgv_chartValues.Rows)
                {
                     string itemName = row.Cells[0].Value.ToString();
                     double points = Convert.ToDouble(row.Cells[1].Value);
                     series.Points.AddXY(itemName, points);
                }
        
                ch_piechart.Legends[0].Enabled = true;
            }
            
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            dgv_chartValues.Rows.Clear();
            ch_piechart.Series.Clear();

           /* // Clear the textboxes
            txtX.Clear();
            txtY.Clear();
            txtZ.Clear();


            // Remove only the previous chart if it exists
            var existingChart = this.Controls.OfType<Chart>().FirstOrDefault();
            if (existingChart != null)
            {
                this.Controls.Remove(existingChart);
            }*/
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            btn_add.Enabled = false;
            btn_save.Enabled = true;

            txt_itemName.Text = "";
            txt_values.Text = "";
            
        }

        bool checkNumber(string input)
        {
            foreach (char c in input)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_itemName.Text) && checkNumber(txt_values.Text))
            {
                dgv_chartValues.Rows.Add(txt_itemName.Text, txt_values.Text);
                MessageBox.Show("Successfully save!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btn_add.Enabled = true;
                btn_save.Enabled = false;
            }
            else
            {
                MessageBox.Show("Item name cannot be empty and Only numbers is allowed in values.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgv_chartValues_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            btn_delete.Enabled = true;
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            if (dgv_chartValues.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in dgv_chartValues.SelectedRows)
                {
                    dgv_chartValues.Rows.Remove(row);
                    btn_delete.Enabled = false;
                }
            }
            else
            {
                MessageBox.Show("Please select a row to delete.");
            }
        }
    }
}
