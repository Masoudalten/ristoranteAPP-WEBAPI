using apiTest.Interface;
using apiTest.Model;
using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;


namespace apiTest.Repository
{
    public class TableRepository : ITableRepository
    {
        private static TableRepository _instance = null;
        private static readonly object _locker = new object();

        private readonly IConfiguration _configuration;
        public TableRepository(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public static TableRepository GetInstance(IConfiguration configuration)
        {
            if (_instance == null)
            {
                lock (_locker)
                {
                    if (_instance == null)
                    {
                        _instance = new TableRepository(configuration);
                    }
                }
            }
            return _instance;
        }
        public IEnumerable<Table> GetTables()
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DBCon").ToString());
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Tables", con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            List<Table> tables = new List<Table>();

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Table table = new Table();
                    table.tNumber = Convert.ToInt32(dt.Rows[i]["TNumber"]);
                    table.name = Convert.ToString(dt.Rows[i]["Name"]);
                    table.capacity = Convert.ToInt32(dt.Rows[i]["Capacity"]);
                    table.status = Convert.ToString(dt.Rows[i]["Status"]);
                    tables.Add(table);
                }
                return tables;
            }
            else
            {
                return new List<Table>();
            }
        }
        public Table GetTable(int id)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DBCon").ToString());
            SqlCommand cmd = new SqlCommand("SELECT * FROM Tables WHERE TNumber = @Id", con);
            cmd.Parameters.AddWithValue("@Id", id);

            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                Table table = new Table();
                table.tNumber = Convert.ToInt32(reader["TNumber"]);
                table.name = Convert.ToString(reader["Name"]);
                table.capacity = Convert.ToInt32(reader["Capacity"]);
                table.status = Convert.ToString(reader["Status"]);
                con.Close();
                return table;
            }
            else
            {
                con.Close();
                return null;
            }
        }
        public void UpdateTableStatus(int id, string status)
        {

            using (var con = new SqlConnection(_configuration.GetConnectionString("DBCon")))
            using (var cmd = new SqlCommand("UPDATE Tables SET Status = @Status WHERE TNumber = @Id", con))
            {
                {
                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                    cmd.Parameters.Add("@Status", SqlDbType.VarChar).Value = status;
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}


