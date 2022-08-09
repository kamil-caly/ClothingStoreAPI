using ClothingStoreModels.Dtos.Create;

namespace ClothingStoreAPI.Services.Interfaces
{
    public interface IAccountService
    {
        void RegisterUser(RegisterUserDto dto);

        string GenerateJwt(LoginUserDto dto);
    }
}
