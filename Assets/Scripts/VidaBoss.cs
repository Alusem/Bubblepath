using UnityEngine;
using System.Collections;

public class VidaBoss : MonoBehaviour
{
    public int vida = 200; // Vida inicial do Boss
    private SpriteRenderer m_spriteRenderer;
    public Color m_damageColor;

    private SceneController sceneController;    

    private void Start(){
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        sceneController = FindObjectOfType<SceneController>();

    }

    // Função para reduzir a vida do Boss
    public void PerderVida(int dano)
    {

        m_spriteRenderer.material.color = m_damageColor;
        vida -= dano; // Reduz a vida do Boss pelo dano recebido
        Debug.Log("Vida do Boss: " + vida); // Exibe a vida atual do Boss

        StartCoroutine(DamageCooldown());
        // Verifica se a vida do Boss chegou a 0 ou menos
        if (vida <= 0)
        {
            Morrer(); // Chama a função de morte do Boss
        }
    }

    // Função chamada quando o Boss morre
    private void Morrer()
    {
        Debug.Log("Boss morreu!"); // Mensagem de morte do Boss
        Destroy(gameObject); // Remove o Boss da cena

        // Carrega a cena de vitória
        sceneController.GoToScene("CenaVitoria"); // Substitua "CenaVitoria" pelo nome exato da cena de vitória
    }

    private IEnumerator DamageCooldown(){

        yield return new WaitForSeconds(0.5f);
            m_spriteRenderer.material.color = Color.white;

    }
}
