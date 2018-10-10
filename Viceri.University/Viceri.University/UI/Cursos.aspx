<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cursos.aspx.cs" Inherits="Viceri.University.UI.Cursos" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Cursos</title>
    <link href="../Content/bootstrap.min.css" rel="stylesheet" />
    <meta charset="utf-8" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <a href="Alunos.aspx">Ir para Alunos</a>

            <hr />
            <div>
                <h1>Cursos</h1>

                Filtrar por nome
                <div class="form-inline">
                    <asp:TextBox CssClass="form-control" ID="txtFiltro" runat="server"></asp:TextBox>
                    <asp:Button ID="btnFiltro" runat="server" Text="Filtrar" OnClick="btnFiltro_Click" CssClass="btn btn-primary" />
                </div>
            </div>
            <br />
            <div>
                <asp:GridView ID="gvCursos" runat="server" OnRowCommand="gvCursos_RowCommand" CellPadding="4" ForeColor="#333333" GridLines="None">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:Button ID="btnExcluir" runat="server" CausesValidation="false" CommandArgument='<% #Eval("ID") %>' CommandName="cmdExcluir" OnClientClick="return confirm('Deseja realmente excluir?')" Text="Excluir" CssClass="btn btn-danger" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:Button ID="btnAtualizar" runat="server" CausesValidation="false" CommandArgument='<% #Eval("ID") %>' CommandName="cmdAtualizar" Text="Atualizar" CssClass="btn btn-warning" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EditRowStyle BackColor="#2461BF" />
                    <EmptyDataTemplate>
                        Vazio
                    </EmptyDataTemplate>
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EFF3FB" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                </asp:GridView>
            </div>
            <br />
            <div>
                <div class="form-group">
                    Nome do Curso<asp:Label ID="lblCurso" runat="server" Visible="False"></asp:Label><br />
                    <asp:TextBox CssClass="form-control" ID="txtNome" runat="server" ValidationGroup="AddCurso"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvNomeCurso" runat="server" ControlToValidate="txtNome" ErrorMessage="Campo obrigatório" ValidationGroup="AddCurso">*</asp:RequiredFieldValidator>
                </div>
                <div class="form-group">
                    Descrição<br />
                    <asp:TextBox CssClass="form-control" ID="txtDescricao" runat="server" ValidationGroup="AddCurso"></asp:TextBox>
                </div>
                <div class="form-group">
                    Data de Início<br />
                    <asp:TextBox CssClass="form-control" ID="txtDataInicio" runat="server" ValidationGroup="AddCurso"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvDataInicio" runat="server" ControlToValidate="txtDataInicio" ErrorMessage="Campo obrigatório" ValidationGroup="AddCurso">*</asp:RequiredFieldValidator>
                </div>
                <div class="form-group">
                    Data de Término<br />
                    <asp:TextBox CssClass="form-control" ID="txtDataFim" runat="server" ValidationGroup="AddCurso"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvDataFim" runat="server" ControlToValidate="txtDataFim" ErrorMessage="Campo obrigatório" ValidationGroup="AddCurso">*</asp:RequiredFieldValidator>
                </div>
                <asp:Button CssClass="btn btn-success" ID="btnAdicionar" runat="server" Text="Adicionar" OnClick="btnAdicionar_Click" ValidationGroup="AddCurso" />
                <asp:Button CssClass="btn btn-success" ID="btnEditar" runat="server" OnClick="btnEditar_Click" Text="Editar" Visible="False" />
                <asp:Button CssClass="btn btn-primary" ID="btnCancelar" runat="server" OnClick="btnCancelar_Click" Text="Cancelar" Visible="False" />
            </div>
        </div>
    </form>
</body>
</html>
