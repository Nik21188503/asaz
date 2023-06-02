using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Funtions;

namespace WindowsFormsApp1
{
    public partial class WorksAdmin : Form
    {
        public WorksAdmin()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void WorksAdmin_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Value = DateTime.Now;
            foreach(var item in InformationList.option)
            {
                comboBox1.Items.Add(item.Value);
            }
            comboBox1.SelectedIndex = 2;
            dateTimePicker1.MaxDate = DateTime.Now;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            string temp = null;
            foreach(var item in InformationList.option)
            {
                if (comboBox1.Text == item.Value)
                {
                    temp = item.Key;
                }
            }
            DataTable data = new DataTable();
            if (ValidateValue.isValidateEmty(textBox1.Text)&&!comboBox2.Visible)
            {
                MessageBox.Show("Thông tin tìm kiếm không được để trống", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (textBox1.Visible == true)
                {
                    data = Search.search(temp, textBox1.Text);
                }
                if (comboBox2.Visible)
                {
                    data = Search.search(temp, comboBox2.Text);
                }
                dataGridView1.DataSource = data;
            }
        }
        private bool chekbox1 = false; 
        private bool chekbox2 = false;
        private void  validateTexBox1()
        {

            if (ValidateValue.isValidateEmty(textBox2.Text))
            {
                chekbox1 = false;
                panel5.Visible = true;
                label6.Text = "Không thể để trống biển số xe";
            }
            else
            {
                if (Search.search("codingregion",textBox1.Text).Rows.Count<0)
                {

                    MessageBox.Show("Biển số xe này chưa được đăng kí", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    chekbox1 = true;
                    panel5.Visible = false;
                }
            }
        }
        private void validateTexBox2()
        {
            if (ValidateValue.isValidateEmty(textBox3.Text))
            {
                chekbox2 = false;
                panel2.Visible = true;
                label7.Text = "Lỗi vi phạm không thể để trống";
            }
            else
            {
                chekbox2 = true;
                panel2.Visible = false;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        { 
            validateTexBox1();
            validateTexBox2();
            if (chekbox1 && chekbox2)
            {
                ValidateValue.ConnectionString();
                ValidateValue.Connection.Open();
                DataTable dt = new DataTable();
                string sqlserach = $"select * from information where codingregion='{textBox2.Text}'";
                SqlDataAdapter sqltable= new SqlDataAdapter(sqlserach,ValidateValue.Connection);
                sqltable.Fill(dt);
                if (ValidateValue.isValidateEmty(dt.Rows[0][13].ToString()))
                {
                    button2_Click(sender, e);
                }
                else
                {
                    string sql = $"update information set PenaltyDays= {(int)numericUpDown1.Value + (int)dt.Rows[0][11]}, FinedAmount={(int)numericUpDown2.Value + (int)dt.Rows[0][12]}, ViolationError=N'{textBox3.Text + ", " + dt.Rows[0][13]}' where codingregion='{textBox2.Text}' ";
                    try
                    {
                        SqlCommand cmd = new SqlCommand(sql, ValidateValue.Connection);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Cập nhật thành công");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Hello World");
                    }

                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            validateTexBox1();
            validateTexBox2();
            if (chekbox1 && chekbox2)
            {

                ValidateValue.ConnectionString();
                ValidateValue.Connection.Open();
                string sql = $"update information set ImmediatelyPunished='{dateTimePicker1.Value}',PenaltyDays={(int)numericUpDown1.Value},FinedAmount={(int)numericUpDown2.Value},ViolationError=N'{textBox3.Text}'where codingregion='{textBox2.Text}'";
                try
                {
                    SqlCommand cmd = new SqlCommand(sql, ValidateValue.Connection);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Cập nhật thành công");
                }
                catch
                {
                    MessageBox.Show("Cập nhật thất bại");
                }
            }
        }
        private void button3_Click_1(object sender, EventArgs e)
        {
            string codding = null;
            string error = null;
            int rows = dataGridView1.RowCount;
            for(int i = 0; i < rows; i++)
            {
                if (dataGridView1.Rows[i].Selected)
                {
                    codding=(dataGridView1.Rows[i].Cells[7].Value.ToString());
                    error = dataGridView1.Rows[i].Cells[8].Value.ToString();
                }
            }
            if (!ValidateValue.isValidateEmty(codding))
            {
                if (!ValidateValue.isValidateEmty(error))
                {
                    ValidateValue.ConnectionString();
                    ValidateValue.Connection.Open();
                    string sql = $"update information set ImmediatelyPunished=null,PenaltyDays=null,FinedAmount=null,ViolationError=null where codingregion ='{codding}'";
                    try
                    {
                        SqlCommand cmd = new SqlCommand(sql, ValidateValue.Connection);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Xóa lõi thành công");

                    }
                    catch
                    {
                        MessageBox.Show("HMMM");
                    }
                }
                else
                {
                    MessageBox.Show("Xe này hiện tại k có lỗi vi phạm");
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox1.Text== "Quê Quán"| comboBox1.Text == "Nơi Đăng Kí Xe")
            {
                List<string> ContrysideInVietNam = new List<string>();
                comboBox2.Visible= true;
                textBox1.Visible = false;
                StreamReader sr = new StreamReader("63TinhThanh.csv");
                while (!sr.EndOfStream)
                {
                    var EachRow = sr.ReadLine();
                    ContrysideInVietNam.Add(EachRow);
                }
                foreach(var item in ContrysideInVietNam)
                {
                    comboBox2.Items.Add(item);
                }
                comboBox2.SelectedIndex = 18;
            }
            else
            {
                comboBox2.Visible = false;
                textBox1.Visible = true;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (ValidateValue.isValidateEmty(textBox2.Text))
            {
                panel5.Visible = true;
                label6.Text = "Không thể để trống biển số xe";
            }
            else
            {
                panel5.Visible = false;
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_Leave(object sender, EventArgs e)
        {

            if (ValidateValue.isValidateEmty(textBox3.Text))
            {
                panel2.Visible = true;
                label7.Text = "Lỗi vi phạm không thể để trống";
            }
            else
            {
                panel2.Visible = false;
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {

            if (ValidateValue.isValidateEmty(textBox2.Text))
            {
                panel5.Visible = true;
                label6.Text = "Không thể để trống biển số xe";
            }
            else
            {
                panel5.Visible = false;
            }
        }

        private void textBox3_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void WorksAdmin_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            int rows = dataGridView1.RowCount;
            for (int i = 0; i < rows; i++)
            {
                if (dataGridView1.Rows[i].Selected)
                {
                    textBox2.Text = dataGridView1.Rows[i].Cells[7].Value.ToString();
                }
            }
        }
    }
}
