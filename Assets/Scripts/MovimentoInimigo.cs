using UnityEngine;

public class MovimentoInimigo : MonoBehaviour
{
    public Transform personagem; // Referência para o personagem
    public float velocidade = 3f; // Velocidade de movimento do inimigo
    public int dano = 10; // Dano que o inimigo causa ao personagem

    private void Update()
    {
        // Se o personagem estiver presente e se ele tiver a tag "Player"
        if (personagem != null && personagem.CompareTag("Player"))
        {
            // Movimenta o inimigo em direção ao personagem
            transform.position = Vector3.MoveTowards(transform.position, personagem.position, velocidade * Time.deltaTime);
        }
    }

    // Detecta a entrada do projétil em um trigger (como o inimigo)
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Entrou no trigger dano inimigo no player");

        if (other.CompareTag("Player"))
        {
            Debug.Log("Deu dano no player");

            // Obtém o componente VidaPersonagem do player
            VidaPersonagem vida = other.gameObject.GetComponent<VidaPersonagem>();
            if (vida != null)
            {
                // Verifica se o dano está sendo aplicado
                Debug.Log("Aplicando dano ao player: " + dano);
                vida.PerderVida(dano); // Aplica dano ao player
            }
            else
            {
                Debug.Log("Componente VidaPersonagem não encontrado!");
            }
        }
    }
}
