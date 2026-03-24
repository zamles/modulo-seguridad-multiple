using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Sistema.entidad;
using Sistema.bll;
using System.Net.Mail;
using System.Net;

/// <summary>
/// Descripción breve de General
/// </summary>
public class General
{

    #region Constantes y librrerias

     const string PARAMETRO_HOST = "HOST";
     const string PARAMETRO_PORT = "PORT";
     const string PARAMETRO_CORREO_ORIGEN = "CORREO ORIGEN";
     const string PARAMETRO_CONTRASENA = "CONTRASEÑA";
     const string PARAMETRO_SSL = "SSL";
     
    public const string PARAMETRO_RPI1 = "RPI1";
    public const string PARAMETRO_RPI2 = "RPI2";

    public const string PUBLIC_KEY = "sistemas";
    public const string PUBLIC_PRIVATE = "sametsis";    


    static ParametrosRepository _parametrosRepository = new ParametrosRepository();

    #endregion


    public static void MensajeExito(Page page, string texto)
    {
        page.ClientScript.RegisterStartupScript(page.GetType(), "randomtext", "swal({" +

             "title: '" + texto + "'," +
             "type: 'success'," +
             "})", true);
    }

    public static void MensajeError(Page page, string texto)
    {
        page.ClientScript.RegisterStartupScript(page.GetType(), "randomtext", "swal({" +
            "type: 'error'," +
            "title: 'Oops...'," +
            "text: '" + texto + "'," +
             "})", true);
    }

    public static void MensajeAdvertencia(Page page, string texto)
    {
        page.ClientScript.RegisterStartupScript(page.GetType(), "randomtext", "swal({" +
            "type: 'warning'," +
            "title: 'Oops...'," +
            "text: '" + texto + "'," +
             "})", true);
    }


    public static string Encrypt(string textToEncrypt,string publickey, string secretkey)
    {
        try
        {
            //string textToEncrypt = "WaterWorld";
            string ToReturn = "";
            //string publickey = "12345678";
            //string secretkey = "87654321";
            byte[] secretkeyByte = { };
            secretkeyByte = System.Text.Encoding.UTF8.GetBytes(secretkey);
            byte[] publickeybyte = { };
            publickeybyte = System.Text.Encoding.UTF8.GetBytes(publickey);
            MemoryStream ms = null;
            CryptoStream cs = null;
            byte[] inputbyteArray = System.Text.Encoding.UTF8.GetBytes(textToEncrypt);
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                ms = new MemoryStream();
                cs = new CryptoStream(ms, des.CreateEncryptor(publickeybyte, secretkeyByte), CryptoStreamMode.Write);
                cs.Write(inputbyteArray, 0, inputbyteArray.Length);
                cs.FlushFinalBlock();
                ToReturn = Convert.ToBase64String(ms.ToArray());
            }
            return ToReturn;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex.InnerException);
        }
    }

    public static string Decrypt(string textToDecrypt, string publickey, string secretkey)
    {
        try
        {
            //string textToDecrypt = "6+PXxVWlBqcUnIdqsMyUHA==";
            string ToReturn = "";
            //string publickey = "12345678";
            //string secretkey = "87654321";
            byte[] privatekeyByte = { };
            privatekeyByte = System.Text.Encoding.UTF8.GetBytes(secretkey);
            byte[] publickeybyte = { };
            publickeybyte = System.Text.Encoding.UTF8.GetBytes(publickey);
            MemoryStream ms = null;
            CryptoStream cs = null;
            byte[] inputbyteArray = new byte[textToDecrypt.Replace(" ", "+").Length];
            inputbyteArray = Convert.FromBase64String(textToDecrypt.Replace(" ", "+"));
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                ms = new MemoryStream();
                cs = new CryptoStream(ms, des.CreateDecryptor(publickeybyte, privatekeyByte), CryptoStreamMode.Write);
                cs.Write(inputbyteArray, 0, inputbyteArray.Length);
                cs.FlushFinalBlock();
                Encoding encoding = Encoding.UTF8;
                ToReturn = encoding.GetString(ms.ToArray());
            }
            return ToReturn;
        }
        catch (Exception ae)
        {
            throw new Exception(ae.Message, ae.InnerException);
        }
    }


    public static void PermisosDeItem(Page page, string item, Guid idRol)
    {
        List<Permiso> lista = new List<Permiso>();
        var permiso = new PemisoRolSistemaRepository().GetByRolItem(idRol, item).ToList();
        foreach (var pi in permiso)
        {
            var p = new PermisoRepository().GetById(pi.IdPermiso);
            lista.Add(p);
        }
        AsignarPermiso(page, lista);
    }
    public static void AsignarPermiso(Control control,List<Permiso> permisos)
    {
        foreach (Control c in control.Controls)
        {
            AsignarPermiso(c,permisos);
            if (c is LinkButton)
                foreach (var item in permisos)
                {
                    if(((LinkButton)c).CommandName == item.Nombre)
                    {
                        ((LinkButton)c).Enabled = true;
                    }
                    
                }
            if (c is Button)
                foreach (var item in permisos)
                {
                    if (((Button)c).CommandName == item.Nombre)
                    {
                        ((Button)c).Enabled = true;
                    }
                    
                }

            
    
        }
    }


    public static void EnviarCorreoRegistro(string enviar_a, string activacion)
    {
        string host = HttpContext.Current.Request.Url.Host;
        string port = HttpContext.Current.Request.Url.Authority;

        //var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);
        var page = host + "/Registro/wfVerificador.aspx?code=" + activacion;

        var cuerpo = "Su registro al sistema de trámites del Registro de la Propiedad de Nicaragua a sido satisfactorio <br/>" +
             "por favor de click en el siguiente enlace para verificar su correo <br/>" +
            "<a href='" + page + "'>Validar correo electrónico</a>";


        NotificarRegistroExitoso(enviar_a, "NOTIFICACIÓN SISTEMA TRAMITES DE RPI", cuerpo);


    }

    private static void NotificarRegistroExitoso(string enviar_a, string asunto, string cuerpo)
    {
        

        var fromCorreo = new MailAddress(_parametrosRepository.GetByCodigo(PARAMETRO_CORREO_ORIGEN), "REGISTRO DE LA PROPIEDAD INTELECTUAL");
        var fromCorreoPassword = _parametrosRepository.GetByCodigo(PARAMETRO_CONTRASENA);

        var toCorreo = new MailAddress(enviar_a);

        string subject = asunto;

        string body = cuerpo;

        var smtp = new SmtpClient
        {
            Host = _parametrosRepository.GetByCodigo(PARAMETRO_HOST),
            Port = Convert.ToInt32(_parametrosRepository.GetByCodigo(PARAMETRO_PORT)),
            EnableSsl = _parametrosRepository.GetByCodigo(PARAMETRO_SSL) == "1",
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(fromCorreo.Address, fromCorreoPassword)
        };

        ServicePointManager.ServerCertificateValidationCallback =
            (s, certificate, chain, sslPolicyErrors) => true;


        var mensaje = new MailMessage(fromCorreo, toCorreo)
        {
            Subject = subject,
            Body = body,
            IsBodyHtml = true,
            Priority = MailPriority.High,

        };

        smtp.Send(mensaje);

    }




}