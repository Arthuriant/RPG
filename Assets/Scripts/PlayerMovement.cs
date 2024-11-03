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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentState = PlayerState.walk;
        animator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
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
        currentState = PlayerState.walk;
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

    public void Knock(float knockTime)
    {
        StartCoroutine(knockCo(knockTime));
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

    
}
