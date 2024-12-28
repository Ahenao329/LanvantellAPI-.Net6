//using EASendMail;
using System;
using LavantellAPIS.Context;
using LavantellAPIS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using LavantellAPIS.Helpers;
using System.Text;
using System.Net.Mime;

namespace LavantellAPIS.Helpers
{
    public class Logic
    {
        public void enviarcorreosa(Contactanos contactanos)
        {
            //created object of SmtpClient details and provides server details
            string mensaje = "\n\r" + " Hola, mi nombre es: " + contactanos.Nombre + ", pueden contactarme a través de mi teléfono el cual es " + contactanos.Telefono + " o mi correo " + contactanos.Correo + ", me encuentro ubicado en la ciudad de " + contactanos.Ciudad + "\n\r" + " con dirección " + contactanos.Direccion;
            string mensajecomplement = "\n\r" + " ¡IMPORTANTE! : " + contactanos.Mensaje;
            SmtpClient MyServer = new SmtpClient();
            MyServer.Host = "smtp.gmail.com";
            MyServer.Port = 587;
            MyServer.EnableSsl = true;

            //Server Credentials
            NetworkCredential NC = new NetworkCredential();
            NC.UserName = "anderhenao329@gmail.com";
            NC.Password = "enovpubytxnnjfou";
            //assigned credetial details to server
            MyServer.Credentials = NC;

            //create sender address
            MailAddress from = new MailAddress("anderhenao329@gmail.com");
            //create receiver address
            MailAddress receiver = new MailAddress("anderhenao329@gmail.com");

            MailMessage Mymessage = new MailMessage(from, receiver);


            string html =  " <html> <head> </head> <body> <div style=\"width: 100%;\"> <div style=\"margin: auto; width: 80%; background-color:black;\"> <h1 style=\"color: white;\">Saludos Amig@s de lavantell</h1> <label style=\"color: white;\"> "+ mensaje + "<br>" + mensajecomplement + " </label><br><br> <button><a href=\"lavantells.azurewebsites.net\">lavantells.azurewebsites.net</a></button> </div> </div> </body> </html>";
            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(html, Encoding.UTF8, MediaTypeNames.Text.Html);

            Mymessage.Subject = contactanos.Nombre;
            Mymessage.Body = mensaje;
            Mymessage.AlternateViews.Add(htmlView);
            MyServer.Send(Mymessage);


        }

        //public static void enviaEmail(string mailAsunto, string mailCuerpo)
        //{

        //    MailMessage mensaje = new MailMessage();

        //    mensaje.IsBodyHtml = true;
        //    mensaje.SubjectEncoding = System.Text.Encoding.UTF8;
        //    mensaje.BodyEncoding = System.Text.Encoding.UTF8;
        //    mensaje.Priority = MailPriority.Normal;


        //    mensaje.Subject = mailAsunto;
        //    mensaje.Body = mailCuerpo;


        //}


    }
}
