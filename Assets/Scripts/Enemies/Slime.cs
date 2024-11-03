using UnityEngine;

public class Slime : Enemy
{
    private Rigidbody2D myRigidbody;
    public Transform target;
    public float chaseRadius;
    public float attackRadius;
    public Transform homePosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentState = EnemyState.idle;
        target = GameObject.FindWithTag("Player").transform;
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckDistance();
    }

    void CheckDistance()
    {
        if(Vector3.Distance(target.position, transform.position)<= chaseRadius && Vector3.Distance(target.position,transform.position)> attackRadius)
        {
            if(currentState == EnemyState.idle || currentState == EnemyState.walk && currentState != EnemyState.stagger)
            {
            Vector3 temp = Vector3.MoveTowards(transform.position,target.position, moveSpeed*Time.deltaTime);
            myRigidbody.MovePosition(temp);
            ChangeState(EnemyState.walk); 
            }
        }
    }

    private void ChangeState(EnemyState newState)
    {
        if (currentState != newState){
            currentState = newState;
        }
    }
}
