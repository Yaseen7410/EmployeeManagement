using Application.Employee;
using Application.Employee.Command;
using Application.Employee.Query;
using Application.Models;
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
    public class EmployeeController : ApiController
    {
        [HttpPost("[action]")]
        public async Task<ActionResult<GridResult<EmpDTO>>> employeesQuery(GetEmployees request)
        {
            return await Mediator.Send(request);
        }
        [HttpPost("[action]")]
        public async Task<ActionResult<Result>> addEmployees(AddEmployeeCommand request)
        {
            return await Mediator.Send(request);
        }
        [HttpPost("[action]")]
        public async Task<ActionResult<Result>> deleteEmployees(DeleteEmployeeCommand request)
        {
            return await Mediator.Send(request);
        }
        [HttpPost("[action]")]
        public async Task<ActionResult<Result>> updateEmployees(updateEmployeeCommand request)
        {
            return await Mediator.Send(request);
        }
    }
}

