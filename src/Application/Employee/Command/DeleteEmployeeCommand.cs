using Application.Common.Interfaces;
using Application.Models;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Application.Employee.Command
{
   public class DeleteEmployeeCommand : IRequest<Result>
    {
        public int EmpId { get; set; }
    }
    public class EmplyeesDeleteHandler : IRequestHandler<DeleteEmployeeCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public EmplyeesDeleteHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
       
        public async Task<Result> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            var emp = await _context.Set<Domain.Entities.Employee>().FirstOrDefaultAsync(a => a.Id == request.EmpId, cancellationToken);
            if (emp == null)
                return Result.Failure(new string[] { "Employees not found" });
            _context.Set<Domain.Entities.Employee>().Remove(emp);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success("Record successfully Delete");
        }
    }
}