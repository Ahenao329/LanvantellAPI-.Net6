using EASendMail;
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



    public class ServiciosController : ControllerBase
    {


        private readonly ApplicationDbContext _context;
        public ServiciosController(ApplicationDbContext context)
        {
            _context = context;
        }


        // GET: api/<ServiciosController>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get()
        {
            try
            {
                var listServicios = await _context.Servicios.ToListAsync();
                return Ok(listServicios);
            }
            catch (Exception )
            {
                //return BadRequest(ex.Message);
                return BadRequest("Hubo un error al tratar de obtener los servicios, por favor contacte con el administrador.");

            }
        }

        // GET api/<ServiciosController>/5
        [HttpGet("{id}")]
        [AllowAnonymous]

        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ServiciosController>
        [HttpPost]

        public async Task<IActionResult> Post([FromBody] Servicios servicios)
        {
            try
            {
                _context.Add(servicios);

                await _context.SaveChangesAsync();
                return Ok(servicios);
            }
            catch (Exception )
            {
                //return BadRequest(ex.Message);
                return BadRequest("Hubo un error al tratar de enviar el servicios, por favor contacte con el administrador.");

            }
        }

        // PUT api/<ServiciosController>/5
        [HttpPut("{id}")]

        public async Task<IActionResult> Put(int id, [FromBody] Servicios servicios)
        {
            try
            {
                if (id != servicios.Id)
                {
                    return NotFound();
                }
                _context.Update(servicios);
                await _context.SaveChangesAsync();
                return Ok(new { message = "La sesion de servicios fue actualizada con exito" });
            }
            catch (Exception )
            {
                //return BadRequest(ex.Message);
                return BadRequest("Hubo un error al tratar de actualizar el servicios, por favor contacte con el administrador.");

            }

        }

        // DELETE api/<ServiciosController>/5
        [HttpDelete("{id}")]

        public async Task<IActionResult> Delete(int id)
        {

            try
            {
                var servicios = await _context.Servicios.FindAsync(id);
                if (servicios == null)
                {
                    return NotFound();
                }
                _context.Servicios.Remove(servicios);
                await _context.SaveChangesAsync();
                return Ok(new { message = "la  sesion de servicios fue eliminada con exito" });
            }
            catch (Exception ex)
            {
                //return BadRequest(ex.Message);
                return BadRequest("Hubo un error al tratar de eliminar el servicios, por favor contacte con el administrador.");

            }

        }


        //public string EnviarCorreo(string correoDestino, string asunto, string mensajeCorreo)
        //{
        //    string mensaje = "Error al enviar correo";


        //    try
        //    {
        //        SmtpMail objetoCorreo = new SmtpMail("TryIt");

        //        objetoCorreo.From = "anderhenao329@gmail.com";
        //        objetoCorreo.To = correoDestino;
        //        objetoCorreo.Subject = asunto;
        //        objetoCorreo.TextBody = mensajeCorreo;

        //        SmtpServer objetoServidor = new SmtpServer("smtp.gmail.com");
        //        objetoServidor.User = "anderhenao329@gmail.com";
        //        objetoServidor.Password = "blakenanayxdpruebas";
        //        objetoServidor.Port = 587;
        //        objetoServidor.ConnectType = SmtpConnectType.ConnectSSLAuto;

        //        SmtpClient objetoCliente = new SmtpClient();
        //        objetoCliente.SendMail(objetoServidor, objetoCorreo);
        //        mensaje = "Correo Enviado Correctamente.";
        //    }
        //    catch (Exception ex)
        //    {
        //        mensaje = "Error al enviar correo." + ex.Message;

        //    }
        //    return mensaje;
        //}
    }
}
