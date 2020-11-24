using Microservice.gateway.api.Model;
using Microservice.gateway.api.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservice.gateway.api.Repository
{
    public interface IUserRepository
    {
		Task<string> Add(Register reg);

		Task<Register> Login(Login login);
		Task<string> Remove(string id);
		Task<Register> GetByUsername(string username);
		Task<Register> GetByEmail(string email);

		IEnumerable<Register> GetUsers();

	}
}
