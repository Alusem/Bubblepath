using UnityEngine;
using System.Collections;

public class BossController : MonoBehaviour
{
    public GameObject projétilPrefab; // Prefab do projétil normal
    public GameObject grandeProjetilPrefab; // Prefab do projétil grande
    public float tempoEntreDisparosNormais = 1f; // Intervalo entre os disparos normais
    public float tempoEntreAtaqueEspecial = 5f; // Intervalo para o ataque especial (projétil grande)
    public float velocidadeProjetil = 10f; // Velocidade dos projéteis normais
    public float velocidadeGrandeProjetil = 5f; // Velocidade do projétil grande
    public float distanciaDoDisparo = 1f; // Distância horizontal dos pontos de disparo em relação ao centro do Boss

    public float velocidadeMovimento = 5f; // Velocidade do movimento do Boss
    public float limiteEsquerdo = -5f; // Limite esquerdo da área de movimento
    public float limiteDireito = 5f; // Limite direito da área de movimento

    public AudioClip bossMusic;

    private GameController gameController; // Referência ao GameController
    private Transform player; // Referência ao Transform do player
    private bool ataqueEspecialEmAndamento = false;
    private AudioManager audioManager;
    private Vector3 pontoAlvo; // Ponto de destino para o movimento do boss

    void Start()
    {
        // Busca o GameController na cena
        gameController = FindObjectOfType<GameController>();
        audioManager = FindObjectOfType<AudioManager>();
        audioManager.PlayMusic(bossMusic);

        // Obtém o Transform do player a partir do GameController
        if (gameController != null)
        {
            player = gameController.playerCena;
        }
        else
        {
            Debug.LogError("GameController não encontrado! Certifique-se de que ele está na cena.");
        }

        // Define o ponto inicial de destino
        pontoAlvo = new Vector3(limiteDireito, transform.position.y, transform.position.z);

        // Inicia as corrotinas de ataque
        StartCoroutine(DispararProjeteisNormais());
        StartCoroutine(AtaqueEspecial());
    }

    void Update()
    {
        // Move o boss entre os limites
        transform.position = Vector3.MoveTowards(transform.position, pontoAlvo, velocidadeMovimento * Time.deltaTime);

        // Verifica se o Boss atingiu o limite e inverte o ponto alvo
        if (transform.position.x <= limiteEsquerdo || transform.position.x >= limiteDireito)
        {
            pontoAlvo = (pontoAlvo.x == limiteDireito) ? new Vector3(limiteEsquerdo, transform.position.y, transform.position.z) : new Vector3(limiteDireito, transform.position.y, transform.position.z);
        }
    }

    IEnumerator DispararProjeteisNormais()
    {
        while (true)
        {
            if (!ataqueEspecialEmAndamento && player != null)
            {
                Vector3 posicaoEsquerda = transform.position + new Vector3(-distanciaDoDisparo, 0, 0);
                Vector3 posicaoDireita = transform.position + new Vector3(distanciaDoDisparo, 0, 0);

                DispararProjetil(posicaoEsquerda);
                DispararProjetil(posicaoDireita);
            }

            yield return new WaitForSeconds(tempoEntreDisparosNormais);
        }
    }

    void DispararProjetil(Vector3 pontoDeDisparo)
    {
        GameObject projétil = Instantiate(projétilPrefab, pontoDeDisparo, Quaternion.identity);
        Rigidbody rb = projétil.GetComponent<Rigidbody>();
        if (rb != null && player != null)
        {
            Vector3 direcaoDoProjétil = (player.position - pontoDeDisparo).normalized;
            rb.linearVelocity = direcaoDoProjétil * velocidadeProjetil; // Corrigido de linearVelocity para velocity
        }
    }

    IEnumerator AtaqueEspecial()
    {
        while (true)
        {
            yield return new WaitForSeconds(tempoEntreAtaqueEspecial);

            ataqueEspecialEmAndamento = true;

            if (player != null)
            {
                DispararGrandeProjetil();
            }

            ataqueEspecialEmAndamento = false;
        }
    }

    void DispararGrandeProjetil()
    {
        GameObject grandeProjetil = Instantiate(grandeProjetilPrefab, transform.position, Quaternion.identity);
        Rigidbody rb = grandeProjetil.GetComponent<Rigidbody>();
        if (rb != null && player != null)
        {
            Vector3 direcao = (player.position - transform.position).normalized;
            rb.linearVelocity = direcao * velocidadeGrandeProjetil;
        }
    }
}