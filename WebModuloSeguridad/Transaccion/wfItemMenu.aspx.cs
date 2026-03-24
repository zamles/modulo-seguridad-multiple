using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sistema.entidad;
using Sistema.bll;

public partial class Transaccion_wfItemMenu : System.Web.UI.Page
{
    ItemMenuRepository _repository = new ItemMenuRepository();
    public ItemMenu ObjetoItemMenu
    {
        get
        {
            if (Session["ObjetoItemMenu"] == null)
            {
                Session["ObjetoItemMenu"] = new ItemMenu();
            }
            return (ItemMenu)Session["ObjetoItemMenu"];
        }
        set
        {
            Session["ObjetoItemMenu"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;
        General.PermisosDeItem(Page, "ItemMenu", Guid.Parse(Session["IdRol"].ToString()));
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
        if(sistema != null)
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
            CargarItemMenus();
        }

    }

    private void CargarItemMenus()
    {
        var id = Guid.Parse(ddlMenu.SelectedValue);
        var item = new ItemMenuRepository().GetAll().Where(w => w.IdMenu == id).ToList();

        gvLista.DataSource = item;
        gvLista.DataBind();
        
        General.PermisosDeItem(Page, "ItemMenu", Guid.Parse(Session["IdRol"].ToString()));
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
            ddlMenu.DataBind();

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
            CargarItemMenus();
        }
        else
        {
            btnMenu.Text = "Seleccionar";
            ddlMenu.Enabled = true;
            Limpiar();        

            gvLista.DataSource = null;
            gvLista.DataBind();
        }
    }

    private void Limpiar()
    {
        txtDetalle.Text = txtNombre.Text = txtPeso.Text = txtEnlace.Text = string.Empty;
        Session.Remove("ObjetoItemMenu");
    }

    protected void btnSistema_Click(object sender, EventArgs e)
    {
        SeleccionSistema();
    }

    protected void btnMenu_Click(object sender, EventArgs e)
    {
        SeleccionMenu();
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        if (ddlMenu.Enabled == false)
        {
            GuardarObjeto();
            Limpiar();
            CargarItemMenus();
        }
        else
        {
            General.MensajeAdvertencia(this, "Se debe seleecionar primero a que menu pertenese");
        }
    }

    protected void lnkEvento_Click(object sender, EventArgs e)
    {
        var id = Guid.Parse(((LinkButton)sender).CommandArgument);
        CargarDatos(id);
    }
    
    private void CargarDatos(Guid id)
    {
        ObjetoItemMenu = _repository.GetById(id);
        txtDetalle.Text = ObjetoItemMenu.Detalle;
        txtNombre.Text = ObjetoItemMenu.Nombre;
        txtPeso.Text = ObjetoItemMenu.Peso.ToString();
        txtEnlace.Text = ObjetoItemMenu.Enlace;
    }

    private void GuardarObjeto()
    {
        if (ObjetoItemMenu.IdItemMenu == Guid.Empty)
        {
            var item = new ItemMenu()
            {
                IdItemMenu = Guid.NewGuid(),
                IdMenu = Guid.Parse(ddlMenu.SelectedValue),
                EsActivo = true,
                Nombre = txtNombre.Text,
                Detalle = txtDetalle.Text,
                Peso = int.Parse(txtPeso.Text),
                FechaRegistro = DateTime.Now,
                Enlace = txtEnlace.Text,                                
            };
            try
            {
                _repository.Insert(item);
                _repository.Commit();
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }
        else
        {
            ObjetoItemMenu.Nombre = txtNombre.Text;
            ObjetoItemMenu.Detalle = txtDetalle.Text;
            ObjetoItemMenu.Peso = int.Parse(txtPeso.Text);
            ObjetoItemMenu.Enlace = txtEnlace.Text;

            _repository.Update(ObjetoItemMenu);
            _repository.Commit();

        }
    }


    protected void lnkPermiso_Click(object sender, EventArgs e)
    {
        var id = Guid.Parse(((LinkButton)sender).CommandArgument);
        var idsis = Guid.Parse(ddlSistema.SelectedValue);
        var idmen =Guid.Parse(ddlMenu.SelectedValue);
        Response.Redirect("~/Transaccion/wfPermiso.aspx?sis=" + idsis + "&men=" + idmen+"&itmen="+id);
    }
}