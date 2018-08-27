<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Alunos.aspx.cs" Inherits="Viceri.University.UI.Alunos" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Alunos</title>
    <meta charset="utf-8"/>
    <link href="../CSS/estilo.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <a href="Cursos.aspx">Ir para Cursos</a>
        </div>
        <hr />
        <div>
            <h1>Alunos</h1>
            Filtrar por Nome: 
            <asp:TextBox ID="txtFiltro" runat="server">
            </asp:TextBox><asp:Button ID="btnFiltrar" runat="server" Text="Filtrar" OnClick="btnFiltrar_Click" />
        </div>
        <br />
        <div>
            <asp:GridView ID="gvAluno" runat="server" BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" CellSpacing="2" ForeColor="Black" OnRowCommand="gvAluno_RowCommand">
                <Columns>
                    <asp:TemplateField ShowHeader="False">
                        <ItemTemplate>
                            <asp:Button ID="btnExcluir" runat="server" CausesValidation="false" CommandArgument='<% #Eval("ID") %>' CommandName="cmdExcluir" OnClientClick="return confirm('Deseja realmente excluir?')" Text="Excluir" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False">
                        <ItemTemplate>
                            <asp:Button ID="btnAtualizar" runat="server" CausesValidation="false" CommandArgument='<% #Eval("ID") %>' CommandName="cmdAtualizar" Text="Atualizar" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    Vazio
                </EmptyDataTemplate>
                <FooterStyle BackColor="#CCCCCC" />
                <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
                <RowStyle BackColor="White" />
                <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#808080" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#383838" />
            </asp:GridView>
        </div>
        <br />
        <div id="cadAluno">
            Nome do Aluno<br />
            <asp:TextBox ID="txtNome" runat="server" ValidationGroup="AddAluno"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvNome" runat="server" ControlToValidate="txtNome" ErrorMessage="Campo obrigatório" ValidationGroup="AddAluno">*</asp:RequiredFieldValidator>
            <asp:Label ID="lblAluno" runat="server" Visible="False"></asp:Label>
            <br />
            Data de Nascimento<br />
            <asp:TextBox ID="txtDataNasc" runat="server" ValidationGroup="AddAluno" TextMode="DateTime"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvNasc" runat="server" ControlToValidate="txtDataNasc" ErrorMessage="Campo obrigatório" ValidationGroup="AddAluno">*</asp:RequiredFieldValidator>
            <br />
            Email<br /> <!--Fazendo a validação do formato de email com TextMode="Email"-->
            <asp:TextBox ID="txtEmail" runat="server" TextMode="Email" ValidationGroup="AddAluno"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail" ErrorMessage="Campo obrigatório" ValidationGroup="AddAluno">*</asp:RequiredFieldValidator>
            <br />
            Curso<br />
            <asp:DropDownList ID="cmbCurso" runat="server"></asp:DropDownList>
            <br />
            <asp:Button ID="btnAdicionar" runat="server" Text="Adicionar" OnClick="btnAdicionar_Click" ValidationGroup="AddAluno" />
            <asp:Button ID="btnEditar" runat="server" Text="Editar" Visible="False" OnClick="btnEditar_Click" />
            <asp:Button ID="btnCancelar" runat="server" OnClick="btnCancelar_Click" Text="Cancelar" Visible="False" />
        </div>
        <div>
        </div>
    </form>
</body>
</html>
