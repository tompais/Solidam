using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using DTO;
using Enums;
using Exceptions;
using Helpers;
using Interfaces;
using Models;
using MotivoDenuncia = Models.MotivoDenuncia;

namespace Services
{
    public class DenunciasService : BaseService<DenunciasService>, IFullGetService<Denuncias>, IPutService<Denuncias>
    {
        private DenunciasService()
        {
        }

        public static void Crear(Denuncias model)
        {
            if (model.IdUsuario == 0) return;
            Db.Denuncias.Add(model);
            Db.CustomSaveChanges();
        }

        public static bool Denuncie(int idPropuesta)
        {
            return Db.Denuncias.Any(d => d.IdPropuesta == idPropuesta && d.IdUsuario == SessionHelper.Usuario.IdUsuario);
        }

        public List<Denuncias> Get(Denuncias model)
        {
            var denuncias = Db.Denuncias.Where(denuncia => denuncia.Estado == (int) DenunciaEstado.EnRevision && denuncia.Propuestas.Estado != (int) PropuestaEstado.Bloqueada && (denuncia.Propuestas.Estado != (int)PropuestaEstado.Cerrada || DateTime.Now <= denuncia.Propuestas.FechaFin)).ToList().AsQueryable();

            if (model == null) return denuncias.ToList();
            if (model.MotivoDenuncia != null && !string.IsNullOrEmpty(model.MotivoDenuncia.Descripcion))
                denuncias = denuncias.Where(d =>
                    d.MotivoDenuncia.Descripcion.ToLower().Contains(model.MotivoDenuncia.Descripcion.ToLower()));

            if (!string.IsNullOrEmpty(model.Comentarios))
                denuncias = denuncias.Where(d => d.Comentarios.ToLower().Equals(model.Comentarios.ToLower()));

            return denuncias.ToList();
        }

        public List<DenunciaDTO> GetAllDenunciasDTOByFilter(Denuncias model, string sortColumn, string sortDir,
            Pager pager)
        {
            var denunciasDTO = Get(model).Select(denuncia => new DenunciaDTO
            {
                IdDenuncia = denuncia.IdDenuncia,
                Comentarios = denuncia.Comentarios,
                FechaCreacion = denuncia.FechaCreacion,
                Motivo = denuncia.MotivoDenuncia.Descripcion,
                Propuesta = $"/{Constant.PropuestaControllerName}/Detalle?id={denuncia.Propuestas.IdPropuesta}"
            });

            if (!string.IsNullOrEmpty(sortColumn) && !string.IsNullOrEmpty(sortDir))
            {
                denunciasDTO = denunciasDTO.OrderBy(sortColumn + " " + sortDir);
            }

            if (pager != null)
                denunciasDTO = denunciasDTO.Skip((int) pager.Start).Take((int) pager.Size);

            return denunciasDTO.ToList();
        }

        public Denuncias Put(Denuncias model)
        {
            var denuncia = GetById((ulong) model.IdDenuncia);
            if(denuncia == null) throw new IdNoValidoException(typeof(Denuncias), (ulong) model.IdDenuncia);
            if (model.Estado >= 0)
            {
                if (model.Estado == (int) DenunciaEstado.Aceptada)
                    denuncia.Propuestas.Estado = (int) PropuestaEstado.Bloqueada;
                denuncia.Estado = model.Estado;
            }
            Db.CustomSaveChanges();
            return denuncia;
        }

        public Denuncias GetById(ulong id)
        {
            return Db.Denuncias.FirstOrDefault(denuncia => denuncia.IdDenuncia == (int) id);
        }
    }
}
