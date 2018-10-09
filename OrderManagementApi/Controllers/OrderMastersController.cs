using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderManagementApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderManagementApi.Controllers
{
    [EnableCors("CORS")]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderMastersController : ControllerBase
    {
        private readonly OrderManagementDBContext _context;

        public OrderMastersController()
        {
            
            _context = new OrderManagementDBContext();
        }

        //// GET: api/OrderMasters
        [HttpGet]
        public List<OrderList> GetOrderMaster()
        {

            var orderlist = (from a in _context.OrderMaster
                             join b in _context.ClientMaster on a.ClientId equals b.ClientId
                             join c in _context.ItemMaster on a.ItemId equals c.ItemId

                             select new OrderList
                             {
                                 OrderId = a.OrderId,
                                 ClientId = a.ClientId,
                                    ClientName = b.ClientName,
                                 ItemId = a.ItemId,
                                 ItemName = c.ItemName,
                                 Quantity = a.Quantity,
                                 OrderDate = a.OrderDate
                             });

            return orderlist.ToList();
            //var orderlist =(from a in OrderMaster join b in ClientMaster on a.ClientId equals b.ClientId 
            //               join c in ItemMaster )

        }

        // GET: api/OrderMasters/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderMaster([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var orderMaster = await _context.OrderMaster.FindAsync(id);

            if (orderMaster == null)
            {
                return NotFound();
            }

            return Ok(orderMaster);
        }

        // PUT: api/OrderMasters/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderMaster([FromRoute] int id, [FromBody] OrderMaster orderMaster)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != orderMaster.OrderId)
            {
                return BadRequest();
            }

            _context.Entry(orderMaster).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderMasterExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/OrderMasters
        [HttpPost]
        public async Task<IActionResult> PostOrderMaster([FromBody] IEnumerable<OrderList> orderMaster)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var order = from a in orderMaster
                        select new OrderMaster
                        {
                            OrderId = a.OrderId,
                            ClientId = a.ClientId,
                            ItemId = a.ItemId,
                            Quantity = a.Quantity,
                            OrderDate = a.OrderDate

                        };
            List<OrderMaster> odermst = order.ToList();
            foreach (var item in odermst)
            {
               
                _context.OrderMaster.Add(item);
            }
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrderMaster", new { }, orderMaster);


        }

        // DELETE: api/OrderMasters/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderMaster([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var orderMaster = await _context.OrderMaster.FindAsync(id);
            if (orderMaster == null)
            {
                return NotFound();
            }

            _context.OrderMaster.Remove(orderMaster);
            await _context.SaveChangesAsync();

            return Ok(orderMaster);
        }

        private bool OrderMasterExists(int id)
        {
            return _context.OrderMaster.Any(e => e.OrderId == id);
        }
    }
}