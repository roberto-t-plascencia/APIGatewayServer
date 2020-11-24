using Microservice.gateway.api.DbContexts;
using Microservice.gateway.api.Model;
using Microservice.gateway.api.VM;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservice.gateway.api.Repository
{
    public class UserRepository : IUserRepository
    {
        private IApplicationDbContext _dbcontext;

        private static Dictionary<string, Register> _context = new Dictionary<string, Register> {
            { "JuanArnao", new Register() {
                Id = "9",
                Email = " j_arnao@difare.com",
                Password = "abc123",
                Username = "JuanArnao",
                Name = "Juan Arnao",
                City = "Guayaquil",
                PhoneNumber = 43731390,
                Active = true
            } },
            { "Ronald", new Register() {
                Id = "10",
                Email = "r_mateo@difare.com",
                Password = "abc123",
                Username = "Ronald",
                Name = "Ronald Mateo",
                City = "Guayaquil",
                PhoneNumber = 43731390,
                Active = true
            } },
            { "Barbara", new Register() {
                Id = "11",
                Email = "barbara.martinez@alphait.us",
                Password = "abc123",
                Username = "Barbara",
                Name = "Barbara Martínez",
                City = "Quito",
                PhoneNumber = 44234234,
                Active = true
            } },

            { "Yovani", new Register() {
                Id = "12",
                Email = "yponce@difare.com",
                Password = "abc123",
                Username = "Yovani",
                Name = "Yovani Ponce",
                City = "Guayaquil",
                PhoneNumber = 43731390,
                Active = true
            } },
            { "robert", new Register() {
                Id = "13",
                Email = "roberto@alpha.com",
                Password = "abc123",
                Username = "robert",
                Name = "roberto plascencia",
                City = "San Diego",
                PhoneNumber = 4324223,
                Active = true
            } },
            { "simon", new Register() {
                Id = "14",
                Email = "simon@alphait.com",
                Password = "abc123",
                Username = "simon",
                Name = "Simon Elmoudi",
                City = "Wilmington",
                PhoneNumber = 2725344,
                Active = true
            } },
            { "harry", new Register() {
                Id = "15",
                Email = "harry@alphait.com",
                Password = "abc123",
                Username = "harry",
                Name = "Harry Virk",
                City = "Wilmington",
                PhoneNumber = 2343242,
                Active = true
            } },
        };

        public UserRepository(IApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<string> Add(Register user)
        {
            string retValue = string.Empty;
            try
            {
                if (string.IsNullOrEmpty(user.Id) && !string.IsNullOrEmpty(user.Email) && !string.IsNullOrEmpty(user.Password) && !string.IsNullOrEmpty(user.Username))
                {
                    var maxValue = _context.Aggregate((x, y) => int.Parse(x.Value.Id) > int.Parse(y.Value.Id) ? x : y).Value.Id;
                    user.Id = (int.Parse(maxValue) + 1).ToString();
                    user.Active = true;
                    if (_context.TryAdd(user.Username, user))
                        retValue = user.Username;
                }
            }
            catch (Exception e)
            {
                // log exception
            }
            return retValue;
        }

        public Task<string> Remove(string username)
        {
            string returnValue = string.Empty;
            if (_context.TryGetValue(username, out Register user))
            {
                user.Active = false;
                returnValue = user.Username;
            }
            return Task.FromResult(returnValue);
        }
        public async Task<Register> GetByUsername(string username)
        {
            Register returnValue = null;
            //var order = await _dbcontext.Orders.Where(orderdet => orderdet.CustomerId == custid).FirstOrDefaultAsync();
            _context.TryGetValue(username, out returnValue);
            return returnValue;
        }

        public async Task<Register> Login(Login login)
        {
            Register retValue = null;
            try
            {
                if (!string.IsNullOrEmpty(login.Username) && !string.IsNullOrEmpty(login.Password))
                {
                    retValue = _context.Where(x => x.Key.Equals(login.Username) && x.Value.Password.Equals(login.Password)).FirstOrDefault().Value;
                }
            }
            catch (Exception e)
            {
                // log exception
            }
            return retValue;
        }

        public async Task<Register> GetByEmail(string email)
        {
            //Register returnValue = null;
            //var order = await _dbcontext.Orders.Where(orderdet => orderdet.CustomerId == custid).FirstOrDefaultAsync();
            var returnValue = _context.Where(x => x.Value.Email.Equals(email)).FirstOrDefault();
            return (Register)returnValue.Value;
        }

        public IEnumerable<Register> GetUsers()
        {
            return  _context.Values.ToList<Register>();
        }

    }
}
