using UnityEngine;

public class VidaInimigo : MonoBehaviour
{
    public int vida = 50; // Vida inicial do inimigo
    public AudioClip somDeDano; // Som ao tomar dano
    public AudioClip somDeMorte; // Som ao morrer
    private AudioManager audioManager; // Referência ao AudioManager
    private GameController gameController;

    private void Start()
    {
        // Encontra o AudioManager na cena
        audioManager = FindObjectOfType<AudioManager>();
        gameController = FindObjectOfType<GameController>();
        if (audioManager == null)
        {
            Debug.LogError("AudioManager não encontrado!");
        }
    }

    // Função para reduzir a vida do inimigo
    public void PerderVida(int dano)
    {
        vida -= dano; // Reduz a vida do inimigo pelo dano recebido
        Debug.Log("Vida do inimigo: " + vida); // Exibe a vida atual do inimigo

        // Toca o som de dano
        if (somDeDano != null && audioManager != null)
        {
            audioManager.PlaySound(somDeDano);
        }

        // Verifica se a vida do inimigo chegou a 0 ou menos
        if (vida <= 0)
        {
            Morrer(); // Chama a função de morte do inimigo
        }
    }

    // Função chamada quando o inimigo morre
    private void Morrer()
    {
        // Toca o som de morte
        if (somDeMorte != null && audioManager != null)
        {
            audioManager.PlaySound(somDeMorte);
        }

        gameController.AddCoins(1);

        // Aqui você pode adicionar efeitos, animações ou outros comportamentos ao inimigo antes de morrer
        Debug.Log("Inimigo morreu!");
        Destroy(gameObject); // Destroi o objeto inimigo
    }
}
