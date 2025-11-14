using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BotaoVoltarMenu : MonoBehaviour
{
    public Button botaoVoltarMenu;
    void Start()
    {
        if (botaoVoltarMenu != null)
            botaoVoltarMenu.onClick.AddListener(VoltarMenuPrincipal);
        else
            Debug.LogWarning("Botão voltar menu não atribuido no inspetor.");
    }

    void VoltarMenuPrincipal()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
