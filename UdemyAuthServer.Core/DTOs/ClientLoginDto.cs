using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UdemyAuthServer.Core.DTOs
{
    public class ClientLoginDto
    {
        public required string ClientId { get; set; }
        public required string ClientSecret { get; set; }
    }
}