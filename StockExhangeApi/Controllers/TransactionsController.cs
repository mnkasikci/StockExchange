using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using StockExchangeDataManager.Library.DataAccess;
using StockExchangeDataManager.Library.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace StockExhangeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TransactionsController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly UserManager<IdentityUser> _userManager;

        public TransactionsController(IConfiguration config, UserManager<IdentityUser> userManager)
        {
            _config = config;
            _userManager = userManager;
        }
        [HttpGet]
        [Route("Admin")]
        [Authorize(Roles = "Admin")]
        public async Task<List<TransactionModel>> GetAllTransactions()
        {
            TransactionData data= new TransactionData(_config);
            return await data.GetAllTransactions();
        }
        [HttpGet]
        public async Task<List<TransactionModel>> GetTransactionsByID()
        {
            TransactionData data = new TransactionData(_config);
            string userID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return await data.GetTransactionsByID(userID);
        }
    }
}

