using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Domain.Interfaces
{
    public interface IJwtTokenService
    {
        string GenerateToken(int userId, string userName);
    }
}
