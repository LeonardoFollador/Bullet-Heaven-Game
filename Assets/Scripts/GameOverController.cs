using UnityEngine;
using TMPro;

public class GameOverController : MonoBehaviour
{
    public TextMeshProUGUI scoreValue;
    private int score;
    public GameObject gameOverCanvas;
    private bool isGameOver = false;
    public DatabaseManager dbManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Começa com o GameOver Canvas oculto
        gameOverCanvas.SetActive(false);
        isGameOver = false;
    }

    public void ShowGameOverUI()
    {
        Debug.Log("Mostrando a tela");
        gameOverCanvas.SetActive(true);
        score = ScoreController.getScore();
        scoreValue.text = "" + score;
        ScoreController.restartScore();

        string nomeJogador = PlayerPrefs.GetString("NomeJogador", "Jogador");
        dbManager.InsertHistory(nomeJogador, score);

    }
}
