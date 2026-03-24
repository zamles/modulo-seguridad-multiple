using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Sistema.bll;
using Sistema.entidad;


public partial class Registro_wfRegistro : System.Web.UI.Page
{
    #region Propiedades y librerias

    private static string codigoActivacion = string.Empty;
    public string CodigoActivacion { get { return codigoActivacion; } set { codigoActivacion = value; } }

    PersonaRepository _personaRepository = new PersonaRepository();
    UsuarioRepository _usuarioRepository = new UsuarioRepository();
    RolSistemaRepository _rolSistemaRepository = new RolSistemaRepository();
    ParametrosRepository _parametrosRepository = new ParametrosRepository();

    #endregion

    #region Eventos

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;
        CargarDepartamento();

    }
    protected void btnCrearCuenta_Click(object sender, EventArgs e)
    {
        if (ValidarTodo())
        {
            if (GuardarDatos())            
            {
                General.MensajeExito(this, "Se guardo con éxito, su registro se mandara un correo de confirmación"); 
                General.EnviarCorreoRegistro(txtEmail.Value,CodigoActivacion);
            }
            else
            { General.MensajeExito(this, "Ocurrio un error al guardar"); }
        }
        
    }
    protected void ddlDepartamento_SelectedIndexChanged(object sender, EventArgs e)
    {
        var id = Guid.Parse(ddlDepartamento.SelectedValue);
        CargarMunicipio(id);
    }
    #endregion

    #region Funciones y metodos

    private bool ValidarTodo()
    {
        bool x = true;

        if (ckTerminos.Checked == false) { spTerminos.InnerText = "Tiene que aceptar los terminos de uso."; x = false; }

        if (txtPrimerNombre.Text == string.Empty) { spPrimerNombre.InnerText = "Primer nombre es requerido."; x = false; }
        if (txtPrimerApellido.Text == string.Empty) { spPrimerApellido.InnerText = "Primer Apellido es requerido."; x = false; }
        if (txtIdentificacion.Text == string.Empty) { spIdentificacion.InnerText = "Numero de Identificacion es requerido."; x = false; }
        if (!FileDocumentoIdentificacion.HasFiles) { spDocumento.InnerText = "Carge su documento de identificacion es requerido."; x = false; }

        if (txtEmail.Value == string.Empty) { spEmail.InnerText = "Nombre de usuario es requerido."; x = false; }
        if (txtContrasenna.Value == string.Empty) { spContra.InnerText = "Contraseña es requerida."; x = false; }

        return x;

    }
   

    private bool GuardarDatos()
    {

        var codigoValidacion = int.Parse(DateTime.Now.ToString("ddhhmmss"));
        var contra = General.Encrypt(txtContrasenna.Value,General.PUBLIC_KEY,General.PUBLIC_PRIVATE);
        var razon = txtPrimerNombre.Text;
        razon = txtSegundoNombre.Text == string.Empty ? razon : razon + " " + txtSegundoNombre.Text;
        razon += " " + txtPrimerApellido.Text;
        razon = txtSegundoApellido.Text == string.Empty ? razon : razon + " " + txtSegundoApellido.Text;
        byte[] docu = null;
        if (FileDocumentoIdentificacion.HasFile)
            docu = FileDocumentoIdentificacion.FileBytes;

        var usuario = new Usuario()
        {
            IdUsuario = Guid.NewGuid(),
            NombreUsuario = txtEmail.Value,
            Contrasena = contra,
            Razonsocial = razon,
            Identificacion = txtIdentificacion.Text,
            Correo = txtEmail.Value,
            NoCarnet = codigoValidacion,
            EsActivo = false
        };
        CodigoActivacion = codigoValidacion.ToString();

        var persona = new Persona()
        {
            IdPersona = usuario.IdUsuario,
            PrimerNombre = txtPrimerApellido.Text,
            SegundoNombre = txtSegundoNombre.Text,
            PrimerApellido = txtPrimerApellido.Text,
            SegundoApellido = txtSegundoApellido.Text,
            Municipio = ddlMunicipio.SelectedItem.Text,
            Departamento = ddlDepartamento.SelectedItem.Text,
            Sexo = ddlSexo.SelectedItem.Text,
            TipoIdentificacion = ddlTipoIdentificación.SelectedItem.Text,
            NumeroIdentificacion = txtIdentificacion.Text,
            Telefono = txtTelefono.Text,
            Direccion = txtDireccion.Text,
            DocumentoIdentificacion = docu,
            RazonSocial = razon,
            CreadoPor = "Usuario",
            FechaRegistro = DateTime.Now,
            EsActivo = true
        };

        var sisRpi1 = _parametrosRepository.GetByCodigo(General.PARAMETRO_RPI1);
        var sisRpi2 = _parametrosRepository.GetByCodigo(General.PARAMETRO_RPI2);

        var rolSistema1 = _rolSistemaRepository.GetByIdSistemaNameRol(Guid.Parse(sisRpi1), "USUARIOPATENTE");
        var rolSistema2 = _rolSistemaRepository.GetByIdSistemaNameRol(Guid.Parse(sisRpi2), "USUARIOMARCA");

        var sistemaRol1 = new UsuarioSistema()
        {
            IdUsuarioSistema = Guid.NewGuid(),
            IdRolSistema = rolSistema1.IdRolSistema,
            IdUsuario = usuario.IdUsuario,
            EsActivo = true
        };
        var sistemaRol2 = new UsuarioSistema()
        {
            IdUsuarioSistema = Guid.NewGuid(),
            IdRolSistema = rolSistema2.IdRolSistema,
            IdUsuario = usuario.IdUsuario,
            EsActivo = true
        };


        using (Sistema.dao.SistemaModuloEntities entities = new Sistema.dao.SistemaModuloEntities())
        {
            using (var trans = entities.Database.BeginTransaction())
            {
                try
                {
                    entities.Usuario.Add(usuario);
                    entities.Persona.Add(persona);
                    entities.UsuarioSistema.Add(sistemaRol1);
                    entities.UsuarioSistema.Add(sistemaRol2);
                    entities.SaveChanges();

                    trans.Commit();

                }
                catch (Exception)
                {
                    trans.Rollback();
                    return false;
                }
            }
        }

        return true;
    }


    private void CargarDepartamento()
    {
        var lista = new DepartamentoRepository().GetAll().ToList();
        ddlDepartamento.DataSource = lista;
        ddlDepartamento.DataValueField = "IdDepartamento";
        ddlDepartamento.DataTextField = "Nombre";
        ddlDepartamento.DataBind();

        var id = Guid.Parse(ddlDepartamento.SelectedValue);
        CargarMunicipio(id);
    }

    private void CargarMunicipio(Guid id)
    {
        var lista = new MunicipioRepository().GetByDepartamento(id);
        ddlMunicipio.DataSource = lista;
        ddlMunicipio.DataValueField = "IdMunicipio";
        ddlMunicipio.DataTextField = "Nombre";
        ddlMunicipio.DataBind();
    }

    #endregion





    
}