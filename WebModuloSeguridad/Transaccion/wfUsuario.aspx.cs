using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sistema.entidad;
using Sistema.bll;

public partial class Transaccion_wfUsuario : System.Web.UI.Page
{
    UsuarioRepository _repository = new UsuarioRepository();
    public Usuario ObjetoUsuario
    {
        get
        {
            if (Session["ObjetoUsuario"] == null)
            {
                Session["ObjetoUsuario"] = new Usuario();
            }
            return (Usuario)Session["ObjetoUsuario"];
        }
        set
        {
            Session["ObjetoUsuario"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;
        General.PermisosDeItem(Page, "Usuarios", Guid.Parse(Session["IdRol"].ToString()));
        CargarUsuarios();
    }

    private void CargarUsuarios()
    {
        var lista = _repository.GetAll();

        gvLista.DataSource = lista.ToList();
        gvLista.DataBind();
        General.PermisosDeItem(Page, "Usuarios", Guid.Parse(Session["IdRol"].ToString()));
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        if (txtContrasena.Text == txtValidad.Text) 
        {
            GuardarObjeto();
            Limpiar();
            CargarUsuarios(); 
        }
        else
        {
            General.MensajeAdvertencia(this, "Las Contraseñas no Coinsiden, verifique");
        }
    }

    private void Limpiar()
    {
        txtUsuario.Text = txtNombre.Text = txtIdentificacion.Text = txtCorreo.Text = txtNoCarnet.Text = txtContrasena.Text = txtValidad.Text = string.Empty;
        Session.Remove("ObjetoUsuario");
    }

    private void GuardarObjeto()
    {
        if (ObjetoUsuario.IdUsuario == Guid.Empty)
        {
            var item = new Usuario()
            {
                IdUsuario = Guid.NewGuid(),
                EsActivo = true,
                NombreUsuario = txtUsuario.Text,
                Razonsocial = txtNombre.Text,
                Correo = txtCorreo.Text,
                Identificacion = txtIdentificacion.Text                
            };

            item.NoCarnet = int.Parse(txtNoCarnet.Text);
            item.Contrasena = General.Encrypt(txtContrasena.Text, "12345678", "87654321");

            _repository.Insert(item);
            _repository.Commit();
        }
        else
        {
            ObjetoUsuario.NombreUsuario = txtUsuario.Text;
            ObjetoUsuario.Razonsocial = txtNombre.Text;
            ObjetoUsuario.Identificacion = txtIdentificacion.Text;
            ObjetoUsuario.Correo = txtCorreo.Text;
            ObjetoUsuario.NoCarnet = int.Parse(txtNoCarnet.Text);
            if(!string.IsNullOrEmpty(txtContrasena.Text))
                ObjetoUsuario.Contrasena = General.Encrypt(txtContrasena.Text, "12345678", "87654321");

            _repository.Update(ObjetoUsuario);
            _repository.Commit();

        }
    }

    protected void lnkEvento_Click(object sender, EventArgs e)
    {
        var id = Guid.Parse(((LinkButton)sender).CommandArgument);
        CargarDatos(id);

    }

    private void CargarDatos(Guid id)
    {
        ObjetoUsuario = _repository.GetById(id);
        txtUsuario.Text = ObjetoUsuario.NombreUsuario;
        txtNombre.Text = ObjetoUsuario.Razonsocial;
        txtIdentificacion.Text = ObjetoUsuario.Identificacion;
        txtCorreo.Text = ObjetoUsuario.Correo;
        txtNoCarnet.Text = ObjetoUsuario.NoCarnet.ToString();        
    }
}