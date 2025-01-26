using UnityEngine;
using UnityEngine.UI; // Para usar a UI de vida, caso precise

public class VidaPersonagem : MonoBehaviour
{
    public int vida = 100; // Vida inicial do personagem
    public Slider barraDeVida; // Barra de vida na UI (opcional)
    public Text textoVida; // Texto que exibe a vida na UI (opcional)

    private void Start()
    {
        // Inicializa a UI, se necessário
        if (barraDeVida != null)
        {
            barraDeVida.maxValue = vida;
            barraDeVida.value = vida;
        }
    }

    public void PerderVida(int dano)
    {
        Debug.Log("Perdendo vida: " + dano); // Verifica se o dano é passado corretamente
        vida -= dano;
        if (vida <= 0)
        {
            VidaMorta();
        }

        // Atualiza a barra de vida na UI
        if (barraDeVida != null)
        {
            barraDeVida.value = vida;
        }

        // Atualiza o texto da vida, se houver
        if (textoVida != null)
        {
            textoVida.text = "Vida: " + vida;
        }
    }

    private void VidaMorta()
    {
        // Aqui você pode implementar o que acontece quando o personagem morre (ex: reiniciar a cena, exibir game over, etc.)
        Debug.Log("Personagem morreu!");
        // Por exemplo, pode destruir o personagem:
        Destroy(gameObject);
    }
}
