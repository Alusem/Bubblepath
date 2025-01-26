using UnityEngine;

public class VidaInimigo : MonoBehaviour
{
    public int vida = 50; // Vida inicial do inimigo

    // Função para reduzir a vida do inimigo
    public void PerderVida(int dano)
    {
        vida -= dano; // Reduz a vida do inimigo pelo dano recebido
        Debug.Log("Vida do inimigo: " + vida); // Exibe a vida atual do inimigo

        // Verifica se a vida do inimigo chegou a 0 ou menos
        if (vida <= 0)
        {
            Morrer(); // Chama a função de morte do inimigo
        }
    }

    // Função chamada quando o inimigo morre
    private void Morrer()
    {
        // Aqui você pode adicionar efeitos, animações ou outros comportamentos ao inimigo antes de morrer
        Debug.Log("Inimigo morreu!");
        Destroy(gameObject); // Destroi o objeto inimigo
    }
}
