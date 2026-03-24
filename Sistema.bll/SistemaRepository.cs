using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sistema.entidad;
using Sistema.dao;

namespace Sistema.bll
{
   public class SistemaRepository:RepositoryBase<entidad.Sistema>
   {
        readonly static SistemaModuloEntities context = new SistemaModuloEntities();

        public SistemaRepository() : base(context)
        {
            if (context == null)
                throw new ArgumentNullException();
        }
   }
}
