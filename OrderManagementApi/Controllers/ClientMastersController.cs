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
    public class ClientMastersController : ControllerBase
    {
        private readonly OrderManagementDBContext _context;

        public ClientMastersController()
        {
            _context =  new OrderManagementDBContext();
        }

        // GET: api/ClientMasters
        [HttpGet]
        public IEnumerable<ClientMaster> GetClientMaster()
        {
            return _context.ClientMaster;
        }

        // GET: api/ClientMasters/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetClientMaster([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var clientMaster = await _context.ClientMaster.FindAsync(id);

            if (clientMaster == null)
            {
                return NotFound();
            }

            return Ok(clientMaster);
        }

        // PUT: api/ClientMasters/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClientMaster([FromRoute] int id, [FromBody] ClientMaster clientMaster)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != clientMaster.ClientId)
            {
                return BadRequest();
            }

            _context.Entry(clientMaster).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientMasterExists(id))
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

        // POST: api/ClientMasters
        [HttpPost]
        public async Task<IActionResult> PostClientMaster([FromBody] ClientMaster clientMaster)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ClientMaster.Add(clientMaster);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClientMaster", new { id = clientMaster.ClientId }, clientMaster);
        }

        // DELETE: api/ClientMasters/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClientMaster([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var clientMaster = await _context.ClientMaster.FindAsync(id);
            if (clientMaster == null)
            {
                return NotFound();
            }

            _context.ClientMaster.Remove(clientMaster);
            await _context.SaveChangesAsync();

            return Ok(clientMaster);
        }

        private bool ClientMasterExists(int id)
        {
            return _context.ClientMaster.Any(e => e.ClientId == id);
        }
    }
}