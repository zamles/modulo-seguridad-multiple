using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sistema.dao;
using Sistema.entidad;
namespace Sistema.bll
{
    public class ParametrosRepository:RepositoryBase<Parametros>
    {

        readonly static SistemaModuloEntities context = new SistemaModuloEntities();
        public ParametrosRepository() : base(context)
        {
            if (context == null)
                throw new ArgumentNullException();
        }

        public string GetByCodigo(string codigo)
        {
            return context.Parametros.FirstOrDefault(f => f.Nombre == codigo).Valor;
        }
        

    }
}
