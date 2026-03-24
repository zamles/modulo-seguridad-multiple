using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sistema.entidad;
using Sistema.bll;

public partial class Transaccion_wfPermiso : System.Web.UI.Page
{
    PermisoRepository _repository = new PermisoRepository();
    public Permiso ObjetoPermiso
    {
        get
        {
            if (Session["ObjetoPermiso"] == null)
            {
                Session["ObjetoPermiso"] = new Permiso();
            }
            return (Permiso)Session["ObjetoPermiso"];
        }
        set
        {
            Session["ObjetoPermiso"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;
        General.PermisosDeItem(Page, "Permisos", Guid.Parse(Session["IdRol"].ToString()));
        CargarSistema();
    }

    private void CargarSistema()
    {
        var lista = new SistemaRepository().GetAll().ToList();

        ddlSistema.DataSource = lista;
        ddlSistema.DataTextField = "Nombre";
        ddlSistema.DataValueField = "IdSistema";
        ddlSistema.DataBind();

        var sistema = Request.QueryString["sis"];
        if (sistema != null)
        {
            ddlSistema.SelectedValue = sistema.ToString();
            ddlSistema.Enabled = false;
            CargarMenu();
        }
    }

    private void CargarMenu()
    {
        var sys = Guid.Parse(ddlSistema.SelectedValue);
        var lista = new MenuRepository().GetAll().Where(w => w.IdSistema == sys).ToList();

        ddlMenu.DataSource = lista;
        ddlMenu.DataTextField = "Nombre";
        ddlMenu.DataValueField = "IdMenu";
        ddlMenu.DataBind();

        var men = Request.QueryString["men"];
        if (men != null)
        {
            ddlMenu.SelectedValue = men.ToString();
            ddlMenu.Enabled = false;
            CargarItemMenu();
        }

    }
    private void CargarItemMenu()
    {
        var sys = Guid.Parse(ddlMenu.SelectedValue);
        var lista = new ItemMenuRepository().GetAll().Where(w => w.IdMenu == sys).ToList();

        ddlItemMenu.DataSource = lista;
        ddlItemMenu.DataTextField = "Nombre";
        ddlItemMenu.DataValueField = "IdItemMenu";
        ddlItemMenu.DataBind();

        var itmen = Request.QueryString["itmen"];
        if (itmen != null)
        {
            ddlItemMenu.SelectedValue = itmen.ToString();
            ddlItemMenu.Enabled = false;
            CargarPermisos();
        }

    }


    private void CargarPermisos()
    {
        var id = Guid.Parse(ddlItemMenu.SelectedValue);
        var item = _repository.GetAll().Where(w => w.IdItemMenu == id).ToList();

        gvLista.DataSource = item;
        gvLista.DataBind();
        General.PermisosDeItem(Page, "Permisos", Guid.Parse(Session["IdRol"].ToString()));
    }

    private void SeleccionSistema()
    {
        if (ddlSistema.Enabled == true)
        {
            ddlSistema.Enabled = false;
            btnSistema.Text = "Deseleccionar";
            CargarMenu();
        }
        else
        {
            btnSistema.Text = "Seleccionar";
            ddlSistema.Enabled = true;
            Limpiar();

            ddlMenu.Items.Clear();
            btnMenu.Text = "Seleccionar";
            ddlMenu.Enabled = true;

            ddlItemMenu.Items.Clear();
            btnItemMenu.Text = "Seleccionar";
            ddlItemMenu.Enabled = true;

            gvLista.DataSource = null;
            gvLista.DataBind();
        }
    }
    private void SeleccionMenu()
    {
        if (ddlMenu.Enabled == true)
        {
            ddlMenu.Enabled = false;
            btnMenu.Text = "Deseleccionar";
            CargarItemMenu();
        }
        else
        {
            btnMenu.Text = "Seleccionar";
            ddlMenu.Enabled = true;
            Limpiar();

            ddlItemMenu.Items.Clear();
            btnItemMenu.Text = "Seleccionar";
            ddlItemMenu.Enabled = true;


            gvLista.DataSource = null;
            gvLista.DataBind();
        }
    }

    private void SeleccionItemMenu()
    {
        if (ddlItemMenu.Enabled == true)
        {
            ddlItemMenu.Enabled = false;
            btnItemMenu.Text = "Deseleccionar";
            CargarPermisos();
        }
        else
        {
            btnItemMenu.Text = "Seleccionar";
            ddlItemMenu.Enabled = true;
            Limpiar();           


            gvLista.DataSource = null;
            gvLista.DataBind();
        }
    }

    private void Limpiar()
    {
        txtNivel.Text = txtNombre.Text = string.Empty;
        Session.Remove("ObjetoPermiso");
    }

    protected void btnSistema_Click(object sender, EventArgs e)
    {
        SeleccionSistema();
    }

    protected void btnMenu_Click(object sender, EventArgs e)
    {
        SeleccionMenu();
    }
    protected void btnItemMenu_Click(object sender, EventArgs e)
    {
        SeleccionItemMenu();
    }
    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        if (ddlItemMenu.Enabled == false)
        {
            GuardarObjeto();
            Limpiar();
            CargarPermisos();
        }
        else
        {
            General.MensajeAdvertencia(this, "Se debe seleecionar primero a que ItemMenu pertenese");
        }
    }

    protected void lnkEvento_Click(object sender, EventArgs e)
    {
        var id = Guid.Parse(((LinkButton)sender).CommandArgument);
        CargarDatos(id);
    }

    private void CargarDatos(Guid id)
    {
        ObjetoPermiso = _repository.GetById(id);
        txtNivel.Text = ObjetoPermiso.Nivel;
        txtNombre.Text = ObjetoPermiso.Nombre;        
    }

    private void GuardarObjeto()
    {
        if (ObjetoPermiso.IdItemMenu == Guid.Empty)
        {
            var item = new Permiso()
            {
                IdPermiso = Guid.NewGuid(),
                IdItemMenu = Guid.Parse(ddlItemMenu.SelectedValue),
                EsActivo = true,
                Nombre = txtNombre.Text,
                FechaRegistro = DateTime.Now,
                Nivel = txtNivel.Text               
                
            };

            _repository.Insert(item);
            _repository.Commit();
        }
        else
        {
            ObjetoPermiso.Nombre = txtNombre.Text;
            ObjetoPermiso.Nivel = txtNivel.Text;
            

            _repository.Update(ObjetoPermiso);
            _repository.Commit();

        }
    }



    protected void lnkPermiso_Click(object sender, EventArgs e)
    {
        var idsis = Guid.Parse(ddlSistema.SelectedValue);
        var idmen = Guid.Parse(ddlMenu.SelectedValue);
        var iditmen = Guid.Parse(ddlItemMenu.SelectedValue);
        Response.Redirect("~/Transaccion/wfPermisoRolSistema.aspx?sis=" + idsis + "&men=" + idmen + "&itmen=" + iditmen);
    }
}