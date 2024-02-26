using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using crudmvc.Data;
using crudmvc.Models;
using System.Net.Http;
using Microsoft.AspNetCore.Authorization;

namespace crudmvc.Controllers.api
{
    //[Authorize]
    //[Route("api/[controller]")]
    //[Produces("application/json")]
    //[ApiController]
    [Authorize]
    [Produces("application/json")]
    [Route("api/Produks")]
    public class ProduksController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProduksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Produks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produk>>> GetProduk()
        {
            return await _context.Produk.ToListAsync();
        }

        // GET: api/Produks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Produk>> GetProduk(int id)
        {
            var produk = await _context.Produk.FindAsync(id);

            if (produk == null)
            {
                return NotFound();
            }

            return produk;
        }

        // PUT: api/Produks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduk(int id, Produk produk)
        {
            if (id != produk.id)
            {
                return BadRequest();
            }

            _context.Entry(produk).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProdukExists(id))
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

        // POST: api/Produks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Produk>> PostProduk(Produk produk)
        {
            _context.Produk.Add(produk);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduk", new { id = produk.id }, produk);
        }

        // DELETE: api/Produks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduk(int id)
        {
            var produk = await _context.Produk.FindAsync(id);
            if (produk == null)
            {
                return NotFound();
            }

            _context.Produk.Remove(produk);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProdukExists(int id)
        {
            return _context.Produk.Any(e => e.id == id);
        }
    }
}
