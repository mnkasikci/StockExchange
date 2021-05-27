using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using StockExchangeDataManager.Library.DataAccess;
using StockExchangeDataManager.Library.Helpers;
using StockExchangeDataManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using static StockExchangeDataManager.Library.DataAccess.MoneyTypeData;

namespace StockExhangeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MoneysController : Controller
    {

        private readonly IConfiguration _config;

        public MoneysController(IConfiguration config)
        {
            _config = config;
        }
        [HttpPost]
        [Route("Pending")]
        public async Task<IActionResult> AddPendingMoney(AddPendingMoneyModel moneyModel)
        {
            if (moneyModel == null || moneyModel.Amount <= 0 || moneyModel.CurrencyCode.Length==0) return BadRequest();

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
        public record AuthorizePendingMoneyrecord(string pendingMoneyId);
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AuthorizePendingMoney(AuthorizePendingMoneyrecord p)
        {
            MoneyTypeData data = new MoneyTypeData(_config);


            int pmID = int.Parse(p.pendingMoneyId);
            PendingMoneyModel pm = await data.GetPendingMoneyById(pmID);

            CurrencyHelper currencyHelper = new CurrencyHelper(_config);
            string currencyCode = pm.CurrencyCode;

            string userID = User.FindFirstValue(ClaimTypes.NameIdentifier);

            try
            {
                decimal currencyRate = currencyHelper.GetCurrencyRateToTurkishLira(currencyCode);
                await data.AuthorizePendingMoney(pmID, userID, currencyRate);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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

        [HttpPost]
        [Route("BuyOffers")]
        public async Task<IActionResult> CreateBuyOffer(OfferModel offer)
        {
            if (offer.Amount <= 0 || offer.UnitPrice <= 0) return BadRequest();

            MoneyTypeData data = new MoneyTypeData(_config);
            string userID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            offer.OffererID = userID;

            var userMoney = await data.GetUserMoneyByID(userID);

            if (userMoney >= offer.Amount * offer.UnitPrice * 1.01m)
            {
                await data.CreateBuyOffer(offer);
                return Ok();
            }
            else
                return BadRequest("User doesn't have neough money");

        }
        [HttpGet]
        [Route("Currencies")]
        public async Task<List<CurrencyType>> GetAllCurrencyTypes()
        {
            MoneyTypeData data = new MoneyTypeData(_config);
            return await data.GetAllCurrencyTypes();
        }

    }
}
