using UnityEngine;
using System.Collections;
using TMPro;

public class GameController : MonoBehaviour
{
    public GameObject[] inimigosPrefabs; // Array com os 4 tipos de inimigos
    public Transform playerCena; // Referência do player
    public GameObject bossPrefab; // Prefab do boss final
    public Transform[] pontosDeSpawn; // Posições onde os inimigos irão aparecer
    public float tempoEntreOndas = 5f; // Tempo entre cada onda de inimigos
    public int inimigosPorOnda = 5; // Número inicial de inimigos por onda
    public int incrementoPorOnda = 2; // Quantidade de inimigos a serem adicionados a cada nova onda
    public float tempoParaBoss = 60f; // Tempo em segundos para o boss aparecer (1 minuto)
    public Transform pontoDeSpawnBoss; // Ponto específico para o boss aparecer (se necessário)
    public TMP_Text textoMoeda; // Texto que exibe a vida na UI (opcional)
    public TMP_Text textoPowerUp; // Texto que exibe a vida na UI (opcional)


    public VidaPersonagem lifeController;

    public int coins;
    public int powerUpMultiplier = 1;

    private bool bossApareceu = false; // Flag para verificar se o boss já apareceu

    void Start()
    {
        // Inicia a geração das ondas de inimigos
        StartCoroutine(GerarOndasInimigos());

        powerUpMultiplier = PlayerPrefs.GetInt("PowerUp");

        // Inicia a contagem para o aparecimento do boss
        StartCoroutine(AguardarAparicaoDoBoss());
    }

    void Update(){
        if (textoMoeda != null)
        {
            textoMoeda.text = $"Moeda: {coins}";
        }
        if (textoPowerUp != null)
        {
            textoPowerUp.text = $"Power Up: {powerUpMultiplier}";
        }
    }

    // Função para gerar as ondas de inimigos
    IEnumerator GerarOndasInimigos()
    {
        while (true)
        {
            if (pontosDeSpawn.Length > 0) // Verifique se o array de pontos de spawn não está vazio
            {
                // Gera inimigos na quantidade definida para aquela onda
                for (int i = 0; i < inimigosPorOnda; i++)
                {
                    // Seleciona um ponto aleatório de spawn
                    Transform pontoDeSpawn = pontosDeSpawn[Random.Range(0, pontosDeSpawn.Length)];

                    // Aqui, selecionamos aleatoriamente um inimigo para aquele ponto de spawn
                    GameObject inimigo = Instantiate(inimigosPrefabs[Random.Range(0, inimigosPrefabs.Length)], pontoDeSpawn.position, pontoDeSpawn.rotation);

                    // Configura o inimigo para seguir o player
                    MovimentoInimigo movimentoInimigo = inimigo.GetComponent<MovimentoInimigo>();
                    if (movimentoInimigo != null)
                    {
                        movimentoInimigo.personagem = playerCena; // Define o jogador como alvo do inimigo
                    }
                }

                // Incrementa a quantidade de inimigos para a próxima onda
                inimigosPorOnda += incrementoPorOnda;
            }
            else
            {
                Debug.LogError("Nenhum ponto de spawn definido! Por favor, adicione pontos de spawn no GameController.");
            }

            // Espera o tempo entre as ondas antes de gerar a próxima onda
            yield return new WaitForSeconds(tempoEntreOndas);
        }
    }

    // Função para aguardar o tempo e depois aparecer o boss
    IEnumerator AguardarAparicaoDoBoss()
    {
        // Espera o tempo necessário para o boss aparecer
        yield return new WaitForSeconds(tempoParaBoss);

        // Se o boss ainda não apareceu, gera o boss
        if (!bossApareceu)
        {
            // Verifica se o ponto de spawn do boss foi configurado
            if (pontoDeSpawnBoss != null)
            {
                // Instancia o boss no ponto de spawn configurado
                Instantiate(bossPrefab, pontoDeSpawnBoss.position, pontoDeSpawnBoss.rotation);
            }
            else
            {
                // Caso não tenha ponto de spawn definido, instancia o boss no centro da tela
                Instantiate(bossPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            }

            // Marca que o boss foi gerado
            bossApareceu = true;

            // Exibe mensagem no console para informar que o boss apareceu
            Debug.Log("O Boss Final Apareceu!");
        }
    }

    public void AddCoins(int coinsToAdd)
    {
        coins += coinsToAdd;
        PlayerPrefs.SetInt("Moedas", coins); // Salva o valor atual das moedas
    }

    public void BuyPowerUp()
    {
        int custoPowerUp = 10; // Define o custo fixo de cada power-up

        // Verifica quantos power-ups o jogador pode comprar
        int quantidadeComprada = coins / custoPowerUp;

        if (quantidadeComprada > 0)
        {
            // Deduz o número de moedas proporcional aos power-ups comprados
            coins -= quantidadeComprada * custoPowerUp;

            // Incrementa o multiplicador de power-ups proporcionalmente
            powerUpMultiplier += quantidadeComprada;

            // Salva o novo multiplicador nos PlayerPrefs
            int currentPowerUp = PlayerPrefs.GetInt("PowerUp");
            currentPowerUp += quantidadeComprada;
            PlayerPrefs.SetInt("PowerUp", currentPowerUp);

            // Atualiza a UI (opcional, se estiver configurada)
            if (textoPowerUp != null)
            {
                textoPowerUp.text = $"Power Up: {powerUpMultiplier}";
            }

            Debug.Log($"Comprado {quantidadeComprada} Power-Up(s). Novo multiplicador: {powerUpMultiplier}. Moedas restantes: {coins}");
        }
        else
        {
            Debug.Log("Moedas insuficientes para comprar um Power-Up!");
        }
    }
}