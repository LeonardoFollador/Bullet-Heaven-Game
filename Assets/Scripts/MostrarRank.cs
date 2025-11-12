using UnityEngine;
using UnityEngine.UI;

public class MostrarRank : MonoBehaviour
{

    public Button botaoRank;

    public DatabaseManager dbManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (botaoRank != null)
            botaoRank.onClick.AddListener(mostrarTopRank);
        else
            Debug.LogWarning("Bot�o 'Jogar' n�o foi atribu�do no Inspector.");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void mostrarTopRank()
    {
        var ranking = dbManager.GetTopRank();

        foreach (var h in ranking)
        {
            Debug.Log($"ID: {h.Id}, Name: {h.Name}, Score: {h.Score}");
        }

    }

}
