using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Funtions
{
    static class Search
    {
        public static DataTable search(string option,string message)
        {

            ValidateValue.ConnectionString();
            ValidateValue.Connection.Open();
            DataTable dt = new DataTable();
            string sql = $"select information.firstname as 'Ho tên',information.contryside as 'Quê quán',information.birthday as 'Ngày sinh',information.place as 'Nơi đăng kí xe',information.phone as 'Số điện thoại',information.email,information.gender as 'Giới tính',information.codingregion as 'Biển số xe',information.ViolationError as 'Lỗi vi phạm',information.ImmediatelyPunished as 'Ngày vi phạm',information.PenaltyDays as 'Số ngày bị phạt',information.FinedAmount as 'Số tiền bị phạt' from information where information.{option} =N'{message}'";

            SqlDataAdapter adapter = new SqlDataAdapter(sql,ValidateValue.Connection);
            adapter.Fill(dt);
            return dt;
        }

    }
}
