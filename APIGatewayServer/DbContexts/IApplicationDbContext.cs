using Microservice.gateway.api.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservice.gateway.api.DbContexts
{
	public interface IApplicationDbContext
	{
		//DbSet<Order> Orders { get; set; }
		Dictionary<string, Order>  Orders { get; set; }
		Task<int> SaveChanges();

	}
}
