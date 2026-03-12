using UnityEngine;
using TMPro;

public class MostrarRank : MonoBehaviour
{

    public TextMeshProUGUI textRank;

    public TextMeshProUGUI positionRank;

    public TextMeshProUGUI nameRank;

    public TextMeshProUGUI scoreRank;

    public DatabaseManager dbManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (textRank != null)
            mostrarTopRank();
        else
            Debug.LogWarning("Texto dos ranks não foi atríbuido no Inspector.");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void mostrarTopRank()
    {
        int cont = 1;
        textRank.text = "";
        var ranking = dbManager.GetTopRank();

        positionRank.text = "";
        nameRank.text = "";
        scoreRank.text = "";

        foreach (var h in ranking)
        {
            positionRank.text += cont.ToString() + '\n';
            nameRank.text += h.Name + '\n';
            scoreRank.text += h.Score.ToString() + '\n';
            cont++;
        }

    }

}
