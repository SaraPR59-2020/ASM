using LocalDeviceProject;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManagement
{
    public class DataBase
    {
        public MySqlConnection MyConnection { get; set; }


        public DataBase() 
        {
            MyConnection = new MySqlConnection();
        }

 
        public bool SendCommand(string command)
        {
            try
            {
                MyConnection.Open();
                SqlCommand tmp = new SqlCommand(command, MyConnection.Connection);
                tmp.ExecuteNonQuery();
                MyConnection.Close();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Komanda neuspesno poslata u bazu podataka! " + e);
                MyConnection.Close();
                return false;
            }
        }

        public void InsertData(LocalDevice localDevice)
        {
            var command = "insert into Local_device (Id,Type,Value,Work_time,Timestamp,LocalDeviceCode) values ('" + localDevice.Id + "', '" + localDevice.Type + "', '" + localDevice.WorkTime + "', '" + localDevice.Timestamp + "', '" + localDevice.LocalDeviceCode + "')";
            SendCommand(command);
        }
        public virtual void CreateDataBase()
        {
            string relativePath = @"..\..\database.mdf";
            string absolutePath = Path.GetFullPath(relativePath);
            string connectionString = String.Format(@"Data Source=(LocalDB)\MSSQLLocalDB; AttachDbFilename={0};Integrated Security=True", absolutePath);
            Console.WriteLine(connectionString);
            MyConnection.Connection = new SqlConnection(connectionString);
            string createTableSql = "CREATE TABLE Local_device ( Id varchar(255),Type varchar(255),Value varchar(255),Work_time decimal(13,2), Timestamp int, LocalDeviceCode varchar(255));";
            SendCommand(createTableSql);
        }

        public virtual void SaveData(List<LocalDevice> devices)
        {
            foreach (LocalDevice device in devices)
            {
                InsertData(device);
            }
        }

    }
}
