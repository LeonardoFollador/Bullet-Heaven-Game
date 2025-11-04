using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class BotaoNovaPartida : MonoBehaviour
{
    public Button botaoNovaPartida;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (botaoNovaPartida != null)
            botaoNovaPartida.onClick.AddListener(RedirecionarParaTelaInicial);
    }


    void RedirecionarParaTelaInicial()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
