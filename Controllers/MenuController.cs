using apiTest.Interface;
using Microsoft.AspNetCore.Mvc;

namespace apiTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IMenuRepository _menuRepository;

        public MenuController(IMenuRepository menuRepository)
        {
            _menuRepository = menuRepository ?? throw new ArgumentNullException(nameof(menuRepository));
        }

        [HttpGet]
        public IActionResult GetMenuItems()
        {
            try
            {
                var menuItems = _menuRepository.GetMenuItems();
                return Ok(menuItems);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
