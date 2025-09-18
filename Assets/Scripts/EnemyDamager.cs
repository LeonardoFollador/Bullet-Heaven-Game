using UnityEngine;

public class EnemyDamager : MonoBehaviour
{
    public float damageAmount;

    public float lifeTime, growSpeed = 4f;
    private Vector3 targetSize;

    public bool destroyParent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Destroy(gameObject, lifeTime);

        targetSize = transform.localScale;
        transform.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = Vector3.MoveTowards(transform.localScale, targetSize, growSpeed * Time.deltaTime);
    
        lifeTime -= Time.deltaTime;

        if(lifeTime <= 0)
        {
            targetSize = Vector3.zero;
            if (transform.localScale.x == 0f)
            {
                Destroy(gameObject);

                if(destroyParent)
                {
                    Destroy(transform.parent.gameObject);
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.GetComponent<EnemyMovement>().TakeDamage(damageAmount);
        }
    }
}
