<%@ Page Title="" Language="C#" MasterPageFile="~/Plantilla.master" AutoEventWireup="true" CodeFile="wfPermiso.aspx.cs" Inherits="Transaccion_wfPermiso" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contenido" Runat="Server">

      <section class="content-header">
      <h1>
       Permisos
        <small> se agrega los permisos de los Item Menu  </small>
      </h1>
      <ol class="breadcrumb">
        <%--<li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
        <li><a href="#">Forms</a></li>
        <li class="active">General Elements</li>--%>
      </ol>
    </section>
    <section class="content">
        <div class="row">
                <div class="col-md-12">
                <div class="box box-primary">
                    <div class="box-header with-border">
                        <h3 class="box-title">Selecione Item Menu </h3>                        
                    </div>
                    <div class="box-body">
                        <div class="form-group">
                            <div class="col-md-4">
                                <label>Sistema</label>
                                <div class="input-group input-group-sm">
                                    <asp:DropDownList ID="ddlSistema" runat="server" CssClass="form-control"></asp:DropDownList>
                                    <div class="input-group-btn">
                                        <asp:Button ID="btnSistema" Text="Seleccionar" runat="server" CssClass="btn btn-primary" OnClick="btnSistema_Click" CommandName="seleccion" Enabled="false"/>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <label>Menu</label>
                                <div class="input-group input-group-sm">
                                    <asp:DropDownList ID="ddlMenu" runat="server" CssClass="form-control"></asp:DropDownList>
                                    <div class="input-group-btn">
                                        <asp:Button ID="btnMenu" Text="Seleccionar" runat="server" CssClass="btn btn-primary" OnClick="btnMenu_Click" CommandName="seleccion" Enabled="false"/>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <label>Item Menu</label>
                                <div class="input-group input-group-sm">
                                    <asp:DropDownList ID="ddlItemMenu" runat="server" CssClass="form-control"></asp:DropDownList>
                                    <div class="input-group-btn">
                                        <asp:Button ID="btnItemMenu" Text="Seleccionar" runat="server" CssClass="btn btn-primary" OnClick="btnItemMenu_Click" CommandName="seleccion" Enabled="false"/>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

             <div class="col-md-6">
                <div class="box box-primary">
                    <div class="box-header with-border">
                        <h3 class="box-title">Agregar Permiso</h3>
                    </div>
                    <!-- /.box-header -->
                    <!-- form start -->
                    <div class="box-body">
                        <div class="form-group">
                            <label ">Nombre </label>
                            <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label ">Nivel </label>
                            <asp:TextBox ID="txtNivel" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>                                                          
                    </div>
                    <!-- /.box-body -->

                    <div class="box-footer">                        
                        <asp:Button id="btnGuardar" Text="Guardar" runat="server" CssClass="btn btn-primary" OnClick="btnGuardar_Click" CommandName="guardar" Enabled="false"/>
                    </div>
                </div>
            </div>

            <div class=" col-md-6">
                <div class="box box-primary"> 
                    <div class="box-header with-border">
                        <h1 class="box-title">ItemMenu de Menu del sistema</h1>
                    </div>
                    <div class="box-body">
                        <div class="form-group">
                            <asp:GridView ID="gvLista" CssClass="dynamic-table table table-striped table-bordered table-hover col-xs-12 col-sm-12 col-lg-12" Width="100%" runat="server" AutoGenerateColumns="False">
                                <Columns>                                    
                                    <asp:BoundField DataField="Nombre" HeaderText="Permiso" />                                                                        
                                    <asp:BoundField DataField="Nivel" HeaderText="Nivel" />                                    
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <div style="display: flex;">
                                                <%--<asp:DropDownList ID="ddlEventos" runat="server" Width="150px"></asp:DropDownList>--%>
                                                <asp:LinkButton ID="lnkEvento" runat="server" Text="<i class='fa fa-arrow-circle-right'></i> Actualizar" CssClass="btn btn-success btn-xs botones" ToolTip="Realizar Accion" CommandArgument='<%#Bind("IdPermiso") %>' CommandName="Actualizar" OnClick="lnkEvento_Click" Enabled="false" />
                                                <asp:LinkButton ID="lnkPermiso" runat="server" Text="<i class='fa fa-arrow-circle-right'></i> Agregar Permiso Rol" CssClass="btn btn-warning btn-xs botones" ToolTip="Realizar Accion" CommandArgument='<%#Bind("IdPermiso") %>' CommandName="Mover" OnClick="lnkPermiso_Click" Enabled="false"/>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    No hay datos para mostrar
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>


        </div>
    </section>



</asp:Content>

