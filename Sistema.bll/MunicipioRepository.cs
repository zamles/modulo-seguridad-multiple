using Sistema.dao;
using Sistema.entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.bll
{
   public class MunicipioRepository:RepositoryBase<Municipio>
    {
        readonly static SistemaModuloEntities context = new SistemaModuloEntities();
        public MunicipioRepository() : base(context)
        {
            if (context == null)
                throw new ArgumentNullException();
        }

        public List<Municipio> GetByDepartamento(Guid idDepartamento)
        {
            var lista = context.Municipio.Where(w => w.IdDepartamento == idDepartamento).ToList();

            return lista;
        }

    }
}
