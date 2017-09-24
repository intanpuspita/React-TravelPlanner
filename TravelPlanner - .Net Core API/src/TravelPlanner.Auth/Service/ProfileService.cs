using IdentityModel;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using TravelPlanner.Logic.Interfaces;

namespace TravelPlanner.Auth.Service
{
    public class ProfileService : IProfileService
    {
        private IAccountService _accountService;

        public ProfileService(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            try
            {
                var subjectId = context.Subject.GetSubjectId();
                var user = _accountService.GetUserById(subjectId);

                var claims = new List<Claim>
            {
                new Claim(JwtClaimTypes.Subject, user.Id.ToString()),
				//add as many claims as you want!new Claim(JwtClaimTypes.Email, user.Email),new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean)
			};

                context.IssuedClaims = claims;
                return Task.FromResult(0);
            }
            catch (Exception)
            {
                return Task.FromResult(0);
            }
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            var user = _accountService.GetUserById(context.Subject.GetSubjectId());
            context.IsActive = (user != null) && user.Active;
            return Task.FromResult(0);
        }
    }
}
