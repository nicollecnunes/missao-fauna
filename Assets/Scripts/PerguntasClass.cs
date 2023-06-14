public class Pergunta
{
    public string enunciado;
    public string alt1;
    public string alt2;
    public string alt3;
    public int resposta;

    public Pergunta(string name, string alt1, string alt2, string alt3, int resposta)
    {
        this.name = name;
        this.alt1 = alt1;
        this.alt2 = alt2;
        this.alt3 = alt3;
        this.resposta = resposta;
    }
}

List<Pergunta> Perguntas = new List<Pergunta>();
Perguntas.Add(new Pergunta("enunciado 1", "resposta 1.1", "resposta 1.2","resposta 1.3", 1);
Perguntas.Add(new Pergunta("enunciado 2", "resposta 2.1", "resposta 2.2","resposta 2.3", 2);
