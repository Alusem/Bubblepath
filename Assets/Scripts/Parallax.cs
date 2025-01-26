using UnityEngine;

public class Parallax : MonoBehaviour
{
    public float velocidadeMovimento = 2f; // Velocidade de movimento do cenário
    public float alturaLimite = -10f; // Limite inferior, onde o fundo será reposicionado
    public Vector3 deslocamentoInicial; // Para armazenar a posição inicial do fundo

    private void Start()
    {
        // Armazena a posição inicial do fundo
        deslocamentoInicial = transform.position;
    }

    private void Update()
    {
        // Move o fundo de cima para baixo
        transform.Translate(Vector3.down * velocidadeMovimento * Time.deltaTime);

        // Verifica se o fundo passou do limite inferior
        if (transform.position.y <= alturaLimite)
        {
            // Reposiciona o fundo para a parte superior da tela, criando o efeito de loop
            transform.position = deslocamentoInicial;
        }
    }
}
