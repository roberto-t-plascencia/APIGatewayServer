﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservice.gateway.api.Model
{
    public class UserLogin
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string EmployeeName { get; set; }
        public string City { get; set; }
        public string Department { get; set; }
    }
}
