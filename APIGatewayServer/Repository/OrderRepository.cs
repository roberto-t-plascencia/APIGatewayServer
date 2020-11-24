using Microservice.gateway.api.DbContexts;
using Microservice.gateway.api.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservice.gateway.api.Repository
{
	public class OrderRepository : IOrderRepository
	{
        private IApplicationDbContext _dbcontext;

        private static Dictionary<string, Order> _context = new Dictionary<string, Order> {
            { "1", new Order() {
                Id = "1",
                Cost = "2.50",
                CustomerId = "Alex",
                Placed = DateTime.Now,
                ProductId = "21",
                Status = "Complete"
            } },
            { "2", new Order() {
                Id = "2",
                Cost = "3.61",
                CustomerId = "Brian",
                Placed = DateTime.Now,
                ProductId = "32",
                Status = "Complete"
            } }, 
            { "3", new Order() {
                Id = "3",
                Cost = "4.72",
                CustomerId = "Carlos",
                Placed = DateTime.Now,
                ProductId = "43",
                Status = "Complete"
                }
            } 
        };

        public OrderRepository(IApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<string> Add(Order order)
        {
            var maxValue = _context.Aggregate((x, y) => int.Parse(x.Value.Id) > int.Parse(y.Value.Id) ? x : y).Value.Id;
            order.Id = (int.Parse(maxValue) + 1).ToString();
            order.Placed = DateTime.Now;

            _context.TryAdd(order.Id, order);
            //_dbcontext.Orders.Add(order);
            //await _dbcontext.SaveChanges();
            return order.Id;
        }
        public async Task<string> Cancel(string id)
        {
            if (_context.TryGetValue(id, out Order order ))
            {
                order.Status = "Cancelled";
            }
  
            return "Order Cancelled Successfully";
        }
        public async Task<Order> GetByCustomerId(string custid)
        {
            Order returnValue = null;
            _context.TryGetValue(custid, out returnValue);
            return returnValue;
        }
        public async Task<Order> GetById(string id)
        {
            //var order = await _dbcontext.Orders.Where(orderdet => orderdet.Id == id).FirstOrDefaultAsync();
            _context.TryGetValue(id, out Order returnValue);
            return returnValue;
        }

        public IEnumerable<Order> GetOrders()
        {
            return _context.Values.ToList<Order>();
        }
    }
}

