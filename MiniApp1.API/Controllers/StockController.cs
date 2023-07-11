using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace MiniApp2.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        [Authorize(Policy ="AgePolicy")]
        [Authorize(Roles = "admin",Policy = "BursaPolicy")]
        [HttpGet]
        public IActionResult GetStocks()
        {
            var userName = HttpContext.User.Identity.Name;

            var userIdClaim = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier) ;

            //veri tabanında userId veya userName ile kullanıcıya özel verileri çekip döndürebiliriz.
            if (userName == null)
            {
                return Ok("App - Stocks");
            }
            return Ok($"UserName:{userName} - UserId: {userIdClaim.Value} - Stocks");
        }
    }
}
