using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sistema.entidad;
using Sistema.dao;
namespace Sistema.bll
{
    public class RolSistemaRepository:RepositoryBase<RolSistema>
    {
        readonly static SistemaModuloEntities context = new SistemaModuloEntities();

        public RolSistemaRepository() : base(context)
        {
            if (context == null)
                throw new ArgumentNullException();
        }

        public List<RolSistema> GetByUserSis(Guid idUsuario, string modulo)
        {
            var objeto = from us in context.UsuarioSistema
                         join rs in context.RolSistema on us.IdRolSistema equals rs.IdRolSistema
                         join s in context.Sistema on rs.IdSistema equals s.IdSistema
                         where us.IdUsuario == idUsuario && s.Nombre.Equals(modulo)
                         select rs;

            return objeto.ToList();
        }

        public RolSistema GetByIdSistemaNameRol(Guid idSistema, string name)
        {
            var objeto = context.RolSistema.FirstOrDefault(f => f.IdSistema == idSistema && f.NombreRol.Equals(name));

            return objeto;
        }
    }
}
