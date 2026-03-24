using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sistema.dao;
using Sistema.entidad;
namespace Sistema.bll
{
    public class PersonaRepository : RepositoryBase<Persona>
    {
        readonly static SistemaModuloEntities context = new SistemaModuloEntities();

        public PersonaRepository() : base(context)
        {
            if (context == null)
                throw new ArgumentNullException();
        }

    }
}
