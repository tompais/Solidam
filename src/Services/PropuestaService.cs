﻿using System;
using System.Collections.Generic;
using System.Linq;
using Enums;
using Helpers;
using Models;

namespace Services
{
    public class PropuestaService : BaseService<PropuestaService>
    {
        public static void AgregarPropuesta(Propuestas p)
        {
            p.IdUsuarioCreador = SessionHelper.Usuario.IdUsuario;
            p.Estado = 0;
            p.FechaCreacion = DateTime.Today;

            Db.Propuestas.Add(p);
            Db.SaveChanges();
        }

        public static void Actualizar(Propuestas p)
        {
            Propuestas propuesta = GetById(p.IdPropuesta);

            propuesta.Nombre = p.Nombre;
            propuesta.Descripcion = p.Descripcion;
            propuesta.FechaFin = p.FechaFin;
            propuesta.PropuestasReferencias.ElementAt(0).Nombre = p.PropuestasReferencias.ElementAt(0).Nombre;
            propuesta.PropuestasReferencias.ElementAt(0).Telefono = p.PropuestasReferencias.ElementAt(0).Telefono;
            propuesta.PropuestasReferencias.ElementAt(1).Nombre = p.PropuestasReferencias.ElementAt(1).Nombre;
            propuesta.PropuestasReferencias.ElementAt(1).Telefono = p.PropuestasReferencias.ElementAt(1).Telefono;
            propuesta.TelefonoContacto = p.TelefonoContacto;

            if (p.Foto != null)
                propuesta.Foto = p.Foto;

            if (propuesta.TipoDonacion == (int)TipoDonacion.Monetaria)
            {
                propuesta.PropuestasDonacionesMonetarias.ElementAt(0).Dinero = p.PropuestasDonacionesMonetarias.ElementAt(0).Dinero;
                propuesta.PropuestasDonacionesMonetarias.ElementAt(0).CBU = p.PropuestasDonacionesMonetarias.ElementAt(0).CBU;
            }
            else
                if(propuesta.TipoDonacion == (int)TipoDonacion.Insumos)
            {
                for(int i = 0; i < p.PropuestasDonacionesInsumos.Count(); i++)
                {
                    propuesta.PropuestasDonacionesInsumos.ElementAt(i).Nombre = p.PropuestasDonacionesInsumos.ElementAt(i).Nombre;
                    propuesta.PropuestasDonacionesInsumos.ElementAt(i).Cantidad = p.PropuestasDonacionesInsumos.ElementAt(i).Cantidad;
                }
            }
            else
            {
                propuesta.PropuestasDonacionesHorasTrabajo.ElementAt(0).CantidadHoras = p.PropuestasDonacionesHorasTrabajo.ElementAt(0).CantidadHoras;
                propuesta.PropuestasDonacionesHorasTrabajo.ElementAt(0).Profesion = p.PropuestasDonacionesHorasTrabajo.ElementAt(0).Profesion;
            }

            Db.SaveChanges();
        }

        public static Propuestas GetById(int id)
        {
            return Db.Propuestas.Include("PropuestasDonacionesHorasTrabajo")
                .Include("PropuestasDonacionesInsumos")
                .Include("PropuestasDonacionesMonetarias")
                .FirstOrDefault(p => p.IdPropuesta == id);
        }

        public static int TotalPropuestasActivas()
        {
            return Db.Propuestas.Count(x => x.Estado == 0 && x.IdUsuarioCreador == SessionHelper.Usuario.IdUsuario);
        }

        public static List<Propuestas> GetPropuestasMasValoradas()
        {
            return Db.Propuestas.Include("PropuestasDonacionesHorasTrabajo")
                .Include("PropuestasDonacionesInsumos")
                .Include("PropuestasDonacionesMonetarias")
                .Include("Usuarios")
                .Where(p => p.Estado == (int)PropuestaEstado.Abierta).OrderByDescending(p => p.Valoracion).Take(5).ToList();
        }

        public static void PutPorcentajeAceptacion(int id)
        {
            var valoraciones = Db.PropuestasValoraciones.Where(pv => pv.IdPropuesta == id).ToList();
            var cantValoraciones = valoraciones.Count;
            var cantMg = valoraciones.Count(v => v.Valoracion);
            var propuesta = GetById(id);
            propuesta.Valoracion = cantValoraciones == 0 ? 0 : cantMg * 100 / cantValoraciones;
            Db.SaveChanges();
        }

        public static List<Propuestas> ObtenerPropuestasPorNombreYUsuario(string nombre)
        {
            var propuestas = Db.Propuestas.AsQueryable();

            if (!string.IsNullOrEmpty(nombre))
                propuestas = propuestas.Where(p => p.Nombre.Contains(nombre));

            if (SessionHelper.Usuario != null)
                propuestas = propuestas.Where(u => u.Usuarios.IdUsuario != SessionHelper.Usuario.IdUsuario);

            return propuestas.ToList();
        }

        public static List<Propuestas> ObtenerPropuestasUsuario(int id)
        {
            var misPropuestas = Db.Propuestas.AsQueryable();

            if (SessionHelper.Usuario != null)
                misPropuestas = misPropuestas.Where(u => u.Usuarios.IdUsuario == SessionHelper.Usuario.IdUsuario);

            return misPropuestas.ToList();
        }

        public static void Finalizar(int idPropuesta)
        {
            var donacion = GetById(idPropuesta);
            donacion.Estado = (int) PropuestaEstado.Cerrada;
            Db.SaveChanges();
        }
    }
}
