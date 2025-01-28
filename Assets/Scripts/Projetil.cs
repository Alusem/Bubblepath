using UnityEngine;

public class Projetil : MonoBehaviour
{
    public float velocidade = 10f; // Velocidade do projétil
    public AudioClip somAtingiuInimigo; // Som ao atingir um inimigo
    public AudioClip somAtingiuBoss; // Som ao atingir o Boss
    private AudioManager audioManager; // Referência ao AudioManager
    private GameController gameController;

    private void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = transform.forward * velocidade; // Define a velocidade do projétil
        }

        // Busca o AudioManager na cena
        audioManager = FindObjectOfType<AudioManager>();
        gameController = FindObjectOfType<GameController>();

        if (audioManager == null)
        {
            Debug.LogError("AudioManager não encontrado!");
        }
    }

    // Detecta a entrada do projétil em um trigger (como o inimigo ou Boss)
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Projétil entrou no trigger");

        if (other.CompareTag("Inimigo"))
        {
            Debug.Log("Atingiu um inimigo");

            // Toca o som ao atingir o inimigo
            if (somAtingiuInimigo != null && audioManager != null)
            {
                audioManager.PlaySound(somAtingiuInimigo);
            }

            // Reduz a vida do inimigo
            VidaInimigo vidaInimigo = other.GetComponent<VidaInimigo>();
            if (vidaInimigo != null)
            {
                vidaInimigo.PerderVida(10+ gameController.powerUpMultiplier); // Exemplo de dano
            }

            Destroy(gameObject); // Destrói o projétil após o impacto
        }
        else if (other.CompareTag("Boss"))
        {
            Debug.Log("Atingiu o Boss");

            // Toca o som ao atingir o Boss
            if (somAtingiuBoss != null && audioManager != null)
            {
                audioManager.PlaySound(somAtingiuBoss);
            }

            // Reduz a vida do Boss
            VidaBoss vidaBoss = other.GetComponent<VidaBoss>();
            if (vidaBoss != null)
            {
                vidaBoss.PerderVida(20 + gameController.powerUpMultiplier); // Exemplo de dano
            }

            Destroy(gameObject); // Destrói o projétil após o impacto
        }
    }
}
