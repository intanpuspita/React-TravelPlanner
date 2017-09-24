using IdentityServer4.Validation;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using TravelPlanner.Logic.Interfaces;
using TravelPlanner.Objects.Models;
using static IdentityModel.OidcConstants;

namespace TravelPlanner.Auth.Service
{
    public class GoogleGrant : IExtensionGrantValidator
    {
        private IAccountService _accountService;

        public GoogleGrant(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public string GrantType
        {
            get
            {
                return "googleAuth";
            }
        }

        public async Task ValidateAsync(ExtensionGrantValidationContext context)
        {
            var userToken = context.Request.Raw.Get("id_token");

            if (string.IsNullOrEmpty(userToken))
            {
                context.Result = new GrantValidationResult(TokenErrors.InvalidGrant, null);
                return;
            }

            // get user's identity
            HttpClient client = new HttpClient();

            var request = client.GetAsync("https://www.googleapis.com/oauth2/v3/tokeninfo?id_token=" + userToken).Result;

            if (request.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var googleResult = await request.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<GoogleValidationResult>(googleResult);

                var existingUser = _accountService.GetUserByEmail(result.email);

                if (existingUser.Active)
                {
                    context.Result = new GrantValidationResult(existingUser.Id, "google");
                }
                else
                {
                    var user = new UserModel
                    {
                        Email = result.email,
                        Name = result.name
                    };

                    Tuple<ResponseModel, UserModel> response = _accountService.RegisterGoogleAccount(user);
                    if (!response.Item1.IsError)
                    {
                        context.Result = new GrantValidationResult(response.Item2.Id, "google");
                    }
                }
                return;
            }
            else
            {
                return;
            }
        }
    }

    public class GoogleValidationResult
    {
        public bool email_verified { get; set; }
        public string email { get; set; }
        public string name { get; set; }
    }
}
