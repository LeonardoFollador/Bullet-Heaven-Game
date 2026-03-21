using UnityEngine;
using UnityEngine.UI;

public class ScreenDarkener : MonoBehaviour
{
    public Image overlay;
    public float fadeSpeed = 0.2f;

    private float targetAlpha = 0f;


    void Update()
    {
        Color color = overlay.color;

        color.a = Mathf.MoveTowards(color.a, targetAlpha, fadeSpeed * Time.deltaTime);

        overlay.color = color;
    }

    public void Darken(float intensity = 0.6f)
    {
        targetAlpha = intensity;
    }

    public void Clear()
    {
        targetAlpha = 0f;
    }
}