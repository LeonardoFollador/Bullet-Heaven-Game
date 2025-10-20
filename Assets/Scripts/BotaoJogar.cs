using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BotaoJogar : MonoBehaviour
{
    public Button botaoJogar; // arraste o botão "Jogar" no Inspector

    void Start()
    {
        // Garante que temos um botão referenciado
        if (botaoJogar != null)
        {
            botaoJogar.onClick.AddListener(IniciarJogo);
        }
        else
        {
            Debug.LogWarning("Botão 'Jogar' não foi atribuído no Inspector.");
        }
    }

    void IniciarJogo()
    {
        SceneManager.LoadScene("Level01");
    }
}

