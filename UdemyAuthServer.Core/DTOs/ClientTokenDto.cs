using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UdemyAuthServer.Core.DTOs
{
    public class ClientTokenDto
    {
        public required string AccessToken { get; set; }
        public DateTime AccessTokenExpiration { get; set; }
    }
}