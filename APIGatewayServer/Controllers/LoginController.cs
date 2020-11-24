using Microservice.gateway.api.Model;
using Microservice.gateway.api.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservice.gateway.api.Controllers
{
    //[ApiVersion("1.0")]
    //[Route("api/v{version:apiVersion}/[controller]")]
    //[ApiController]
    //[EnableCors("CORS")]
    [Route("api/[controller]")]
	[ApiController]
	public class OrderController : ControllerBase
	{
        private IOrderRepository _orderRepository;
        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        //[Authorize] //for AD integration
        [HttpPost]
        [Route("Add")]
        public async Task<ActionResult> Add([FromBody] Order orderdet)
        {
            orderdet.Placed = DateTime.Now;

            string orderid = await _orderRepository.Add(orderdet);
            return Ok(orderid);
        }

        //[Authorize] //for AD integration
        [HttpGet]
        [Route("GetByCustomerId/{id}")]
        public async Task<ActionResult> GetByCustomerId(string id)
        {
            var orders = await _orderRepository.GetByCustomerId(id);
            return Ok(orders);
        }

        //[Authorize] //for AD integration
        [HttpGet]
        [Route("GetById/{id}")]
        public async Task<ActionResult> GetById(string id)
        {
            var orderdet = await _orderRepository.GetById(id);
            return Ok(orderdet);
        }

        //[Authorize] //for AD integration
        [HttpPost]
        [Route("Cancel/{id}")]
        public async Task<IActionResult> Cancel(string id)
        {
            string resp = await _orderRepository.Cancel(id);
            return Ok(resp);
        }

        [Route("GetOrders")]
        [HttpGet]
        public IEnumerable<Order> GetAll()
        {
            return _orderRepository.GetOrders();
        }
    }
}
