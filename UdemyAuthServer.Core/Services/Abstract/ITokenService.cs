using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UdemyAuthServer.Core.Configuration;
using UdemyAuthServer.Core.DTOs;
using UdemyAuthServer.Core.Models;

namespace UdemyAuthServer.Core.Services.Abstract
{
    public interface ITokenService
    {
        Task<TokenDto> CreateToken(UserApp userApp);
        ClientTokenDto CreateTokenByClient(Client client);
    }
}