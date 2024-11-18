using System;
using System.Collections;
using UnityEngine;

public class Slime : Enemy
{
    protected Rigidbody2D myRigidbody;
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
        anim.SetBool("wakeUp",true);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckDistance();
    }

    public virtual void CheckDistance()
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
            }else if(currentState == EnemyState.stagger){
               
            }
        }else if (Vector3.Distance(target.position, transform.position)> chaseRadius ){

            anim.SetBool("wakeUp",false);
    
        }
    }

    private IEnumerator takehit(float time)
    {
        anim.SetBool("Damaged", true);
        yield return new WaitForSecondsRealtime(time);
        anim.SetBool("Damaged", false);
    }
    

    private void SetAnimFloat(Vector2 setVector){
        anim.SetFloat("moveX" , setVector.x);
        anim.SetFloat("moveY" , setVector.y);

    }

    public void changeAnim(Vector2 direction){
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
