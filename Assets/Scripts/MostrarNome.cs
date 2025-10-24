using UnityEngine;
using TMPro;

public class MostrarNome : MonoBehaviour
{
    public TextMeshProUGUI textoNome;

    void Start()
    {
        string nome = PlayerPrefs.GetString("NomeJogador", "Jogador");
        textoNome.text = $"{nome}";
    }
}
