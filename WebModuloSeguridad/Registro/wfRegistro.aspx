<%@ Page Language="C#" AutoEventWireup="true" CodeFile="wfRegistro.aspx.cs" Inherits="Registro_wfRegistro" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>

  <!-- Bootstrap 3.3.7 -->
    <link rel="stylesheet" href="<%=ResolveClientUrl("../theme/bower_components/bootstrap/dist/css/bootstrap.min.css")%>" />
    <link href="<%=ResolveClientUrl("../theme/bower_components/font-awesome/css/font-awesome.min.css")%>" rel="stylesheet" />
    <link href="<%=ResolveClientUrl("../theme/bower_components/Ionicons/css/ionicons.min.css")%>" rel="stylesheet" />
    <link href="<%=ResolveClientUrl("../theme/dist/css/AdminLTE.min.css")%>" rel="stylesheet" />   
    <link href="<%=ResolveClientUrl("../theme/plugins/iCheck/square/blue.css")%>" rel="stylesheet" />

 


    <style>
        .register-box {
            width: 60% !important;
            margin-top: 0 !important;
        }
    </style>

</head>

<body class="hold-transition register-page">
    <form id="form1" runat="server">

        <div class="register-box">
            <div class="register-logo">
                <img src="../theme/img/plantilla/logomific.png" />
            </div>
            <div class="register-box-body">

                <div class="card">
                    <div class="card-body register-card-body">
                        <p class="login-box-msg">Datos del usuario</p>

                        <div class="row" style="padding: 5px 0 5px 0">
                            <div class="col-xs-6">
                                <asp:TextBox ID="txtPrimerNombre" runat="server" CssClass="form-control" placeholder="Primer Nombre" autocomplete="off"></asp:TextBox>
                                <span style="color: red; font-size: smaller;" runat="server" id="spPrimerNombre"></span>
                            </div>
                            <div class="col-xs-6">
                                <asp:TextBox ID="txtSegundoNombre" runat="server" CssClass="form-control" autocomplete="off" placeholder="Segundo Nombre"></asp:TextBox>
                            </div>

                        </div>
                        <div class="row" style="padding: 5px 0 5px 0">
                            <div class="col-xs-6">
                                <asp:TextBox ID="txtPrimerApellido" runat="server" CssClass="form-control" autocomplete="off" placeholder="Primer Apellido"></asp:TextBox>
                                <span style="color: red; font-size: smaller;" runat="server" id="spPrimerApellido"></span>
                            </div>
                            <div class="col-xs-6">
                                <asp:TextBox ID="txtSegundoApellido" runat="server" CssClass="form-control" autocomplete="off" placeholder="Segundo Apellido"></asp:TextBox>
                            </div>
                        </div>


                        <div class="row" style="padding: 5px 0 5px 0">
                            <div class="col-xs-4">
                                <asp:DropDownList ID="ddlSexo" CssClass="form-control" runat="server">
                                    <asp:ListItem Value="F">Femenino</asp:ListItem>
                                    <asp:ListItem Value="M">Masculino</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-xs-4">
                                <asp:DropDownList ID="ddlDepartamento" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDepartamento_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                            <div class="col-xs-4">
                                <asp:DropDownList ID="ddlMunicipio" CssClass="form-control" runat="server">
                                </asp:DropDownList>
                            </div>
                        </div>


                        <div class="row" style="padding: 5px 0 5px 0">
                            <div class="col-xs-4">
                                <div class="form-group">
                                    <label>Tipo Identificación</label>
                                    <asp:DropDownList ID="ddlTipoIdentificación" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="Cédula de Identidad">Cédula de Identidad</asp:ListItem>
                                        <asp:ListItem Value="Cédula de Residencia">Cédula de Residencia</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-xs-4">
                                <div class="form-group">
                                    <label>Número de identificación</label>
                                    <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="form-control" autocomplete="off" placeholder="Número de identificación" MaxLength="14"></asp:TextBox>
                                    <span style="color: red; font-size: smaller;" runat="server" id="spIdentificacion"></span>
                                </div>
                            </div>
                            <div class="col-xs-4">
                                <div class="form-group">
                                    <label>Documento de identificación</label>
                                    <asp:FileUpload ID="FileDocumentoIdentificacion" runat="server" CssClass="form-control" />
                                    <span style="color: red; font-size: smaller;" runat="server" id="spDocumento"></span>
                                </div>

                            </div>
                        </div>

                        <div class="row" style="padding: 5px 0 5px 0">
                            <div class="col-xs-6">
                                <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" autocomplete="off" placeholder="Teléfono" MaxLength="8"></asp:TextBox>
                            </div>
                            <div class="col-xs-6">
                                <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control" autocomplete="off" placeholder="Dirección"></asp:TextBox>
                            </div>
                        </div>

                        <div class="row text-center" style="padding: 5px 0 5px 0">
                            <p>Credenciales de acceso al sistema</p>
                        </div>

                        <div class="row" style="padding: 5px 0 5px 0">
                            <div class="col-xs-12">
                                <div class="form-group has-feedback">
                                    <input type="email" id="txtEmail" runat="server" autocomplete="off" placeholder="Correo electrónico" class="form-control" />
                                    <span style="color: red; font-size: smaller;" runat="server" id="spEmail"></span>
                                    <span class="glyphicon glyphicon-envelope form-control-feedback"></span>
                                </div>
                            </div>
                            <div class="col-xs-12">
                                <div class="form-group has-feedback">
                                    <input type="password" id="txtContrasenna" runat="server" autocomplete="off" placeholder="Contraseña" class="form-control" onkeyup="checkPasswordStrength()" />
                                    <span style="color: red; font-size: smaller;" runat="server" id="spContra"></span>
                                    <span class="glyphicon glyphicon-lock form-control-feedback"></span>
                                </div>
                            </div>
                        </div>

                        <div class="row" style="padding: 5px 0 5px 0">
                            <div class="col-xs-8">
                                <asp:CheckBox ID="ckTerminos" runat="server" />
                                <a href="Terminosycondicionesdeuso.html" target="_blank">Acepto los términos y condiciones</a>
                                <span style="color: red; font-size: smaller;" runat="server" id="spTerminos"></span>
                            </div>

                            <div class="col-xs-4">
                                <asp:Button ID="btnCrearCuenta" runat="server" Text="Crear Cuenta" CssClass="btn btn-primary btn-block" OnClick="btnCrearCuenta_Click" />
                            </div>
                        </div>
                    </div>
                    <div class="social-auth-links text-center">
                    </div>
                    <a href="login.html" class="text-center">Ya tengo un registro</a>
                </div>
            </div>
        </div>


    </form>
    <script src="<%=ResolveClientUrl("../theme/bower_components/jquery/dist/jquery.min.js")%>"></script>
    <script src="<%=ResolveClientUrl("../theme/bower_components/bootstrap/dist/js/bootstrap.min.js")%>"></script>
    <script src="<%=ResolveClientUrl("../theme/plugins/iCheck/icheck.min.js")%>"></script>
    <script src="<%=ResolveClientUrl("../theme/plugins/sweetalert2/sweetalert2.all.js")%>"></script>

    <script>
        jQuery.noConflict();

        jQuery(document).ready(function () {
            jQuery('input').iCheck({
                checkboxClass: 'icheckbox_square-blue',
                radioClass: 'iradio_square-blue',
                increaseArea: '20%' /* optional */
            });



        });

    </script>

</body>
</html>
