using Application.Models;
using Application.RegisterUsers.Query;
using Application.UserAccount.Registration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ApiController
    {
        [HttpPost("[action]")]
        public async Task<ActionResult<GridResult<RegisterDTO>>> registerQuery(GetRegisterQuery request)
        {
            return await Mediator.Send(request);
        }
        [HttpPost("[action]")]
        public async Task<ActionResult<RegisterDTO>> registerQuerybyId(GetByIdRegister request)
        {
            return await Mediator.Send(request);
        }
    }
}
