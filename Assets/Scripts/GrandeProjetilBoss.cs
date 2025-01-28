using UnityEngine;

public class GrandeProjetilBoss : MonoBehaviour
{
    public float danoGrande = 50f; // Dano causado pelo projétil grande
    public float tempoDeVida = 7f; // Tempo de vida do projétil grande, caso não atinja o jogador

    private void Start()
    {
        // Destroi o projétil após o tempo de vida
        Destroy(gameObject, tempoDeVida);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica se o projétil grande colidiu com o jogador
        if (other.CompareTag("Player"))
        {
            // Chama a função de perder vida no jogador
            VidaPersonagem vidaPersonagem = other.GetComponent<VidaPersonagem>();
            if (vidaPersonagem != null)
            {
                vidaPersonagem.PerderVida((int)danoGrande); // Aplica o dano ao player
            }

            // Destrói o projétil após causar o dano
            Destroy(gameObject);
        }
    }
}
