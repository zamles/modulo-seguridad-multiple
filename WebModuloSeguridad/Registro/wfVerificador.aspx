<%@ Page Language="C#" AutoEventWireup="true" CodeFile="wfVerificador.aspx.cs" Inherits="Registro_wfVerificador" %>

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
                        <h1 class="login-box-msg" id="pVerificaion" runat="server">Verificacion de usuario</h1>
                       
                        <div class="row" style="padding: 5px 0 5px 0">
                            <div class="col-xs-4">
                                <a ID="btnAcceso" runat="server" class="btn btn-primary btn-block" href="http://demorpi.mific.gob.ni/">Acceder a Sistema de patente</a>
                            </div>
                            <div class="col-xs-4">
                            
                            </div>
                            <div class="col-xs-4">
                                <a ID="btnAcceso2" runat="server" class="btn btn-primary btn-block" href="http://demorpi.mific.gob.ni/">Acceder a Sistema de Marcas</a>
                            </div>
                        </div>
                    </div>                                        
                </div>
            </div>
        </div>


    </form>
    <script src="<%=ResolveClientUrl("../theme/bower_components/jquery/dist/jquery.min.js")%>"></script>
    <script src="<%=ResolveClientUrl("../theme/bower_components/bootstrap/dist/js/bootstrap.min.js")%>"></script>
    <script src="<%=ResolveClientUrl("../theme/plugins/iCheck/icheck.min.js")%>"></script>

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