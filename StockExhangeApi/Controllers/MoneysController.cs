using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using StockExchangeDataManager.Library.DataAccess;
using StockExchangeDataManager.Library.Models;
using System.Collections.Generic;
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
        [HttpGet]
        [Route("Pending")]
        [Authorize(Roles = "Admin")]
        public async Task<List<PendingMoneyModel>> GetAllPendingMoneys()
        {
            MoneyTypeData data = new MoneyTypeData(_config);
            return await data.GetAllPendingMoneys();

        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AuthorizePendingMoney(PendingMoneyModel pendingMoney)
        {

            MoneyTypeData data = new MoneyTypeData(_config);
            string userID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await data.AuthorizePendingMoney(pendingMoney.PendingId, userID);
            return Ok();
        }
        [HttpPost]
        [Route("Refuse")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RefusePendingMoney(PendingMoneyModel pendingMoney)
        {

            MoneyTypeData data = new MoneyTypeData(_config);
            string userID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await data.RefusePendingMoney(pendingMoney.PendingId, userID);
            return Ok();
        }




    }
}
