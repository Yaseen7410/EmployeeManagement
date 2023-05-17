using Application.Common.Interfaces;
using Application.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UserAccount.Registration.Commands
{
    public class updatestatusCommand : RegisterDTO, IRequest<Result>
    {
    }
    public class UpdateCommandHandler : IRequestHandler<updatestatusCommand, Result>
    {
        private readonly IEmailApproved _emailApproved;
        private readonly IApplicationDbContext _context;
        public UpdateCommandHandler(IApplicationDbContext context,IEmailApproved emailApproved)
        {
            _context = context;
            _emailApproved = emailApproved;
        }

        public async Task<Result> Handle(updatestatusCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var status = await _context.Set<Domain.Entities.Register>().FindAsync(request.Id);
                if (status != null)
                {
                   
                    request.isVerified = true;
                    status.isVerified = request.isVerified;
                    status.Id = request.Id;
                    _context.Set<Domain.Entities.Register>().UpdateRange(status);
                    await _context.SaveChangesAsync(cancellationToken);
                    await _emailApproved.SendEmailAsync(status.Email, "This is the Approval Male" , status);
                   
                   }
                return Result.Success(new string[] { "User Login successfully" });

            }
            catch (Exception)
            {

                return Result.Failure(new string[] { "Invalid  Password" });
            }
        }
    }
}
