using UnityEngine;

public class ProjetilBoss : MonoBehaviour
{
    public float dano = 20f; // Dano causado pelo projétil normal
    public float tempoDeVida = 5f; // Tempo de vida do projétil, caso não atinja o jogador

    private void Start()
    {
        // Destroi o projétil após o tempo de vida
        Destroy(gameObject, tempoDeVida);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica se o projétil colidiu com o jogador
        if (other.CompareTag("Player"))
        {
            // Chama a função de perder vida no jogador
            VidaPersonagem vidaPersonagem = other.GetComponent<VidaPersonagem>();
            if (vidaPersonagem != null)
            {
                vidaPersonagem.PerderVida((int)dano); // Aplica o dano ao player
            }

            // Destrói o projétil após causar o dano
            Destroy(gameObject);
        }
    }
}
