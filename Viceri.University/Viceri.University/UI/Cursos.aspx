<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cursos.aspx.cs" Inherits="Viceri.University.UI.Cursos" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Cursos</title>
    <meta charset ="utf-8"/>
    <link href="../CSS/estilo.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <a href="Alunos.aspx">Ir para Alunos</a>
        </div>
        <hr/>
        <div>
            <h1>Cursos</h1>
            Filtrar por Nome: 
            <asp:TextBox ID="txtFiltro" runat="server"></asp:TextBox>
            <asp:Button ID="btnFiltro" runat="server" Text="Filtrar" OnClick="btnFiltro_Click" />
        </div>
        <br/>
        <div>
            <asp:GridView ID="gvCursos" runat="server" OnRowCommand="gvCursos_RowCommand" BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" CellSpacing="2" ForeColor="Black">
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
        <div>
            Nome do Curso<asp:Label ID="lblCurso" runat="server" Visible="False"></asp:Label><br />
            <asp:TextBox ID="txtNome" runat="server" ValidationGroup="AddCurso"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvNomeCurso" runat="server" ControlToValidate="txtNome" ErrorMessage="Campo obrigatório" ValidationGroup="AddCurso">*</asp:RequiredFieldValidator>
            <br />
            Descrição<br />
            <asp:TextBox ID="txtDescricao" runat="server" ValidationGroup="AddCurso"></asp:TextBox>
            <br />
            Data de Início<br />
            <asp:TextBox ID="txtDataInicio" runat="server" ValidationGroup="AddCurso"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvDataInicio" runat="server" ControlToValidate="txtDataInicio" ErrorMessage="Campo obrigatório" ValidationGroup="AddCurso">*</asp:RequiredFieldValidator>
            <br />
            Data de Término<br />
            <asp:TextBox ID="txtDataFim" runat="server" ValidationGroup="AddCurso"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvDataFim" runat="server" ControlToValidate="txtDataFim" ErrorMessage="Campo obrigatório" ValidationGroup="AddCurso">*</asp:RequiredFieldValidator>
            <br />
            <asp:Button ID="btnAdicionar" runat="server" Text="Adicionar" OnClick="btnAdicionar_Click" ValidationGroup="AddCurso" />
            <asp:Button ID="btnEditar" runat="server" OnClick="btnEditar_Click" Text="Editar" Visible="False" />
            <asp:Button ID="btnCancelar" runat="server" OnClick="btnCancelar_Click" Text="Cancelar" Visible="False" />
        </div>
    </form>
</body>
</html>
