using LavantellAPIS.Context;
using LavantellAPIS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LavantellAPIS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class TarjetaController : ControllerBase
    {

        private readonly ApplicationDbContext _context;
        public TarjetaController(ApplicationDbContext context)
        {
            _context = context;
        }
        


        // GET: api/<TarjetaController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var listTarjetas = await _context.Tarjeta.ToListAsync();
                    return Ok(listTarjetas);
            }
            catch (Exception )
            {
                //return BadRequest(ex.Message);
                return BadRequest("Hubo un error al tratar de obtener las tarjettas, por favor contacte con el administrador.");

            }
        }

        // GET api/<TarjetaController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<TarjetaController>
        [HttpPost]

        public async Task<IActionResult> Post([FromBody] Tarjeta tarjeta)
        {
            try
            {
                _context.Add(tarjeta);

                await _context.SaveChangesAsync();
                return Ok(tarjeta);
            }
            catch (Exception )
            {
                //return BadRequest(ex.Message);
                return BadRequest("Hubo un error al tratar de enviar la tarjeta, por favor contacte con el administrador.");

            }
        }

        // PUT api/<TarjetaController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult>  Put(int id, [FromBody] Tarjeta tarjeta)
        {
            try
            {
                if(id != tarjeta.Id) 
                {
                    return NotFound();
                }
                _context.Update(tarjeta);
                await _context.SaveChangesAsync();
                return Ok(new { message = "la tarjeta fue actualizada con exito" });
            }
            catch (Exception )
            {
                //return BadRequest(ex.Message);
                return BadRequest("Hubo un error al tratar de actualizar las tarjettas, por favor contacte con el administrador.");

            }

        }

        // DELETE api/<TarjetaController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {

            try
            {
                var tarjeta = await _context.Tarjeta.FindAsync(id);
                if (tarjeta == null)
                {
                    return NotFound();
                }
                _context.Tarjeta.Remove(tarjeta);
                await _context.SaveChangesAsync();
                return Ok(new { message = "la tarjeta fue eliminada con exito" });
            }
            catch (Exception )
            {
                //return BadRequest(ex.Message);
                return BadRequest("Hubo un error al tratar de eliminar las tarjettas, por favor contacte con el administrador.");

            }

        }
    }
}
