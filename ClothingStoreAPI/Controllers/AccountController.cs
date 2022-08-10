using ClothingStoreAPI.Services.Interfaces;
using ClothingStoreModels.Dtos.Create;
using ClothingStoreModels.Dtos.Delete;
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
            
            return Ok();
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

        [HttpPost("addMoney/{money}")]
        public ActionResult AddMoney([FromRoute] int money, [FromBody] LoginUserDto dto)
        {
            accountService.AddMoney(money, dto);

            return Ok();
        }
    }
}
