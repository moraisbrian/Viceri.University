<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Alunos.aspx.cs" Inherits="Viceri.University.UI.Alunos" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Alunos</title>
    <link href="../Content/bootstrap.min.css" rel="stylesheet" />
    <meta charset="utf-8" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <a href="Cursos.aspx">Ir para Cursos</a>
            <hr />
            <h1>Alunos</h1>

            Filtrar por nome
            <div class="form-inline">
                <asp:TextBox ID="txtFiltro" runat="server" CssClass="form-control"></asp:TextBox><asp:Button ID="btnFiltrar" runat="server" Text="Filtrar" OnClick="btnFiltrar_Click" CssClass="btn btn-primary" />
            </div>

            <br />
            <div>
                <asp:GridView ID="gvAluno" runat="server" CellPadding="4" ForeColor="#333333" OnRowCommand="gvAluno_RowCommand" GridLines="None">
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
                    Nome do Aluno<br />
                    <asp:TextBox ID="txtNome" runat="server" ValidationGroup="AddAluno" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvNome" runat="server" ControlToValidate="txtNome" ErrorMessage="Campo obrigatório" ValidationGroup="AddAluno">*</asp:RequiredFieldValidator>
                    <asp:Label ID="lblAluno" runat="server" Visible="False"></asp:Label>
                </div>
                <div class="form-group">
                    Data de Nascimento<br />
                    <asp:TextBox ID="txtDataNasc" runat="server" ValidationGroup="AddAluno" TextMode="DateTime" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvNasc" runat="server" ControlToValidate="txtDataNasc" ErrorMessage="Campo obrigatório" ValidationGroup="AddAluno">*</asp:RequiredFieldValidator>
                </div>
                <div class="form-group">
                    Email<br />
                    <!--Fazendo a validação do formato de email com TextMode="Email"-->
                    <asp:TextBox ID="txtEmail" runat="server" TextMode="Email" ValidationGroup="AddAluno" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail" ErrorMessage="Campo obrigatório" ValidationGroup="AddAluno">*</asp:RequiredFieldValidator>
                </div>
                <div class="form-group">
                    Curso<br />
                    <asp:DropDownList ID="cmbCurso" runat="server" CssClass="form-control"></asp:DropDownList>
                </div>
                <asp:Button ID="btnAdicionar" runat="server" Text="Adicionar" OnClick="btnAdicionar_Click" ValidationGroup="AddAluno" CssClass="btn btn-success" />
                <asp:Button ID="btnEditar" runat="server" Text="Editar" Visible="False" OnClick="btnEditar_Click" CssClass="btn btn-success" />
                <asp:Button ID="btnCancelar" runat="server" OnClick="btnCancelar_Click" Text="Cancelar" Visible="False" CssClass="btn btn-primary" />
            </div>
            <div>
            </div>
        </div>
    </form>
</body>
</html>
