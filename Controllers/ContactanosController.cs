using LavantellAPIS.Context;
using LavantellAPIS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
//using System.Net.Mail;
using System.Net;
using EASendMail;
using LavantellAPIS.Helpers;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LavantellAPIS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]

    public class ContactanosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ContactanosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/<ContactanosController>
        [HttpGet]
        [Authorize]

        public async Task<IActionResult> Get()
        {
            try
            {
                var listContactanos = await _context.Contactanos.ToListAsync();
                return Ok(listContactanos);
            }
            catch (Exception)
            {
                //return BadRequest(ex.Message);
                return BadRequest("Hubo un error al tratar de obtener los contactos, por favor contacte con el administrador.");
            }
        }

        // GET api/<ContactanosController>/5
        [HttpGet("{id}")]
        [Authorize]

        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ContactanosController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Contactanos contactanos)
        {
            try
            {

                Logic objetologic = new Logic();
                //objetologic.EnviarCorreo(contactanos.Correo, contactanos.Nombre, contactanos.Mensaje);
                //objetologic.enviarcorreosa(contactanos.Correo, contactanos.Nombre, contactanos.Mensaje, contactanos.Telefono, contactanos.Ciudad, contactanos.Direccion);
                objetologic.enviarcorreosa(contactanos);

                _context.Add(contactanos);

                await _context.SaveChangesAsync();
                return Ok(contactanos);
            }
            catch (Exception)
            {
                //return BadRequest(ex.Message);
                return BadRequest("Hubo un error al tratar de enviar el contacto, por favor contacte con el administrador.");

            }
        }

        // PUT api/<ContactanosController>/5
        [HttpPut("{id}")]
        [Authorize]

        public async Task<IActionResult> Put(int id, [FromBody] Contactanos contactanos)
        {
            try
            {
                if (id != contactanos.Id)
                {
                    return NotFound();
                }
                _context.Update(contactanos);
                await _context.SaveChangesAsync();
                return Ok(new { message = "la ses{on de contáctanos fue actualizada con éxito" });
            }
            catch (Exception)
            {
                //return BadRequest(ex.Message);
                return BadRequest("Hubo un error al tratar de actualizar  el contacto, por favor contacte con el administrador.");


            }

        }

        // DELETE api/<ContactanosController>/5
        [HttpDelete("{id}")]
        [Authorize]

        public async Task<IActionResult> Delete(int id)
        {

            try
            {
                var contactanos = await _context.Contactanos.FindAsync(id);
                if (contactanos == null)
                {
                    return NotFound();
                }
                _context.Contactanos.Remove(contactanos);
                await _context.SaveChangesAsync();
                return Ok(new { message = "El contacto fue eliminado con éxito" });

            }
            catch (Exception)
            {
                //return BadRequest(ex.Message);
                return BadRequest("Hubo un error al tratar de eliminar  el contacto, por favor contacte con el administrador.");


            }

        }


    }
}
