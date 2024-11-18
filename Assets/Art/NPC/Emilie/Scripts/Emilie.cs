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
    private Rigidbody2D myRigidbody;
    private Vector2 Change;
    private Animator animator;
    public EmilieState currentState;
    private bool waitStatus;

    //Rounding
        public Transform[] path;
        public int currentPoint;
        public Transform currentGoal;
        public float RoundingDistance;

    void Start()
    {
        currentState = EmilieState.idle;
        animator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        animator.SetBool("Walking",true);

        
    }

    void Update()
    {


    }

 

    void FixedUpdate()
    {
        MoveCharacter();
    }



    void MoveCharacter()
    {
            if(Vector3.Distance(transform.position , path[currentPoint].position)>RoundingDistance)
            {
            Vector3 temp = Vector3.MoveTowards(transform.position,path[currentPoint].position, Speed*Time.deltaTime);
            myRigidbody.MovePosition(temp);
            changeAnim(temp - transform.position);
            }else
            {
                
                ChangeGoal();
            }
    }


        private void ChangeGoal()
    {
        if(currentPoint == path.Length -1)
        {
            currentPoint = 0;
            currentGoal = path[0];
        }else
        {
            currentPoint ++;
            currentGoal = path[currentPoint];
        }
    }

        public void changeAnim(Vector2 direction){
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
        animator.SetFloat("moveX" , setVector.x);
        animator.SetFloat("moveY" , setVector.y);

    }

    
}
