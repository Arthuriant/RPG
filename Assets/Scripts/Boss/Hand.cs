using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HandState{
    idle,
    casting,
}
public class Hand : MonoBehaviour
{
    public Transform target;
    public Transform player;
    public float chaseRadius;
    public float attackRadius;
    public HandState currentState;
    private Animator anim;
    private Rigidbody2D myRb;
    private float spawnDelaySeconds = 3f;
    private float castingDelaySecond = 3f;
    private bool cooldown;
    public float fireDelay;
    private float fireDelaySeconds;

    public float beamDelay;
    private float beamDelaySeconds;
    bool beamShot;
    public GameObject projectile;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
        myRb = GetComponent<Rigidbody2D>();
        currentState = HandState.idle;
        anim.SetBool("Casting", false);
        StartCoroutine(SpawnMovement());
        fireDelaySeconds = fireDelay;
        beamDelaySeconds = beamDelay;
        beamShot = false;
        cooldown = true;

        
    }

    void Update()
    {
        fireDelaySeconds -= Time.deltaTime;
        if(fireDelaySeconds <= 0)
        {
            cooldown = false;
            fireDelaySeconds =  fireDelay;
        }

        beamDelaySeconds -= Time.deltaTime;
        if(beamDelaySeconds <= 0)
        {
            beamShot = true;
            beamDelaySeconds = beamDelay;
        }

        Casting();

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        CheckDistance();
    }

    private IEnumerator SpawnMovement()
    {
        float timer = 0f;
        Vector2 startPosition = transform.position;
        Vector2 targetPosition = new Vector2(startPosition.x, startPosition.y - 4); 

        while (timer < spawnDelaySeconds)
        {
            timer += Time.deltaTime;
            float progress = timer / spawnDelaySeconds; 
            transform.position = Vector2.Lerp(startPosition, targetPosition, progress); 
            yield return null;
        }

        

        currentState = HandState.idle; 
    }

    private void Casting()
    {
        if (!cooldown )
        {
            if(beamShot)
            {
            Vector2 tempVector = player.transform.position - transform.position;
            GameObject current = Instantiate(projectile, transform.position, Quaternion.identity);
            current.GetComponent<Projectile>().Launch(tempVector);
            beamShot = false;
            StartCoroutine(CastingMovement());
            }
        }else if(cooldown)
        {
            beamShot = false;
            anim.SetBool("Casting",false);
        }
    }

        private IEnumerator CastingMovement()
    {
        anim.SetBool("Casting",true);
        yield return new WaitForSeconds(castingDelaySecond);
        cooldown = true;


        
        
    }

    //mainin Cooldown
    



    void CheckDistance()
{
    float distanceToTarget = Vector3.Distance(target.position, transform.position);

    if(distanceToTarget <= chaseRadius && distanceToTarget > attackRadius && distanceToTarget >5 )
    {
        Vector3 temp = Vector3.MoveTowards(transform.position, target.position, 10 * Time.deltaTime);
        myRb.MovePosition(temp);
    }
    else if (distanceToTarget <= 5 && distanceToTarget > attackRadius)
    {
        Vector3 temp = Vector3.MoveTowards(transform.position, target.position, 3 * Time.deltaTime);
        myRb.MovePosition(temp);
    }
    else if (distanceToTarget > chaseRadius)
    {
        //kalau lepas dari radius
    }
}

        private void ChangeState(HandState newState)
    {
        if (currentState != newState){
            currentState = newState;
        }
    }
}
