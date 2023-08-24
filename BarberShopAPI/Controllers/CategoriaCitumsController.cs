using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BarberShopAPI.Models;

namespace BarberShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaCitumsController : ControllerBase
    {
        private readonly BARBERIAContext _context;

        public CategoriaCitumsController(BARBERIAContext context)
        {
            _context = context;
        }

        // GET: api/CategoriaCitums
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriaCitum>>> GetCategoriaCita()
        {
          if (_context.CategoriaCita == null)
          {
              return NotFound();
          }
            return await _context.CategoriaCita.ToListAsync();
        }

        // GET: api/CategoriaCitums/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoriaCitum>> GetCategoriaCitum(int id)
        {
          if (_context.CategoriaCita == null)
          {
              return NotFound();
          }
            var categoriaCitum = await _context.CategoriaCita.FindAsync(id);

            if (categoriaCitum == null)
            {
                return NotFound();
            }

            return categoriaCitum;
        }

        // PUT: api/CategoriaCitums/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategoriaCitum(int id, CategoriaCitum categoriaCitum)
        {
            if (id != categoriaCitum.CategoriaCitaID)
            {
                return BadRequest();
            }

            _context.Entry(categoriaCitum).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoriaCitumExists(id))
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

        // POST: api/CategoriaCitums
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CategoriaCitum>> PostCategoriaCitum(CategoriaCitum categoriaCitum)
        {
          if (_context.CategoriaCita == null)
          {
              return Problem("Entity set 'BARBERIAContext.CategoriaCita'  is null.");
          }
            _context.CategoriaCita.Add(categoriaCitum);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategoriaCitum", new { id = categoriaCitum.CategoriaCitaID }, categoriaCitum);
        }

        // DELETE: api/CategoriaCitums/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoriaCitum(int id)
        {
            if (_context.CategoriaCita == null)
            {
                return NotFound();
            }
            var categoriaCitum = await _context.CategoriaCita.FindAsync(id);
            if (categoriaCitum == null)
            {
                return NotFound();
            }

            _context.CategoriaCita.Remove(categoriaCitum);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoriaCitumExists(int id)
        {
            return (_context.CategoriaCita?.Any(e => e.CategoriaCitaID == id)).GetValueOrDefault();
        }
    }
}
