using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sistema.entidad;
using Sistema.bll;

public partial class Transaccion_wfUsuarioSistema : System.Web.UI.Page
{
    UsuarioSistemaRepository _repository = new UsuarioSistemaRepository();
    public UsuarioSistema ObjetoUsuarioSistema
    {
        get
        {
            if (Session["ObjetoUsuarioSistema"] == null)
            {
                Session["ObjetoUsuarioSistema"] = new UsuarioSistema();
            }
            return (UsuarioSistema)Session["ObjetoUsuarioSistema"];
        }
        set
        {
            Session["ObjetoUsuarioSistema"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;
        General.PermisosDeItem(Page, "Usuario Sistema", Guid.Parse(Session["IdRol"].ToString()));
        CargarSistemas();
    }

    private void CargarSistemas()
    {
        var lista = new SistemaRepository().GetAll().ToList();

        ddlSistema.DataSource = lista;
        ddlSistema.DataTextField = "Nombre";
        ddlSistema.DataValueField = "IdSistema";
        ddlSistema.DataBind();
        // CargarMenus(Guid.Parse(ddlSistema.SelectedValue));
        var usuarios = new UsuarioRepository().GetAll().ToList();

        ddlUsuario.DataSource = usuarios;
        ddlUsuario.DataTextField = "Razonsocial";
        ddlUsuario.DataValueField = "IdUsuario";
        ddlUsuario.DataBind();
        
    }

    private void CargarComboRol(Guid guid)
    {
        var rol = new RolSistemaRepository().GetAll().Where(w => w.IdSistema == guid).ToList();

        ddlRol.DataSource = rol;
        ddlRol.DataTextField = "NombreRol";
        ddlRol.DataValueField = "IdRolSistema";
        ddlRol.DataBind();
    }

    private void CargarUsuarioSistema(Guid guid)
    {
        var usuarioSistema = _repository.GetAll().Where(w => w.RolSistema.IdSistema == guid).ToList();
        var lista = from us in usuarioSistema
                    let u = new UsuarioRepository().GetById(us.IdUsuario)
                    where u != null
                    let r = new RolSistemaRepository().GetById(us.IdRolSistema)
                    where r != null
                    select new
                    {
                        Usuario = u.Razonsocial,
                        Rol = r.NombreRol,
                        us.IdUsuarioSistema
                    };


        gvLista.DataSource = lista;
        gvLista.DataBind();
        General.PermisosDeItem(Page, "Usuario Sistema", Guid.Parse(Session["IdRol"].ToString()));
    }

    private void Limpiar()
    {
        ddlRol.Items.Clear();
        ddlUsuario.Enabled = true;
        Session.Remove("ObjetoUsuarioSistema");
    }
    private void CargarDatos(Guid id)
    {
        ObjetoUsuarioSistema = _repository.GetById(id);
        ddlRol.SelectedValue = ObjetoUsuarioSistema.IdRolSistema.ToString();
        ddlUsuario.SelectedValue = ObjetoUsuarioSistema.IdUsuario.ToString();
        
    }

    private void GuardarObjeto()
    {
        if (ObjetoUsuarioSistema.IdUsuarioSistema == Guid.Empty)
        {
            var item = new UsuarioSistema()
            {
                IdUsuarioSistema= Guid.NewGuid(),
                IdRolSistema = Guid.Parse(ddlRol.SelectedValue),
                IdUsuario = Guid.Parse(ddlUsuario.SelectedValue),
                EsActivo = true               
            };

            _repository.Insert(item);
            _repository.Commit();
        }
        else
        {
            ObjetoUsuarioSistema.IdRolSistema = Guid.Parse(ddlRol.SelectedValue);
            ObjetoUsuarioSistema.IdUsuario = Guid.Parse(ddlUsuario.SelectedValue);

            _repository.Update(ObjetoUsuarioSistema);
            _repository.Commit();

        }
    }


    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        if (ddlSistema.Enabled == false)
        {
            GuardarObjeto();            
            ddlUsuario.Enabled = true;
            CargarUsuarioSistema(Guid.Parse(ddlSistema.SelectedValue));
        }
        else
        {
            General.MensajeAdvertencia(this, "Se debe seleecionar primero a que sistema pertenese");
        }
    }

    protected void lnkEvento_Click(object sender, EventArgs e)
    {
        var id = Guid.Parse(((LinkButton)sender).CommandArgument);
        ddlUsuario.Enabled = false;
        CargarDatos(id);
    }

    protected void btnSeleccion_Click(object sender, EventArgs e)
    {
        Seleccion();
    }
    private void Seleccion()
    {
        if (ddlSistema.Enabled == true)
        {
            ddlSistema.Enabled = false;
            btnSeleccion.Text = "Deseleccionar";
            CargarComboRol(Guid.Parse(ddlSistema.SelectedValue));
            CargarUsuarioSistema(Guid.Parse(ddlSistema.SelectedValue));
        }
        else
        {
            btnSeleccion.Text = "Seleccionar";
            ddlSistema.Enabled = true;
            Limpiar();
            gvLista.DataSource = null;
            gvLista.DataBind();
        }
    }



    protected void lnkEliminar_Click(object sender, EventArgs e)
    {
        var id = Guid.Parse(((LinkButton)sender).CommandArgument);
       if(EliminarUsuarioSistema(id))        
            General.MensajeExito(this, "Se Elimino Corectamente");
        else
            General.MensajeError(this, "Comuniquese con el administrador");

        CargarUsuarioSistema(Guid.Parse(ddlSistema.SelectedValue));
    }

    private bool EliminarUsuarioSistema(Guid id)
    {
        var us = _repository.GetById(id);
        try
        {
            _repository.Delete(us);
            _repository.Commit();

            return true;
        }
        catch (Exception ex)
        {
            return false;            
        }
    }
}