using Application.Common.Helper;
using Application.Common.Interfaces;
using Application.Models;
using Application.UserAccount.Registration;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RegisterDTO = Application.UserAccount.Registration.RegisterDTO;

namespace Application.RegisterUsers.Query
{
  public  class GetRegisterQuery : GridQuery, IRequest<GridResult<RegisterDTO>>
    {

    }
    public class GetListQueryHandler : IRequestHandler<GetRegisterQuery, GridResult<RegisterDTO>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetListQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<GridResult<RegisterDTO>> Handle(GetRegisterQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var data = await _context.Set<Domain.Entities.Register>().Select(x => new RegisterDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    Email=x.Email,
                    Address=x.Address,
                    PhoneNo=x.PhoneNo,
                    isVerified=x.isVerified
                   
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