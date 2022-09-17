using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_Operacional
{
    class Processo
    {
        private string nomeProcesso;
        private int validacao;
        public Processo()
        {
            validacao = 2;
        }
        public string NomeProcesso { get => nomeProcesso; set => nomeProcesso = value; }
        public int Validacao { get=> validacao; set => validacao = value;}
    }
}

