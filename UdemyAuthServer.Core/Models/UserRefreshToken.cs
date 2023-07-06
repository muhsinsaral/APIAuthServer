using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UdemyAuthServer.Core.Models
{
    public class UserRefreshToken
    {
        public required string UserId { get; set; }
        public required string Code { get; set; }
        public DateTime Expiration { get; set; }
    }
}