using Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface ICurrentUserService
    {
        Task<RefreshToken> GenerateEncodedToken(string id, string userRole, Domain.Entities.Roles roles);  
        string UserId { get; }
    }
}
