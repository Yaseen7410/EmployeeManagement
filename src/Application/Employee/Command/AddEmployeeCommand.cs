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
    public class AddEmployeeCommand : EmpDTO, IRequest<Result>
    {
    }
    public class AddEmployeehandler : IRequestHandler<AddEmployeeCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public AddEmployeehandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }

        public async Task<Result> Handle(AddEmployeeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var emp = new Domain.Entities.Employee();
                if(emp!=null)
                {
                    emp.Name = request.Name;
                    emp.Address = request.Address;
                    emp.Phone = request.Phone;
                    emp.DepartmentId = request.DepartmentId;
                }
                await _context.Set<Domain.Entities.Employee>().AddAsync(emp);
                await _context.SaveChangesAsync(cancellationToken);
                return Result.Success(new string[] { "Record Successfully Saved" });

            }
            catch (Exception eg)
            {

                return Result.Failure(new string[] { eg.Message });
            }
        }
    }
}