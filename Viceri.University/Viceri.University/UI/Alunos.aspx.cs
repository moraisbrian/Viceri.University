using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Viceri.University.UI
{
    public partial class Alunos : System.Web.UI.Page
    {
        //Instanciando objetos das classes BLL e DAL
        DAL.AlunosDAL alunosDAL = new DAL.AlunosDAL();
        BLL.Alunos alunos = new BLL.Alunos();
        DAL.CursosDAL cursosDAL = new DAL.CursosDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Verificando se não é um PostBack
            if (!IsPostBack)
            {
                //Carregando a GridView com o método Consultar do alunosDAL
                gvAluno.DataSource = alunosDAL.Consultar();
                gvAluno.DataBind();

                //Atribuindo o valor da chave estrangeira para o cmbCurso
                cmbCurso.DataSource = cursosDAL.Consultar();
                //Definindo texto de exibição da cmbCurso
                cmbCurso.DataTextField = "NOME DO CURSO";
                //Definindo valor da cmbCurso
                cmbCurso.DataValueField = "ID";
                cmbCurso.DataBind();
            }
        }

        protected void btnAdicionar_Click(object sender, EventArgs e)
        {
            //try para evitar que o usuário insira valores inesperados
            try
            {
                //Carregando o objeto alunos com os valores das TextBox e DropDownList
                alunos.Nome = txtNome.Text;
                alunos.Data_nasc = Convert.ToDateTime(txtDataNasc.Text);

                //A validação do formato de email(TextMode="Email") está na linha 65 do form Alunos.aspx
                alunos.Email = txtEmail.Text;
                alunos.Id_cursos = Convert.ToInt32(cmbCurso.SelectedValue);

                //Fazendo a verificação da idade do aluno
                TimeSpan intervalo = DateTime.Now - alunos.Data_nasc;
                int anos = intervalo.Days / 365;

                //Se a idade for menor que 18 o alunos não será cadastrado
                if (anos < 18)
                {
                    //Mensagem de alerta informando que o aluno é menor de 18 anos
                    Response.Write("<script>alert('Aluno menor de 18 anos')</script>");
                }
                else
                {
                    //Chamada do método que faz a verificação do email, se retornar true o aluno não será cadastrado
                    if (alunosDAL.VerificaEmail(alunos) == false)
                    {
                        //Chamada do método Cadastrar do objeto alunosDAL
                        alunosDAL.Cadastrar(alunos);

                        //Mensagem de alerta informando o cadastro
                        Response.Write("<script>alert('Aluno cadastrado')</script>");

                        //Atualizando a GridView com o evento Click do btnFiltrar
                        btnFiltrar_Click(null, null);
                        
                        //Limpando TextBox e retornando o valor padrão do indice do DropDownList
                        txtEmail.Text = "";
                        txtNome.Text = "";
                        txtDataNasc.Text = "";
                        cmbCurso.SelectedIndex = 0;

                        //Retornado o cursor para o txtNome
                        txtNome.Focus();
                    }
                    else
                    {
                        //Mensagem de alerta informando que o email já existe no sistema
                        Response.Write("<script>alert('O email já existe')</script>");
                    }
                }
            }
            catch
            {
                //Mensagem de alerta informando que os dados inserido são inválidos
                Response.Write("<script>alert('Formato de dados inválido')</script>");
            }

            

        }

        protected void gvAluno_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //Obtendo o Id pelo CommandArgument
            //O CommandArgument está sendo obtido na linha 30 do form Alunos.aspx
            //CommandArgument='<% #Eval("ID") %>'

            alunos.Id = Convert.ToInt32(e.CommandArgument);

            //Verificando se o CommandName é igual a cmdExcluir
            if (e.CommandName == "cmdExcluir")
            {
                //A mensagem de confirmação da exclusão está na linha 30 Form Alunos.aspx
                //OnClientClick="return confirm('Deseja realmente excluir?')"

                //Chamada do método Excluir do objeto alunosDAL
                alunosDAL.Excluir(alunos);
                //Mensagem de alerta informando a exclusão do aluno
                Response.Write("<script>alert('Aluno excluído')</script>");
                //Atualizando a GridView com o evento Click do btnFiltrar
                btnFiltrar_Click(null, null);
            }
            //Verificando se o CommandName é igual a cmdAtualizar
            else if (e.CommandName == "cmdAtualizar")
            {
                //Chamada do método Preencher do objeto alunosDAL
                alunos = alunosDAL.Preencher(alunos);
                //Bloqueando botão adicionar e liberando botões Editar e Cancelar
                btnAdicionar.Enabled = false;
                btnEditar.Visible = true;
                btnCancelar.Visible = true;
                //Verificando se o alunos.Id é diferente de 0
                if (alunos.Id != 0)
                {
                    //Passando valores para os TextBox e DropDownList
                    lblAluno.Text = alunos.Id.ToString();
                    txtNome.Text = alunos.Nome;
                    txtEmail.Text = alunos.Email;
                    txtDataNasc.Text = alunos.Data_nasc.ToString();
                    cmbCurso.SelectedValue = alunos.Id_cursos.ToString();
                }
                else
                {
                    //Mensagem de alerta informando que o aluno não foi encontrado
                    Response.Write("<script>alert('Aluno não encontrado')</script>");

                    //Liberando botão Adicionar e bloqueando botões Editar e Cancelar
                    btnEditar.Visible = false;
                    btnAdicionar.Enabled = true;
                    btnCancelar.Visible = false;

                }
            }
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            //Obtendo o valor da txtFiltro
            alunos.Nome = txtFiltro.Text;
            //Carregando o GridView com o método Consultar com o parametro alunos
            gvAluno.DataSource = alunosDAL.Consultar(alunos);
            gvAluno.DataBind();
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            //try para evitar que o usuário insira valores inesperados
            try
            {
                //Carregando o objeto alunos com os valores das TextBox e DropDownList
                alunos.Id = Convert.ToInt32(lblAluno.Text);
                alunos.Nome = txtNome.Text;

                //A validação do formato de email(TextMode="Email") está na linha 65 do form Alunos.aspx
                alunos.Email = txtEmail.Text;
                alunos.Data_nasc = Convert.ToDateTime(txtDataNasc.Text);
                alunos.Id_cursos = Convert.ToInt32(cmbCurso.SelectedValue);

                //Fazendo a verificação da idade do aluno
                TimeSpan intervalo = DateTime.Now - alunos.Data_nasc;
                int anos = intervalo.Days / 365;

                //Se a idade for menor que 18 o alunos não será atualizado
                if (anos < 18)
                {
                    //Mensagem de alerta informando que o aluno é menor de 18 anos
                    Response.Write("<script>alert('Aluno menor de 18 anos')</script>");
                }
                else
                {
                    //Chamada dos métodos para verificação do email
                    if (alunosDAL.VerificaIDeEmail(alunos) || alunosDAL.VerificaId(alunos) && alunosDAL.VerificaEmail(alunos) == false)
                    {
                        //Chamada do método Atualizar do objeto alunosDAL
                        alunosDAL.Atualizar(alunos);
                        Response.Write("<script>alert('Aluno Atualizado')</script>");
                    }
                    else
                    {
                        //Mensagem de alerta informando que o email já existe
                        Response.Write("<script>alert('O email já existe')</script>");
                    }
                }

                //Atualizando a GridView com o evento Click do btnFiltrar
                btnFiltrar_Click(null, null);

                //Limando TextBox, desblequeando o botão Adicionar e bloqueando botões Editar e Cancelar
                btnCancelar_Click(null, null);
            }
            catch
            {
                //Mensagem de alerta informando que os dados inserido são inválidos
                Response.Write("<script>alert('Formato de dados inválido')</script>");
            }
            
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            //Limpando TextBox e retornando o valor padrão do indice do DropDownList
            lblAluno.Text = "";
            txtDataNasc.Text = "";
            txtNome.Text = "";
            txtEmail.Text = "";
            cmbCurso.SelectedIndex = 0;

            //Liberando botão Adicionar e bloqueando botões Editar e Cancelar
            btnEditar.Visible = false;
            btnAdicionar.Enabled = true;
            btnCancelar.Visible = false;
        }
    }
}