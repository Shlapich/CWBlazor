using System.IO;
using Microsoft.Data.SqlClient;

namespace CWBlazor.Server.KeyStore
{
    public class DBKeyStorage : DidiSoft.Pgp.Storage.IKeyStorage
    {
        private const string DefaultSqlIns = "SELECT TOP 1 KeyStoreData FROM KeyStore";

        private string connectionString;
        public DBKeyStorage(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public Stream GetInputStream()
        {
            var myConnection = new SqlConnection(this.connectionString);
            myConnection.Open();

            using (var cmdIns = new SqlCommand(DefaultSqlIns, myConnection))
            using (var dr = cmdIns.ExecuteReader())
            {
                var data = new byte[] { };
                if (dr.Read())
                {
                    data = (byte[])dr["KeyStoreData"];
                }
                return new MemoryStream(data);
            }
        }

        public void Store(Stream dataStream, int dataLength)
        {
            var myConnection = new SqlConnection(this.connectionString);
            myConnection.Open();

            string sqlIns;
            var cmdSelect = new SqlCommand(DefaultSqlIns, myConnection);
            var dr = cmdSelect.ExecuteReader();
            var rowExists = dr.Read();
            dr.Close();
            cmdSelect.Dispose();

            if (rowExists)
            {
                sqlIns = "UPDATE KeyStore SET KeyStoreData = @data";
                var cmdIns = new SqlCommand(sqlIns, myConnection);
                cmdIns.Parameters.AddWithValue("@data", dataStream);
                cmdIns.ExecuteNonQuery();
                cmdIns.Dispose();
            }
            else
            {
                sqlIns = "INSERT INTO KeyStore(KeyStoreData) VALUES (@data)";
                var cmdIns = new SqlCommand(sqlIns, myConnection);
                cmdIns.Parameters.AddWithValue("@data", dataStream);
                cmdIns.ExecuteNonQuery();
                cmdIns.Dispose();
            }
            myConnection.Close();
        }
    }
}