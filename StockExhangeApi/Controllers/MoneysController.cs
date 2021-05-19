using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using StockExchangeDataManager.Library.DataAccess;
using StockExchangeDataManager.Library.Models;
using System.Security.Claims;
using System.Threading.Tasks;

namespace StockExhangeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MoneysController : Controller
    {

        private readonly IConfiguration _config;

        public MoneysController(IConfiguration config, UserManager<IdentityUser> userManager)
        {
            _config = config;
        }
        [HttpPost]
        [Route("Pending")]
        public async Task<IActionResult> AddPendingMoney(AddPendingMoneyModel moneyModel)
        {
            if (moneyModel == null || moneyModel.Amount <= 0) return BadRequest();


            MoneyTypeData data = new MoneyTypeData(_config);
            string userID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            moneyModel.UserID = userID;
            await data.AddPendingMoney(moneyModel);
            return Ok();
        }
        [HttpGet]
        public async Task<decimal> GetUserMoneyByID()
        {
            MoneyTypeData data = new MoneyTypeData(_config);
            string userID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var amount = await data.GetUserMoneyByID(userID);
            return amount;

        }




    }
}
