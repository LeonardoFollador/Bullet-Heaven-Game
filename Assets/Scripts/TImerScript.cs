using UnityEngine;
using TMPro;

public class TImerScript : MonoBehaviour
{
    public float time;
    public TextMeshProUGUI timerText;
    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;   
        timerText.text = ((int)time).ToString();     
    }
}
