using Application.Common.Interfaces;
using Application.Models;
using Application.UserAccount.Registration;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.RegisterUsers.Query
{
    public class GetByIdRegister : IRequest<RegisterDTO>
    {
        public int RegId { get; set; }
    }
    public class EditHandler : IRequestHandler<GetByIdRegister, RegisterDTO>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public EditHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

          

       public async  Task<RegisterDTO>Handle(GetByIdRegister request, CancellationToken cancellationToken)
        {
                var emp = await _context.Set<Domain.Entities.Register>().FirstOrDefaultAsync(a => a.Id == request.RegId, cancellationToken);
                var edit = new RegisterDTO();
            if (emp != null)
            {
                edit.Id = emp.Id;
                edit.Name = emp.Name;
                edit.Email = emp.Email;
                edit.Address = emp.Address;
                edit.PhoneNo = emp.PhoneNo;
                edit.RolesId = emp.RolesId;
            }
            return edit;
      
            }
        }
    }

