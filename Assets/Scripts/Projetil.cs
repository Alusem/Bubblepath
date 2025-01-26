using UnityEngine;

public class Projetil : MonoBehaviour
{
    public float velocidade = 10f; // Velocidade do projétil

    private void Start()
    {
        // Aplica a velocidade inicial ao Rigidbody do projétil
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = transform.forward * velocidade; // Corrigido de linearVelocity para velocity
        }
    }

    // Detecta a entrada do projétil em um trigger (como o inimigo)
    private void OnTriggerEnter2D(Collider2D other)
    {
	Debug.Log("Entrou no trigger");
        if (other.CompareTag("Inimigo"))
        {
		Debug.Log("Achou o inimigo");
            // Chama a função para reduzir a vida do inimigo
            VidaInimigo vidaInimigo = other.gameObject.GetComponent<VidaInimigo>();
            if (vidaInimigo != null)
            {
                vidaInimigo.PerderVida(20); // Exemplo de dano ao inimigo
            }

            // Destrói o projétil após atingir o inimigo
            Destroy(gameObject); // Isso destrói o projétil
        }
    }
}
