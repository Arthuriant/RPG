using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using CameraShake;

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
    protected Animator anim;
    private SpriteRenderer sprite;
    private Rigidbody2D myRb;

    private float spawnDelaySeconds = 1f;

    public Transform target;
    public float chaseRadius;
    public float attackRadius;

    public float health;
    public GameObject staff;
    public GameObject hand;

    protected BossAudio audioManager;

    public GameObject deathEffect;

    private void Awake()
    {
        audioManager = GameObject.FindWithTag("AudioBoss").GetComponent<BossAudio>();
    }

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
            timer += 0.02f;
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
        if (currentState != BossState.idle && currentState != BossState.dash)
        {
            StartCoroutine(DashToTarget());
        }
    }
    else if (distanceToTarget <= 5 && distanceToTarget > attackRadius)
    {
        if (currentState == BossState.idle || currentState == BossState.walk && currentState != BossState.stagger)
        {
            Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * 0.02f);
            changeAnim(temp - transform.position);

            myRb.MovePosition(temp);
            ChangeState(BossState.walk);

            anim.SetBool("SpawnDone", true);
        }
        else if (currentState == BossState.stagger)
        {
            // Tambahan kondisi jika stagger
        }
    }
    else if (distanceToTarget > chaseRadius)
    {
        anim.SetBool("SpawnDone", false);
    }
}

private IEnumerator DashToTarget()
{
    ChangeState(BossState.idle);

    yield return new WaitForSeconds(1.5f);

    float distancetoTarget = Vector3.Distance(transform.position,target.position);
    while(distancetoTarget>5f)
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Vector3 dashMovement = direction * 40 * 0.02f;  // Menghitung pergerakan per frame

        // Menggerakkan boss menggunakan Rigidbody2D
        myRb.MovePosition(myRb.position + new Vector2(dashMovement.x, dashMovement.y));

        // Update jarak ke target setiap frame
        distancetoTarget = Vector3.Distance(transform.position, target.position);

        yield return null;
    }
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

        private void TakeDamage(float damage){
        if(health != 0)
        {
            audioManager.playSFXBody(audioManager.hurt);
            health -= damage;
        }
        if(health <= 0){
            hand.gameObject.SetActive(false);
            staff.gameObject.SetActive(false);
            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            this.gameObject.SetActive(false);
        }
    }
    public void knock(Rigidbody2D myRigdBody, float knockTime, float damage)
    {

        StartCoroutine(knockCo(myRigdBody,knockTime,damage));
    }

    
     private void OnTriggerEnter2D(Collider2D other)
     {
        if(other.CompareTag("Hitbox")){
            Hurt();
        }
     }

        private IEnumerator knockCo(Rigidbody2D myRigidBody, float knockTime, float damage)
     {
        if(myRigidBody != null )
        {
            yield return new WaitForSeconds(knockTime);
            myRigidBody .linearVelocity = Vector2.zero;
            currentState = BossState.idle;
             TakeDamage(damage);
        }
     }

         private IEnumerator hitflash(){
        int i = 0;
        while(i<5){
            sprite.color = Color.red;
            yield return new WaitForSeconds(0.02f);
            sprite.color = Color.white;
            yield return new WaitForSeconds(0.02f);
            i++;
        }
    }

         private void Hurt(){
        CameraShaker.Presets.Explosion2D(0.5f , 1.2f,0.15f);
        StartCoroutine(hitflash());
        HitStop.instances.init(0.075f);
        

    
     }
}
