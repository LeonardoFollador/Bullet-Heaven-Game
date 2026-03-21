using UnityEngine;

public class BossSkillController : MonoBehaviour
{
    public GameObject spell2Prefab;

    public float firstCastDelay = 5f; // ⏱️ espera após spawn
    public float cooldown = 8f; // tempo entre skills

    private float timer;
    private bool started = false;

    void Start()
    {
        timer = firstCastDelay;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            CastSpell();

            // depois da primeira vez, usa cooldown normal
            timer = cooldown;
            started = true;
        }
    }

    void CastSpell()
    {
        Instantiate(spell2Prefab, transform.position, Quaternion.identity);
    }
}