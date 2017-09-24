using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using TravelPlanner.Logic.Interfaces;
using TravelPlanner.Objects.Models;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace TravelPlanner.API.Controllers
{
    [Route("api/[controller]")]
    public class AccountController
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public string Test()
        {
            return "haaa";
        }
        
        [HttpPost]
        public ResponseModel RegisterByEmail([FromBody]UserModel user)
        {
            return _accountService.RegisterEmail(user);
        }

        [HttpPut("{id}")]
        public void ConfirmAccount(Guid id)
        {
            _accountService.ConfirmAccount(id);
        }
    }
}
