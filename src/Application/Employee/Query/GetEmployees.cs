using Application.Common.Helper;
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

namespace Application.Employee.Query
{
    public class GetEmployees : GridQuery, IRequest<GridResult<EmpDTO>>
    {
    }
    public class GetCustomersListQueryHandler : IRequestHandler<GetEmployees, GridResult<EmpDTO>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetCustomersListQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GridResult<EmpDTO>> Handle(GetEmployees request, CancellationToken cancellationToken)
        {
            try
            {
                var VelData = await _context.Set<Domain.Entities.Employee>().Select(x => new EmpDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    Address = x.Address,
                    Phone = x.Phone,
                    //City = x.City,  
                    DepartmentId = x.DepartmentId
                }).DynamicPageAsync(request, cancellationToken);
                return VelData;
            }
            catch (Exception )
            {

                return null;
            }
        }
    }
}
