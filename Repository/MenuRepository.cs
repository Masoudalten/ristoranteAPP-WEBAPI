using apiTest.Interface;
using apiTest.Model;
using Microsoft.Data.SqlClient;
using System.Data;

namespace apiTest.Repository
{
    public class MenuRepository : IMenuRepository
    {
        private readonly IConfiguration _configuration;

        public MenuRepository(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }
        public IEnumerable<Piatto> GetMenuItems()
        {
            var connectionString = _configuration.GetConnectionString("DBCon");

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM MenuItems", con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                List<Piatto> menuItems = new List<Piatto>();

                foreach (DataRow row in dt.Rows)
                {
                    Piatto piatto = new Piatto();
                    piatto.nome = Convert.ToString(row["Nome"]);
                    piatto.ricetta = Convert.ToString(row["Ricetta"]);
                    piatto.prezzo = Convert.ToInt32(row["Prezzo"]);
                    menuItems.Add(piatto);
                }
                return menuItems;
            }
        }
    }
}
