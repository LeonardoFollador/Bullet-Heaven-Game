using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BotaoAcessarRanking : MonoBehaviour
{
    public Button botaoAcessarRanking;
    void Start()
    {
        if (botaoAcessarRanking != null)
            botaoAcessarRanking.onClick.AddListener(VoltarMenuPrincipal);
        else
            Debug.LogWarning("Botão voltar accessar ranking não atribuido no inspetor.");
    }

    void VoltarMenuPrincipal()
    {
        SceneManager.LoadScene("Ranking");
    }
}
