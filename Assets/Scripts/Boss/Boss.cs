using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public enum BossState{
    idle,
    walk,
    dash,
    attack,
    stagger
}

public class Boss : MonoBehaviour
{   
    public BossState currentState;
    public float moveSpeed;// Kecepatan gerakan boss turun
    private Animator anim;
    private SpriteRenderer sprite;
    private Rigidbody2D myRb;

    private float spawnDelaySeconds = 3f;

    public Transform target;
    public float chaseRadius;
    public float attackRadius;
    bool isDashing = false;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        myRb = GetComponent<Rigidbody2D>();

        StartCoroutine(SpawnMovement());
    }

    void FixedUpdate()
    {
        CheckDistance();
    }

    private IEnumerator SpawnMovement()
    {
        float timer = 0f;
        Vector2 startPosition = transform.position;
        Vector2 targetPosition = new Vector2(startPosition.x, startPosition.y - 4); // Posisi akhir boss setelah turun

        while (timer < spawnDelaySeconds)
        {
            timer += Time.deltaTime;
            float progress = timer / spawnDelaySeconds; // Kemajuan animasi dari 0 ke 1 dalam waktu 4 detik
            transform.position = Vector2.Lerp(startPosition, targetPosition, progress); // Interpolasi posisi
            yield return null;
        }

        // Setelah waktu spawn selesai

        currentState = BossState.idle; // Contoh: bisa mengubah state boss ke idle
    }


    void CheckDistance()
{
    float distanceToTarget = Vector3.Distance(target.position, transform.position);

    if(distanceToTarget <= chaseRadius && distanceToTarget > attackRadius && distanceToTarget >5 )
    {
         StartCoroutine(DashToTarget());
    }
    else if (distanceToTarget <= 5 && distanceToTarget > attackRadius)
    {
        if (currentState == BossState.idle || currentState == BossState.walk && currentState != BossState.stagger)
        {
            Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
            changeAnim(temp - transform.position);

            myRb.MovePosition(temp);
            ChangeState(BossState.walk);

            anim.SetBool("wakeUp", true);
        }
        else if (currentState == BossState.stagger)
        {
            // Tambahan kondisi jika stagger
        }
    }
    else if (distanceToTarget > chaseRadius)
    {
        anim.SetBool("wakeUp", false);
    }
}

private IEnumerator DashToTarget()
{
    ChangeState(BossState.dash);
    isDashing = true;

    yield return new WaitForSeconds(3);

    // Menggerakkan musuh ke target position dengan cepat
    Vector3 temp = Vector3.MoveTowards(transform.position, target.position, 40 * Time.deltaTime);
    Debug.Log(Time.deltaTime);
    myRb.MovePosition(temp);
    // Mengatur musuh kembali ke keadaan walk setelah dash
    ChangeState(BossState.walk);
}




        private void ChangeState(BossState newState)
    {
        if (currentState != newState){
            currentState = newState;
        }
    }

        private void changeAnim(Vector2 direction){
        if(Mathf.Abs(direction.x)>Mathf.Abs(direction.y)){
            if (direction.x > 0 ){
                SetAnimFloat(Vector2.right);
            }else if (direction.x<0){
                 SetAnimFloat(Vector2.left);
            }
        }else if(Mathf.Abs(direction.x)<Mathf.Abs(direction.y)){
            if (direction.y > 0 ){
                 SetAnimFloat(Vector2.up);
            }else if (direction.y<0){
                 SetAnimFloat(Vector2.down);
            }
        }

        
    }

        private void SetAnimFloat(Vector2 setVector){
        anim.SetFloat("moveX" , setVector.x);
        anim.SetFloat("moveY" , setVector.y);

    }
}
