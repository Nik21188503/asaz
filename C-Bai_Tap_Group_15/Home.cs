using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.IO;
using Newtonsoft.Json;
using WindowsFormsApp1.Funtions;

namespace WindowsFormsApp1
{
    public partial class Home : Form
    {
        bool MoveDown=false;
        public Home()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                string text = File.ReadAllText("log.json");
                var account = JsonConvert.DeserializeObject<Account>(text);
                textBox1.Text = Encode.EncodeLog(account.name);
                textBox2.Text = Encode.EncodeLog(account.password);
                checkBox1.Checked = true;
            }
            catch
            {

            }
            timer1.Enabled = true;
            button2.FlatAppearance.BorderSize = 0;
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            button1.BackColor = Color.Gray;
        }

        private void button1_MouseLeave(object sender, EventArgs e) 
        {
            button1.BackColor = Color.Black;
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            panel7.Visible = false;
            pictureBox6.Visible = false;
            label6.ForeColor = Color.Red;
            while (true)
            {
                if ((ValidateValue.isValidateEmty(textBox1.Text)||(panel6.Visible&&ValidateValue.isValidateEmty(textBox3.Text))||(panel4.Visible && ValidateValue.isValidateEmty(textBox2.Text))))
                {
                    panel7.Visible = true;
                    label6.Text = "Các thông tin không được để trống";
                    break;
                }
                if (!ValidateValue.isValidateEmail(textBox1.Text)&&label1.Text!= "TÀI KHOẢN")
                {
                    panel7.Visible = true;
                    label6.Text = "email không hợp lệ";
                    break;
                }
                if (ValidateValue.isValidatePassWord(textBox2.Text, textBox3.Text) && panel6.Visible)
                {
                    panel7.Visible = true;
                    label6.Text = "Mật khẩu xác nhận không khớp ";
                    break;
                }
                if(label1.Text=="EMAIL"&& !panel6.Visible)
                {
                    if (ValidateValue.isInListUser(textBox1.Text, textBox2.Text))
                    {
                        InformationList.email=textBox1.Text;
                        File.Delete("log.json");
                        if (checkBox1.Checked)
                        {
                            Account ac = new Account();
                            ac.name = Encode.EncodeLog(textBox1.Text);
                            ac.password = Encode.EncodeLog(textBox2.Text);
                            var json = JsonConvert.SerializeObject(ac);
                            using (var sr = new StreamWriter("log.json", true))
                            {
                                sr.WriteLine(json.ToString());
                                sr.Close();
                            }
                        }
                        this.Close();
                        Contact contact = new Contact();
                        contact.ShowDialog();
                    }
                    else
                    {
                        panel7.Visible = true;
                        label6.Text = "Tài khoản hoặc mật khẩu không đúng";
                    }
                    break;
                }
                if (label1.Text=="TÀI KHOẢN")
                {
                    if (ValidateValue.isInListAdmin(textBox1.Text,textBox2.Text))
                    {
                        this.Close();
                        WorksAdmin admin = new WorksAdmin();
                        admin.ShowDialog();
                        //ghim ten nguoi phat
                    }
                    else
                    {
                        panel7.Visible = true;
                        label6.Text = "Tài khoản hoặc mật khẩu không đúng";
                    }
                    break;
                }
                if (!panel4.Visible)
                {
                    if (ValidateValue.isInListUser(textBox1.Text,null))
                    {
                        // forgot password code heere
                    }
                    else
                    {
                        panel7.Visible = true;
                        label6.Text = "Email này chưa từng đăng kí";
                    }
                    break;
                }
                if (panel6.Visible)
                {
                    if (ValidateValue.isInListUser(textBox1.Text,null))
                    {
                        panel7.Visible = true;
                        label6.Text = "Email này đã được đăng kí";
                    }
                    else {
                        ValidateValue.InsertAccout(textBox1.Text, textBox2.Text);
                        panel7.Visible = true;
                        pictureBox5.Visible = false;
                        pictureBox6.Visible=true;
                        label6.Text = "Chúc mừng bạn đã đăng kí tài khỏan thàng công!!";
                        label6.ForeColor = Color.Green;
                        textBox1.Clear();
                        textBox2.Clear();
                        textBox3.Clear();
                    }
                    break;
                }
                break;
            }
        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            panel4.Visible = true;
            checkBox1.Visible = true;
            MoveDown=false;
            panel6.Visible = false;
            button1.Text = "ĐĂNG NHẬP";
            textBox1.Text = null; textBox2.Text=null;
            panel7.Visible=false;
            if (linkLabel1.Text.ToString() == "admin<")
            {
                label1.Text = "TÀI KHOẢN";
                label2.Text = "MẬT KHẨU";
                linkLabel1.Text = "^user^";
            }
            else
            {
                label1.Text = "EMAIL";
                label2.Text = "PASSWORLD";
                linkLabel1.Text = "admin<";
            }
        }
        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            label1.Text = "Find Your Account\nPlease enter your email to search for account";
            panel4.Visible = false;
            textBox1.Text = null; textBox2.Text = null;
            checkBox1.Visible=false;
            panel6.Visible = false;
            panel7.Visible = false;
            if (panel4.Visible == false)
            {
                MoveDown = true;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (MoveDown && panel3.Top<100)
            {
                label1.Top -= 1;
                panel3.Top += 7;
                button1.Text = "Search";
            }
            if(!MoveDown && panel3.Top>=-20)
            {
                label1.Top += 1;
                panel3.Top -= 7;
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            textBox1.Text = null; textBox2.Text = null;textBox3.Text = null;
            label1.Text = "EMAIL";
            label2.Text = "PASSWORLD";
            MoveDown = false;
            panel4.Visible=true;
            panel6.Visible = true;
            checkBox1.Visible = false;
            panel7.Visible = false;
            button1.Text = "ĐĂNG KÍ";
        }
        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult exit = MessageBox.Show("Bạn có chắc chắn muốn thoát chương trình", "!!Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (exit == DialogResult.Yes)
            {
                Close();
            }
        }


        private void pictureBox2_Click(object sender, EventArgs e)
        {
            pictureBox2.Visible = false;
            pictureBox3.Visible = true;
            textBox2.PasswordChar = '\0';
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            pictureBox2.Visible = true;
            pictureBox3.Visible = false;
            textBox2.PasswordChar = '^';
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            pictureBox4.Visible = false;
            pictureBox5.Visible = true;
            textBox3.PasswordChar = '^';
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            pictureBox4.Visible = true;
            pictureBox5.Visible = false;
            textBox3.PasswordChar = '\0';
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
