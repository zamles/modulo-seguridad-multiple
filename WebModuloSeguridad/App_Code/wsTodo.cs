using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Sistema.entidad;
using Sistema.bll;

/// <summary>
/// Descripción breve de wsTodo
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
[System.Web.Script.Services.ScriptService]
public class wsTodo : System.Web.Services.WebService
{

    public wsTodo()
    {

        //Elimine la marca de comentario de la línea siguiente si utiliza los componentes diseñados 
        //InitializeComponent(); 
    }

    [WebMethod]
    public List<MunicipioSw> ObtenerMunicipios(string departamento)
    {
        var id = Guid.Parse(departamento);
        var listaM = new MunicipioRepository().GetByDepartamento(id);

        var lista = from l in listaM
                    select new MunicipioSw
                    {
                        Id = l.IdMunicipio,
                        Nombre = l.Nombre
                    };

        return lista.ToList();
    }

}
public class MunicipioSw
{
    public Guid Id { get; set; }
    public string Nombre { get; set; }
}
