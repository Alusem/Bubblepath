using UnityEngine;
using System.Collections;

public class BossController : MonoBehaviour
{
    public GameObject projétilPrefab; // Prefab do projétil normal
    public GameObject grandeProjetilPrefab; // Prefab do projétil grande
    public Transform player; // Referência para o jogador
    public float tempoEntreDisparosNormais = 1f; // Intervalo entre os disparos normais
    public float tempoEntreAtaqueEspecial = 5f; // Intervalo para o ataque especial (projétil grande)
    public float velocidadeProjetil = 10f; // Velocidade dos projéteis normais
    public float velocidadeGrandeProjetil = 5f; // Velocidade do projétil grande
    public float dano = 20f; // Dano do projétil normal
    public float danoGrandeProjetil = 50f; // Dano do projétil grande

    public float distanciaDoDisparo = 1f; // Distância horizontal dos pontos de disparo em relação ao centro do Boss

    private bool ataqueEspecialEmAndamento = false;

    void Start()
    {
        // Inicia a corrotina para disparar os projéteis normais
        StartCoroutine(DispararProjeteisNormais());
        // Inicia a corrotina para o ataque especial
        StartCoroutine(AtaqueEspecial());
    }

    // Corrotina para disparar projéteis normais
    IEnumerator DispararProjeteisNormais()
    {
        while (true)
        {
            if (!ataqueEspecialEmAndamento)
            {
                // Dispara projéteis de cada ponto de disparo
                DispararProjetilNaExtremidade(-1); // Disparo à esquerda
                DispararProjetilNaExtremidade(1);  // Disparo à direita
            }

            // Espera o tempo entre os disparos
            yield return new WaitForSeconds(tempoEntreDisparosNormais);
        }
    }

    // Função para disparar um projétil de uma extremidade do Boss
    void DispararProjetilNaExtremidade(int direcao)
    {
        // Calcula a posição do disparo à esquerda ou à direita do Boss
        Vector3 pontoDeDisparo = transform.position + new Vector3(direcao * distanciaDoDisparo, 0, 0);

        // Instancia o projétil no ponto calculado
        GameObject projétil = Instantiate(projétilPrefab, pontoDeDisparo, Quaternion.identity);
        Rigidbody rb = projétil.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // Direciona o projétil na direção do jogador
            Vector3 direcaoDoProjétil = (player.position - pontoDeDisparo).normalized;
            rb.linearVelocity = direcaoDoProjétil * velocidadeProjetil; // Corrigido para velocidadeProjetil
        }
    }

    // Corrotina para o ataque especial
    IEnumerator AtaqueEspecial()
    {
        while (true)
        {
            // Espera o tempo definido para o ataque especial
            yield return new WaitForSeconds(tempoEntreAtaqueEspecial);

            ataqueEspecialEmAndamento = true; // Inicia o ataque especial

            // Lógica para disparar um grande projétil
            DispararGrandeProjetil();

            // Aguarda um intervalo antes de permitir outro ataque especial
            yield return new WaitForSeconds(tempoEntreAtaqueEspecial);

            ataqueEspecialEmAndamento = false; // Finaliza o ataque especial
        }
    }

    // Função para disparar um grande projétil
    void DispararGrandeProjetil()
    {
        GameObject grandeProjetil = Instantiate(grandeProjetilPrefab, transform.position, Quaternion.identity);
        Rigidbody rb = grandeProjetil.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // Direciona o grande projétil na direção do jogador
            Vector3 direcao = (player.position - transform.position).normalized;
            rb.linearVelocity = direcao * velocidadeGrandeProjetil; // Corrigido para velocidadeGrandeProjetil
        }
    }
}
