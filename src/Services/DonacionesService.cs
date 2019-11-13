using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using DTO;
using Enums;
using Helpers;
using Interfaces;
using Models;

namespace Services
{
    public class DonacionesService : BaseService<DonacionesService>, IGetService<DonacionesHorasTrabajo>, IGetService<DonacionesInsumos>, IGetService<DonacionesMonetarias>
    {
        private DonacionesService()
        {
        }

        public List<DonacionesHorasTrabajo> Get(DonacionesHorasTrabajo model)
        {
            var query = Db.DonacionesHorasTrabajo.AsQueryable();

            if (model == null) return query.ToList();
            if (model.IdUsuario > 0)
            {
                query = query.Where(q => q.IdUsuario == model.IdUsuario);
            }

            if (model.PropuestasDonacionesHorasTrabajo?.Propuestas != null &&
                !string.IsNullOrEmpty(model.PropuestasDonacionesHorasTrabajo.Propuestas.Nombre))
            {
                query = query.Where(q =>
                    q.PropuestasDonacionesHorasTrabajo.Propuestas.Nombre.Contains(
                        model.PropuestasDonacionesHorasTrabajo.Propuestas.Nombre));
            }
            return query.ToList();
        }

        public List<DonacionesInsumos> Get(DonacionesInsumos model)
        {
            var query = Db.DonacionesInsumos.AsQueryable();

            if (model == null) return query.ToList();
            if (model.IdUsuario > 0)
            {
                query = query.Where(q => q.IdUsuario == model.IdUsuario);
            }

            if (model.PropuestasDonacionesInsumos?.Propuestas != null &&
                !string.IsNullOrEmpty(model.PropuestasDonacionesInsumos.Propuestas.Nombre))
            {
                query = query.Where(q =>
                    q.PropuestasDonacionesInsumos.Propuestas.Nombre.Contains(
                        model.PropuestasDonacionesInsumos.Propuestas.Nombre));
            }
            return query.ToList();
        }

        public List<DonacionesMonetarias> Get(DonacionesMonetarias model)
        {
            var query = Db.DonacionesMonetarias.AsQueryable();

            if (model == null) return query.ToList();
            if (model.IdUsuario > 0)
            {
                query = query.Where(q => q.IdUsuario == model.IdUsuario);
            }

            if (model.PropuestasDonacionesMonetarias?.Propuestas != null &&
                !string.IsNullOrEmpty(model.PropuestasDonacionesMonetarias.Propuestas.Nombre))
            {
                query = query.Where(q =>
                    q.PropuestasDonacionesMonetarias.Propuestas.Nombre.Contains(
                        model.PropuestasDonacionesMonetarias.Propuestas.Nombre));
            }
            return query.ToList();
        }

        public long GetAllCount(int idUsuario) =>
            Db.DonacionesMonetarias.Count(dm => dm.IdUsuario == idUsuario) +
            Db.DonacionesHorasTrabajo.Count(dht => dht.IdUsuario == idUsuario) +
            Db.DonacionesInsumos.Count(di => di.IdUsuario == idUsuario);

