using LavantellAPIS.Context;
using LavantellAPIS.Models;
using LavantellAPIS.Models.Request;
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
    public class NosotrosController : ControllerBase
    {
        private readonly InosotrosRepository _inosotrosRepository;
        private readonly ApplicationDbContext _context;
        public NosotrosController(ApplicationDbContext context, InosotrosRepository _inosotrosRepository)
        {
            this._inosotrosRepository = _inosotrosRepository;
            _context = context;
        }

        // GET: api/<NosotrosController>
        [HttpGet]

        //public IActionResult GetAll()
        //{
        //    return Ok(this._inosotrosRepository.getAll());
        //}

        public async Task<IActionResult> Get()
        {
            try
            {
                var listNosotros = await _context.Nosotros.ToListAsync();
                return Ok(listNosotros);
            }
            catch (Exception)
            {
                //return BadRequest(ex.Message);
                return BadRequest("Hubo un error al tratar de obtener los datos de la sesión nosotros, por favor contacte con el administrador.");
            }
        }

        // GET api/<NosotrosController>/5
        [HttpGet("{id}")]

        public IActionResult GetById(int id)
        {
            var nosotros = this._inosotrosRepository.GetById(id);
            if (nosotros != null)
            {
                return Ok(nosotros);
            }

            return NotFound();
        }

        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/<NosotrosController>
        [HttpPost]
        //[Authorize]

        public async Task<IActionResult> Post([FromBody] Nosotros nosotros)
        {

            try
            {
                _context.Add(nosotros);

                await _context.SaveChangesAsync();
                return Ok(nosotros);
            }
            catch (Exception)
            {
                //return BadRequest(ex.Message);
                return BadRequest("Hubo un error al tratar de enviar los datos de la sesión nosotros, por favor contacte con el administrador.");

            }
        }
        //public async Task<IActionResult> Create([FromForm] NosotrosRequest request)
        //{
        //    return Ok(await this._inosotrosRepository.Add(request));
        //}

        // PUT api/<NosotrosController>/5
        [HttpPut("{id}")]
        [Authorize]

        public async Task<IActionResult> Update([FromRoute] int id, [FromForm] NosotrosRequest request)
        {
            return Ok(await this._inosotrosRepository.Update(id, request));
        }
        //public async Task<IActionResult> Put(int id, [FromBody] Nosotros nosotros)
        //{
        //    try
        //    {
        //        if (id != nosotros.Id)
        //        {
        //            return NotFound();
        //        }
        //        _context.Update(nosotros);
        //        await _context.SaveChangesAsync();
        //        return Ok(new { message = "la sesión de quiénes somos fue actualizada con exito" });
        //    }
        //    catch (Exception )
        //    {
        //        //return BadRequest(ex.Message);
        //        return BadRequest("Hubo un error al tratar de actualizar los datos de la sesión nosotros, por favor contacte con el administrador.");

        //    }

        //}

        // DELETE api/<NosotrosController>/5
        [HttpDelete("{id}")]
        [Authorize]

        public async Task<IActionResult> Remove(int id)
        {
            await this._inosotrosRepository.Delete(id);
            return NoContent();
        }

        //public async Task<IActionResult> Delete(int id)
        //{

        //    try
        //    {
        //        var nosotros = await _context.Nosotros.FindAsync(id);
        //        if (nosotros == null)
        //        {
        //            return NotFound();
        //        }
        //        _context.Nosotros.Remove(nosotros);
        //        await _context.SaveChangesAsync();
        //        return Ok(new { message = "la sesión de quiénes somos fue eliminada con exito" });
        //    }
        //    catch (Exception )
        //    {
        //        //return BadRequest(ex.Message);
        //        return BadRequest("Hubo un error al tratar de eliminar los datos de la sesión nosotros, por favor contacte con el administrador.");

        //    }

        //}

    }
}
