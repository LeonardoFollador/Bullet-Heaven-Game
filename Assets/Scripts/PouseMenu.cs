using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausePanel; // aqui tu arrasta no Inspector o painel que vai aparecer
    private bool isPaused = false;

    // Função que o botão vai chamar
    public void TogglePause()
    {
        if (isPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    void PauseGame()
    {
        pausePanel.SetActive(true);  // mostra o menu
        Time.timeScale = 0f;         // congela o jogo
        isPaused = true;
    }

    void ResumeGame()
    {
        pausePanel.SetActive(false); // esconde o menu
        Time.timeScale = 1f;         // volta o jogo
        isPaused = false;
    }
}
