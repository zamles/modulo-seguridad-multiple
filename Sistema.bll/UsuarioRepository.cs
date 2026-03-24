using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sistema.entidad;
using Sistema.dao;

namespace Sistema.bll
{
    public class UsuarioRepository:RepositoryBase<Usuario>
    {
        readonly static SistemaModuloEntities context = new SistemaModuloEntities();

        public UsuarioRepository() : base(context)
        {
            if (context == null)
                throw new ArgumentNullException();
        }

        public Usuario GetByUserPassSis(string text, string v)
        {
            var objeto = from u in context.Usuario                                                  
                         where u.NombreUsuario.Equals(text) && u.Contrasena.Equals(v) && u.EsActivo
                         select u;
            return objeto.FirstOrDefault();
                         
        }

        public Usuario GetByCodigo(int codigo)
        {
            var usuari = context.Usuario.FirstOrDefault(f => f.NoCarnet == codigo);
            return usuari;
        }
    }
}
