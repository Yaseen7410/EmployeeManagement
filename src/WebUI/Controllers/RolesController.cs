using Application.Models;
using Application.UserAccount.Roles;
using Application.UserAccount.Roles.Queries;
using MediatR;
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
    public class RolesController : ApiController
    {
        [HttpPost("[action]")]
        public async Task<ActionResult<GridResult<RolesDTO>>> RolesQuery(GetRoles request)
        {
            return await Mediator.Send(request);
        }
    }
}
