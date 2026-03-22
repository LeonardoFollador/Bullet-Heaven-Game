using UnityEngine;

public class BossSpawnEffect2 : MonoBehaviour
{
    public float darkness = 0.6f;

    void Start()
    {
        ScreenDarkener1 darkener = FindAnyObjectByType<ScreenDarkener1>();

        if (darkener != null)
        {
            darkener.Darken(darkness);
        }
    }
}