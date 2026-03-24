<%@ Page Title="" Language="C#" MasterPageFile="~/Plantilla.master" AutoEventWireup="true" CodeFile="wfSistema.aspx.cs" Inherits="Transaccion_wfSistema" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contenido" Runat="Server">

    <section class="content-header">
      <h1>
        Sistemas
        <small>Sistema en linea</small>
      </h1>
      <ol class="breadcrumb">
        <%--<li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
        <li><a href="#">Forms</a></li>
        <li class="active">General Elements</li>--%>
      </ol>
    </section>

    <section class="content">
        <div class="row">
            <div class="col-md-6">
                <div class="box box-primary">
                    <div class="box-header with-border">
                        <h3 class="box-title">Agregar Sistema</h3>
                    </div>
                    <!-- /.box-header -->
                    <!-- form start -->
                    <div class="box-body">
                        <div class="form-group">
                            <label ">Sistema </label>
                            <asp:TextBox ID="txtModulo" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>                        
                        <div class="form-group">
                            <label ">Descripcion de Sistema </label>
                            <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
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
                        <h1 class="box-title">Sistemas</h1>
                    </div>
                    <div class="box-body">
                        <div class="form-group">
                            <asp:GridView ID="gvLista" CssClass="dynamic-table table table-striped table-bordered table-hover col-xs-12 col-sm-12 col-lg-12" Width="100%" runat="server" AutoGenerateColumns="False">
                                <Columns>
                                    <asp:BoundField DataField="Nombre" HeaderText="Sistema" />                                    
                                    <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" />                                    
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <div style="display: flex;">
                                                <%--<asp:DropDownList ID="ddlEventos" runat="server" Width="150px"></asp:DropDownList>--%>
                                                <asp:LinkButton ID="lnkEvento" runat="server" Text="<i class='fa fa-arrow-circle-right'></i> Actualizar" CssClass="btn btn-success btn-xs botones" ToolTip="Realizar Accion" CommandArgument='<%#Bind("IdSistema") %>' CommandName="Actualizar" OnClick="lnkEvento_Click" Enabled="false"/>
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

