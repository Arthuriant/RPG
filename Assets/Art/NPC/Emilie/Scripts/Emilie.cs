using UnityEngine;
using System.Collections;

public enum EmilieState
{
    walk,
    idle
}

public class Emilie : MonoBehaviour
{
    public float Speed;
    private int Randomx;
    private int Randomy;
    private Rigidbody2D myRigidbody;
    private Vector2 Change;
    private Animator animator;
    private Vector3 HomePosition;
    private Vector3 MaxPosition;
    private Vector3 CurrentPosition;
    public EmilieState currentState;

    void Start()
    {
        currentState = EmilieState.idle;
        animator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        HomePosition = transform.position;
        MaxPosition = new Vector3(HomePosition.x + 3, HomePosition.y +3, 0);
        
        StartCoroutine(RandomMove());
    }

    void Update()
    {
        CurrentPosition = transform.position;
        if (currentState == EmilieState.walk || currentState == EmilieState.idle)
        {
            UpdateAnimationAndMove();
        }
    }

    private IEnumerator RandomMove()
    {
        while (true)
        {
            // Menghasilkan arah acak -1, 0, atau 1 untuk sumbu x dan y
            Randomx = UnityEngine.Random.Range(-1, 2);
            Randomy = UnityEngine.Random.Range(-1, 2);

            // Memperbarui vektor Change berdasarkan arah acak yang dihasilkan
            Change = new Vector2(Randomx, Randomy);

            // Ubah status menjadi 'walk' jika ada perubahan arah, atau 'idle' jika tidak bergerak
            if (Change != Vector2.zero)
            {
                currentState = EmilieState.walk;
            }
            else
            {
                currentState = EmilieState.idle;
            }

            // Tunggu 1 detik sebelum mengubah arah lagi
            yield return new WaitForSeconds(1f);
            Change = Vector2.zero;
            yield return new WaitForSeconds(1f);
        }
    }

    void FixedUpdate()
    {
        MoveCharacter();
    }

    private void UpdateAnimationAndMove()
    {
        if (Change != Vector2.zero)
        {
            Change.Normalize();
            animator.SetFloat("moveX", Change.x);
            animator.SetFloat("moveY", Change.y);
            animator.SetBool("Walking", true);
        }
        else
        {
            animator.SetBool("Walking", false);
        }
    }

    void MoveCharacter()
    {
        myRigidbody.MovePosition(myRigidbody.position + Change * Speed * Time.deltaTime);
    }
}
