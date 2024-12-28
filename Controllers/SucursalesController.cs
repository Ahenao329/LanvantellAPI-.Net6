using LavantellAPIS.Context;
using LavantellAPIS.Models;
using LavantellAPIS.Models.Response;
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
    //[Authorize]

    public class SucursalesController : ControllerBase
    {

        private readonly ApplicationDbContext _context;
        public SucursalesController(ApplicationDbContext context)
        {
            _context = context;
        }


        // GET: api/<SucursalesController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            Respuesta oRespuesta = new Respuesta();

            try
            {
                var listSucursales = await _context.Sucursales.ToListAsync();
                oRespuesta.Exito = 1;
                oRespuesta.Data = listSucursales;
                //return Ok(listSucursales);
            }
            catch (Exception )
            {
                //return BadRequest(ex.Message);
                //return BadRequest("Hubo un error al tratar de obtener las sucursales, por favor contacte con el administrador.");
                oRespuesta.Mensaje = "Hubo un error al tratar de obtener las sucursales, por favor contacte con el administrador.";
            }
            return Ok(oRespuesta);
        }

        // GET api/<SucursalesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<SucursalesController>
        [HttpPost]
        [Authorize]

        public async Task<IActionResult> Post([FromBody] Sucursales sucursales)
        {
            Respuesta oRespuesta = new Respuesta();

            try
            {
                _context.Add(sucursales);

                await _context.SaveChangesAsync();
                //return Ok(sucursales);
                oRespuesta.Exito = 1;

            }
            catch (Exception )
            {
                //return BadRequest(ex.Message);
                //return BadRequest("Hubo un error al tratar de enviar la sucursal, por favor contacte con el administrador.");
                oRespuesta.Mensaje = "Hubo un error al tratar de enviar la sucursal, por favor contacte con el administrador.";


            }
            return Ok(oRespuesta);

        }

        // PUT api/<SucursalesController>/5
        [HttpPut("{id}")]
        [Authorize]

        public async Task<IActionResult> Put(int id, [FromBody] Sucursales sucursales)
        {
            Respuesta oRespuesta = new Respuesta();

            try
            {
                if (id != sucursales.Id)
                {
                    return NotFound();
                }
                _context.Update(sucursales);
                await _context.SaveChangesAsync();
                //return Ok(new { message = "la sesión de sucursales fue actualizada con exito" });
                oRespuesta.Exito = 1;
                oRespuesta.Mensaje = "la sesión de sucursales fue actualizada con exito";


            }
            catch (Exception )
            {
                //return BadRequest(ex.Message);
                //return BadRequest("Hubo un error al tratar de actualizar la sucursal, por favor contacte con el administrador.");
                oRespuesta.Mensaje = "Hubo un error al tratar de actualizar la sucursal, por favor contacte con el administrador.";


            }
            return Ok(oRespuesta);

        }

        // DELETE api/<SucursalesController>/5
        [HttpDelete("{id}")]
        [Authorize]

        public async Task<IActionResult> Delete(int id)
        {
            Respuesta oRespuesta = new Respuesta();

            try
            {
                var sucursales = await _context.Sucursales.FindAsync(id);
                if (sucursales == null)
                {
                    return NotFound();
                }
                _context.Sucursales.Remove(sucursales);
                await _context.SaveChangesAsync();
                oRespuesta.Exito = 1;
                //return Ok(new { message = "la sesión de sucursales fue eliminada con exito" });
                oRespuesta.Mensaje = "la sesión de sucursales fue eliminada con exito";

            }
            catch (Exception )
            {
                //return BadRequest(ex.Message);
                //return BadRequest("Hubo un error al tratar de eliminar la sucursal, por favor contacte con el administrador.");
                oRespuesta.Mensaje = "Hubo un error al tratar de eliminar la sucursal, por favor contacte con el administrador.";

            }
            return Ok(oRespuesta);

        }
    }
}

