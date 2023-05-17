using Application.Common.Interfaces;
using Application.Models;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using Encrypt = BCrypt.Net.BCrypt;

namespace Application.UserAccount.Registration.Commands
{
    public class RegisterCommand : RegisterDTO, IRequest<Result>
    {
    }
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper     _mapper;
        private readonly IEmailSender _emailSender;

        public RegisterCommandHandler(IApplicationDbContext context, IMapper mapper,IEmailSender emailSender)
        {
            _context = context;
            _mapper = mapper;
            _emailSender = emailSender;
        }

        public async Task<Result> Handle(RegisterCommand request, CancellationToken cancellationToken)  
        {

            try
            {
                var regs = new Domain.Entities.Register();
                var message = " Password and ConfirmPassword Not Matched";
                var emailmessage = "Email Exists";
                if (request.RolesId == 1)
                {
                    regs.RolesId = request.RolesId;
                }
                else if (request.RolesId == 2)
                {
                    regs.RolesId = request.RolesId;
                }
                else
                {
                    regs.RolesId = 3;
                }
                if (request.Password == request.ConfirmPassword)
                    {
                        var log = await _context.Set<Domain.Entities.Register>().FirstOrDefaultAsync(x => x.Email == request.Email);
                        if (log == null)
                        {
                            regs.Name = request.Name;
                            regs.Address = request.Address;
                            regs.Email = request.Email;
                            regs.PhoneNo = request.PhoneNo;
                            regs.Password = Encrypt.HashPassword(request.Password);
                            regs.ConfirmPassword = Encrypt.HashPassword(request.ConfirmPassword);
                            regs.CreatedBy = request.Name;
                            regs.isVerified = false;
                           _context.Set<Domain.Entities.Register>().AddRange(regs);
                            await _context.SaveChangesAsync(cancellationToken);
                            //var email = regs;
                            if (regs.Email != null)
                            {
                                await _emailSender.SendEmailAsync(regs.Email, "This is test email subject from Management Team", +regs.Id, regs);
                            }
                            return Result.Success(new string[] { "Record SuccessFully Saved" });

                        }
                        else
                        {
                            return Result.Failure(new string[] { emailmessage });
                        }

                }

                    else
                    {
                        return Result.Failure(new string[] { message });
                    }
                
            }
            catch (Exception eg)
            {
                return Result.Failure(new string[] { eg.Message });
            }
        }
    }
}
