using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using StockExhangeApi.Data;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace StockExhangeApi.Controllers
{
    public class TokenController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public TokenController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        [Route("/Token")]
        [HttpPost]
        public async Task<IActionResult> Create(string userNameorEmail, string password, string granttype)
        {
            
            IdentityUser identity = await _userManager.FindByNameAsync(userNameorEmail);
            if(identity==null) identity = await _userManager.FindByEmailAsync(userNameorEmail);
            if (identity == null) return BadRequest();

            if (await _userManager.CheckPasswordAsync(identity, password))
            {
                var x =  new ObjectResult(GenerateToken(identity));
                return x;
            }
            else
                return BadRequest();
        }
        private dynamic GenerateToken(IdentityUser identity)
        {
            var roles = from ur in _context.UserRoles
                        join r in _context.Roles on ur.RoleId equals r.Id
                        where ur.UserId == identity.Id
                        select new { ur.UserId, ur.RoleId, r.Name };
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,identity.UserName),
                new Claim(ClaimTypes.NameIdentifier,identity.Id),
                new Claim(JwtRegisteredClaimNames.Nbf,new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Exp,new DateTimeOffset(DateTime.Now.AddDays(1)).ToUnixTimeSeconds().ToString())

            };
            foreach (var role in roles)
                claims.Add(new Claim(ClaimTypes.Role, role.Name));

            var token = new JwtSecurityToken(
                new JwtHeader(
                    new SigningCredentials(
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Ihopeitislongenoughtomakeapassword")), SecurityAlgorithms.HmacSha256)), new JwtPayload(claims));
            var access_Token = new JwtSecurityTokenHandler().WriteToken(token);

            var output = new
            {
                Access_Token = access_Token,
                UserID = identity.Id
            };

            return output;

        }
    }
}
