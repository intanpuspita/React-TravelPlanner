using IdentityServer4.Models;
using IdentityServer4.Validation;
using System.Threading.Tasks;
using TravelPlanner.Logic.Interfaces;

namespace TravelPlanner.Auth.Service
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private IAccountService _accountService;

        public ResourceOwnerPasswordValidator(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            if (_accountService.ValidatePassword(context.UserName, context.Password))
            {
                context.Result = new GrantValidationResult(_accountService.GetUserByEmail(context.UserName).Id.ToString(), "password", null, "local", null);
                return Task.FromResult(context.Result);
            }
            context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "The username and password do not match", null);
            return Task.FromResult(context.Result);
        }
    }
}
