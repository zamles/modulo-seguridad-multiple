using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sistema.entidad;
using Sistema.bll;

public partial class Transaccion_wfRolSistema : System.Web.UI.Page
{
    RolSistemaRepository _repository = new RolSistemaRepository();
    public RolSistema ObjetoRolSistema
    {
        get
        {
            if (Session["ObjetoRolSistema"] == null)
            {
                Session["ObjetoRolSistema"] = new RolSistema();
            }
            return (RolSistema)Session["ObjetoRolSistema"];
        }
        set
        {
            Session["ObjetoRolSistema"] = value;
        }
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;
        General.PermisosDeItem(Page, "Roles", Guid.Parse(Session["IdRol"].ToString()));
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
    }

    private void CargarMenus(Guid guid)
    {
        var menus = _repository.GetAll().Where(w => w.IdSistema == guid).ToList();

        gvLista.DataSource = menus;
        gvLista.DataBind();
        General.PermisosDeItem(Page, "Roles", Guid.Parse(Session["IdRol"].ToString()));
    }

    private void Limpiar()
    {
        txtdescripcion.Text = txtNombre.Text = string.Empty;
        Session.Remove("ObjetoRolSistema");
    }
    private void CargarDatos(Guid id)
    {
        ObjetoRolSistema = _repository.GetById(id);
        txtdescripcion.Text = ObjetoRolSistema.Descripcion;
        txtNombre.Text = ObjetoRolSistema.NombreRol;
        
    }

    private void GuardarObjeto()
    {
        if (ObjetoRolSistema.IdRolSistema == Guid.Empty)
        {
            var item = new RolSistema()
            {
                IdRolSistema = Guid.NewGuid(),
                IdSistema = Guid.Parse(ddlSistema.SelectedValue),
                EsActivo = true,
                NombreRol = txtNombre.Text,
                Descripcion = txtdescripcion.Text,                                
            };

            _repository.Insert(item);
            _repository.Commit();
        }
        else
        {
            ObjetoRolSistema.NombreRol = txtNombre.Text;
            ObjetoRolSistema.Descripcion = txtdescripcion.Text;
            

            _repository.Update(ObjetoRolSistema);
            _repository.Commit();

        }
    }


    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        if (ddlSistema.Enabled == false)
        {
            GuardarObjeto();
            Limpiar();
            CargarMenus(Guid.Parse(ddlSistema.SelectedValue));
        }
        else
        {
            General.MensajeAdvertencia(this, "Se debe seleecionar primero a que sistema pertenese");
        }
    }

    protected void lnkEvento_Click(object sender, EventArgs e)
    {
        var id = Guid.Parse(((LinkButton)sender).CommandArgument);
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
            CargarMenus(Guid.Parse(ddlSistema.SelectedValue));
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

    protected void lnkUsuario_Click(object sender, EventArgs e)
    {
        var id = Guid.Parse(((LinkButton)sender).CommandArgument);
        var idsis = Guid.Parse(ddlSistema.SelectedValue);
       // Response.Redirect("~/Transaccion/wfItemMenu.aspx?sis=" + idsis + "&men=" + id);
    }

}