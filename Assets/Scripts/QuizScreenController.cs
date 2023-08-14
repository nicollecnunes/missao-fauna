using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Quiz;
using TMPro;
using UnityEngine.UI;
using System.Threading;

public class QuizScreenController : MonoBehaviour
{
   public static bool isPaused = false;
    public GameObject quizMenuUI, rightFeedback, wrongFeedback;
    public TMP_Text enunciado, alt1, alt2, alt3;
    public Button option1Button, option2Button, option3Button;
    private int indexPerguntaSelecionada;
    private HealthAndPointsController hpController;
    private bool gotItRight = false;

    public List<Pergunta> createData()
    {
        List<Pergunta> perguntas = new List<Pergunta>();
        //A
        perguntas.Add(new Pergunta("O que é a camada de ozônio?", "Uma camada de gás que protege a Terra dos raios ultravioleta", "Uma camada de gases poluentes na atmosfera", "Uma camada de nuvens que retém o calor", 1));
        perguntas.Add(new Pergunta("Qual é o maior reservatório de água doce do planeta?", "Calotas polares", "Oceanos", "Aquíferos subterrâneos", 1));
        perguntas.Add(new Pergunta("O que é a reciclagem?", "Processo de transformação de resíduos em novos produtos", "Descarte de resíduos em aterros sanitários", "Queima de resíduos para gerar energia", 1));
        perguntas.Add(new Pergunta("O que é a energia eólica?", "Energia gerada a partir do vento", "Energia gerada a partir do sol", "Energia gerada a partir da água", 1));
        perguntas.Add(new Pergunta("Qual é a principal consequência do derretimento das calotas polares?", "Elevação do nível do mar", "Diminuição das chuvas", "Aumento da biodiversidade", 1));
        perguntas.Add(new Pergunta("O que é o desequilíbrio ecológico?", "Alteração nas relações entre os seres vivos e o ambiente", "Aumento da produção de alimentos", "Aquecimento global", 1));
        perguntas.Add(new Pergunta("O que é a degradação ambiental?", "Processo de danos e perda da qualidade dos recursos naturais", "Processo de recuperação dos ecossistemas", "Uso sustentável dos recursos naturais", 1));
        perguntas.Add(new Pergunta("O que é a preservação ambiental?", "Ações para proteger e conservar os recursos naturais", "Aumento da produção industrial", "Exploração intensiva dos recursos naturais", 1));
        //B
        perguntas.Add(new Pergunta("O que é o efeito estufa?", "Fenômeno natural que mantém a temperatura da Terra adequada", "Aquecimento global causado pela ação humana", "Processo de resfriamento da atmosfera", 2));
        perguntas.Add(new Pergunta("Qual é o processo pelo qual o solo perde sua fertilidade natural?", "Desertificação", "Erosão", "Contaminação química", 2));
        perguntas.Add(new Pergunta("O que é a poluição do ar?",  "Presença de plásticos nos oceanos", "Presença de substâncias tóxicas na atmosfera", "Presença de resíduos químicos no solo", 2));
        perguntas.Add(new Pergunta("Qual das opções é uma fonte de energia renovável?", "Carvão mineral", "Energia solar", "Gás natural", 2));
        perguntas.Add(new Pergunta("Qual é a principal causa da poluição da água?", "Emissões de gases de efeito estufa", "Despejo de resíduos industriais e esgoto", "Extração excessiva de água subterrânea", 2));
        perguntas.Add(new Pergunta("Qual é o principal objetivo do desenvolvimento sustentável?", "Aumentar a produção industrial", "Atender às necessidades atuais sem comprometer as gerações futuras", "Explorar recursos naturais ilimitadamente", 2));
        perguntas.Add(new Pergunta("O que são áreas de preservação ambiental?", "Áreas destinadas à construção de indústrias","Áreas destinadas à conservação da natureza e proteção da biodiversidade",  "Áreas destinadas à exploração agrícola", 2));
        perguntas.Add(new Pergunta("O que são fontes de energia não renováveis?", "Fontes de energia solar e eólica", "Fontes que se esgotam ao longo do tempo, como petróleo e carvão", "Fontes de energia geradas pela natureza", 2));
        //C
        // perguntas.Add(new Pergunta("Qual desses animais NÃO está ameaçado de extinção?", "Tamanduá-bandeira", "Tatu-bola", "Capivara", 3));
        perguntas.Add(new Pergunta("Qual é o principal gás responsável pelo aquecimento global?", "Oxigênio", "Metano","Dióxido de carbono", 3));
        perguntas.Add(new Pergunta("Qual é a maior fonte de lixo plástico nos oceanos?", "Derramamento de petróleo", "Atividades de pesca","Descarte inadequado de resíduos plásticos", 3));
        perguntas.Add(new Pergunta("O que é a chuva ácida?", "Chuva carregada de poeira e areia", "Chuva com altas temperaturas", "Chuva com pH abaixo do normal devido à poluição",  3));
        perguntas.Add(new Pergunta("O que é a acidificação dos oceanos?", "Aumento da salinidade das águas oceânicas", "Aumento da temperatura média das águas oceânicas", "Aumento da acidez das águas oceânicas devido ao aumento de dióxido de carbono", 3));
        perguntas.Add(new Pergunta("Qual é a principal causa da destruição dos recifes de coral?", "Exploração excessiva de recursos marinhos", "Pesca predatória", "Aumento da temperatura da água e poluição", 3));
        perguntas.Add(new Pergunta("O que é a desertificação?", "Processo de transformação de desertos em áreas verdes", "Processo de erosão das margens dos rios", "Processo de degradação das terras secas, tornando-as áridas e improdutivas", 3));
        perguntas.Add(new Pergunta("Qual é a importância das abelhas para o meio ambiente?", "Controle de pragas agrícolas", "Produção de mel", "Polinização das plantas e manutenção da biodiversidade", 3));
        perguntas.Add(new Pergunta("O que são espécies invasoras?", "Espécies em risco de extinção", "Espécies que vivem exclusivamente em áreas protegidas", "Espécies não nativas que se estabelecem em ecossistemas e causam impactos negativos", 3));

        return perguntas;
    }

