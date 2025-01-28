using UnityEngine;
using UnityEngine.UI; // Para usar a UI de vida
using TMPro;

public class VidaPersonagem : MonoBehaviour
{
    public int vida = 100 ; // Vida inicial do personagem
    public int numMortes = 0; // Vezes que o personagem ja morreu
    public Slider barraDeVida; // Barra de vida na UI (opcional)
    public TMP_Text textoVida; // Texto que exibe a vida na UI (opcional)
    public AudioClip somDeDano; // Som ao perder vida

    private GameController gameController; // Referência ao GameController
    public SceneController sceneController; // Referência ao SceneController
    private AudioManager audioManager; // Referência ao AudioManager

    public GameObject OptionsPanel;

    private void Start()
    {

        numMortes = PlayerPrefs.GetInt("Mortes");
        // Inicializa a UI, se necessário
        if (barraDeVida != null)
        {
            barraDeVida.maxValue = vida;
            barraDeVida.value = vida;
        }

        if (textoVida != null)
        {
            textoVida.text = $"Vida: {vida}";
        }

        // Encontra o GameController na cena
        gameController = FindObjectOfType<GameController>();
        if (gameController == null)
        {
            Debug.LogError("GameController não encontrado!");
        }

        // Encontra o AudioManager na cena
        audioManager = FindObjectOfType<AudioManager>();
        if (audioManager == null)
        {
            Debug.LogError("AudioManager não encontrado!");
        }
    }

    public void PerderVida(int dano)
    {
        Debug.Log("Perdendo vida: " + dano); // Verifica se o dano é passado corretamente
        vida -= dano;

        // Toca o som de dano, se disponível
        if (somDeDano != null && audioManager != null)
        {
            audioManager.PlaySound(somDeDano);
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

        // Verifica se a vida acabou
        if (vida <= 0)
        {
            VidaMorta();
        }
    }

    private void VidaMorta()
    {
        // Aqui você pode implementar o que acontece quando o personagem morre
        Debug.Log("Personagem morreu!");

        numMortes++;
        PlayerPrefs.SetInt("Mortes", numMortes);
        // Chama a função do SceneController para reiniciar o jogo
        OptionsPanel.SetActive(true);
        
    }

    public void ResetarGame(){
        if (sceneController != null)
        {
            sceneController.ReiniciarJogo();
        }
    }

    private void Update(){
        if(Input.GetKeyDown("r")){
            PlayerPrefs.DeleteAll();
        }
    }
}
