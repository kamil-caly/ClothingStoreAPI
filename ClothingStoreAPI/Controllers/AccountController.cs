using ClothingStoreAPI.Services.Interfaces;
using ClothingStoreModels.Dtos;
using ClothingStoreModels.Dtos.Create;
using ClothingStoreModels.Dtos.Delete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClothingStoreAPI.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService accountService;

        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        [HttpPost("register")]
        public ActionResult RegisterUser([FromBody] RegisterUserDto dto)
        {
            accountService.RegisterUser(dto);
            
            return Ok("Account created.");
        }

        [HttpPost("login")]
        public ActionResult Login([FromBody] LoginUserDto dto)
        {
            string token = accountService.GenerateJwt(dto);

            return Ok(token);
        }

        [HttpDelete("deleteAccount")]
        public ActionResult Delete([FromBody] DeleteUserDto dto)
        {
            accountService.DeleteUser(dto);

            return NoContent();
        }

        [HttpPost("addMoney")]
        public ActionResult AddMoney([FromBody] AddUserMoney userMoneyParams)
        {
            accountService.AddMoney(userMoneyParams);

            return Ok("Money successfully added to you account.");
        }

        [Authorize(Policy = "makeUserAsPremiumAfter10Purchases")]
        [HttpPut("makeUserPremiumIf10Purchases")]
        public ActionResult MakePremium([FromBody] LoginUserDto dto)
        {
            accountService.MakePremium(dto);

            return Ok("You are premium now!");
        }
    }
}
