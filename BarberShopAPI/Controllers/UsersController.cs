using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BarberShopAPI.Models;
using BarberShopAPI.Attributes;

namespace BarberShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[ApiKey]
    public class UsersController : ControllerBase
    {
        private readonly BARBERIAContext _context;

        public UsersController(BARBERIAContext context)
        {
            _context = context;
        }


        [HttpGet("ValidateLogin")]

        public async Task<ActionResult<User>> ValidateLogin(string username, string password)
        {
            var user = await _context.Users.SingleOrDefaultAsync(e => e.Email == username && e.Password == password);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }



        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
          if (_context.Users == null)
          {
              return NotFound();
          }
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
          if (_context.Users == null)
          {
              return NotFound();
          }
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.UserId)
            {
                return BadRequest();
            }

            User? NewEFUser = GetUserByID(user.UserId);

            if (NewEFUser != null)
            {
                NewEFUser.Email = user.Email;
                NewEFUser.Name = user.Name;
                NewEFUser.BackUpEmail = user.BackUpEmail;
                NewEFUser.PhoneNumber = user.PhoneNumber;
                NewEFUser.Address = user.Address;

                _context.Entry(NewEFUser).State = EntityState.Modified;
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchUser(int id, User user)
        {
            if (id != user.UserId)
            {
                return BadRequest();
            }

            User? NewEFUser = GetUserByID(user.UserId);

            if (NewEFUser != null)
            {
                NewEFUser.Password = user.Password;

                _context.Entry(NewEFUser).State = EntityState.Modified;
            }


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        private User? GetUserByID(int id)
        {
            var User = _context.Users?.Find(id);

            return User;
        }


        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
          if (_context.Users == null)
          {
              return Problem("Entity set 'BARBERIAContext.Users'  is null.");
          }
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.UserId }, user);
        }


        private bool UserExists(int id)
        {
            return (_context.Users?.Any(e => e.UserId == id)).GetValueOrDefault();
        }

        [HttpGet("GetUserInfoByEmail")]

        public ActionResult<IEnumerable<User>> GetUserInfoByEmail(string Pemail)
        {
            //acá creamos un linq que combina información de dos entidades como en este caso User y UserRol
            //y la agrega en el objecto dta de usuario

            var query = (
                from u in _context.Users
                join ur in _context.UserRoles
                on u.UserRoleId equals ur.UserRoleId
                where u.Active == true && u.Email == Pemail 
                select new
                {
                    UserID = u.UserId,
                    correo = u.Email,
                    contrasennia = u.Password,
                    nombre = u.Name,
                    correorespaldo = u.BackUpEmail,
                    telefono = u.PhoneNumber,
                    direccion = u.Address,
                    activo = u.Active,
                    idrol = ur.UserRoleId,
                    descripcionrol = ur.Description
                }).ToList();

            //creamos un objecto del tipo que retorna la función
            List<User> list = new List<User>();

            foreach (var item in query)
            {
                User NewItem = new User()
                {
                    UserId = item.UserID,
                    Email = item.correo,
                    Password = item.contrasennia,
                    Name = item.nombre,
                    BackUpEmail = item.correorespaldo,
                    PhoneNumber = item.telefono,
                    Address = item.direccion,
                    Active = item.activo,
                    UserRoleId = item.idrol
                    //DescripcionRol = item.descripcionrol
                };
                list.Add(NewItem);
            }

            if (list == null) { return NotFound(); }

            return list;


        }





    }
}
