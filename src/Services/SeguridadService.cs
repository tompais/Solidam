using Enums;
using Exceptions;
using Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using Helpers;
using Utils;

namespace Services
{
    public class SeguridadService : BaseService<SeguridadService>, IPostService<Usuarios>, IGetService<Usuarios>,
        IPutService<Usuarios>
    {
        private SeguridadService()
        {
        }

        public List<Usuarios> Get(Usuarios model)
        {
            var usuarios = Db.Usuarios.AsQueryable();

            if (model == null) return usuarios.ToList();

            if (!string.IsNullOrEmpty(model.Email))
                usuarios = usuarios.Where(u => u.Email.Equals(model.Email));

            if (!string.IsNullOrEmpty(model.Password))
                usuarios = usuarios.Where(u => u.Password.Equals(model.Password));

            if (!string.IsNullOrEmpty(model.Token))
                usuarios = usuarios.Where(u => u.Token.Equals(model.Token));


            return usuarios.ToList();
        }

        public Usuarios Post(Usuarios model)
        {
            model.Activo = false;
            model.FechaCracion = DateTime.Now;
            model.TipoUsuario = 2;
            model.Token = Guid.NewGuid().ToString().Substring(0, 29);

            model.Password = Sha1.GetSHA1(model.Password);

            Db.Usuarios.Add(model);
            Db.CustomSaveChanges();

            return model;
        }

        public Usuarios Put(Usuarios model)
        {
            var usuarioAModificar = Get(model).FirstOrDefault();

            if (usuarioAModificar != null)
            {
                if (!string.IsNullOrEmpty(model.Nombre))
                    usuarioAModificar.Nombre = model.Nombre;

                if (!string.IsNullOrEmpty(model.Apellido))
                    usuarioAModificar.Apellido = model.Apellido;

                if (!string.IsNullOrEmpty(model.UserName))
                    usuarioAModificar.UserName = model.UserName;

                if (model.Activo)
                    usuarioAModificar.Activo = model.Activo;
            }
            

            Db.CustomSaveChanges();

            return usuarioAModificar;
        }


        public void EnviarCorreo(string token, string email)
        {
            var msg = new MailMessage();

            msg.To.Add(email);
            msg.Subject = "Activar cuenta en Solidam";
            msg.SubjectEncoding = Encoding.UTF8;

            var link = "http://localhost:" + HttpContext.Current.Request.Url.Port + "/Seguridad/Activar?token=" + token;

            var etiquita =
                $"<a href=\"{link}\" style='font-size: 20px; font-family: Helvetica, Arial, sans-serif; text-decoration: none; border-radius: 4.8px; line-height: 30px; display: inline-block; font-weight: normal; white-space: nowrap; background-color: #028817; color: #ffffff; padding: 8px 16px; border: white;'>Activar ahora</a>";

            msg.Body = CrearCuerpoCorreo(etiquita);
            msg.IsBodyHtml = true;
            msg.BodyEncoding = Encoding.Default;

            const string correo = "solidam.unlam@gmail.com";
            const string password = "solidam2019";

            msg.From = new MailAddress(correo);

            var cliente = new SmtpClient
            {
                Credentials = new NetworkCredential(correo, password),
                Port = 587,
                EnableSsl = true,
                Host = "smtp.gmail.com"
            };

            try
            {
                Task.Run(() => { cliente.SendMailAsync(msg); });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public string CrearCuerpoCorreo(string link)
        {
            var cuerpo = string.Empty;

            using (var reader = new StreamReader(AppContext.BaseDirectory + "Views\\Seguridad\\Email.html"))
            {
                cuerpo = reader.ReadToEnd();
            }

            cuerpo = cuerpo.Replace("{Link}", link);

            return cuerpo;
        }
    }
}