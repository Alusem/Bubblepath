using UnityEngine;

public class Disparo : MonoBehaviour
{
    public GameObject projetilPrefab; // Referência ao prefab do projétil
    public Transform pontoDeDisparo;  // Ponto onde o projétil será disparado
    public float velocidadeDoProjetil = 10f; // Velocidade do projétil
    public float intervaloDeDisparo = 0.5f; // Intervalo entre disparos

    private float tempoDesdeUltimoDisparo = 0f; // Tempo desde o último disparo

    private void Update()
    {
        // Verifica se o jogador pressionou a tecla de disparo (Ex: Espaço)
        if (Input.GetKey(KeyCode.Space) && Time.time > tempoDesdeUltimoDisparo + intervaloDeDisparo)
        {
            DispararProjetil();
            tempoDesdeUltimoDisparo = Time.time; // Reseta o tempo de disparo
        }
    }

    // Função para disparar o projétil
    void DispararProjetil()
    {
        if (projetilPrefab != null && pontoDeDisparo != null)
        {
            // Cria o projétil na posição do ponto de disparo
            GameObject projetil = Instantiate(projetilPrefab, pontoDeDisparo.position, pontoDeDisparo.rotation);

            // Adiciona uma força no projétil para que ele se mova
            Rigidbody rb = projetil.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = pontoDeDisparo.forward * velocidadeDoProjetil; // Corrigido de linearVelocity para velocity
            }
        }
    }
}
