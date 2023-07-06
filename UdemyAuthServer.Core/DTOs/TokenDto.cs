using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UdemyAuthServer.Core.DTOs
{
    public class TokenDto
    {
        public required string AccessToken { get; set; }
        public DateTime AccessTokenExpiration { get; set; }
        public required string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiration { get; set; }

    }
}