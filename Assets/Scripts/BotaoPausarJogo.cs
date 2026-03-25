using UnityEngine;
using UnityEngine.UI;

public class BotaoPausarJogo : MonoBehaviour
{
    private bool pausado = false;

    public Button botaoPausarPartida;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (botaoPausarPartida != null)
            botaoPausarPartida.onClick.AddListener(PausarJogo);
    }

    private void PausarJogo()
    {
        pausado = !pausado;
        Time.timeScale = pausado ?  0f : 1f;
    }
}
