using apiTest.Classes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Data;

namespace apiTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        public readonly IConfiguration _configuration;
        public MenuController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        public string GetMenuItems()
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DBCon").ToString());
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM MenuItems", con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            List<Piatto> menuItems = new List<Piatto>();

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Piatto piatto = new Piatto();
                    piatto.nome = Convert.ToString(dt.Rows[i]["Nome"]);
                    piatto.ricetta = Convert.ToString(dt.Rows[i]["Ricetta"]);
                    piatto.prezzo = Convert.ToInt32(dt.Rows[i]["Prezzo"]);
                    menuItems.Add(piatto);
                }
                return JsonConvert.SerializeObject(menuItems);
            }
            else
            {
                return "No value";
            }

        }
    }
}
