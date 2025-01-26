using UnityEngine;

public class DestruirProjetilAoSairDaTela : MonoBehaviour
{
    public Camera cameraPrincipal;  // Referência à câmera principal

    private void Update()
    {
        // Encontra todos os projéteis na cena
        GameObject[] projeteis = GameObject.FindGameObjectsWithTag("Projetil");

        // Verifica se algum projétil está fora da área visível da câmera
        foreach (GameObject projetil in projeteis)
        {
            // Obtém a posição do projétil na tela (viewport)
            Vector3 viewportPos = cameraPrincipal.WorldToViewportPoint(projetil.transform.position);

            // Verifica se o projétil está fora da tela (fora da área 0 a 1)
            if (viewportPos.x < 0 || viewportPos.x > 1 || viewportPos.y < 0 || viewportPos.y > 1)
            {
                // Se estiver fora da tela, destrua o projétil
                Destroy(projetil);
            }
        }
    }
}
