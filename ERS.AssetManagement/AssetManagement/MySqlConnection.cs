using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManagement
{
    public class MySqlConnection
    {
        public SqlConnection Connection { get; set; }

        public MySqlConnection()
        {

        }

        public void Close()
        {
            Connection.Close();
        }

        public void Open()
        {
            Connection.Open();
        }
    }
}
