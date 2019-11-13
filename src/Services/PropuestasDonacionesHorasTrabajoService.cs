using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Enums;
using Helpers;
using Models;

namespace Services
{
    public class PropuestasDonacionesHorasTrabajoService : BaseService<PropuestasDonacionesHorasTrabajoService>
    {
        public static void Crear(PropuestasDonacionesHorasTrabajo model)
        {
            
        }

        public static PropuestasDonacionesHorasTrabajo GetById(int id)
        {
            return Db.PropuestasDonacionesHorasTrabajo.FirstOrDefault(p => p.IdPropuestaDonacionHorasTrabajo == id);
        }
        public static void Delete(int id)
        {
            Db.PropuestasDonacionesHorasTrabajo.Remove(GetById(id));
        }

        public static void Update(DonacionesHorasTrabajo donacion)
        {
            var propuesta = Db.PropuestasDonacionesHorasTrabajo.FirstOrDefault(pdi =>
                pdi.IdPropuestaDonacionHorasTrabajo == donacion.IdPropuestaDonacionHorasTrabajo);
            if(propuesta == null) throw new Exception();
            propuesta.CantidadHoras -= donacion.Cantidad;
            Db.CustomSaveChanges();
        }

        public static bool EsCompletada(DonacionesHorasTrabajo donacion)
        {
            var propuesta = Db.PropuestasDonacionesHorasTrabajo.FirstOrDefault(p =>
                p.IdPropuestaDonacionHorasTrabajo == donacion.IdPropuestaDonacionHorasTrabajo);
            var horasObjetivo = propuesta?.CantidadHoras;
            var horasObtenidas = Db.DonacionesHorasTrabajo
                .Where(d => d.IdPropuestaDonacionHorasTrabajo == donacion.IdPropuestaDonacionHorasTrabajo)
                .Sum(d => d.Cantidad);
            if (horasObtenidas != horasObjetivo) return false;
            PropuestaService.Finalizar(propuesta.IdPropuesta);
            return true;
        }

        public static int GetHorasDonadasByPropuestaId(int propuestaIdPropuesta)
        {
            var donaciones = Db.DonacionesHorasTrabajo.Where(d =>
                d.IdPropuestaDonacionHorasTrabajo == propuestaIdPropuesta).ToList();
            return donaciones.Count == 0 ? 0 : donaciones.Sum(d => d.Cantidad);
        }
    }
}
