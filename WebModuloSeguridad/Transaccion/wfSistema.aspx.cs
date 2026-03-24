using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sistema.entidad;
using Sistema.bll;
using eSistema = Sistema.entidad.Sistema;

public partial class Transaccion_wfSistema : System.Web.UI.Page
{
    SistemaRepository _repository = new SistemaRepository();
    public eSistema ObjetoSistema
    {
        get
        {
            if (Session["ObjetoSistema"] == null)
            {
                Session["ObjetoSistema"] = new eSistema();
            }
            return (eSistema)Session["ObjetoSistema"];
        }
        set
        {
            Session["ObjetoSistema"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;
        General.PermisosDeItem(Page, "Sistema", Guid.Parse(Session["IdRol"].ToString()));
        CargarSitemas();
    }

    private void CargarSitemas()
    {
        var lista = _repository.GetAll();

        gvLista.DataSource = lista.ToList();
        gvLista.DataBind();
        General.PermisosDeItem(Page, "Sistema", Guid.Parse(Session["IdRol"].ToString()));
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        GuardarSistema();
        Limpiar();
        CargarSitemas();
    }

    private void Limpiar()
    {
        txtDescripcion.Text = txtModulo.Text = string.Empty;
        Session.Remove("ObjetoSistema");
    }

    private void GuardarSistema()
    {
        if (ObjetoSistema.IdSistema == Guid.Empty)
        {
            var item = new eSistema()
            {
                IdSistema = Guid.NewGuid(),
                EsActivo = true,
                Nombre = txtModulo.Text,
                Descripcion = txtDescripcion.Text
            };

            _repository.Insert(item);
            _repository.Commit();
        }
        else
        {
            ObjetoSistema.Nombre = txtModulo.Text;
            ObjetoSistema.Descripcion = txtDescripcion.Text;

            _repository.Update(ObjetoSistema);
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
        ObjetoSistema = _repository.GetById(id);
        txtModulo.Text = ObjetoSistema.Nombre;
        txtDescripcion.Text = ObjetoSistema.Descripcion;    
    }
}