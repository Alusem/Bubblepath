using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
    public GameObject inimigoPrefab; // Prefab do inimigo
    public Transform playerCena; // Referência do player
    public GameObject bossPrefab; // Prefab do boss final
    public Transform[] pontosDeSpawn; // Posições onde os inimigos irão aparecer
    public float tempoEntreOndas = 5f; // Tempo entre cada onda de inimigos
    public int inimigosPorOnda = 5; // Número de inimigos por onda
    public float tempoParaBoss = 60f; // Tempo em segundos para o boss aparecer (1 minuto)
    public Transform pontoDeSpawnBoss; // Ponto específico para o boss aparecer (se necessário)

    private bool bossApareceu = false; // Flag para verificar se o boss já apareceu

    void Start()
    {
        // Inicia a geração das ondas de inimigos
        StartCoroutine(GerarOndasInimigos());
        
        // Inicia a contagem para o aparecimento do boss
        StartCoroutine(AguardarAparicaoDoBoss());
    }

    // Função para gerar as ondas de inimigos
    IEnumerator GerarOndasInimigos()
    {
        while (true)
        {
            if (pontosDeSpawn.Length > 0) // Verifique se o array de pontos de spawn não está vazio
            {
                // Gera inimigos em cada ponto de spawn
                for (int i = 0; i < inimigosPorOnda; i++)
                {
                    // Seleciona um ponto aleatório de spawn
                    Transform pontoDeSpawn = pontosDeSpawn[Random.Range(0, pontosDeSpawn.Length)];
                    
                    // Instancia o inimigo no ponto de spawn escolhido
                    GameObject inimigo = Instantiate(inimigoPrefab, pontoDeSpawn.position, pontoDeSpawn.rotation);

                    // Configura o inimigo para seguir o player
                    MovimentoInimigo movimentoInimigo = inimigo.GetComponent<MovimentoInimigo>();
                    if (movimentoInimigo != null)
                    {
                        movimentoInimigo.personagem = playerCena; // Define o jogador como alvo do inimigo
                    }
                }
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
}
