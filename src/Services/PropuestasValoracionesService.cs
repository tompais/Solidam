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
    public class PropuestasValoracionesService : BaseService<PropuestasValoracionesService>
    {
        public static void Crear(PropuestasValoraciones model)
        {
            if (model.IdUsuario != 0)
            {
                var valore = Valore(model.IdPropuesta);
                if(valore != null) Db.PropuestasValoraciones.Remove(valore);
                if(valore == null || valore.Valoracion != model.Valoracion) Db.PropuestasValoraciones.Add(model);

                try
                {
                    Db.CustomSaveChanges();
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException ex)
                {
                    foreach (var errors in ex.EntityValidationErrors)
                    {
                        foreach (var validationError in errors.ValidationErrors)
                        {
                            // get the error message 
                            string errorMessage = validationError.ErrorMessage;
                        }
                    }
                }
            }
        }

        public static PropuestasValoraciones Valore(int idPropuesta)
        {
            if (SessionHelper.Usuario == null) return null;
            return Db.PropuestasValoraciones.FirstOrDefault(pv => pv.IdUsuario == SessionHelper.Usuario.IdUsuario &&
                                                                    pv.IdPropuesta == idPropuesta);
        }
        public static PropuestasValoraciones GetById(int id)
        {
            return Db.PropuestasValoraciones.FirstOrDefault(p => p.IdValoracion == id);
        }
        public static void Delete(int id)
        {
            Db.PropuestasValoraciones.Remove(GetById(id));
        }
    }
}
