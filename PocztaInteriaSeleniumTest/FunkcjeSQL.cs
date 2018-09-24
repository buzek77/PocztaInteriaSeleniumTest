using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocztaInteriaSeleniumTest
{
    public class FunkcjeSQL
    {
        public object PobierzDane(string tekst)
        {
            using (SqlConnection con = new SqlConnection(Settings.connetionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(tekst, con))
                {
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        var lst = new List<object>();
                        Object[] wartosc = new Object[2];
                        while (rdr.Read())
                        {
                            int liczbakolumn = rdr.GetValues(wartosc);
                            for (int i = 0; i < liczbakolumn; i++)
                            {
                                string row = "";
                                row = rdr.GetValue(i).ToString();
                                lst.Add(row);
                            }
                        }
                        Object[] tabela = lst.ToArray();
                        return tabela;
                        rdr.Close();
                    }
                }
            }
        }
    }
}
