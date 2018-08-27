using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Viceri.University.UI
{
    public partial class Cursos : System.Web.UI.Page
    {
        //Instanciando objetos das classes BLL e DAL
        BLL.Cursos cursos = new BLL.Cursos();
        DAL.CursosDAL cursosDAL = new DAL.CursosDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Verificando se não é um PostBack
            if (!IsPostBack)
            {
                //Carregando a GridView com o método Consultar do cursosDAL
                gvCursos.DataSource = cursosDAL.Consultar();
                gvCursos.DataBind();
            }
        }

        protected void gvCursos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //Obtendo o Id pelo CommandArgument
            //O CommandArgument está sendo obtido na linha 30 do form Cursos.aspx
            //CommandArgument='<% #Eval("ID") %>'

            cursos.Id = Convert.ToInt32(e.CommandArgument);

            //Verificando se o CommandName é igual a cmdExcluir
            if (e.CommandName == "cmdExcluir")
            {
                //A mensagem de confirmação da exclusão está na linha 30 Form Cursos.aspx
                //OnClientClick="return confirm('Deseja realmente excluir?')"

                //try para evitar exclusão de curso que esteja atrelado a alunos
                try
                {
                    //Chamada do método Excluir do objeto cursosDAL
                    cursosDAL.Excluir(cursos);
                    //Mensagem de alerta informando a exclusão do curso
                    Response.Write("<script>alert('Curso excluído')</script>");
                    //Atualizando a GridView com o evento Click do btnFiltrar
                    btnFiltro_Click(null, null);
                }
                catch
                {
                    //Mensagem de alerta informando que o curso é atrelado a alunos
                    Response.Write("<script>alert('Curso atrelado a alunos')</script>");
                }

            }
            //Verificando se o CommandName é igual a cmdAtualizar
            else if (e.CommandName == "cmdAtualizar")
            {
                //Chamada do método Preencher do objeto cursosDAL
                cursos = cursosDAL.Preencher(cursos);
                //Bloqueando botão adicionar e liberando botões Editar e Cancelar
                btnAdicionar.Enabled = false;
                btnEditar.Visible = true;
                btnCancelar.Visible = true;
                //Verificando se o cursos.Id é diferente de 0
                if (cursos.Id != 0)
                {
                    //Passando valores para os TextBox
                    lblCurso.Text = cursos.Id.ToString();
                    txtNome.Text = cursos.Nome_curso;
                    txtDescricao.Text = cursos.Descricao;
                    txtDataInicio.Text = cursos.Data_inicio.ToString();
                    txtDataFim.Text = cursos.Data_fim.ToString();
                }
                else
                {
                    //Mensagem de alerta informando que o curso não foi encontrado
                    Response.Write("<script>alert('Curso não encontrado')</script>");

                    //Liberando botão Adicionar e bloqueando botões Editar e Cancelar
                    btnEditar.Visible = false;
                    btnAdicionar.Enabled = true;
                    btnCancelar.Visible = false;
                }
            }
        }

        protected void btnAdicionar_Click(object sender, EventArgs e)
        {
            //try para evitar que o usuário insira valores inesperados
            try
            {
                //Carregando o objeto cursos com os valores das TextBox
                cursos.Nome_curso = txtNome.Text;
                cursos.Descricao = txtDescricao.Text;
                cursos.Data_inicio = Convert.ToDateTime(txtDataInicio.Text);
                cursos.Data_fim = Convert.ToDateTime(txtDataFim.Text);

                //Chamada do método que faz a verificação do nome do curso
                if (cursosDAL.VerificaCurso(cursos))
                {
                    //Mensagem de alerta informando que o nome do curso já está cadastrado
                    Response.Write("<script>alert('O curso já existe')</script>");
                }
                else
                {
                    //Verificação do tamanho do nome do curso e descrição
                    if (txtNome.Text.Length > 100 || txtDescricao.Text.Length > 500)
                    {
                        //Mensagem de alerta informando que o nome ou descrição excederam 
                        Response.Write("<script>alert('Nome de Curso ou descrição muito extensos')</script>");

                    }
                    else
                    {
                        //Chamada do método Cadastrar do objeto cursosDAL
                        cursosDAL.Cadastrar(cursos);
                        //Mensagem de alerta informando o cadastro
                        Response.Write("<script>alert('Curso cadastrado')</script>");

                        //Limpando TexBox  
                        txtNome.Text = "";
                        txtDescricao.Text = "";
                        txtDataFim.Text = "";
                        txtDataInicio.Text = "";
                        txtNome.Focus();

                        //Atualizando a GridView com o evento Click do btnFiltrar
                        btnFiltro_Click(null, null);
                    }

                }
            }
            catch
            {
                //Mensagem de alerta informando que os dados inserido são inválidos
                Response.Write("<script>alert('Formato de dados inválido')</script>");
            }


        }

        protected void btnFiltro_Click(object sender, EventArgs e)
        {
            //Obtendo o valor da txtFiltro
            cursos.Nome_curso = txtFiltro.Text;
            //Carregando o GridView com o método Consultar com o parametro cursos
            gvCursos.DataSource = cursosDAL.Consultar(cursos);
            gvCursos.DataBind();
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            //try para evitar que o usuário insira valores inesperados
            try
            {
                //Carregando o objeto cursos com os valores das TextBox
                cursos.Id = Convert.ToInt32(lblCurso.Text);
                cursos.Nome_curso = txtNome.Text;
                cursos.Descricao = txtDescricao.Text;
                cursos.Data_inicio = Convert.ToDateTime(txtDataInicio.Text);
                cursos.Data_fim = Convert.ToDateTime(txtDataFim.Text);

                //Chamada dos métodos para verificação do nome do curso
                if (cursosDAL.VerificaIDeCurso(cursos) || cursosDAL.VerificaId(cursos) && cursosDAL.VerificaCurso(cursos) == false)
                {
                    //Verificação do tamanho do nome do curso e descrição
                    if (txtNome.Text.Length > 100 || txtDescricao.Text.Length > 500)
                    {
                        //Mensagem de alerta informando que o nome ou descrição excederam 
                        Response.Write("<script>alert('Nome de Curso ou descrição muito extensos')</script>");

                    }
                    else
                    {
                        //Chamada do método Atualizar do objeto cursosDAL
                        cursosDAL.Atualizar(cursos);
                        Response.Write("<script>alert('Curso Atualizado')</script>");
                    }
                    
                }
                else
                {
                    //Mensagem de alerta informando que o nome do curso já está cadastrado
                    Response.Write("<script>alert('O curso já existe')</script>");
                }

                //Atualizando a GridView com o evento Click do btnFiltrar
                btnFiltro_Click(null, null);

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
            //Limpando TextBox 
            lblCurso.Text = "";
            txtNome.Text = "";
            txtDescricao.Text = "";
            txtDataFim.Text = "";
            txtDataInicio.Text = "";
            txtNome.Focus();

            //Liberando botão Adicionar e bloqueando botões Editar e Cancelar
            btnEditar.Visible = false;
            btnAdicionar.Enabled = true;
            btnCancelar.Visible = false;
        }


    }
}