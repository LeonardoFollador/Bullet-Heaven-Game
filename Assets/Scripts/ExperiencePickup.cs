using UnityEngine;

public class ExperiencePickup : MonoBehaviour
{
    public int expValue;

    private bool movingToPlayer;
    public float moveSpeed;

    public float timeBetweenChecks = 0.2f;
    private float checkCounter;

    private PlayerMovement1 player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = PlayerHealth.instance.GetComponent<PlayerMovement1>();
        Destroy(gameObject, 120f);
    }

    // Update is called once per frame
    void Update()
    {
        if (movingToPlayer)
        {
            transform.position = Vector3.MoveTowards(transform.position, PlayerHealth.instance.transform.position, moveSpeed * Time.deltaTime);
        }
        else
        {
            checkCounter -= Time.deltaTime;
            if (checkCounter <= 0)
            {
                checkCounter = timeBetweenChecks;

                if (Vector3.Distance(transform.position, player.transform.position) < player.pickupRange)
                {
                    movingToPlayer = true;
                    moveSpeed += player.moveSpeed;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            ExperienceLevelController.instance.GetExp(expValue);

            Destroy(gameObject);
        }
    }
}
