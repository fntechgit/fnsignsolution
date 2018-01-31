using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace schedInterface
{
    public class database
    {
        public SqlConnection connect(bool prod)
        {
            if (prod)
                return new SqlConnection("Data Source=172.16.1.65\\FNTECH;Initial Catalog=fntech_signage;Persist Security Info=True;User ID=sa;Password=OhHowHeLovesUs!");
            return new SqlConnection("Data Source=172.16.1.65\\FNTECH;Initial Catalog=fntech_signage;Persist Security Info=True;User ID=sa;Password=OhHowHeLovesUs!");
        }
    }
}