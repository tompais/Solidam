using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using Helpers;
using Interfaces;
using Models;
using Utils;
using Exceptions;
using Enums;


namespace Services
{
    public class UsuarioService : BaseService<UsuarioService>, IPostService<Usuario>, IGetService<Usuario>, IPutService<Usuario>
    {
        private UsuarioService()
        {
        }

        public Usuario Post(Usuario model)
        {
            model.Activo = false;
            model.FechaCracion = DateTime.Now;
            model.TipoUsuario = 2;
            model.Token = Guid.NewGuid().ToString();
            model.Password = Sha1.GetSHA1(model.Password);

            EmailAddressAttribute e = new EmailAddressAttribute();

            if (!e.IsValid(model.Email))
            {
                throw new UsuarioException("Formato de Email incorrecto", ErrorCode.EmailInvalidoUsuario);
            }

            //Db.Usuario.Add(model);
            //Db.SaveChanges();

            //EnviarCorreo(model.Token, model.Email); 

            return model;

        }

        public Usuario Get(Usuario model)
        {
            model.Password = Sha1.GetSHA1(model.Password);

            Usuario usuario = Db.Usuario.FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password);

            if (usuario != null)
            {
                SessionHelper.Usuario = usuario;
            }

            return usuario;
        }

        public Usuario Put(Usuario model)
        {
            Usuario usuario = SolidamEntities.Instance.Usuario.FirstOrDefault(u => u.Token == model.Token);

            usuario.Activo = true;

            SolidamEntities.Instance.SaveChanges();

            return usuario;
        }


        public void EnviarCorreo(string token, string email)
        {
            MailMessage msg = new MailMessage();

            msg.To.Add(email);
            msg.Subject = "Activar cuenta en Solidam";
            msg.SubjectEncoding = Encoding.UTF8;

            string link = "http://localhost:" + HttpContext.Current.Request.Url.Port + "/Usuario/Activar?token=" + token;

            string etiquita = $"<a href=\"{link}\" style='font-size: 20px; font-family: Helvetica, Arial, sans-serif; text-decoration: none; border-radius: 4.8px; line-height: 30px; display: inline-block; font-weight: normal; white-space: nowrap; background-color: #028817; color: #ffffff; padding: 8px 16px; border: white;'>Activar ahora</a>";

            msg.Body = CrearCuerpoCorreo(etiquita);
            msg.IsBodyHtml = true;
            msg.BodyEncoding = Encoding.Default;

            var correo = "solidam.unlam@gmail.com";
            var password = "solidam2019";

            msg.From = new MailAddress(correo);

            SmtpClient cliente = new SmtpClient();

            cliente.Credentials = new NetworkCredential(correo, password);

            cliente.Port = 587;
            cliente.EnableSsl = true;
            cliente.Host = "smtp.gmail.com";

            try
            {
                cliente.Send(msg);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public string CrearCuerpoCorreo(string link)
        {
            string cuerpo = string.Empty;

            using (StreamReader reader = new StreamReader(AppContext.BaseDirectory + "Views\\Usuario\\Email.html"))
            {
                cuerpo = reader.ReadToEnd();
            }

            cuerpo = cuerpo.Replace("{Link}", link);

            return cuerpo;
        }

    }
}