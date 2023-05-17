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

namespace Application.UserAccount.Roles.Queries
{
    public class GetRoles : GridQuery, IRequest<GridResult<RolesDTO>>
    {

    }
    public class GetRolesListQueryHandler : IRequestHandler<GetRoles, GridResult<RolesDTO>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetRolesListQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GridResult<RolesDTO>> Handle(GetRoles request, CancellationToken cancellationToken)
        {
            try
            {
                var data = await _context.Set<Domain.Entities.Roles>().Select(x => new RolesDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                }).DynamicPageAsync(request, cancellationToken);
                return data;
            }
            catch (Exception)
            {

                return null;
            }
        }
    }
}