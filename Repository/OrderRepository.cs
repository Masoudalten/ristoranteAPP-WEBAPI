using apiTest.Interface;
using apiTest.Model;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;

namespace apiTest.Repository
{
    public class OrderRepository : IOrderRepository
    {

        public readonly IConfiguration _configuration;
        public OrderRepository(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }


        public void CreateNewOrder(Order[] orderItems, int total)
        {
            string connectionString = _configuration.GetConnectionString("DBCon");

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand command = new SqlCommand("INSERT INTO Orders (order_array, total) VALUES (@orderItems, @total)", con);
                command.Parameters.AddWithValue("@orderItems", JsonConvert.SerializeObject(orderItems));
                command.Parameters.AddWithValue("@total", total);
                command.ExecuteNonQuery();
            }
        }

        public void DeleteOrder(int id)
        {
            string connectionString = _configuration.GetConnectionString("DBCon");

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand command = new SqlCommand("DELETE FROM Orders WHERE order_id = @order_id", con);
                command.Parameters.AddWithValue("@order_id", id);
                command.ExecuteNonQuery();
            };
        }

        public IEnumerable<OrderGroup> GetOrderList()
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DBCon").ToString());
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Orders", con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            List<OrderGroup> orderList = new List<OrderGroup>();

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
            return orderList;
        }
    }
}

