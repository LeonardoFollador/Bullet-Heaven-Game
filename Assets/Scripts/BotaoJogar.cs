using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class BotaoJogar : MonoBehaviour
{
    public Button botaoJogar;
    public TMP_InputField inputNome;

    void Start()
    {
        if (botaoJogar != null)
            botaoJogar.onClick.AddListener(IniciarJogo);
        else
            Debug.LogWarning("Botão 'Jogar' não foi atribuído no Inspector.");
    }

    void IniciarJogo()
    {
        if (inputNome != null && !string.IsNullOrEmpty(inputNome.text))
        {
            PlayerPrefs.SetString("NomeJogador", inputNome.text);
            PlayerPrefs.Save();
            Debug.Log("Nome salvo: " + inputNome.text);
        }
        else
        {
            Debug.LogWarning("Campo de nome está vazio ou não atribuído!");
        }

        SceneManager.LoadScene("Level01");
    }
}
