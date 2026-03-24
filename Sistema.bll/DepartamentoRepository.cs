using Sistema.entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sistema.dao;

namespace Sistema.bll
{
    public class DepartamentoRepository:RepositoryBase<Departamento>
    {
        readonly static SistemaModuloEntities context = new SistemaModuloEntities();
        public DepartamentoRepository() : base(context)
        {
            if (context == null)
                throw new ArgumentNullException();
        }

    }
}
