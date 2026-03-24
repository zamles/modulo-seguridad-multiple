using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sistema.entidad;
using Sistema.dao;

namespace Sistema.bll
{
   public class ItemMenuRepository:RepositoryBase<ItemMenu>
   {
        readonly static SistemaModuloEntities context = new SistemaModuloEntities();

        public ItemMenuRepository():base(context)
        {
            if (context == null)
                throw new ArgumentNullException();
        }

        public List<Menu> GetBySysRol(Guid guid)
        {
            var lista = context.PemisoRolSistema.Where(w => w.IdRolSistema == guid).Select(s => s.Permiso.ItemMenu.Menu).Distinct();
            return lista.ToList();
        }

        public List<ItemMenu> GetByRol(Guid guid, Guid idMenu)
        {
            var lista = context.PemisoRolSistema.Where(w => w.IdRolSistema == guid).Select(s => s.Permiso.ItemMenu).Distinct().Where(w => w.IdMenu == idMenu).Select(s=>s);
            return lista.ToList();
        }
    }
}
