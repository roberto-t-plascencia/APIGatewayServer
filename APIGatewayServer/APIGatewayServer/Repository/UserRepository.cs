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
                Email = " juan.arnao@grupodifare.com",
                Password = "abc123",
                Username = "JuanArnao",
                Name = "Juan Arnao",
                City = "Guayaquil",
                PhoneNumber = 43731390
            } },
            { "Ronald", new Register() {
                Id = "10",
                Email = "ronald.mateo@GrupoDifare.com",
                Password = "abc123",
                Username = "Ronald",
                Name = "Ronald Mateo",
                City = "Guayaquil",
                PhoneNumber = 43731390
            } },
            { "Barbara", new Register() {
                Id = "11",
                Email = "barbara.martinez@alphait.us",
                Password = "abc123",
                Username = "Barbara",
                Name = "Barbara Martínez",
                City = "Guayaquil",
                PhoneNumber = 44234234
            } },

            { "Yovani", new Register() {
                Id = "12",
                Email = "yovani.ponce@grupodifare.com",
                Password = "abc123",
                Username = "Yovani",
                Name = "Yovani Ponce",
                City = "Guayaquil",
                PhoneNumber = 43731390
            } },
            { "robert", new Register() {
                Id = "13",
                Email = "roberto.plascencia@alphait.us",
                Password = "abc123",
                Username = "robert",
                Name = "roberto plascencia",
                City = "San Diego",
                PhoneNumber = 4324223
            } },
            { "simon", new Register() {
                Id = "14",
                Email = "simon.elmoudi@alphait.us",
                Password = "abc123",
                Username = "simon",
                Name = "Simon Elmoudi",
                City = "Wilmington",
                PhoneNumber = 2725344
            } },
            { "harry", new Register() {
                Id = "15",
                Email = "harry@alphait.us",
                Password = "abc123",
                Username = "harry",
                Name = "Harry Virk",
                City = "Wilmington",
                PhoneNumber = 2343242
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

        public async Task<string> Remove(string username)
        {
            return _context.Remove(username).ToString();
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
