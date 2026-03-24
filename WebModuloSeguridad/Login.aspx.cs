using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sistema.entidad;
using Sistema.bll;
using System.Web.Security;

public partial class Login : System.Web.UI.Page
{
    const string Modulo = "Modulo";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;
        
    }

    private int VerificarUsuario()
    {
        if(txtUser.Text == string.Empty || txtPass.Text == string.Empty)
        {            
            return 1;
        }

        var user = new UsuarioRepository().GetByUserPassSis(txtUser.Text, General.Encrypt(txtPass.Text,"12345678","87654321"));

        if(user == null)
        {            
            return 2;
        }

        var rol = new RolSistemaRepository().GetByUserSis(user.IdUsuario,Modulo);

        if(rol.Count == 0)
        {            
            return 3;
        }

        Session["IdRol"] = rol.FirstOrDefault().IdRolSistema;
        Session["NombreUsuario"] = user.Razonsocial;
        return 0;

    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        switch (VerificarUsuario())
        {
            case 1:
                General.MensajeAdvertencia(this.Page, "Todos los campos son obligatorios");
                break;
            case 2:
                General.MensajeAdvertencia(this.Page, "Usuario o Contrasdeña no validas");
                break;
            case 3:
                General.MensajeAdvertencia(this.Page, "Credenciales no validas para este sistema");
                break;
            default:
                Autenticacion();
                //Response.Redirect("Default.aspx");
            break;
        }
        
    }

    private void Autenticacion()
    {
        //CREAMOS EL TICKET
        FormsAuthenticationTicket ticket =
            new FormsAuthenticationTicket(1,
                this.txtUser.Text
                , DateTime.Now
                , DateTime.Now.AddMinutes(30)
                , true
                ,Session["IdRol"].ToString());
        //ENCRIPTAR LOS DATOS DEL TICKET
        String informacion = FormsAuthentication.Encrypt(ticket);
        //CREAR LA COOKIE
        HttpCookie cookie =
            new HttpCookie(FormsAuthentication.FormsCookieName
                , informacion);
        //ALMACENAR LA COOKIE
        Response.Cookies.Add(cookie);
        //RECUPERAR LA PAGINA DE DESTINO
        String destino =
            FormsAuthentication.GetRedirectUrl(this.txtUser.Text
            , true);
        //REDIRECCIONAMOS AL DESTINO
        Response.Redirect(destino);
    }
}