     private void Awake()
    {
        hpController = GetComponent<HealthAndPointsController>();
    }

    public Pergunta selectQuestion(List<Pergunta> lista)
    {
        indexPerguntaSelecionada = Random.Range(0, lista.Count);
        return lista[indexPerguntaSelecionada];
    }

    public void setTexts(Pergunta pgt)
    {
        enunciado.SetText(pgt.enunciado);
        alt1.SetText(pgt.alt1);
        alt2.SetText(pgt.alt2);
        alt3.SetText(pgt.alt3);
    }

    private void Start() {
        List<Pergunta> listaPerguntas = createData();
        setTexts(selectQuestion(listaPerguntas));
        quizMenuUI.SetActive(false);
    }

    public void OpenMenu()
    {
        List<Pergunta> listaPerguntas = createData();
        setTexts(selectQuestion(listaPerguntas));
        rightFeedback.SetActive(false);
        quizMenuUI.SetActive(true);
        wrongFeedback.SetActive(false);
        Time.timeScale = 0f;
        isPaused = true;

        option1Button.enabled = true;
        option2Button.enabled = true;
        option3Button.enabled = true;

        int respostaCerta = listaPerguntas[indexPerguntaSelecionada].resposta;
        Debug.Log(respostaCerta);

        option1Button.onClick.AddListener(() => CheckAnswer(1, respostaCerta));
        option2Button.onClick.AddListener(() => CheckAnswer(2, respostaCerta));
        option3Button.onClick.AddListener(() => CheckAnswer(3, respostaCerta));
        Debug.Log("hit");
    }

    public void CheckAnswer(int selectedOption, int resposta)
    {
        Debug.Log(selectedOption);
        Debug.Log("checando!!!!!!!");
        if (selectedOption == resposta)
        {
            hpController.AddPoint(1);
            rightFeedback.SetActive(true);
            wrongFeedback.SetActive(false);
            gotItRight = true;
            Debug.Log("certo!!!!!!!");
        }
        else{
            rightFeedback.SetActive(false);
            wrongFeedback.SetActive(true);
            Debug.Log("errado!!!!!!!");
            gotItRight = false;
        }

        option1Button.onClick.RemoveAllListeners();
        option2Button.onClick.RemoveAllListeners();
        option3Button.onClick.RemoveAllListeners();

        option1Button.enabled = false;
        option2Button.enabled = false;
        option3Button.enabled = false;
        StartCoroutine("WaitToClose");
 
    }

    public IEnumerator WaitToClose() {
        Debug.Log("waiting.....;");
        yield return new WaitForSecondsRealtime(2);
        if(gotItRight) BackToGame();
        else OpenMenu();
        Debug.Log("backing.....;");
    } 


    public void BackToGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        quizMenuUI.SetActive(false);
    }
}
