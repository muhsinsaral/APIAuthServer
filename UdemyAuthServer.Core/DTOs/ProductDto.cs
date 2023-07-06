using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UdemyAuthServer.Core.DTOs
{
    public class ProductDto
    {
         public int Id { get; set; }
        public required string Name { get; set; }
        public Decimal Price { get; set; }
        public required string UserId { get; set; }
    }
}