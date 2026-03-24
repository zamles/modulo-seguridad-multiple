using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sistema.entidad;
using Sistema.bll;
using eSistema = Sistema.entidad.Sistema;
using eMenu = Sistema.entidad.Menu;

public partial class Transaccion_wfMenu : System.Web.UI.Page
{
    MenuRepository _repository = new MenuRepository();
    public eMenu ObjetoMenu
    {
        get
        {
            if (Session["ObjetoMenu"] == null)
            {
                Session["ObjetoMenu"] = new eMenu();
            }
            return (eMenu)Session["ObjetoMenu"];
        }
        set
        {
            Session["ObjetoMenu"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;
        General.PermisosDeItem(Page, "Menus", Guid.Parse(Session["IdRol"].ToString()));
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
        var menus = new MenuRepository().GetAll().Where(w=>w.IdSistema == guid).ToList();

        gvLista.DataSource = menus;
        gvLista.DataBind();
        General.PermisosDeItem(Page, "Menus", Guid.Parse(Session["IdRol"].ToString()));
    }

    private void Limpiar()
    {
        txtFuente.Text = txtNombre.Text =txtPeso.Text = string.Empty;
        Session.Remove("ObjetoMenu");
    }
    private void CargarDatos(Guid id)
    {
        ObjetoMenu = _repository.GetById(id);
        txtFuente.Text = ObjetoMenu.Fuente;
        txtNombre.Text = ObjetoMenu.Nombre;
        txtPeso.Text = ObjetoMenu.Peso.ToString();
    }

    private void GuardarObjeto()
    {
        if (ObjetoMenu.IdMenu == Guid.Empty)
        {
            var item = new eMenu()
            {
                IdMenu = Guid.NewGuid(),
                IdSistema = Guid.Parse(ddlSistema.SelectedValue),
                EsActivo = true,
                Nombre = txtNombre.Text,
                Fuente = txtFuente.Text,
                Peso = int.Parse(txtPeso.Text),
                FechaRegistro= DateTime.Now                
            };

            _repository.Insert(item);
            _repository.Commit();
        }
        else
        {
            ObjetoMenu.Nombre = txtNombre.Text;
            ObjetoMenu.Fuente = txtFuente.Text;
            ObjetoMenu.Peso = int.Parse(txtPeso.Text);

            _repository.Update(ObjetoMenu);
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
        if(ddlSistema.Enabled == true)
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

    protected void lnkItem_Click(object sender, EventArgs e)
    {
        var id = Guid.Parse(((LinkButton)sender).CommandArgument);
        var idsis = Guid.Parse(ddlSistema.SelectedValue);
        Response.Redirect("~/Transaccion/wfItemMenu.aspx?sis="+idsis+"&men="+id);
    }
}