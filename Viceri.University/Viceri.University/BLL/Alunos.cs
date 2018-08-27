using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Viceri.University.BLL
{
    public class Alunos
    {
        private int id;
        private string nome;
        private DateTime data_nasc;
        private string email;
        private int id_cursos;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Nome
        {
            get { return nome; }
            set { nome = value; }
        }

        public DateTime Data_nasc
        {
            get { return data_nasc; }
            set { data_nasc = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public int Id_cursos
        {
            get { return id_cursos; }
            set { id_cursos = value; }
        }
    }
}