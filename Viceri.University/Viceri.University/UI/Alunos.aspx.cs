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
        DAL.AlunosDAL alunosDAL = new DAL.AlunosDAL();
        BLL.Alunos alunos = new BLL.Alunos();
        DAL.CursosDAL cursosDAL = new DAL.CursosDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                gvAluno.DataSource = alunosDAL.Consultar();
                gvAluno.DataBind();

                cmbCurso.DataSource = cursosDAL.Consultar();
                cmbCurso.DataTextField = "NOME DO CURSO";
                cmbCurso.DataValueField = "ID";
                cmbCurso.DataBind();
            }
        }

        protected void btnAdicionar_Click(object sender, EventArgs e)
        {
            try
            {
                alunos.Nome = txtNome.Text;
                alunos.Data_nasc = Convert.ToDateTime(txtDataNasc.Text);

                alunos.Email = txtEmail.Text;
                alunos.Id_cursos = Convert.ToInt32(cmbCurso.SelectedValue);

                TimeSpan intervalo = DateTime.Now - alunos.Data_nasc;
                int anos = intervalo.Days / 365;

                if (anos < 18)
                {
                    Response.Write("<script>alert('Aluno menor de 18 anos')</script>");
                }
                else
                {
                    if (alunosDAL.VerificaEmail(alunos) == false)
                    {
                        alunosDAL.Cadastrar(alunos);

                        Response.Write("<script>alert('Aluno cadastrado')</script>");

                        btnFiltrar_Click(null, null);
                        
                        txtEmail.Text = "";
                        txtNome.Text = "";
                        txtDataNasc.Text = "";
                        cmbCurso.SelectedIndex = 0;

                        txtNome.Focus();
                    }
                    else
                    {
                        Response.Write("<script>alert('O email já existe')</script>");
                    }
                }
            }
            catch
            {
                Response.Write("<script>alert('Formato de dados inválido')</script>");
            }

            

        }

        protected void gvAluno_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            alunos.Id = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "cmdExcluir")
            {
                alunosDAL.Excluir(alunos);
                Response.Write("<script>alert('Aluno excluído')</script>");
                btnFiltrar_Click(null, null);
            }
            else if (e.CommandName == "cmdAtualizar")
            {
                alunos = alunosDAL.Preencher(alunos);
                btnAdicionar.Enabled = false;
                btnEditar.Visible = true;
                btnCancelar.Visible = true;
                if (alunos.Id != 0)
                {
                    lblAluno.Text = alunos.Id.ToString();
                    txtNome.Text = alunos.Nome;
                    txtEmail.Text = alunos.Email;
                    txtDataNasc.Text = alunos.Data_nasc.ToString();
                    cmbCurso.SelectedValue = alunos.Id_cursos.ToString();
                }
                else
                {
                    Response.Write("<script>alert('Aluno não encontrado')</script>");

                    btnEditar.Visible = false;
                    btnAdicionar.Enabled = true;
                    btnCancelar.Visible = false;

                }
            }
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            alunos.Nome = txtFiltro.Text;
            gvAluno.DataSource = alunosDAL.Consultar(alunos);
            gvAluno.DataBind();
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                alunos.Id = Convert.ToInt32(lblAluno.Text);
                alunos.Nome = txtNome.Text;

                alunos.Email = txtEmail.Text;
                alunos.Data_nasc = Convert.ToDateTime(txtDataNasc.Text);
                alunos.Id_cursos = Convert.ToInt32(cmbCurso.SelectedValue);

                TimeSpan intervalo = DateTime.Now - alunos.Data_nasc;
                int anos = intervalo.Days / 365;

                if (anos < 18)
                {
                    Response.Write("<script>alert('Aluno menor de 18 anos')</script>");
                }
                else
                {
                    if (alunosDAL.VerificaIDeEmail(alunos) || alunosDAL.VerificaId(alunos) && alunosDAL.VerificaEmail(alunos) == false)
                    {
                        alunosDAL.Atualizar(alunos);
                        Response.Write("<script>alert('Aluno Atualizado')</script>");
                    }
                    else
                    {
                        Response.Write("<script>alert('O email já existe')</script>");
                    }
                }

                btnFiltrar_Click(null, null);

                btnCancelar_Click(null, null);
            }
            catch
            {
                Response.Write("<script>alert('Formato de dados inválido')</script>");
            }
            
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            lblAluno.Text = "";
            txtDataNasc.Text = "";
            txtNome.Text = "";
            txtEmail.Text = "";
            cmbCurso.SelectedIndex = 0;

            btnEditar.Visible = false;
            btnAdicionar.Enabled = true;
            btnCancelar.Visible = false;
        }
    }
}