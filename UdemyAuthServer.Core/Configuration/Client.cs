using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UdemyAuthServer.Core.Configuration
{
    public class Client
    {
        public required string Id { get; set; }
        public required string Secret { get; set; }
        public required List<string> Audiences { get; set; }

    }
}   