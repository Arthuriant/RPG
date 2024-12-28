using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StaffState{
    idle,
    casting,
}
public class Staff : MonoBehaviour
{
    public Transform target;

    public float chaseRadius;
    public float attackRadius;
    public StaffState currentState;
   
    private Rigidbody2D myRb;
    private float spawnDelaySeconds = 1f;
    private float castingDelaySecond = 2f;
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
       
        myRb = GetComponent<Rigidbody2D>();
        currentState = StaffState.idle;
        
        //StartCoroutine(SpawnMovement());
        fireDelaySeconds = fireDelay;
        beamDelaySeconds = beamDelay;
        beamShot = false;
        cooldown = true;

        
    }

    void Update()
    {
        fireDelaySeconds -= 0.02f;
        if(fireDelaySeconds <= 0)
        {
            cooldown = false;
            fireDelaySeconds =  fireDelay;
        }

        beamDelaySeconds -= 0.02f;
        if(beamDelaySeconds <= 0)
        {
            beamShot = true;
            beamDelaySeconds = beamDelay;
        }

        float distanceToTarget = Vector3.Distance(target.position, transform.position);
        if(distanceToTarget <= chaseRadius && distanceToTarget > attackRadius)
        {
            Casting();  
        }
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
            timer += 0.02f;
            float progress = timer / spawnDelaySeconds; 
            transform.position = Vector2.Lerp(startPosition, targetPosition, progress); 
            yield return null;
        }

        

        currentState = StaffState.idle; 
    }

    private void Casting()
    {
        if (!cooldown )
        {
            if(beamShot)
            {
            Vector2 tempVector = target.transform.position - transform.position;
            GameObject current = Instantiate(projectile, transform.position, Quaternion.identity);
            current.GetComponent<Projectile>().Launch(tempVector);
            beamShot = false;
            StartCoroutine(CastingMovement());
            }
        }else if(cooldown)
        {
           
        }
    }

        private IEnumerator CastingMovement()
    {
       
        yield return new WaitForSeconds(castingDelaySecond);
        cooldown = true;


        
        
    }

    //mainin Cooldown
    



    void CheckDistance()
{
    float distanceToTarget = Vector3.Distance(target.position, transform.position);

    if(distanceToTarget <= chaseRadius && distanceToTarget > attackRadius )
    {
        Vector3 temp = Vector3.MoveTowards(transform.position, target.position, 5 * 0.02f);
        myRb.MovePosition(temp);
    }
}

        private void ChangeState(StaffState newState)
    {
        if (currentState != newState){
            currentState = newState;
        }
    }
}
