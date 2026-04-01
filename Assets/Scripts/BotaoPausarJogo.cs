using UnityEngine;
using UnityEngine.UI;

public class BotaoPausarJogo : MonoBehaviour
{
    private bool pausado = false;

    public Button botaoPausarPartida;
    public Button botaoDespausarPartida;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (botaoPausarPartida != null)
            botaoPausarPartida.onClick.AddListener(PausarJogo);
        
        if (botaoDespausarPartida != null)
            botaoDespausarPartida.onClick.AddListener(DespausarJogo);
        
        botaoDespausarPartida.gameObject.SetActive(pausado);
    }

    private void PausarJogo()
    {
        pausado = true;
        Time.timeScale = 0f;
        botaoDespausarPartida.gameObject.SetActive(pausado);
        botaoPausarPartida.gameObject.SetActive(!pausado);
    }

    private void DespausarJogo()
    {
        pausado = false;
        Time.timeScale = 1f; 
        botaoDespausarPartida.gameObject.SetActive(pausado);
        botaoPausarPartida.gameObject.SetActive(!pausado);
    }
}
