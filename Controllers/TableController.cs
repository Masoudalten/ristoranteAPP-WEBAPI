using apiTest.Interface;
using apiTest.Model;
using Microsoft.AspNetCore.Mvc;

namespace apiTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TableController : ControllerBase
    {
        public readonly IConfiguration _configuration;
        private readonly ITableRepository _tableRepository;
        public TableController(ITableRepository tableRepository)
        {
            _tableRepository = tableRepository ?? throw new ArgumentNullException(nameof(tableRepository));
        }
        [HttpGet]
        public IActionResult GetTables()
        {
            try
            {
                var tables = _tableRepository.GetTables();
                return Ok(tables);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpGet("{id}")]
        public IActionResult GetTable(int id)
        {
            try
            {
                var table = _tableRepository.GetTable(id);
                if (table != null)
                {
                    return Ok(table);
                }
                else
                {
                    return NotFound("Table not found");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpPut("{id}")]
        public IActionResult UpdateTableStatus(int id, string status)
        {
            try
            {
                _tableRepository.UpdateTableStatus(id, status);
                    return Ok(new { message = "Record updated successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Internal server error: {ex.Message}" });
            }
        }
    }
}