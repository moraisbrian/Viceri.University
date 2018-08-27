using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace Viceri.University.DAL
{
    public class Conexao
    {
        //Declarando uma variável do tipo SqlConnection
        SqlConnection con;

        //Método construtor com a string de conexão
        public Conexao()
        {
            con = new SqlConnection();
            con.ConnectionString = @"DATA SOURCE = (LOCAL)\SQLEXPRESS;
                                   INITIAL CATALOG = BD_ViceriUniversity;
                                   INTEGRATED SECURITY = TRUE";
        }

        //Método de conexão
        public SqlConnection Conectar()
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            return con;
        }

        //Método para desconectar
        public void Desconectar()
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
    }
}