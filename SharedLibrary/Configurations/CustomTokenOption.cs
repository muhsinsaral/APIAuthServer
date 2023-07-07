using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharedLibrary.Configurations
{
    public class CustomTokenOption
    {
        public required List<string> Audience { get; set; }
        public required string Issuer { get; set; }
        public int AccessTokenExpiration { get; set; }
        public int RefreshTokenExpiration { get; set; }
        public required string SecurityKey { get; set; } 
    }
}