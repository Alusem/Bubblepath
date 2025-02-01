using UnityEngine;
using UnityEngine.UI; // Para usar a UI de vida
using TMPro;

public class VidaPersonagem : MonoBehaviour
{
    public int vida = 100; // Vida inicial do personagem
    public int numMortes = 0; // Vezes que o personagem já morreu
    public Slider barraDeVida; // Barra de vida na UI (opcional)
    public TMP_Text textoVida; // Texto que exibe a vida na UI (opcional)
    public AudioClip somDeDano; // Som ao perder vida

    private GameController gameController; // Referência ao GameController
    public SceneController sceneController; // Referência ao SceneController
    private AudioManager audioManager; // Referência ao AudioManager

    public GameObject OptionsPanel; // Painel de opções exibido ao morrer

    private void Start()
    {
        numMortes = PlayerPrefs.GetInt("Mortes", 0);

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
        Debug.Log("Perdendo vida: " + dano);
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
        Debug.Log("Personagem morreu!");

        gameObject.SetActive(false);

        numMortes++;
        PlayerPrefs.SetInt("Mortes", numMortes);

        // Exibe o painel de opções
        if (OptionsPanel != null)
        {
            OptionsPanel.SetActive(true);
        }
    }

    public void ResetarGame()
    {
        if (sceneController != null)
        {
            // Reativa o objeto do jogador e redefine seus atributos
            gameObject.SetActive(true);
            vida = 100;

            if (barraDeVida != null)
            {
                barraDeVida.value = vida;
            }

            if (textoVida != null)
            {
                textoVida.text = "Vida: " + vida;
            }

            // Reinicia o jogo
            sceneController.ReiniciarJogo();
        }
    }

    public void ResetarProgresso()
    {
        PlayerPrefs.DeleteAll(); // Remove todos os dados salvos

        if (gameController != null)
        {
            gameController.coins = 0;
            gameController.powerUpMultiplier = 1;

            // Atualiza a UI
            if (gameController.textoMoeda != null)
            {
                gameController.textoMoeda.text = $"Moeda: {gameController.coins}";
            }
        }

        Debug.Log("Progresso resetado.");
    }

    private void Update()
    {
        if (Input.GetKeyDown("r"))
        {
            PlayerPrefs.DeleteAll();
            Debug.Log("PlayerPrefs resetados.");
        }
    }
}