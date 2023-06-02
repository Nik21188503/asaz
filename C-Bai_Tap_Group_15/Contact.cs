using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace WindowsFormsApp1
{
    public partial class Contact : Form
    {
        public bool IdCheck = false;
        public bool EmailCheck = false;
        public bool NameCheck = false;
        public bool PhoneCheck = false;
        private static List<string> ContrysideInVietNam=new List<string>();
        public Contact()
        {
            InitializeComponent();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            button1.BackColor = Color.IndianRed;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.BackColor = Color.Firebrick;
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            if (ValidateValue.isValidateEmty(textBox8.Text))
            {
                panel12.Visible = true;
                label12.Text = "Email không thể để trống";

            }
            else
            {
                if (!ValidateValue.isValidateEmail(textBox8.Text))
                {
                    panel12.Visible = true;
                    label12.Text = "Email không đúng";
                }
                else
                {
                    panel12.Visible = false;
                    EmailCheck = true;
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            textBox1_TextChanged(sender, e);
            textBox2_TextChanged(sender, e);
            textBox6_TextChanged(sender, e);
            textBox8_TextChanged(sender, e);
            if (IdCheck&&NameCheck&&PhoneCheck&&EmailCheck){
                string gender=null;
                if (radioButton3.Checked)
                {
                    gender = "Nữ";
                }
                else
                {
                    gender = "Nam";
                }
                string email = InformationList.email;
                string CodingRegion = Coding.getCoding(comboBox2.Text);
                string insert = $"insert into information values('{textBox1.Text}',N'{textBox2.Text}',N'{comboBox1.Text}','{dateTimePicker1.Value}',N'{comboBox2.Text}','{textBox6.Text}',N'{textBox8.Text}',N'{gender}','{CodingRegion}',N'{email}',null,null,null,null)";
                ValidateValue.ConnectionString();
                ValidateValue.Connection.Open();
                SqlCommand cmd = new SqlCommand(insert, ValidateValue.Connection);
                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Chúc mừng bạn đã đăng kí biển số xe thành công", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MessageBox.Show($"Biển số xe của bạn là {CodingRegion}","", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                catch
                {
                    MessageBox.Show("Số căn cước này đã đăng kí trước đấy", "WARRNING!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            StreamReader sr = new StreamReader("63TinhThanh.csv");
            while (!sr.EndOfStream)
            {
                var EachRow = sr.ReadLine();
                ContrysideInVietNam.Add(EachRow);
            }
            foreach (var item in ContrysideInVietNam)
            {
                comboBox1.Items.Add(item.ToString());
                comboBox2.Items.Add(item.ToString());
            }
            comboBox1.SelectedIndex = 5;
            comboBox2.SelectedIndex = 5;
            dateTimePicker1.MaxDate = DateTime.Now;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (ValidateValue.isValidateEmty(textBox1.Text))
            {
                panel5.Visible = true;
                label5.Text = "Số căn cước không thể để trống";
            }
            else
            {
                if (!ValidateValue.isNumber(textBox1.Text))
                {
                    panel5.Visible = true;
                    label5.Text = "Số căn cước chỉ có thể chứa số";
                }
                else
                {
                    if (textBox1.Text.Length != 12)
                    {
                        panel5.Visible = true;
                        label5.Text = "Số căn cước có 12 số";
                    }
                    else
                    {
                        panel5.Visible = false;
                        IdCheck = true;
                    }
                }
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (ValidateValue.isValidateEmty(textBox2.Text))
            {
                panel13.Visible = true;
                label13.Text = "Họ tên không thể để trống";
            }
            else
            {
                if (!ValidateValue.isLetter(textBox2.Text))
                {
                    panel13.Visible = true;
                    label13.Text = "Họ tên chỉ có thể chứa chữ";
                }
                else
                {
                    panel13.Visible = false;

                    NameCheck = true;
                }
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            if (ValidateValue.isValidateEmty(textBox6.Text))
            {
                panel15.Visible = true;
                label15.Text = "Số điện thoại không thể để trống";
            }
            else
            {
                if (!ValidateValue.isNumber(textBox6.Text))
                {
                    panel15.Visible = true;
                    label15.Text = "Số điện thoại chỉ có thể chứa số";
                }
                else
                {
                    if (textBox6.Text.Length != 10)
                    {
                        panel15.Visible = true;
                        label15.Text = "Số điện thoại chỉ có 10 sô";
                    }
                    else
                    {
                        panel15.Visible = false;
                        PhoneCheck = true;
                    }
                }
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            textBox2_TextChanged(sender, e);
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            textBox1_TextChanged(sender, e);
        }

        private void textBox6_Leave(object sender, EventArgs e)
        {
            textBox6_TextChanged(sender, e);
        }

        private void textBox8_Leave(object sender, EventArgs e)
        {
            textBox8_TextChanged(sender, e);
        }

        private void Contact_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
