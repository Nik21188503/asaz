using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Funtions
{
    static class Encode
    {
        public static void Swap(char a, char b)
        {
            char temp; ;
            temp = a;
            a = b;
            b = temp;
        }
        public static string EncodeLog(string name)
        {
            for(int i = 0; i < name.Length / 2; i++)
            {
                Swap(name[i], name[name.Length - i - 1]);
            }
            StringBuilder sb = new StringBuilder();
            foreach(char c in name)
            {
                if (c >= 79)
                {
                    int a = (int)c;
                    a = a - 47;
                    char b = (char)a;
                    sb.Append(b);
                }
                else
                {
                    int a = (int)c;
                    a = a + 47;
                    char b = (char)a;
                    sb.Append(b);
                }
            }
            return sb.ToString();
        }
    }
}
