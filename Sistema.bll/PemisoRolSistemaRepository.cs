using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sistema.entidad;
using Sistema.dao;
namespace Sistema.bll
{
   public class PemisoRolSistemaRepository:RepositoryBase<PemisoRolSistema>
   {
        readonly static SistemaModuloEntities context = new SistemaModuloEntities();

        public PemisoRolSistemaRepository():base(context)
        {
            if (context == null)
                throw new ArgumentNullException();
        }

        public List<PemisoRolSistema> GetByRolItem(Guid idRol, string item)
        {
            var permisos = from p in context.PemisoRolSistema
                           join i in context.Permiso on p.IdPermiso equals i.IdPermiso
                           join im in context.ItemMenu on i.IdItemMenu equals im.IdItemMenu
                           where p.IdRolSistema == idRol && im.Nombre == item
                           select p;

            return permisos.ToList();
        }
    }
}
