using System;
using UnityEngine;

public class Slime : Enemy
{
    private Rigidbody2D myRigidbody;
    public Transform target;
    public float chaseRadius;
    public float attackRadius;
    public Transform homePosition;
    public Animator anim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentState = EnemyState.idle;
        target = GameObject.FindWithTag("Player").transform;
        myRigidbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
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
            changeAnim(temp - transform.position);
            myRigidbody.MovePosition(temp);
            ChangeState(EnemyState.walk); 
            anim.SetBool("wakeUp", true);
            }
        }else if (Vector3.Distance(target.position, transform.position)> chaseRadius ){
            anim.SetBool("wakeUp",false);
        }
    }

    private void SetAnimFloat(Vector2 setVector){
        anim.SetFloat("moveX" , setVector.x);
        anim.SetFloat("moveY" , setVector.y);

    }

    private void changeAnim(Vector2 direction){
        if(Mathf.Abs(direction.x)>MathF.Abs(direction.y)){
            if (direction.x > 0 ){
                SetAnimFloat(Vector2.right);
            }else if (direction.x<0){
                 SetAnimFloat(Vector2.left);
            }
        }else if(Mathf.Abs(direction.x)<MathF.Abs(direction.y)){
            if (direction.y > 0 ){
                 SetAnimFloat(Vector2.up);
            }else if (direction.y<0){
                 SetAnimFloat(Vector2.down);
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
