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
        BLL.Cursos cursos = new BLL.Cursos();
        DAL.CursosDAL cursosDAL = new DAL.CursosDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                gvCursos.DataSource = cursosDAL.Consultar();
                gvCursos.DataBind();
            }
        }

        protected void gvCursos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            cursos.Id = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "cmdExcluir")
            {
                try
                {
                    cursosDAL.Excluir(cursos);
                    Response.Write("<script>alert('Curso excluído')</script>");
                    btnFiltro_Click(null, null);
                }
                catch
                {
                    Response.Write("<script>alert('Curso atrelado a alunos')</script>");
                }

            }
            else if (e.CommandName == "cmdAtualizar")
            {
                cursos = cursosDAL.Preencher(cursos);
                btnAdicionar.Enabled = false;
                btnEditar.Visible = true;
                btnCancelar.Visible = true;
                if (cursos.Id != 0)
                {
                    lblCurso.Text = cursos.Id.ToString();
                    txtNome.Text = cursos.Nome_curso;
                    txtDescricao.Text = cursos.Descricao;
                    txtDataInicio.Text = cursos.Data_inicio.ToString();
                    txtDataFim.Text = cursos.Data_fim.ToString();
                }
                else
                {
                    Response.Write("<script>alert('Curso não encontrado')</script>");

                    btnEditar.Visible = false;
                    btnAdicionar.Enabled = true;
                    btnCancelar.Visible = false;
                }
            }
        }

        protected void btnAdicionar_Click(object sender, EventArgs e)
        {
            try
            {
                cursos.Nome_curso = txtNome.Text;
                cursos.Descricao = txtDescricao.Text;
                cursos.Data_inicio = Convert.ToDateTime(txtDataInicio.Text);
                cursos.Data_fim = Convert.ToDateTime(txtDataFim.Text);

                if (cursosDAL.VerificaCurso(cursos))
                {
                    Response.Write("<script>alert('O curso já existe')</script>");
                }
                else
                {
                    if (txtNome.Text.Length > 100 || txtDescricao.Text.Length > 500)
                    {
                        Response.Write("<script>alert('Nome de Curso ou descrição muito extensos')</script>");
                    }
                    else
                    {
                        cursosDAL.Cadastrar(cursos);
                        Response.Write("<script>alert('Curso cadastrado')</script>");

                        txtNome.Text = "";
                        txtDescricao.Text = "";
                        txtDataFim.Text = "";
                        txtDataInicio.Text = "";
                        txtNome.Focus();

                        btnFiltro_Click(null, null);
                    }

                }
            }
            catch
            {
                Response.Write("<script>alert('Formato de dados inválido')</script>");
            }


        }

        protected void btnFiltro_Click(object sender, EventArgs e)
        {
            cursos.Nome_curso = txtFiltro.Text;
            gvCursos.DataSource = cursosDAL.Consultar(cursos);
            gvCursos.DataBind();
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                cursos.Id = Convert.ToInt32(lblCurso.Text);
                cursos.Nome_curso = txtNome.Text;
                cursos.Descricao = txtDescricao.Text;
                cursos.Data_inicio = Convert.ToDateTime(txtDataInicio.Text);
                cursos.Data_fim = Convert.ToDateTime(txtDataFim.Text);

                if (cursosDAL.VerificaIDeCurso(cursos) || cursosDAL.VerificaId(cursos) && cursosDAL.VerificaCurso(cursos) == false)
                {
                    if (txtNome.Text.Length > 100 || txtDescricao.Text.Length > 500)
                    {
                        Response.Write("<script>alert('Nome de Curso ou descrição muito extensos')</script>");
                    }
                    else
                    {
                        cursosDAL.Atualizar(cursos);
                        Response.Write("<script>alert('Curso Atualizado')</script>");
                    }
                    
                }
                else
                {
                    Response.Write("<script>alert('O curso já existe')</script>");
                }

                btnFiltro_Click(null, null);
                btnCancelar_Click(null, null);
            }
            catch
            {
                Response.Write("<script>alert('Formato de dados inválido')</script>");
            }


        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            lblCurso.Text = "";
            txtNome.Text = "";
            txtDescricao.Text = "";
            txtDataFim.Text = "";
            txtDataInicio.Text = "";
            txtNome.Focus();

            btnEditar.Visible = false;
            btnAdicionar.Enabled = true;
            btnCancelar.Visible = false;
        }
    }
}