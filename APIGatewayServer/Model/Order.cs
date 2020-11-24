using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservice.gateway.api.Model
{
	public class Order
	{
		public string Id { get; set; }
		public string ProductId { get; set; }
		public string Cost { get; set; }
		public DateTime Placed { get; set; }
		public string CustomerId { get; set; }
		public string Status { get; set; }

	}
}
