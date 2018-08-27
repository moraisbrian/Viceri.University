using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace Viceri.University.DAL
{
    public class CursosDAL
    {
        //Intanciando objeto da classe de Conexao
        Conexao con = new Conexao();

        //Método de consulta sem passagem de parametro
        public DataTable Consultar()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con.Conectar();
            cmd.CommandText = @"SELECT ID,
                                       NOME_CURSO AS 'NOME DO CURSO',
                                       DESCRICAO AS 'DESCRIÇÃO',
                                       DATA_INICIO AS 'DATA DE INÍCIO',
                                       DATA_FIM AS 'DATA DE TÉRMINO'
                                FROM 
                                       CURSOS";
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Desconectar();
            return dt;
        }

        //Método de exclusao
        public void Excluir(BLL.Cursos c)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con.Conectar();
            cmd.CommandText = @"DELETE FROM CURSOS 
                                WHERE ID = @id";
            cmd.Parameters.AddWithValue("@id", c.Id);
            cmd.ExecuteNonQuery();
            con.Desconectar();
        }

        //Método de cadastro
        public void Cadastrar(BLL.Cursos c)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con.Conectar();
            cmd.CommandText = @"INSERT INTO CURSOS
                                (NOME_CURSO, DESCRICAO, DATA_INICIO, DATA_FIM)
                                VALUES
                                (@nome, @descricao, @datainicio, @datafim)";
            cmd.Parameters.AddWithValue("@nome", c.Nome_curso);
            cmd.Parameters.AddWithValue("@descricao", c.Descricao);
            cmd.Parameters.AddWithValue("@datainicio", c.Data_inicio);
            cmd.Parameters.AddWithValue("@datafim", c.Data_fim);
            cmd.ExecuteNonQuery();
            con.Desconectar();
        }

        //Método de consulta com passagem de parametro
        public DataTable Consultar(BLL.Cursos c)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con.Conectar();
            cmd.CommandText = @"SELECT ID,
                                       NOME_CURSO AS 'NOME DO CURSO',
                                       DESCRICAO AS 'DESCRIÇÃO',
                                       DATA_INICIO AS 'DATA DE INÍCIO',
                                       DATA_FIM AS 'DATA DE TÉRMINO'
                                FROM 
                                       CURSOS
                                WHERE
                                       NOME_CURSO LIKE @nome";
            cmd.Parameters.AddWithValue("@nome", "%" + c.Nome_curso + "%");
            cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Desconectar();
            return dt;
        }

        //Método para preencher um objeto para fazer uma atualização
        public BLL.Cursos Preencher(BLL.Cursos c)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con.Conectar();
            cmd.CommandText = @"SELECT ID,
                                       NOME_CURSO,
                                       DESCRICAO,
                                       DATA_INICIO,
                                       DATA_FIM
                                FROM 
                                       CURSOS
                                WHERE
                                       ID = @id";
            cmd.Parameters.AddWithValue("@id", c.Id);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                c.Id = Convert.ToInt32(dr["ID"]);
                c.Nome_curso = dr["NOME_CURSO"].ToString();
                c.Descricao = dr["DESCRICAO"].ToString();
                c.Data_inicio = Convert.ToDateTime(dr["DATA_INICIO"]);
                c.Data_fim = Convert.ToDateTime(dr["DATA_FIM"]);
                dr.Close();
            }
            else
            {
                c.Id = 0;
            }
            con.Desconectar();
            return c;
        }

        //Método de atualização
        public void Atualizar(BLL.Cursos c)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con.Conectar();
            cmd.CommandText = @"UPDATE
                                    CURSOS
                                SET
                                    NOME_CURSO = @nome,
                                    DESCRICAO = @descricao,
                                    DATA_INICIO = @datainicio,
                                    DATA_FIM = @datafim
                                WHERE
                                    ID = @id";
            cmd.Parameters.AddWithValue("@id", c.Id);
            cmd.Parameters.AddWithValue("@nome", c.Nome_curso);
            cmd.Parameters.AddWithValue("@descricao", c.Descricao);
            cmd.Parameters.AddWithValue("@datainicio", c.Data_inicio);
            cmd.Parameters.AddWithValue("@datafim", c.Data_fim);
            cmd.ExecuteNonQuery();
            con.Desconectar();
        }

        //Método para verificação do nome do curso, retorno verdadeiro ou falso
        public bool VerificaCurso(BLL.Cursos c)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con.Conectar();
            cmd.CommandText = @"SELECT
                                       NOME_CURSO
                                FROM 
                                       CURSOS
                                WHERE
                                       NOME_CURSO = @nome";
            cmd.Parameters.AddWithValue("@nome", c.Nome_curso);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                con.Desconectar();
                return true;
            }
            else
            {
                con.Desconectar();
                return false;
            }
            
        }

        //Método para verificação do nome do curso e id, retorno verdadeiro ou falso
        public bool VerificaIDeCurso(BLL.Cursos c)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con.Conectar();
            cmd.CommandText = @"SELECT
                                       NOME_CURSO
                                FROM 
                                       CURSOS
                                WHERE
                                       NOME_CURSO = @nome
                                AND
                                       ID = @id";
            cmd.Parameters.AddWithValue("@nome", c.Nome_curso);
            cmd.Parameters.AddWithValue("@id", c.Id);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                con.Desconectar();
                return true;
            }
            else
            {
                con.Desconectar();
                return false;
            }

        }

        //Método para verificação do Id, retorno verdadeiro ou falso
        public bool VerificaId(BLL.Cursos c)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con.Conectar();
            cmd.CommandText = @"SELECT
                                       ID,
                                       NOME_CURSO
                                FROM 
                                       CURSOS
                                WHERE
                                       ID = @id";
            cmd.Parameters.AddWithValue("@id", c.Id);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                con.Desconectar();
                return true;
            }
            else
            {
                con.Desconectar();
                return false;
            }

        }

        
    }
}