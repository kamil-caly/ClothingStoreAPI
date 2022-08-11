using ClothingStoreAPI.Services.Interfaces;
using ClothingStoreModels.Dtos;
using ClothingStoreModels.Dtos.Create;
using ClothingStoreModels.Dtos.Delete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClothingStoreAPI.Controllers
{
    [Route("Api/Account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService accountService;

        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        [HttpPost("Register")]
        public ActionResult RegisterUser([FromBody] RegisterUserDto dto)
        {
            accountService.RegisterUser(dto);
            
            return Ok("Account created.");
        }

        [HttpPost("Login")]
        public ActionResult Login([FromBody] LoginUserDto dto)
        {
            string token = accountService.GenerateJwt(dto);

            return Ok(token);
        }

        [HttpDelete("DeleteAccount")]
        public ActionResult Delete([FromBody] DeleteUserDto dto)
        {
            accountService.DeleteUser(dto);

            return NoContent();
        }

        [HttpPost("AddMoney")]
        public ActionResult AddMoney([FromBody] AddUserMoney userMoneyParams)
        {
            accountService.AddMoney(userMoneyParams);

            return Ok("Money successfully added to your account.");
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
