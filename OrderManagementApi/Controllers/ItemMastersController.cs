using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderManagementApi.Models;

namespace OrderManagementApi.Controllers
{
    [EnableCors("CORS")]
    [Route("api/[controller]")]
    [ApiController]
    public class ItemMastersController : ControllerBase
    {
        private readonly OrderManagementDBContext _context;

        public ItemMastersController()
        {
            _context = new OrderManagementDBContext();
        }

        // GET: api/ItemMasters
        [HttpGet]
        public IEnumerable<ItemMaster> GetItemMaster()
        {
            return _context.ItemMaster;
        }

        // GET: api/ItemMasters/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetItemMaster([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var itemMaster = await _context.ItemMaster.FindAsync(id);

            if (itemMaster == null)
            {
                return NotFound();
            }

            return Ok(itemMaster);
        }

        // PUT: api/ItemMasters/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItemMaster([FromRoute] int id, [FromBody] ItemMaster itemMaster)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != itemMaster.ItemId)
            {
                return BadRequest();
            }

            _context.Entry(itemMaster).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemMasterExists(id))
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

        // POST: api/ItemMasters
        [HttpPost]
        public async Task<IActionResult> PostItemMaster([FromBody] ItemMaster itemMaster)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ItemMaster.Add(itemMaster);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ItemMasterExists(itemMaster.ItemId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetItemMaster", new { id = itemMaster.ItemId }, itemMaster);
        }

        // DELETE: api/ItemMasters/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItemMaster([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var itemMaster = await _context.ItemMaster.FindAsync(id);
            if (itemMaster == null)
            {
                return NotFound();
            }

            _context.ItemMaster.Remove(itemMaster);
            await _context.SaveChangesAsync();

            return Ok(itemMaster);
        }

        private bool ItemMasterExists(int id)
        {
            return _context.ItemMaster.Any(e => e.ItemId == id);
        }
    }
}