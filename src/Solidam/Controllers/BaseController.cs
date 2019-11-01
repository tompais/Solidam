﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Exceptions;
using Helpers;
using Models;

namespace Solidam.Controllers
{
    public class BaseController : Controller
    {
        private static Usuarios UsuarioSesion => SessionHelper.Usuario;
        protected override void Initialize(RequestContext requestContext)
        {
            if (!SessionHelper.MostrePopUpCompletarPerfil && UsuarioSesion != null &&
                string.IsNullOrEmpty(UsuarioSesion.Nombre) && string.IsNullOrEmpty(UsuarioSesion.Apellido) &&
                string.IsNullOrEmpty(UsuarioSesion.Foto) && string.IsNullOrEmpty(UsuarioSesion.UserName))
            {
                SessionHelper.MostrarPopUpCompletarPerfil = SessionHelper.MostrePopUpCompletarPerfil = true;
            }
            else
            {
                SessionHelper.MostrarPopUpCompletarPerfil = false;
            }

            base.Initialize(requestContext);

            
        }

        private static bool EstaUsuarioLogueado() => UsuarioSesion != null;

        private static bool EstaPerfilUsuarioCompleto() =>
            !string.IsNullOrEmpty(UsuarioSesion.Nombre) &&
            !string.IsNullOrEmpty(UsuarioSesion.Apellido) &&
            !string.IsNullOrEmpty(UsuarioSesion.Foto) && !string.IsNullOrEmpty(UsuarioSesion.UserName);

        private static bool EsBusquedaDePropuesta(string controllerName, string actionName) =>
            controllerName.Equals(Constant.PropuestaControllerName) &&
            actionName.Equals(Constant.BuscarPropuestaActionName);

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            var controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName.ToLower();
            var actionName = filterContext.ActionDescriptor.ActionName.ToLower();

            if (!EstaUsuarioLogueado() && !controllerName.Equals(Constant.InicioControllerName.ToLower()) && !controllerName.Equals(Constant.SeguridadControllerName.ToLower()) && !EsBusquedaDePropuesta(controllerName, actionName))
                throw new AccesoNoAutorizadoException(filterContext.HttpContext.Request.Url?.AbsoluteUri);
            if(EstaUsuarioLogueado() && !EstaPerfilUsuarioCompleto() && controllerName.Equals(Constant.PropuestaControllerName.ToLower()) && (actionName.Contains("crear") || actionName.Equals(Constant.MisPropuestasActionName.ToLower())))
                throw new PerfilUsuarioNoCompletadoException();

        }

        protected override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);

            var exception = filterContext.Exception;

            switch (exception)
            {
                case AccesoNoAutorizadoException ex:
                    filterContext.Result = RedirectToAction("Iniciar", "Seguridad", new { pr = Convert.ToBase64String(Encoding.ASCII.GetBytes(ex.Url)) });
                    break;
                case PerfilUsuarioNoCompletadoException _:
                    SessionHelper.MostrePopUpCompletarPerfil = false;
                    filterContext.Result = RedirectToAction("MiPerfil", "Usuario");
                    break;
                default:
                    Response.Clear();
                    var error = exception is HttpException httpException ? httpException.GetHttpCode() : 0;
                    Server.ClearError();
                    filterContext.Result = RedirectToAction("Error", "Home", new RouteValueDictionary(new Dictionary<string, object>
                    {
                        { "ErrorCode", error }
                    }));
                    break;
            }
        }
    }
}