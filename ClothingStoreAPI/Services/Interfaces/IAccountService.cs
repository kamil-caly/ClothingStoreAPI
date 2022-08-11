using ClothingStoreModels.Dtos;
using ClothingStoreModels.Dtos.Create;
using ClothingStoreModels.Dtos.Delete;

namespace ClothingStoreAPI.Services.Interfaces
{
    public interface IAccountService
    {
        void RegisterUser(RegisterUserDto dto);
        string GenerateJwt(LoginUserDto dto);
        void DeleteUser(DeleteUserDto dto);

        void AddMoney(AddUserMoney userMoneyParams);
        void MakePremium(LoginUserDto dto);
    }
}
