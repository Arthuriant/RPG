using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Movement Stuff")]
    public float moveSpeed;
    public Vector2 directionToMove;
    [Header("Lifetime")]
    public float lifetime;
    private float lifetimeSeconds;
    public Rigidbody2D myRigidbody;
    public GameObject Purple;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        lifetimeSeconds = lifetime; 
    }

    // Update is called once per frame
    void Update()
    {
        lifetimeSeconds -= Time.deltaTime;
        if(lifetimeSeconds <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void Launch(Vector2 initialVel)
    {
        myRigidbody.linearVelocity = initialVel *moveSpeed;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("ProjectileRed") || other.gameObject.CompareTag("ProjectileBlue"))
        {
            if(other.gameObject.CompareTag("ProjectileRed") && other.isTrigger)
            {
                Destroy(this.gameObject);
                
                GameObject current = Instantiate(Purple, transform.position, Quaternion.identity);
                current.GetComponent<Purple>().Launch();
            }
        }

        if(other.gameObject.CompareTag("ProjectilePurple") || other.gameObject.CompareTag("Player"))
        {
             Destroy(this.gameObject);
        }

    }
}
