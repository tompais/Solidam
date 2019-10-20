using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using Enums;
using Exceptions;
using Helpers;
using Interfaces;
using Models;
using Utils;


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

            var emailExitente = Db.Usuario.FirstOrDefault(u => u.Email == model.Email);

            Usuario retorno = null;

            if (emailExitente == null)
            {
                ValidarUsuario(model);

                model.Password = Sha1.GetSHA1(model.Password);

                Db.Usuario.Add(model);
                Db.SaveChanges();

                retorno = model;
            }
            else
            {
                retorno = new Usuario() { Error = "Ya existe un usuario registrado con este correo" };
            }

            return retorno;
        }

        public List<Usuario> Get(Usuario model)
        {
            var usuarios = Db.Usuario.AsQueryable();

            if (model == null) return usuarios.ToList();

            if (!string.IsNullOrEmpty(model.Email))
                usuarios = usuarios.Where(u => u.Email.Equals(model.Email));

            if (!string.IsNullOrEmpty(model.Password))
                usuarios = usuarios.Where(u => u.Password.Equals(model.Password));

            if (!string.IsNullOrEmpty(model.Token))
                usuarios = usuarios.Where(u => u.Token.Equals(model.Token));

            return usuarios.ToList();
        }

        public Usuario Put(Usuario model)
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
            else
            {
                throw new UsuarioException("El usuario buscado no existe.", ErrorCode.UsurioInexistente);
            }

            Db.SaveChanges();

            return usuarioAModificar;
        }

        public void ValidarUsuario(Usuario model)
        {
            if (string.IsNullOrEmpty(model.FechaNacimiento.ToString()))
            {
                throw new UsuarioException("La fecha de nacimiento no puede se vacia o nula.", ErrorCode.FechaNacimientoInvalida);
            }

            if ((DateTime.Now.Year - model.FechaNacimiento.Year) < 18)
            {
                throw new UsuarioException("El usuario no puede ser menor a 18 años", ErrorCode.FechaNacimientoInvalida);
            }

            EmailAddressAttribute email = new EmailAddressAttribute();

            if (string.IsNullOrEmpty(model.Email) || !email.IsValid(model.Email))
            {
                throw new UsuarioException("Formato de email incorrecto.", ErrorCode.EmailInvalido);
            }

            var poseeNumeros = new Regex(@"[0-9]+");
            var poseeLetraMayus = new Regex(@"[A-Z]+");
            var posee8Caracteres = new Regex(@".{8,}");

            var regexPassValidacion = poseeNumeros.IsMatch(model.Password) && poseeLetraMayus.IsMatch(model.Password) && posee8Caracteres.IsMatch(model.Password);
            

            if (string.IsNullOrEmpty(model.Password) || !regexPassValidacion)
            {
                throw new UsuarioException("Formato de password incorrecto.", ErrorCode.PasswordInvalida);
            }

            if (model.TipoUsuario != 2)
            {
                throw new UsuarioException("La cuenta no puede ser de otro tipo que no sea usario", ErrorCode.TipoUsuarioInvalido);
            }

            if (string.IsNullOrEmpty(model.Token))
            {
                throw new UsuarioException("La cuenta debe poseer un token obligatoriamente", ErrorCode.TokenInvalido);
            }

        }


        public void EnviarCorreo(string token, string email)
        {
            var msg = new MailMessage();

            msg.To.Add(email);
            msg.Subject = "Activar cuenta en Solidam";
            msg.SubjectEncoding = Encoding.UTF8;

            var link = "http://localhost:" + HttpContext.Current.Request.Url.Port + "/Usuario/Activar?token=" + token;

            var etiquita = $"<a href=\"{link}\" style='font-size: 20px; font-family: Helvetica, Arial, sans-serif; text-decoration: none; border-radius: 4.8px; line-height: 30px; display: inline-block; font-weight: normal; white-space: nowrap; background-color: #028817; color: #ffffff; padding: 8px 16px; border: white;'>Activar ahora</a>";

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