        public List<DonacionDTO> GetAllDonacionesDTOByFilter(int idUsuario, string searchValue, Sorter sorter,
            Pager pager)
        {
            var donacionesDTO = new List<DonacionDTO>();
            var propuesta = new Propuestas
            {
                Nombre = searchValue
            };
            donacionesDTO.AddRange(Get(new DonacionesHorasTrabajo
            {
                IdUsuario = idUsuario,
                PropuestasDonacionesHorasTrabajo = new PropuestasDonacionesHorasTrabajo
                {
                    Propuestas = propuesta
                }
            }).Select(dht => new DonacionDTO
            {
                Propuesta = $"/{Constant.PropuestaControllerName}/Detalle?id={dht.PropuestasDonacionesHorasTrabajo.Propuestas.IdPropuesta}",
                Estado = GetEstadoString((PropuestaEstado) dht.PropuestasDonacionesHorasTrabajo.Propuestas.Estado),
                MiDonacion = dht.Cantidad + " hs",
                Nombre = dht.PropuestasDonacionesHorasTrabajo.Propuestas.Nombre,
                TipoDonacion = GetTipoDonacionString(dht.GetType()),
                TotalRecaudado = GetRecaudacionTotal(dht.PropuestasDonacionesHorasTrabajo.Propuestas.IdPropuesta, dht.GetType())
            }));
            donacionesDTO.AddRange(Get(new DonacionesInsumos
            {
                IdUsuario = idUsuario,
                PropuestasDonacionesInsumos = new PropuestasDonacionesInsumos
                {
                    Propuestas = propuesta
                }
            }).Select(di => new DonacionDTO
            {
                Propuesta = $"/{Constant.PropuestaControllerName}/Detalle?id={di.PropuestasDonacionesInsumos.Propuestas.IdPropuesta}",
                Estado = GetEstadoString((PropuestaEstado) di.PropuestasDonacionesInsumos.Propuestas.Estado),
                MiDonacion = Db.DonacionesInsumos.Where(d => d.IdPropuestaDonacionInsumo == di.IdPropuestaDonacionInsumo).Sum(d => d.Cantidad) + $" ({di.PropuestasDonacionesInsumos.Nombre})",
                Nombre = di.PropuestasDonacionesInsumos.Propuestas.Nombre,
                TipoDonacion = GetTipoDonacionString(di.GetType()),
                TotalRecaudado = GetRecaudacionTotal(di.PropuestasDonacionesInsumos.IdPropuestaDonacionInsumo, di.GetType())
            }));
            donacionesDTO.AddRange(Get(new DonacionesMonetarias()).Select(dm => new DonacionDTO
            {
                Propuesta = $"/{Constant.PropuestaControllerName}/Detalle?id={dm.PropuestasDonacionesMonetarias.Propuestas.IdPropuesta}",
                Estado = GetEstadoString((PropuestaEstado) dm.PropuestasDonacionesMonetarias.Propuestas.Estado),
                FechaDonacion = dm.FechaCreacion.ToString("dd/MM/yyyy"),
                MiDonacion = "$ " + dm.Dinero,
                Nombre = dm.PropuestasDonacionesMonetarias.Propuestas.Nombre,
                TipoDonacion = GetTipoDonacionString(dm.GetType()),
                TotalRecaudado = GetRecaudacionTotal(dm.PropuestasDonacionesMonetarias.IdPropuesta, dm.GetType())
            }));

            if (!string.IsNullOrEmpty(sorter.Property) && !string.IsNullOrEmpty(sorter.Direction))
            {
                donacionesDTO = donacionesDTO.OrderBy(sorter.Property + " " + sorter.Direction).ToList();
            }

            if (pager != null)
            {
                donacionesDTO = donacionesDTO.Skip((int) pager.Start).Take((int) pager.Size).ToList();
            }

            return donacionesDTO;
        }

        private static string GetRecaudacionTotal(int idPropuesta, Type type)
        {
            string recaudacionTotal;

            if (typeof(DonacionesHorasTrabajo) == type)
            {
                recaudacionTotal = Db.DonacionesHorasTrabajo.Where(dht => dht.PropuestasDonacionesHorasTrabajo.Propuestas.IdPropuesta == idPropuesta).Sum(dht => dht.Cantidad) + " hs";
            }
            else if(typeof(DonacionesMonetarias) == type)
            {
                recaudacionTotal = "$ " + Db.DonacionesMonetarias.Where(dm => dm.PropuestasDonacionesMonetarias.Propuestas.IdPropuesta == idPropuesta).Sum(dm => dm.Dinero);
            }
            else
            {
                var insumos =
                    Db.PropuestasDonacionesInsumos.First(pdi => pdi.IdPropuestaDonacionInsumo == idPropuesta).Cantidad;
                recaudacionTotal = insumos == 1 ? insumos + " unidad" : insumos + " unidades";
            }

            return recaudacionTotal;
        }

        private string GetEstadoString(PropuestaEstado estado)
        {
            string estadoString;
            switch (estado)
            {
                case PropuestaEstado.Abierta:
                    estadoString = estado.ToString();
                    break;
                case PropuestaEstado.Cerrada:
                    estadoString = estado.ToString();
                    break;
                case PropuestaEstado.EnRevision:
                    estadoString = "En Revisión";
                    break;
                case PropuestaEstado.Bloqueada:
                    estadoString = estado.ToString();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(estado), estado, null);
            }

            return estadoString;
        }

        private static string GetTipoDonacionString(Type donacionType)
        {
            string tipoDonacionString;

            if (donacionType == typeof(DonacionesHorasTrabajo))
            {
                tipoDonacionString = "Horas de Trabajo";
            }
            else if (donacionType == typeof(DonacionesInsumos))
            {
                tipoDonacionString = "Insumos";
            }
            else
            {
                tipoDonacionString = "Monetaria";
            }

            return tipoDonacionString;
        }

    }
}