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
using Decrypt = BCrypt.Net.BCrypt;

namespace Application.UserAccount.Login
{
    public class LoginCommand : LoginDTO, IRequest<Result>
    {
    }
    public class LoginCommandHandler : IRequestHandler<LoginCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private ICurrentUserService _currentUserService;

        public LoginCommandHandler(IApplicationDbContext context, IMapper mapper, ICurrentUserService currentUserService)
        {
            _context = context;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<Result> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var logindetails = await _context.Set<Domain.Entities.Register>().FirstOrDefaultAsync(x => x.Email == request.Email);
                if (logindetails != null)
                {
                    if (logindetails.isVerified == true)
                    {
                        if (Decrypt.Verify(request.Password, logindetails.Password))
                        {
                            // generate token 
                            var jwtToken = _currentUserService.GenerateEncodedToken(Convert.ToString(logindetails.Id), logindetails.Email, logindetails.Roles);
                            //  return Result.Success(new string[] { "User Login successfully", });
                            return Result.Success(new string[] { "User Login successfully", jwtToken.Result.encodedJwt });
                        }

                        else
                        {
                            return Result.Failure(new string[] { "Invalid  Password" });
                        }
                    }
                    else
                    {
                        return Result.Failure(new string[] { "You Are Not Verified By the Admin Yet" });
                    }
                }

                else
                {
                    return Result.Failure(new string[] { "Invalid Email or Password " });
                }
            }
            catch (Exception)
            {
                return Result.Failure(new string[] { "Invalid Email or Password" });
            }
        }
    }
}
