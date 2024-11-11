using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum PlayerState{
    walk,
    idle,
    attack,
    interact,
    stagger
}
public class PlayerMovement : MonoBehaviour
{



    public float speed;
    private Rigidbody2D myRigidbody;
    private Vector2 Change; 
    private Animator animator;
    public PlayerState currentState;
    public FloatValue currentHealth;
    public Signal playerHealthSIgnal;
    public VectorValue startingPosition;

    private SpriteRenderer sprite;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position = startingPosition.initialValue;
        currentState = PlayerState.walk;
        animator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator.SetFloat("moveX",0);
        animator.SetFloat("moveY",-1);
    }

    // Update is called once per frame
    void Update()
    {   
        Change = Vector2.zero;
        Change.x = Input.GetAxisRaw("Horizontal");
        Change.y = Input.GetAxisRaw("Vertical"); 
        if(Input.GetButtonDown("Attack") && currentState != PlayerState.attack){
            myRigidbody.MovePosition(myRigidbody.position + Change*speed*Time.deltaTime );
            StartCoroutine(AttackCo());
        }
        else if(currentState == PlayerState.walk ||currentState == PlayerState.idle ){
            UpdateAnimationAndMove();
        }
    }

    private IEnumerator AttackCo(){
        animator.SetBool("attacking", true);
        currentState = PlayerState.attack;
        yield return null;
        animator.SetBool("attacking",false);
        yield return new WaitForSeconds(.3f);
        currentState = PlayerState.idle;
    }

    void  FixedUpdate()
    {
         MoveCharacter();   
    }

    void UpdateAnimationAndMove(){
        if (Change != Vector2.zero){
            Change.Normalize();         
            animator.SetFloat("moveX", Change.x);
            animator.SetFloat("moveY", Change.y);
            animator.SetBool("moving", true);
        }else{
            animator.SetBool("moving", false);
        }
    }

    void MoveCharacter()
    {
        if (currentState != PlayerState.attack && currentState != PlayerState.stagger){
            myRigidbody.MovePosition(myRigidbody.position + Change*speed*Time.deltaTime );
        }
    } 

    public void Knock(float knockTime, float damage)
    {
        currentHealth.RuntimeValue -= damage;
        playerHealthSIgnal.Raise();
        if (currentHealth.RuntimeValue >0)
        {
             StartCoroutine(knockCo(knockTime));
             StartCoroutine(attacked());
             StartCoroutine(hitflash());
        }else{
            this.gameObject.SetActive(false);
        }
    }

    private IEnumerator knockCo(float knockTime)
     {
        if(myRigidbody != null )
        {
            yield return new WaitForSeconds(knockTime);
            myRigidbody.linearVelocity = Vector2.zero;
            currentState = PlayerState.idle;
        }
     }

     private IEnumerator attacked()
     {
            animator.SetBool("moving", false);
            animator.SetBool("attacked", true);
            yield return new WaitForSeconds(0.2f);
            animator.SetBool("attacked", false);
     }

        private IEnumerator hitflash(){
        int i = 0;
        while(i<5){
            sprite.color = Color.clear;
            yield return new WaitForSeconds(0.02f);
            sprite.color = Color.white;
            yield return new WaitForSeconds(0.02f);
            i++;
        }
    }
     

    
}
