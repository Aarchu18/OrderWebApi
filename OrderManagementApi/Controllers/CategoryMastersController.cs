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
    public class CategoryMastersController : ControllerBase
    {
        private readonly OrderManagementDBContext _context;

        public CategoryMastersController()
        {
            _context = new OrderManagementDBContext();
        }

        // GET: api/CategoryMasters
        [HttpGet]
        public IEnumerable<CategoryMaster> GetCategoryMaster()
        {
            return _context.CategoryMaster;
        }

        // GET: api/CategoryMasters/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryMaster([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var categoryMaster = await _context.CategoryMaster.FindAsync(id);

            if (categoryMaster == null)
            {
                return NotFound();
            }

            return Ok(categoryMaster);
        }

        // PUT: api/CategoryMasters/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategoryMaster([FromRoute] int id, [FromBody] CategoryMaster categoryMaster)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != categoryMaster.CategoryId)
            {
                return BadRequest();
            }

            _context.Entry(categoryMaster).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryMasterExists(id))
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

        // POST: api/CategoryMasters
        [HttpPost]
        public async Task<IActionResult> PostCategoryMaster([FromBody] CategoryMaster categoryMaster)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.CategoryMaster.Add(categoryMaster);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategoryMaster", new { id = categoryMaster.CategoryId }, categoryMaster);
        }

        // DELETE: api/CategoryMasters/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoryMaster([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var categoryMaster = await _context.CategoryMaster.FindAsync(id);
            if (categoryMaster == null)
            {
                return NotFound();
            }

            _context.CategoryMaster.Remove(categoryMaster);
            await _context.SaveChangesAsync();

            return Ok(categoryMaster);
        }

        private bool CategoryMasterExists(int id)
        {
            return _context.CategoryMaster.Any(e => e.CategoryId == id);
        }
    }
}