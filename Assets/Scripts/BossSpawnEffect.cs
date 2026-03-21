using UnityEngine;

public class BossSpawnEffect : MonoBehaviour
{
    public float darkness = 0.6f;

    void Start()
    {
        ScreenDarkener darkener = FindAnyObjectByType<ScreenDarkener>();

        if (darkener != null)
        {
            darkener.Darken(darkness);
        }
    }
}