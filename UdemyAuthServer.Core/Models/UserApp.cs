using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace UdemyAuthServer.Core.Models
{
    public class UserApp : IdentityUser
    {
        public string? City { get; set; }
    }
}