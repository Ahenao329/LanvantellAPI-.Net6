using LavantellAPIS.Models;
using LavantellAPIS.Models.Request;
using LavantellAPIS.Models.Response;
using LavantellAPIS.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace LavantellAPIS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _UserService;

        public UserController(IUserService userservice)
        {
            _UserService = userservice;
        }

        [HttpPost("login")]
        public IActionResult Autentificar([FromBody] AuthRequest model)
        {
            Respuesta respuesta = new Respuesta();

            var userrresponse = _UserService.Auth(model);

            if (userrresponse == null)
            {

                respuesta.Exito = 0;
                respuesta.Mensaje = "Usuario o contraseña incorrecta";
                return BadRequest(respuesta);
            }
            respuesta.Exito = 1;
            respuesta.Data = userrresponse;
            return Ok(respuesta);
        }

        //[HttpGet]
        //public IActionResult Get()
        //{
        //    Respuesta oRespuesta = new Respuesta();

        //    try
        //    {
        //        using (LavantellContext db = new LavantellContext())
        //        {

        //            var list = db.Usuarios.OrderByDescending(d => d.Id).ToList();
        //            oRespuesta.Exito = 1;
        //            oRespuesta.Data = list;

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        oRespuesta.Mensaje = ex.Message;
        //    }
        //    return Ok(oRespuesta);

        //}



    }
}
