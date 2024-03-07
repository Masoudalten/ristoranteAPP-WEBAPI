using apiTest.Classes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;

namespace apiTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TableController : ControllerBase
    {
        public readonly IConfiguration _configuration;
        public TableController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        public String GetTables()
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
                return JsonConvert.SerializeObject(tables);
            }
            else
            {
                return "No value";
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetTable(int id)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DBCon").ToString());
            SqlCommand cmd = new SqlCommand("SELECT * FROM Tables WHERE TNumber = @Id", con);
            cmd.Parameters.AddWithValue("@Id", id);

            try
            {
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
                    return Ok(table);
                }
                else
                {
                    con.Close();
                    return NotFound("Table not found");
                }
            }
            catch (Exception ex)
            {
                con.Close();
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTableStatus(int id, Table updatedTable)
        {
            try
            {
                using (var con = new SqlConnection(_configuration.GetConnectionString("DBCon")))
                {
                    con.Open();
                    using (var cmd = new SqlCommand("UPDATE Tables SET Status = @Status WHERE TNumber = @Id", con))
                    {
                        cmd.Parameters.AddWithValue("@Id", id);
                        cmd.Parameters.AddWithValue("@Status", updatedTable.status);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            return Ok(new { message = "Record updated successfully" });
                        }
                        else
                        {
                            return NotFound(new { message = "Table not found" });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Internal server error: {ex.Message}" });
            }
        }
    }
}