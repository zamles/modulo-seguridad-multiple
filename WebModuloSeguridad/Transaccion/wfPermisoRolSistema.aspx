<%@ Page Title="" Language="C#" MasterPageFile="~/Plantilla.master" AutoEventWireup="true" CodeFile="wfPermisoRolSistema.aspx.cs" Inherits="Transaccion_wfPermisoRolSistema" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contenido" Runat="Server">

     <section class="content-header">
      <h1>
       Permisos
        <small> se agrega los permisos a los roles </small>
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
                            <label ">Seleccione Rol </label>
                            <asp:DropDownList ID="ddlRol" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="clearfix"></div>
                        <div class="form-group">
                            <label ">seleccione Permisos </label>                            
                            <div  id="divChk" runat="server" >
                                <asp:Repeater runat="server" ID="rptCheck">
                                    <ItemTemplate>
                                        <asp:CheckBox id="chk" runat="server" CssClass="col-md-12" Text='<%#Bind("Nombre") %>' />
                                        <asp:HiddenField ID="hfValor" runat="server" Value='<%#Bind("IdPermiso") %>' />
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                            <%--<asp:CheckBoxList ID="chklPermiso" runat="server" CssClass="" TextAlign="Left" RepeatDirection="Vertical" CellPadding="10" CellSpacing="10">                               
                            </asp:CheckBoxList>--%>
                        </div>                                                          
                    </div>                    
                    <!-- /.box-body -->

                    <div class="box-footer">                        
                        <asp:Button id="btnGuardar" Text="Guardar" runat="server" CssClass="btn btn-primary" OnClick="btnGuardar_Click" CommandName="guardar" Enabled="false" />
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
                            <%--<asp:GridView ID="gvLista" CssClass="dynamic-table table table-striped table-bordered table-hover col-xs-12 col-sm-12 col-lg-12" Width="100%" runat="server" AutoGenerateColumns="False">
                                <Columns>                                    
                                    <asp:BoundField DataField="Rol " HeaderText="Rol" />                                                                        
                                    <asp:BoundField DataField="Permiso" HeaderText="Permiso" />                                    
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <div style="display: flex;">
                                                <%--<asp:DropDownList ID="ddlEventos" runat="server" Width="150px"></asp:DropDownList>--%>
                                               <%-- <asp:LinkButton ID="lnkEvento" runat="server" Text="<i class='fa fa-arrow-circle-right'></i> Eliminar" CssClass="btn btn-success btn-xs botones" ToolTip="Realizar Accion" CommandArgument='<%#Bind("IdRolPermiso") %>' CommandName="Actualizar" OnClick="lnkEvento_Click" />
                                                
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    No hay datos para mostrar
                                </EmptyDataTemplate>
                            </asp:GridView>--%>
                            <asp:Repeater runat="server" ID="rptLista">
                                <HeaderTemplate>
                                    <table id="tblLista" class="tblListadoSimple table table-striped table-bordered table-hover col-xs-12 col-sm-12 col-lg-12">
                                        <thead>
                                            <tr></tr>
                                            <tr>
                                                <th>Rol</th>
                                                <th>Permino</th>                                                
                                                <th></th>
                                            </tr>
                                        </thead>
                                </HeaderTemplate>
                                <ItemTemplate>

                                    <tr>
                                        <td><%# DataBinder.Eval(Container.DataItem, "Rol") %></td>
                                        <td><%# DataBinder.Eval(Container.DataItem, "Permiso") %></td>                                        
										 <td style="width: 5%;">
                                        <asp:LinkButton ID="lnkEvento" runat="server" Text="<i class='fa fa-arrow-circle-right'></i> Eliminar" CssClass="btn btn-success btn-xs botones" ToolTip="Realizar Accion" CommandArgument='<%#Bind("IdPemisoRolSistema") %>' CommandName="Actualizar" OnClick="lnkEvento_Click"/>
                                        </td>
                                    </tr>

                                </ItemTemplate>
                                <FooterTemplate>
                                    </table>
                                </FooterTemplate>
                            </asp:Repeater>




                        </div>
                    </div>
                </div>
            </div>


        </div>
    </section>



</asp:Content>

