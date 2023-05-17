using Application.Common.Interfaces;
using Application.Models;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Employee.Command
{
    public class updateEmployeeCommand :EmpDTO, IRequest<Result>
    {
       
    }
    public class updateEmployeeHandler : IRequestHandler<updateEmployeeCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public updateEmployeeHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result> Handle(updateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var emp = new Domain.Entities.Employee();
            if (emp == null)
                return Result.Failure(new string[] { "Employees not found" });
            else
            {
                emp.Id = request.Id;
                emp.Name = request.Name;
                emp.Address = request.Address;
                emp.Phone = request.Phone;
                emp.DepartmentId = request.DepartmentId;
            }
             _context.Set<Domain.Entities.Employee>().Update(emp);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success(new string[] { "Record Successfully Saved" });

        }
    }
    }

