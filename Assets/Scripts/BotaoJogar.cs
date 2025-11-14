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
            Debug.LogWarning("Bot�o 'Jogar' n�o foi atribu�do no Inspector.");
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
            Debug.LogWarning("Campo de nome est� vazio ou n�o atribu�do!");
        }

        SceneManager.LoadScene("Level01");
    }
}
