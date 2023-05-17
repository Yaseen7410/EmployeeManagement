using Application.Models;
using Application.UserAccount.Login;
using Application.UserAccount.Registration.Commands;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController :ApiController
    {
        [HttpPost("[action]")]
        public async Task<ActionResult<Result>> registerRequest(RegisterCommand command)
        {

            return await Mediator.Send(command);
        }
        [HttpPost("[action]")]
        public async Task<ActionResult<Result>> loginRequest(LoginCommand command)
      
        {
            return await Mediator.Send(command);
        }
        [HttpPost("[action]")]
        public async Task<ActionResult<Result>> updateUserVerificationStatus(updatestatusCommand command)

        {
            return await Mediator.Send(command);
        }


    }
}
