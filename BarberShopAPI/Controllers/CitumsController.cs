using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BarberShopAPI.Models;
using BarberShopAPI.ModelsDTO;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BarberShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitumsController : ControllerBase
    {
        private readonly BARBERIAContext _context;

        public CitumsController(BARBERIAContext context)
        {
            _context = context;
        }

        // GET: api/Citums
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Citum>>> GetCita()
        {
          if (_context.Cita == null)
          {
              return NotFound();
          }
            return await _context.Cita.ToListAsync();
        }

        // GET: api/Citums/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Citum>> GetCitum(int id)
        {
          if (_context.Cita == null)
          {
              return NotFound();
          }
            var citum = await _context.Cita.FindAsync(id);

            if (citum == null)
            {
                return NotFound();
            }

            return citum;
        }

        

        [HttpGet("GetCitasListByUsuario")]
        public ActionResult<IEnumerable<CitaDTO>> GetCitasInfoList(int id)
        {
            var query = (
                        from c in _context.Cita
                        join cu in _context.Clientes
                        on c.ClienteId equals cu.ClienteId
                        where c.UserId == id && c.Active == true
                        select new 
                        {
                            IdCita = c.CitaID,
                            description = c.Description,
                            fecha = c.Fecha,
                            hora = c.Hora,
                            clienteid = c.ClienteId,
                            nombre = cu.Nombre,
                            apellidos = cu.Apellidos,
                            celular = cu.Celular
                            

                        }).ToList();

            List<CitaDTO> list = new List<CitaDTO>();

            foreach (var item in query) 
            {
                CitaDTO NewItem = new CitaDTO();
                {
                    NewItem.Citaid = item.IdCita;
                    NewItem.Descripcion = item.description;
                    NewItem.Fechad = item.fecha;
                    NewItem.Horad = item.hora;
                    NewItem.IdCliente = item.clienteid;
                    NewItem.NombreDeCliente = item.nombre;
                    NewItem.ApellidosDeCliente  = item.apellidos;
                    NewItem.CelularDeCliente = item.celular;


                };
                list.Add(NewItem);
            }

            if (list == null) { return NotFound(); }
            return list;

        }





        [HttpPut("{id}")]
        public async Task<IActionResult> PutCita(int id, Citum cita)
        {
            if (id != cita.CitaID)
            {
                return BadRequest();
            }

            Citum? NewEFCita = GetCitaByID(cita.CitaID);

            if (NewEFCita != null)
            {
                NewEFCita.Description = cita.Description;
                NewEFCita.Fecha = cita.Fecha;
                NewEFCita.Hora = cita.Hora;
                NewEFCita.CategoriaCitaId = cita.CategoriaCitaId;

                _context.Entry(NewEFCita).State = EntityState.Modified;
            }



            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CitumExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }


        private Citum? GetCitaByID(int id)
        {
            var cita = _context.Cita?.Find(id);

            return cita;
        }

        // POST: api/Citums
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Citum>> PostCitum(Citum citum)
        {
          if (_context.Cita == null)
          {
              return Problem("Entity set 'BARBERIAContext.Cita'  is null.");
          }
            _context.Cita.Add(citum);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCitum", new { id = citum.CitaID }, citum);
        }

       

        private bool CitumExists(int id)
        {
            return (_context.Cita?.Any(e => e.CitaID == id)).GetValueOrDefault();
        }
    }
}
