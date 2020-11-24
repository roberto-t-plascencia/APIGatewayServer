using Microservice.gateway.api.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservice.gateway.api.DbContexts
{
	public class ApplicationDbContext : DbContext, IApplicationDbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{
		}

        //public DbSet<Order> Orders { get; set; }
        public Dictionary<string, Order> Orders { get; set; }

		public new async Task<int> SaveChanges()
		{
			return await base.SaveChangesAsync();
		}
	}
}
