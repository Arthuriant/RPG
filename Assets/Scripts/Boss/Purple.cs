using UnityEngine;

public class Purple : MonoBehaviour
{
    [Header("Movement Stuff")]
    public float moveSpeed;
    public Vector2 directionToMove;

    [Header("Lifetime")]
    public float lifetime;
    private float lifetimeSeconds;

    public Rigidbody2D myRigidbody;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        lifetimeSeconds = lifetime;

        // Cek apakah PlayerMovement.Instance valid
        if (PlayerMovement.Instance != null)
        {
            Launch();
        }
        else
        {
            Debug.LogError("Player instance not found!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        lifetimeSeconds -= Time.deltaTime;
        if (lifetimeSeconds <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void Launch()
    {
        if (PlayerMovement.Instance != null)
        {
            // Ambil posisi Player dari Singleton
            Vector2 playerPosition = PlayerMovement.Instance.transform.position;

            Vector2 tempVector = playerPosition - (Vector2)transform.position;
            myRigidbody.linearVelocity = tempVector.normalized * moveSpeed;
        }
        else
        {
            Debug.LogWarning("Player instance not assigned or not found!");
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }
}
