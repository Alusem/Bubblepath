using UnityEngine;
using UnityEngine.SceneManagement; // Adiciona esta linha para usar o SceneManager

public class SceneController: MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Função que reinicia a cena quando o jogador morre
    public void ReiniciarJogo()
    {
        // Carrega novamente a cena atual
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToScene(string SceneName){
        SceneManager.LoadScene(SceneName);
    }

    public void Quit(){
        Application.Quit();
    }

}
