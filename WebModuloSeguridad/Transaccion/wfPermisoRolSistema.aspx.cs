using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sistema.entidad;
using Sistema.bll;

public partial class Transaccion_wfPermisoRolSistema : System.Web.UI.Page
{
    PemisoRolSistemaRepository _repository = new PemisoRolSistemaRepository();
    public PemisoRolSistema ObjetoPemisoRolSistema
    {
        get
        {
            if (Session["ObjetoPemisoRolSistema"] == null)
            {
                Session["ObjetoPemisoRolSistema"] = new PemisoRolSistema();
            }
            return (PemisoRolSistema)Session["ObjetoPemisoRolSistema"];
        }
        set
        {
            Session["ObjetoPemisoRolSistema"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;
        General.PermisosDeItem(Page, "Permiso por Rol", Guid.Parse(Session["IdRol"].ToString()));
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
            CargarRoles();
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
            CargarPermisoItem();
            CargarPermisos();            
        }

    }
    private void CargarRoles()
    {
        var idSistema = Guid.Parse(ddlSistema.SelectedValue);

        var roles = new RolSistemaRepository().GetAll().Where(w => w.IdSistema == idSistema).ToList();
        ddlRol.DataSource = roles;
        ddlRol.DataTextField = "NombreRol";
        ddlRol.DataValueField = "IdRolSistema";
        ddlRol.DataBind();
    }
    private void CargarPermisoItem()
    {
        var idItem = Guid.Parse(ddlItemMenu.SelectedValue);
        var permisos = new PermisoRepository().GetAll().Where(w => w.IdItemMenu == idItem).ToList();

        rptCheck.DataSource = permisos;
        rptCheck.DataBind();
        //foreach (var item in permisos)
        //{
        //    var itemAdd = new CheckBox() { Text = item.Nombre};
        //    itemAdd.ID = item.IdPermiso.ToString();
        //    itemAdd.CssClass = "col-md-12";
           
        //    divChk.Controls.Add(itemAdd);
        //}
        //divChk.DataBind();

    }

    private void CargarPermisos()
    {
        var id = Guid.Parse(ddlItemMenu.SelectedValue);        
        var item = _repository.GetAll().Where(w => w.Permiso.IdItemMenu == id).Distinct().ToList();

        var lista = from i in item
                    let p = new PermisoRepository().GetById(i.IdPermiso)
                    where p != null
                    let r = new RolSistemaRepository().GetById(i.IdRolSistema)
                    where r != null
                    select new
                    {
                        Rol = r.NombreRol,
                        Permiso = p.Nombre,
                        i.IdPemisoRolSistema
                    };


        rptLista.DataSource = lista;
        rptLista.DataBind();
        //gvLista.DataSource = lista;
        //gvLista.DataBind();

    }

    private void SeleccionSistema()
    {
        if (ddlSistema.Enabled == true)
        {
            ddlSistema.Enabled = false;
            btnSistema.Text = "Deseleccionar";
            CargarRoles();
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

            //gvLista.DataSource = null;
            //gvLista.DataBind();
            rptCheck.DataSource = null;
            rptCheck.DataBind();

            rptLista.DataSource = null;
            rptLista.DataBind();
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


            //gvLista.DataSource = null;
            //gvLista.DataBind();
            rptCheck.DataSource = null;
            rptCheck.DataBind();

            rptLista.DataSource = null;
            rptLista.DataBind();
        }
    }

    private void SeleccionItemMenu()
    {
        if (ddlItemMenu.Enabled == true)
        {
            ddlItemMenu.Enabled = false;
            btnItemMenu.Text = "Deseleccionar";
            CargarPermisoItem();
            CargarPermisos();
        }
        else
        {
            btnItemMenu.Text = "Seleccionar";
            ddlItemMenu.Enabled = true;
            Limpiar();


            rptCheck.DataSource = null;
            rptCheck.DataBind();

            rptLista.DataSource = null;
            rptLista.DataBind();
        }
    }

    private void Limpiar()
    {        
        Session.Remove("ObjetoPemisoRolSistema");
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
            CargarPermisoItem();
        }
        else
        {
            General.MensajeAdvertencia(this, "Se debe seleecionar primero a que ItemMenu pertenese");
        }
    }

    protected void lnkEvento_Click(object sender, EventArgs e)
    {
        var id = Guid.Parse(((LinkButton)sender).CommandArgument);
        EliminarPermiso(id);

    }

    private void EliminarPermiso(Guid id)
    {
        ObjetoPemisoRolSistema = _repository.GetById(id);

        _repository.Delete(ObjetoPemisoRolSistema);
        _repository.Commit();

        Limpiar();
        CargarPermisos();

        General.MensajeExito(this, "Se elimino permiso del rol");

    }

    private void GuardarObjeto()
    {
        var id = Guid.Parse(ddlItemMenu.SelectedValue);
        var perRol = _repository.GetAll().Where(w => w.Permiso.IdItemMenu == id).Distinct().ToList();

        foreach (RepeaterItem i in rptCheck.Items)
        {
            var control = i.FindControl("chk");
            if (control != null)
            {
                var itemChk = ((CheckBox)control);
                if (itemChk.Checked)
                {
                    var hf = i.FindControl("hfValor");
                    var hfValor = (HiddenField)hf;                    

                    var item = new PemisoRolSistema()
                    {
                        IdPemisoRolSistema = Guid.NewGuid(),
                        IdRolSistema = Guid.Parse(ddlRol.SelectedValue),
                        IdPermiso = Guid.Parse(hfValor.Value),
                        EsActivo = true,
                        FechaRegistro = DateTime.Now
                    };


                    _repository.Insert(item);
                    _repository.Commit();
                }

            }
        }

    }
}