using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    static class Coding
    {
        public static string getCoding(string region)
        {
            string Footer = null;
            List<string> listCoding = new List<string>();
            string[] splitCoding = new string[3];
            var filepath = "MaVung.csv";
            string region1=null;
            string region2=null;
            int index = 0;
            string region3=null;
            using (StreamReader sr = new StreamReader(filepath))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine();
                    var value = line.Trim(',').Split(',');
                    if (value[0] == region)
                    {
                        ValidateValue.ConnectionString();
                        ValidateValue.Connection.Open();
                        string sql = $"select max(information.codingregion) from information where place=N'{region}'";
                        using(SqlCommand cmd = new SqlCommand(sql,ValidateValue.Connection)) {
                            using(SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    try
                                    {
                                        Footer = reader.GetString(0).Trim();
                                    }
                                    catch {
                                        Footer = null;
                                    }
                                }
                            }
                        }
                        //Tim kiem cai bien so xe cuoi cung trong co so du lieu day mk gan truc tiep 
                        if (Footer != null)
                        {
                            splitCoding = Footer.Split(new char[] { '-', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            region1 = splitCoding[0];
                        }
                        else
                        {
                            region1= value[1];
                        }
                        for (int i = 1; i < value.Length; i++)
                        {
                            listCoding.Add(value[i]);
                        }
                        break;
                    }

                }
            }
            for (int i = 0; i < listCoding.Count; i++)
            {
                if (listCoding[i] == splitCoding[0])
                {
                    region1 = listCoding[i];
                    index = i + 1;
                }
            }
            var reslts = new StringBuilder();
            if (Footer == null)
            {
                reslts.AppendLine($"{region1}-A0 0001");
            }
            else
            {
                region3 = splitCoding[2].TrimStart('0');
                if (region3.Length == 0)
                {
                    region3 = "0";
                }
                region3=(int.Parse(region3)+1).ToString();
                if (region3.Length <= 4)
                {
                    while (region3.Length < 4)
                    {
                        region3 = "0" + region3;
                    }
                }
                else
                {
                    region3 = "0001";
                }
                region2 = splitCoding[1];
                if (region3 == "0001")
                {
                    if (region2[1] != '9')
                    {
                        char two = ((char)Convert.ToInt16(region2[1] + 1));
                        char first = region2[0];
                        region2 = String.Concat(first, two);
                    }
                    else
                    {
                        if (region2[0] >= 65 && region2[0] < 90)
                        {
                            int unicode = region2[0];
                            unicode = unicode + 1;
                            char first = Convert.ToChar(unicode);
                            char two = '0';
                            region2 = String.Concat(first, two);
                        }
                        else
                        {
                            try
                            {
                                region1 = listCoding[index];
                                region2 = "A0";
                                region3 = "0001";
                            }
                            catch
                            {
                                return "Không thể cấp phát biển số xe";
                            }
                        }
                    }
                }
                reslts.Append(region1 + "-" + region2 + " " + region3);
            }
            return reslts.ToString();
        }
    }
}
