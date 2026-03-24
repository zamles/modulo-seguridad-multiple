using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sistema.entidad;
using Sistema.bll;
using System.Web.Security;

public partial class Plantilla : System.Web.UI.MasterPage
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;
        if (Session["IdRol"] == null) Response.Redirect("~/Login.aspx");
        LabelNombreUsuario.Text = Session["NombreUsuario"].ToString();        

        CargarMenu();

    }
    private void CargarMenu()
    {
        var listadoMenu = new ItemMenuRepository().GetBySysRol(Guid.Parse(Session["IdRol"].ToString()));

        RptrMenu.DataSource = listadoMenu;
        RptrMenu.DataBind();
    }

    protected void RptrMenu_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            RepeaterItem item = e.Item;

            var IdMenu = Guid.Parse(Convert.ToString(DataBinder.Eval(item.DataItem, "IdMenu")));

            Repeater RptrMenuItem = (Repeater)e.Item.FindControl("RptrMenuItem");

            var listado = new ItemMenuRepository().GetByRol(Guid.Parse(Session["IdRol"].ToString()), IdMenu).OrderBy(o => o.Peso);

            RptrMenuItem.DataSource = listado;
            RptrMenuItem.DataBind();

        }
    }

    protected void btnSalir_Click(object sender, EventArgs e)
    {
        //Session.Remove("iniciarSesion");
        //Session.Remove("IdUsuario");
        //Session.Remove("IdEmpleado");
        //Session.Remove("Perfil");
        Session.Remove("NombreUsuario");
        Session.Remove("IdRol");

        FormsAuthentication.SignOut();        
        Response.Redirect("~/Login.aspx");
     
    }

    
}
