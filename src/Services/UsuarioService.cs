using System;
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


namespace Services
{
    public class UsuarioService : BaseService<TestService>, IPostService<Usuario>
    {
        public UsuarioService()
        {

        }

        public Usuario Post(Usuario model)
        {
            return null;

        }

        public Usuario BuscarUsuario(Usuario usuario)
        {
            usuario.Password = Sha1.GetSHA1(usuario.Password);

            Usuario usuarioBuscado = SolidamEntities.Instance.Usuario.FirstOrDefault(u => u.Email == usuario.Email && u.Password == usuario.Password);

            if (usuarioBuscado != null)
            {
                SessionHelper.Usuario = usuarioBuscado;
            }

            return usuarioBuscado;
        }

        public void RegistrarUsuario(Usuario usuario)
        {

            usuario.Activo = false;
            usuario.FechaCracion = DateTime.Now;
            usuario.TipoUsuario = 2;
            usuario.Token = Guid.NewGuid().ToString();
            usuario.Password = Sha1.GetSHA1(usuario.Password);

            SolidamEntities.Instance.Usuario.Add(usuario);

            SolidamEntities.Instance.SaveChanges();

            EnviarCorreo(usuario.Token, usuario.Email);

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

        public void ActivarUsuario(String token)
        {

            Usuario usuario = SolidamEntities.Instance.Usuario.FirstOrDefault(u => u.Token == token);

            usuario.Activo = true;

            SolidamEntities.Instance.SaveChanges();

        }
    }
}