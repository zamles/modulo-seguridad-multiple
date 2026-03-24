using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sistema.entidad;
using Sistema.bll;

public partial class Registro_wfVerificador : System.Web.UI.Page
{
    UsuarioRepository _usuarioRepository = new UsuarioRepository();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;
        var codigo = Request.QueryString["code"];

        ValidacionCodigo(codigo);


    }

    private void ValidacionCodigo(string codigo)
    {
        if (codigo == null || codigo == string.Empty)
        {
            VerificacionCorrecta(false);
        }
        else
        {
            var codi = int.Parse(codigo);
            var usuario = _usuarioRepository.GetByCodigo(codi);

            if(usuario == null)
            {
                VerificacionCorrecta(false);
            }
            else if (ActualizarUsuario(usuario))
            {
                VerificacionCorrecta(true);
            }
            else
            {
                VerificacionCorrecta(false);
            }            
        }

    }

    private void VerificacionCorrecta(bool x)
    {
        if (x)
        {
            pVerificaion.InnerText = "Verificación de usuario Exitosa";
            pVerificaion.Attributes.Add("style", "color:green;");
            btnAcceso.Visible = btnAcceso2.Visible = true;

        }
        else
        {
            pVerificaion.InnerText = "Verificación de usuario fallida";
            pVerificaion.Attributes.Add("style", "color:red;");
            btnAcceso.Visible = btnAcceso2.Visible = false;
        }
    }

    private bool ActualizarUsuario(Usuario usuario)
    {
        try
        {
            usuario.EsActivo = true;
            _usuarioRepository.Update(usuario);
            _usuarioRepository.Commit();

        }
        catch (Exception)
        {
            return false;            
        }
        return true;
    }
}