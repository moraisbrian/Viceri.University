using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Viceri.University.BLL
{
    public class Cursos
    {
        private int id;
        private string nome_curso;
        private string descricao;
        private DateTime data_inicio;
        private DateTime data_fim;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Nome_curso
        {
            get { return nome_curso; }
            set { nome_curso = value; }
        }

        public string Descricao
        {
            get { return descricao; }
            set { descricao = value; }
        }

        public DateTime Data_inicio
        {
            get { return data_inicio; }
            set { data_inicio = value; }
        }

        public DateTime Data_fim
        {
            get { return data_fim; }
            set { data_fim = value; }
        }
    }
}