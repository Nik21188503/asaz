using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    static class InformationList
    {
        public  static List<Account> UserAccount =new List<Account>();
        public static  List<Account> AdminAccount=new List<Account>();
        public static string email=null;
        public static List<CatchError> cath = new List<CatchError>();
        public static Dictionary<string,string> option = new Dictionary<string, string>()
        {
            {"id" ,"Số căn cước" },
            { "firstname","Họ Tên" },
            {"codingregion" ,"Biên số xe" },
            {"contryside", "Quê Quán" },
            { "place","Nơi Đăng Kí Xe" },
            { "phone","Số điện thoại" },
            {"email","Email" }
        };
        
    }
}
