using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace Viceri.University.DAL
{
    public class AlunosDAL
    {
        Conexao con = new Conexao();

        public DataTable Consultar()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con.Conectar();
            cmd.CommandText = @"SELECT ALUNOS.ID,
                                       ALUNOS.NOME,
                                       DATEDIFF(YEAR, ALUNOS.DATA_NASC, GETDATE()) AS IDADE,
                                       ALUNOS.EMAIL,
                                       CURSOS.NOME_CURSO AS 'NOME DO CURSO'
                                FROM 
                                       ALUNOS
                                INNER JOIN
                                       CURSOS
                                ON
                                       ALUNOS.ID_CURSOS = CURSOS.ID";
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Desconectar();
            return dt;
        }

        public DataTable Consultar(BLL.Alunos a)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con.Conectar();
            cmd.CommandText = @"SELECT ALUNOS.ID,
                                       ALUNOS.NOME,
                                       DATEDIFF(YEAR, ALUNOS.DATA_NASC, GETDATE()) AS IDADE,
                                       ALUNOS.EMAIL,
                                       CURSOS.NOME_CURSO AS 'NOME DO CURSO'
                                FROM 
                                       ALUNOS
                                INNER JOIN
                                       CURSOS
                                ON
                                       ALUNOS.ID_CURSOS = CURSOS.ID
                                WHERE
                                       ALUNOS.NOME LIKE @nome";
            cmd.Parameters.AddWithValue("@nome", "%" + a.Nome + "%");
            cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Desconectar();
            return dt;
        }

        public void Cadastrar(BLL.Alunos a)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con.Conectar();
            cmd.CommandText = @"INSERT INTO ALUNOS
                                (NOME, DATA_NASC, EMAIL, ID_CURSOS)
                                VALUES
                                (@nome, @datanasc, @email, @curso)";
            cmd.Parameters.AddWithValue("@nome", a.Nome);
            cmd.Parameters.AddWithValue("@datanasc", a.Data_nasc);
            cmd.Parameters.AddWithValue("@email", a.Email);
            cmd.Parameters.AddWithValue("@curso", a.Id_cursos);
            cmd.ExecuteNonQuery();
            con.Desconectar();
        }

        public void Excluir(BLL.Alunos a)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con.Conectar();
            cmd.CommandText = @"DELETE FROM ALUNOS
                                WHERE ID = @id";
            cmd.Parameters.AddWithValue("@id", a.Id);
            cmd.ExecuteNonQuery();
            con.Desconectar();
        }

        public BLL.Alunos Preencher(BLL.Alunos a)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con.Conectar();
            cmd.CommandText = @"SELECT ALUNOS.ID,
                                       ALUNOS.NOME,
                                       ALUNOS.DATA_NASC,
                                       ALUNOS.EMAIL,
                                       ALUNOS.ID_CURSOS
                                FROM 
                                       ALUNOS
                                WHERE
                                       ID = @id";
            cmd.Parameters.AddWithValue("@id", a.Id);
            cmd.ExecuteNonQuery();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                a.Id = Convert.ToInt32(dr["ID"]);
                a.Nome = dr["NOME"].ToString();
                a.Email = dr["EMAIL"].ToString();
                a.Data_nasc = Convert.ToDateTime(dr["DATA_NASC"]);
                a.Id_cursos = Convert.ToInt32(dr["ID_CURSOS"]);
                dr.Close();
            }
            else
            {
                a.Id = 0;
            }
            con.Desconectar();
            return a;
        }

        public void Atualizar(BLL.Alunos a)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con.Conectar();
            cmd.CommandText = @"UPDATE 
                                    ALUNOS
                                SET
                                    NOME = @nome,
                                    EMAIL = @email,
                                    DATA_NASC = @data,
                                    ID_CURSOS = @idcursos
                                WHERE 
                                    ID = @id";
            cmd.Parameters.AddWithValue("@id", a.Id);
            cmd.Parameters.AddWithValue("@nome", a.Nome);
            cmd.Parameters.AddWithValue("@email", a.Email);
            cmd.Parameters.AddWithValue("@data", a.Data_nasc);
            cmd.Parameters.AddWithValue("@idcursos", a.Id_cursos);
            cmd.ExecuteNonQuery();
            con.Desconectar();
        }

        public bool VerificaEmail(BLL.Alunos a)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con.Conectar();
            cmd.CommandText = @"SELECT 
                                       ALUNOS.ID,
                                       ALUNOS.EMAIL
                                FROM 
                                       ALUNOS
                                WHERE
                                       ALUNOS.EMAIL = @email";
            cmd.Parameters.AddWithValue("@email", a.Email);
            cmd.ExecuteNonQuery();
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

        public bool VerificaId(BLL.Alunos a)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con.Conectar();
            cmd.CommandText = @"SELECT 
                                       ALUNOS.ID,
                                       ALUNOS.EMAIL
                                FROM 
                                       ALUNOS
                                WHERE
                                       ALUNOS.ID = @id";
            cmd.Parameters.AddWithValue("@id", a.Id);
            cmd.ExecuteNonQuery();
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

        public bool VerificaIDeEmail(BLL.Alunos a)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con.Conectar();
            cmd.CommandText = @"SELECT 
                                       ALUNOS.ID,
                                       ALUNOS.EMAIL
                                FROM 
                                       ALUNOS
                                WHERE
                                       ALUNOS.ID = @id
                                AND
                                       ALUNOS.EMAIL = @email";
            cmd.Parameters.AddWithValue("@id", a.Id);
            cmd.Parameters.AddWithValue("@email", a.Email);
            cmd.ExecuteNonQuery();
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