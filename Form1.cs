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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace lab1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Create_chart_dots();
            radioButton2.Checked = true;
        }
        List<double> X = new List<double>();
        List<double> Y = new List<double>();
        List<double> X2 = new List<double>();
        List<double> Y2 = new List<double>();
        int count = 1, count1, swap = 0, temp = 0;

        void Swap<T>(List<T> list, int i, int j)
        {
            (list[i], list[j]) = (list[j], list[i]);
        }
        void Create_chart_dots()
        {
            chart1.Series[0].Color = Color.White;
            chart1.Series[0].Points.AddXY(0, 0);
            chart1.Series[0].Points.AddXY(10, 10);
            chart1.Series[0].Points.AddXY(-10, -10);
            chart1.Series[0].Points.AddXY(10, -10);
            chart1.Series[0].Points.AddXY(-10, 10);
            chart1.Series[0].ToolTip = "X = #VALX, Y = #VALY";
            chart1.Series[1].ToolTip = "X = #VALX, Y = #VALY";
            chart1.Series[1].BorderWidth = 5;
            chart1.ChartAreas[0].AxisX.RoundAxisValues();
            chart1.Legends.Clear();
            chart1.Series[2].Points.Clear();
        }
        private void chart1_MouseClick(object sender, MouseEventArgs e)
        {
            double x, y;
            chart1.Series[2].Color = Color.Black;
            chart1.Series[2].BorderWidth = 5;
            if (count < 3)
            {
                x = chart1.ChartAreas[0].AxisX.PixelPositionToValue(e.X);
                y = chart1.ChartAreas[0].AxisY.PixelPositionToValue(e.Y);
                X.Add(Math.Round(x));
                Y.Add(Math.Round(y));
                listBox1.Items.Add("x" + (count).ToString() + "= " + Math.Round(x) + ";   y" + (count).ToString() + "= " + Math.Round(y));
                count++;
                chart1.Series[2].Points.AddXY(x, y);
            }
            else if(count >= 3 && count < 5 && checkBox1.Checked == false)
            {
                if(X2.Count == 0) count = 3;
                if (count == 4)
                {
                    double x1, y1;
                    x = chart1.ChartAreas[0].AxisX.PixelPositionToValue(e.X);
                    y = chart1.ChartAreas[0].AxisY.PixelPositionToValue(e.Y);
                    int c4 = 0;
                    for (int i = 0; i < X2.Count; i++)
                    {
                        x1 = Math.Round(X2[i]) - Math.Round(x);
                        y1 = Math.Round(Y2[i]) - Math.Round(y);
                        if (Math.Abs(x1) <= 1 && Math.Abs(y1) <= 1)
                        {
                            X2.RemoveAt(i);
                            Y2.RemoveAt(i);
                            //chart1.Series[2].Points.RemoveAt(2 + i);
                            chart1.Series[2].Points.RemoveAt(chart1.Series[2].Points.Count-1);
                            count1 = i;
                            listBox1.Items.RemoveAt(listBox1.Items.Count-1);
                            c4 = 1;
                            break;
                        }
                        if (i == X2.Count - 1 && c4 == 0)
                        {
                            x = chart1.ChartAreas[0].AxisX.PixelPositionToValue(e.X);
                            y = chart1.ChartAreas[0].AxisY.PixelPositionToValue(e.Y);
                            if (swap == 1)
                            {
                                X2.Add(Math.Round(x));
                                Y2.Add(Math.Round(y));
                                Swap<double>(X2, 0, 1);
                                Swap<double>(Y2, 0, 1);
                                swap = 0;
                            }
                            else
                            {
                                X2.Add(Math.Round(x));
                                Y2.Add(Math.Round(y));
                            }
                            listBox1.Items.Add("x" + (count).ToString() + "= " + Math.Round(x) + ";   y" + (count).ToString() + "= " + Math.Round(y));
                            count++;
                            chart1.Series[2].Points.AddXY(x, y);
                            if (temp == 0) 
                            { 
                                chart1.Series[1].Points.Clear();
                                chart1.Series[4].Points.Clear();
                                chart1.Series[5].Points.Clear();
                            }
                            else
                            {
                                int k = chart1.Series[1].Points.Count - 1;
                                do
                                {
                                    chart1.Series[1].Points.RemoveAt(chart1.Series[1].Points.Count - 1);
                                    k--;
                                } while (chart1.Series[1].Points[k].XValue != X[0]);
                            }
                            Draw();
                            break;
                        }
                    }
                }
                else
                {
                    x = chart1.ChartAreas[0].AxisX.PixelPositionToValue(e.X);
                    y = chart1.ChartAreas[0].AxisY.PixelPositionToValue(e.Y);
                    X2.Add(Math.Round(x));
                    Y2.Add(Math.Round(y));
                    listBox1.Items.Add("x" + (count).ToString() + "= " + Math.Round(x) + ";   y" + (count).ToString() + "= " + Math.Round(y));
                    count++;
                    chart1.Series[2].Points.AddXY(x, y);
                    if (temp == 0)
                    {
                        chart1.Series[1].Points.Clear();
                        chart1.Series[4].Points.Clear();
                        chart1.Series[5].Points.Clear();
                    }
                    else
                    {
                        int k3 = 0;
                        for(int k2 = 0; k2 < chart1.Series[1].Points.Count; k2++)
                        {
                            if(chart1.Series[1].Points[k2].XValue == X[1])
                            {
                                k3 = 1;
                                break;
                            }
                        }
                        if (k3 == 1)
                        {
                            int k = chart1.Series[1].Points.Count - 1;
                            do
                            {
                                chart1.Series[1].Points.RemoveAt(chart1.Series[1].Points.Count - 1);
                                k--;
                            } while (chart1.Series[1].Points[k].XValue != X[0]);
                        }
                    }
                    Draw();
                }
            }
            else if(count == 5 && checkBox1.Checked == false)
            {
                double x1, y1;
                x = chart1.ChartAreas[0].AxisX.PixelPositionToValue(e.X);
                y = chart1.ChartAreas[0].AxisY.PixelPositionToValue(e.Y);
                for(int i = 0; i < X2.Count; i++)
                {
                    x1 = Math.Round(X2[i]) - Math.Round(x);
                    y1 = Math.Round(Y2[i]) - Math.Round(y);
                    if (Math.Abs(x1) <= 1 && Math.Abs(y1) <= 1)
                    {
                        X2.RemoveAt(i);
                        Y2.RemoveAt(i);
                        chart1.Series[2].Points.RemoveAt(2+i);
                        count1 = i;
                        listBox1.Items.RemoveAt(2 + i);
                        count++;
                        break;
                    }
                }
            }
            else if(count == 6 && checkBox1.Checked == false)
            {
                double x1, y1;
                x = chart1.ChartAreas[0].AxisX.PixelPositionToValue(e.X);
                y = chart1.ChartAreas[0].AxisY.PixelPositionToValue(e.Y);
                for (int i = 0; i < X2.Count; i++)
                {
                    x1 = Math.Round(X2[i]) - Math.Round(x);
                    y1 = Math.Round(Y2[i]) - Math.Round(y);
                    if (Math.Abs(x1) <= 1 && Math.Abs(y1) <= 1)
                    {
                        X2.RemoveAt(i);
                        Y2.RemoveAt(i);
                        chart1.Series[2].Points.RemoveAt(2 + i);
                        count1 = i;
                        listBox1.Items.RemoveAt(2 + i);
                        count = 4;
                        swap = 1;
                        break;
                    }
                }
                if (count == 6)
                {
                    X2.Insert(count1, Math.Round(x));
                    Y2.Insert(count1, Math.Round(y));
                    //X3 = cha
                    DataPoint d = new DataPoint(x, y);
                    listBox1.Items.Insert(count1 + 2, "x" + (count).ToString() + "= " + Math.Round(x) + ";   y" + (count).ToString() + "= " + Math.Round(y));
                    chart1.Series[2].Points.Insert(count1 + 2, d);
                    if (temp == 0)
                    {
                        chart1.Series[1].Points.Clear();
                        chart1.Series[4].Points.Clear();
                        chart1.Series[5].Points.Clear();
                    }
                    Draw();
                }
            }
            if(checkBox1.Checked == true)
            {
                double x1, y1;
                int c5 = 0;
                if (count == 4 || count == 5 || count == 6)
                {
                    x = chart1.ChartAreas[0].AxisX.PixelPositionToValue(e.X);
                    y = chart1.ChartAreas[0].AxisY.PixelPositionToValue(e.Y);
                    for (int i = 0; i < X2.Count; i++)
                    {
                        x1 = Math.Round(X2[i]) - Math.Round(x);
                        y1 = Math.Round(Y2[i]) - Math.Round(y);
                        if (Math.Abs(x1) > 1 && Math.Abs(y1) > 1)
                        {
                            c5++;
                        }
                    }
                    if(c5 != 0)
                    {
                        X2.Clear();
                        Y2.Clear();
                        X.RemoveAt(0);
                        Y.RemoveAt(0);
                        X.Add(Math.Round(x, 3));
                        Y.Add(Math.Round(y, 3));
                        listBox1.Items.Add("x" + (count).ToString() + "= " + Math.Round(x) + ";   y" + (count).ToString() + "= " + Math.Round(y));
                        count = 3;
                        chart1.Series[2].Points.AddXY(x, y);
                        temp = 1;
                        checkBox1.Checked = false;
                    }
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();
            chart1.Series[2].Points.Clear();
            chart1.Series[3].Points.Clear();
            chart1.Series[4].Points.Clear();
            chart1.Series[5].Points.Clear();
            X.Clear();
            Y.Clear();
            X2.Clear();
            Y2.Clear();
            count = 1;
            listBox1.Items.Clear();
            Create_chart_dots();
            temp = 0;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            double z = Convert.ToDouble(tbX.Text);//x
            double k = Convert.ToDouble(tbY.Text);//y
            chart1.Series[0].Color = Color.White;
            chart1.Series[0].Points.AddXY(z, k);
        }
        void Draw()
        {
            
            if(X2.Count == 1)
            {
                List<double> x_1 = new List<double>();
                List<double> y_1 = new List<double>();
                chart1.Series[4].BorderWidth = 5;
                chart1.Series[5].BorderWidth = 5;
                double deltt = Convert.ToDouble(tbt.Text);
                double x4 = deltt;
                int i = 0;
                if (checkBox5.Checked) chart1.Series[1].Points.AddXY(X[0], Y[0]);
                else if (checkBox6.Checked) chart1.Series[5].Points.AddXY(X[0], Y[0]);
                else if (checkBox7.Checked) chart1.Series[4].Points.AddXY(X[0], Y[0]);
                else chart1.Series[1].Points.AddXY(X[0], Y[0]);
                do
                {
                    x_1.Add(Math.Pow(1 - x4, 2) * X[0] + 2 * (1 - x4) * x4 * X2[0] + x4 * x4 * X[1]);
                    y_1.Add(Math.Pow(1 - x4, 2) * Y[0] + 2 * (1 - x4) * x4 * Y2[0] + x4 * x4 * Y[1]);
                    //chart1.Series[1].Points.AddXY(x_1[i], y_1[i]);
                    if (checkBox5.Checked) chart1.Series[1].Points.AddXY(x_1[i], y_1[i]);
                    else if (checkBox6.Checked) chart1.Series[5].Points.AddXY(x_1[i], y_1[i]);
                    else if (checkBox7.Checked) chart1.Series[4].Points.AddXY(x_1[i], y_1[i]);
                    else chart1.Series[1].Points.AddXY(x_1[i], y_1[i]);
                    i++;
                    x4 += deltt;
                } while (x4 <= 1);
                if(checkBox5.Checked) chart1.Series[1].Points.AddXY(X[1], Y[1]);
                else if(checkBox6.Checked) chart1.Series[5].Points.AddXY(X[1], Y[1]);
                else if (checkBox7.Checked) chart1.Series[4].Points.AddXY(X[1], Y[1]);
                else chart1.Series[1].Points.AddXY(X[1], Y[1]);
            }
            else if (X2.Count == 2)
            {
                //chart1.Series[1].Points.Clear();
                List<double> x_1 = new List<double>();
                List<double> y_1 = new List<double>();
                double deltt = Convert.ToDouble(tbt.Text);
                double x4 = deltt;
                int i = 0;
                if (checkBox5.Checked) chart1.Series[1].Points.AddXY(X[0], Y[0]);
                else if (checkBox6.Checked) chart1.Series[5].Points.AddXY(X[0], Y[0]);
                else if (checkBox7.Checked) chart1.Series[4].Points.AddXY(X[0], Y[0]);
                else chart1.Series[1].Points.AddXY(X[0], Y[0]);
                do
                {
                    x_1.Add(Math.Pow(1 - x4, 3) * X[0] + 3 * Math.Pow(1 - x4,2) * x4 * X2[0] + 3 * (1 - x4) * x4 * x4 * X2[1] + x4 * x4 * x4 * X[1]);
                    y_1.Add(Math.Pow(1 - x4, 3) * Y[0] + 3 * Math.Pow(1 - x4, 2) * x4 * Y2[0] + 3 * (1 - x4) * x4 * x4 * Y2[1] + x4 * x4 * x4 * Y[1]);
                    //chart1.Series[1].Points.AddXY(x_1[i], y_1[i]);
                    if (checkBox5.Checked) chart1.Series[1].Points.AddXY(x_1[i], y_1[i]);
                    else if (checkBox6.Checked) chart1.Series[5].Points.AddXY(x_1[i], y_1[i]);
                    else if (checkBox7.Checked) chart1.Series[4].Points.AddXY(x_1[i], y_1[i]);
                    else chart1.Series[1].Points.AddXY(x_1[i], y_1[i]);
                    i++;
                    x4 += deltt;
                } while (x4 <= 1);
                if (checkBox5.Checked) chart1.Series[1].Points.AddXY(X[1], Y[1]);
                else if (checkBox6.Checked) chart1.Series[5].Points.AddXY(X[1], Y[1]);
                else if (checkBox7.Checked) chart1.Series[4].Points.AddXY(X[1], Y[1]);
                else chart1.Series[1].Points.AddXY(X[1], Y[1]);
            }
            //else MessageBox.Show("Eror");
            
        }

        

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox7.Checked == false) chart1.Series[4].Points.Clear();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
                if (radioButton1.Checked)
                {
                    chart1.Series[3].BorderWidth = 5;
                    if (X2.Count == 1)
                    {
                        chart1.Series[3].Points.AddXY(X[0], Y[0]);
                        chart1.Series[3].Points.AddXY(X[1], Y[1]);
                        chart1.Series[3].Points.AddXY(X2[0], Y2[0]);
                        chart1.Series[3].Points.AddXY(X[0], Y[0]);
                    }
                    else
                    {
                        chart1.Series[3].Points.AddXY(X[0], Y[0]);
                        chart1.Series[3].Points.AddXY(X2[0], Y2[0]);
                        chart1.Series[3].Points.AddXY(X[1], Y[1]);
                        chart1.Series[3].Points.AddXY(X2[1], Y2[1]);
                        chart1.Series[3].Points.AddXY(X[0], Y[0]);
                    }
                    chart1.Series[2].Color = Color.White;
                }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                chart1.Series[2].Color = Color.Black;
                chart1.Series[3].Points.Clear();
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
            {
                chart1.Series[3].Points.Clear();
                chart1.Series[2].Color = Color.White;
            }
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox6.Checked == false) chart1.Series[5].Points.Clear();
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox5.Checked == false) chart1.Series[1].Points.Clear();
        }

        private void btnDraw_Click(object sender, EventArgs e)
        {
            Draw();
        }
    }
}
