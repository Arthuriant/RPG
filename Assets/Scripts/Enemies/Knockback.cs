using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class Knockback : MonoBehaviour
{
    public float thrust;
    public float knockTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created


     void OnTriggerEnter2D(Collider2D other){

         if(other.gameObject.CompareTag("breakable") && other.gameObject.CompareTag("Player") ){
            other.GetComponent<Pot>().Smash();
        }

        if(other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Player")){
            Rigidbody2D hit = other.GetComponent<Rigidbody2D>();
            if(hit != null){
                Vector2 difference = hit.transform.position - transform.position;
                difference = difference.normalized * thrust;
                hit.AddForce(difference, ForceMode2D.Impulse);
                if(other.gameObject.CompareTag("Enemy")){
                    hit.GetComponent<Enemy>().currentState = EnemyState.stagger;
                    other.GetComponent<Enemy>().knock(hit,knockTime);
                    Debug.Log("halo");
                }

                if(other.gameObject.CompareTag("Player")){
                    hit.GetComponent<PlayerMovement>().currentState = PlayerState.stagger;
                    other.GetComponent<PlayerMovement>().Knock(knockTime);
                }




            } 
        }else{

        }  
     }

}
