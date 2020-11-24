using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservice.gateway.api.Model
{
    public class Register
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string City { get; set; }
        public int PhoneNumber { get; set; }

    }
}
