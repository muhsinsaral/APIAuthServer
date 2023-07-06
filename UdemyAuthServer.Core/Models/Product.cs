using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UdemyAuthServer.Core.Models
{
    public class Product
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public Decimal Price { get; set; }
        public int Stock { get; set; }
        public required string UserId { get; set; }
    }
}