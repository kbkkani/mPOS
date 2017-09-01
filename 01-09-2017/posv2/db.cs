using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
namespace posv2
{
    class db
    {
        //Main vars
        private MySqlConnection _con;
        public MySqlCommand cmd;
        private MySqlDataAdapter _da;
        private DataTable _dt;
        private MySqlDataReader _dr;

        //server
        private string server = "localhost";
        private string server_user = "root";
        private string server_db = "restaurant_gall";
        private string server_passsword = "";

        public db()
        {
            //create new connection
            //_con = new MySqlConnection("SERVER='localhost';DATABASE=pos_kot;UID='root';PASSWORD='WrKu3whDNPT9E7Sm';");

            _con = new MySqlConnection("SERVER='" + server + "';DATABASE='" + server_db + "';UID='" + server_user + "';PASSWORD='WrKu3whDNPT9E7Sm';");
            _con.Open();

        }
        public void conClose()
        {
            _con.Close();
        }
        public void MysqlQuery(string QueryText)
        {
            using (cmd = new MySqlCommand(QueryText, _con)) ;
        }

        public DataTable QueryEx()
        {
            _da = new MySqlDataAdapter(cmd);
            _dt = new DataTable();
            _da.Fill(_dt);
            return _dt;
        }

        public MySqlDataAdapter QueryExDataAdapter()
        {
            _da = new MySqlDataAdapter(cmd);
            return _da;
        }

        public long NonQueryEx()
        {
            cmd.ExecuteNonQuery();
            return cmd.LastInsertedId;
        }

        public MySqlDataReader ExReader()
        {
            _dr = cmd.ExecuteReader();
            return _dr;
        }

    }
}
