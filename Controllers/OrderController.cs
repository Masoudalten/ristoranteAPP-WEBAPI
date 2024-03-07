using apiTest.Classes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Data;

namespace apiTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        public readonly IConfiguration _configuration;
        public OrderController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        public IActionResult GetOrderList()
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DBCon").ToString());
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Orders", con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            List<OrderGroup> orderList = new List<OrderGroup>();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    List<Order> orderItems = new List<Order>();
                    string orderArrayJson = row["order_array"].ToString();
                    JArray orderArray = JArray.Parse(orderArrayJson);
                    int order_id = Convert.ToInt32(row["order_id"]);
                    int total = Convert.ToInt32(row["total"]);
                    DateTime dateTime = (DateTime)(row["date"]);
                    string time = dateTime.ToString("yyyy-MM-dd HH:mm:ss");


                    foreach (JObject orderObject in orderArray)
                    {
                        Order order = new Order();
                        order.tNumber = Convert.ToInt32(orderObject["tNumber"]);
                        order.Name = Convert.ToString(orderObject["Name"]);
                        order.Price = Convert.ToInt32(orderObject["Price"]);
                        order.Quantity = Convert.ToInt32(orderObject["Quantity"]);

                        orderItems.Add(order);
                    }
                    OrderGroup orderGroup = new OrderGroup()
                    {
                        OrderId = order_id,
                        OrderItems = orderItems,
                        total = total,
                        time = time
                    };
                    orderList.Add(orderGroup);
                }
                return Ok(orderList);
            }
            else
            {
                return Ok(new List<OrderGroup>());
            }
        }
        [HttpPost]
        public IActionResult CreateNewOrder([FromBody] Order[] orderItems, [FromQuery] int total)
        {
            string connectionString = _configuration.GetConnectionString("DBCon");

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    SqlCommand command = new SqlCommand("INSERT INTO Orders (order_array, total) VALUES (@orderItems, @total)", con);
                    command.Parameters.AddWithValue("@orderItems", JsonConvert.SerializeObject(orderItems));
                    command.Parameters.AddWithValue("@total", total);
                    command.ExecuteNonQuery();
                    return Ok(new { message = "Record created successfully" });
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Internal server error: {ex.Message}");
                }
            }
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(int id)
        {
            string connectionString = _configuration.GetConnectionString("DBCon");

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    SqlCommand command = new SqlCommand("DELETE FROM Orders WHERE order_id = @order_id", con);
                    command.Parameters.AddWithValue("@order_id", id);
                    command.ExecuteNonQuery();
                    return Ok(new { message = "Record removed successfully" });
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Internal server error: {ex.Message}");
                }
            };
        }

    }
}
