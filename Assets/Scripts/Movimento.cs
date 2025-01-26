using UnityEngine;

public class MovimentoSubmarino : MonoBehaviour
{
    public float velocidade = 5f; // Velocidade do movimento
    public float sensibilidadeVertical = 3f; // Sensibilidade para mover para cima/baixo

    private void Update()
    {
        // Variáveis para capturar as teclas pressionadas
        float movimentoHorizontal = 0f;
        float movimentoVertical = 0f;

        // Captura os inputs de movimento horizontal (setas esquerda/direita)
        if (Input.GetKey(KeyCode.LeftArrow)) // Se pressionar seta para a esquerda
        {
            movimentoHorizontal = -1f;
        }
        if (Input.GetKey(KeyCode.RightArrow)) // Se pressionar seta para a direita
        {
            movimentoHorizontal = 1f;
        }

        // Captura os inputs de movimento vertical (setas cima/baixo)
        if (Input.GetKey(KeyCode.UpArrow)) // Se pressionar seta para cima, sobe
        {
            transform.Translate(Vector3.up * sensibilidadeVertical * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.DownArrow)) // Se pressionar seta para baixo, desce
        {
            transform.Translate(Vector3.down * sensibilidadeVertical * Time.deltaTime);
        }

        // Movimento para frente e para trás (em Z)
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
        {
            movimentoVertical = 0f;
        }

        // Aplica o movimento para frente (Z) e lateral (X)
        Vector3 movimentoFrontal = transform.forward * movimentoVertical * velocidade * Time.deltaTime;
        Vector3 movimentoLateral = transform.right * movimentoHorizontal * velocidade * Time.deltaTime;

        // Aplica o movimento final
        transform.Translate(movimentoFrontal + movimentoLateral);
    }
}
