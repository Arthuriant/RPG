using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BoundedNPC : Interactable
{
    private Vector3 directionVector;
    private Transform myTransform;
    public float speed;
    private Rigidbody2D myRigidBody;
    private Animator anim;
    public Collider2D bounds;
    public float moveTime;
    private float moveTimeSeconds;
    public float waitTime;
    private float waitTimeSeconds;
    private bool isMoving;
    public GameObject dialogBox;
    public Text dialogText;
    public string dialog;

    void Start()
    {
        anim = GetComponent<Animator>();
        myTransform = GetComponent<Transform>();
        myRigidBody = GetComponent<Rigidbody2D>();
        ChangeDirection();
        moveTimeSeconds = moveTime;
        waitTimeSeconds = waitTime;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && playerInRange){
            if(dialogBox.activeInHierarchy){
                dialogBox.SetActive(false);
            }else{
                dialogBox.SetActive(true);
                dialogText.text = dialog;
            }
        }

        if(isMoving)
        {
            moveTimeSeconds -= Time.deltaTime;
            if(moveTimeSeconds<=0)
            {
                moveTimeSeconds = moveTime;
                isMoving = false;

            }
             if(!playerInRange)
             {
             Move();
            }
        }
        else{
            waitTimeSeconds -= Time.deltaTime;
            if(waitTimeSeconds<=0)
            {
                if(!playerInRange)
                {
                    ChooseDifferentDirection();
                }
                isMoving = true;
                waitTimeSeconds = waitTime;
            }
        }
    
    }
    private void ChooseDifferentDirection()
    {
        Vector3 temp = directionVector;
        ChangeDirection();
        int loops = 0;
        while(temp == directionVector && loops<100)
        {
            loops++;
            ChangeDirection();
        }  
    }

    private void Move()
    {
        Vector3 temp = myTransform.position + directionVector * speed * Time.deltaTime;
        if(bounds.bounds.Contains(temp))
        {
             myRigidBody.MovePosition(temp);
        }
        else
        {
            ChangeDirection();
        }
    }

    void UpdateAnimation()
    {
        anim.SetFloat("MoveX", directionVector.x);
        anim.SetFloat("MoveY", directionVector.y);
    }
    void ChangeDirection()
    {
        int direction = Random.Range (0,4);
        switch(direction)
        {
            case 0:
            //walking to right
            directionVector = Vector3.right;
                break;
            case 1:
            //walking to up
             directionVector = Vector3.up;
                break;
            case 2:
            //walking to left
             directionVector = Vector3.left;
                break;
            case 3:
            //walking to down
             directionVector = Vector3.down;
                break;
            default:
                break;
        }
        UpdateAnimation();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        ChooseDifferentDirection();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !other.isTrigger && playerInRange){
            context.Raise();
            playerInRange = false;
            dialogBox.SetActive(false);

        }
    }
}
