using System.Collections;
using System.Collections.Generic;
namespace Quiz
{
    public class Pergunta
    {
        public string enunciado;
        public string alt1;
        public string alt2;
        public string alt3;
        public int resposta;

        public Pergunta(string enunciado, string alt1, string alt2, string alt3, int resposta)
        {
            this.enunciado = enunciado;
            this.alt1 = alt1;
            this.alt2 = alt2;
            this.alt3 = alt3;
            this.resposta = resposta;
        }
    }
}