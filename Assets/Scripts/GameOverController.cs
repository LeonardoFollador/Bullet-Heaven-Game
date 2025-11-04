using UnityEngine;
using TMPro;

public class GameOverController : MonoBehaviour
{
    public TextMeshProUGUI scoreValue;
    public GameObject gameOverCanvas;
    private bool isGameOver = false;
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
        scoreValue.text = "" + ScoreController.getScore();
        ScoreController.restartScore();
    }
}
