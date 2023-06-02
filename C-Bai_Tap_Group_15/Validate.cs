using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Contexts;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace WindowsFormsApp1
{
    public static class ValidateValue {
        public static SqlConnection Connection;
        public static void ConnectionString()
        {
            string connstr = "Server=192.168.220.1;Database=group15;User Id=sa;Password=211885";
            Connection = new SqlConnection(connstr);
        }
        public static ulong u = 0;
        public static bool isValidateEmty(string emty) => (emty == null || emty.Trim().Length == 0) ? true : false;
        public static bool isValidatePassWord(string password,string ConfirmPassWorld)=>(password!=ConfirmPassWorld)?true:false;
        public static bool isValidateEmail(string email) => Regex.IsMatch(email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
        public static bool isNumber(string number) => ulong.TryParse(number, out u);
        public static bool isLetter(string Letter)
        {
            foreach(char c in Letter)

            {
                if ((c < 65 && c > 32) || (c > 90 && c < 97) || (c > 122 && c <= 126))
                {
                    return false;
                }
            }
            return true;
        }
        public static bool isInListUser(string Name ,string Password)
        {
            ConnectionString();
            string sql = null;
            if (Password == null)
            {
                sql = $@"select * from useraccount where account ='{Name}'"; 
            }
            else
            {
                sql = $@"select * from useraccount where account = '{Name}' and passworda = '{Password}'";
            }
            DataTable data = new DataTable();
            Connection.Open();
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter(sql, Connection);
                adapter.Fill(data);
                
                if(data.Rows.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
        public static bool isInListAdmin(string Name, string Password)
        {
            ConnectionString();
            string sql = $@"select * from adminaccount where account = '{Name}' and passworda = '{Password}'";
            DataTable data = new DataTable();
            Connection.Open();
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter(sql, Connection);
                adapter.Fill(data);
                if (data.Rows.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
        public static void InsertAccout(string name,string password)
        {
            ConnectionString();
            Connection.Open();
            string insert_into = $"insert into useraccount values(N'{name}','{password}')";
            SqlCommand cmd = new SqlCommand(insert_into,Connection);
            cmd.ExecuteNonQuery();
        }
    }
}
