using System;
using TravelPlanner.Objects.Models;

namespace TravelPlanner.Logic.Interfaces
{
    public interface IAccountService
    {
        UserModel GetUserById(string id);

        bool ValidatePassword(string email, string password);

        UserModel GetUserByEmail(string email);

        ResponseModel RegisterEmail(UserModel user);

        Tuple<ResponseModel, UserModel> RegisterGoogleAccount(UserModel user);

        void ConfirmAccount(Guid id);
    }
}
