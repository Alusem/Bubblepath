using UnityEngine;

public class Disparo : MonoBehaviour
{
    public GameObject projetilPrefab; // Referência ao prefab do projétil
    public Transform pontoDeDisparo; // Ponto onde o projétil será disparado
    public float velocidadeDoProjetil = 10f; // Velocidade do projétil
    public float intervaloDeDisparo = 0.5f; // Intervalo entre disparos
    public AudioClip somDeDisparo; // Som do disparo

    private float tempoDesdeUltimoDisparo = 0f; // Tempo desde o último disparo
    private AudioManager audioManager; // Referência ao AudioManager

    private void Start()
    {
        // Procura o AudioManager na cena
        audioManager = FindObjectOfType<AudioManager>();
        
        if (audioManager == null)
        {
            Debug.LogError("AudioManager não encontrado na cena!");
        }
    }

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
            GameObject projetil = Instantiate(projetilPrefab, pontoDeDisparo.position, Quaternion.identity);

            // Adiciona uma força no projétil para que ele se mova
            Rigidbody rb = projetil.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = pontoDeDisparo.forward * velocidadeDoProjetil;
            }

            // Toca o som do disparo
            if (somDeDisparo != null && audioManager != null)
            {
                audioManager.PlaySound(somDeDisparo);
            }
        }
    }
}