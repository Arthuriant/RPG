using System;
using System.Collections;
using UnityEngine;

public enum EnemyState{
    idle,
    walk,
    attack,
    stagger
}
public class Enemy : MonoBehaviour
{   
    public EnemyState currentState;
    public int health;
    public string enemyName;
    public int baseAttack;
    public float moveSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void knock(Rigidbody2D myRigdBody, float knockTime)
    {
        StartCoroutine(knockCo(myRigdBody,knockTime));
    }

    private IEnumerator knockCo(Rigidbody2D myRigidBody, float knockTime)
     {
        if(myRigidBody != null )
        {
            yield return new WaitForSeconds(knockTime);
            myRigidBody .linearVelocity = Vector2.zero;
            currentState = EnemyState.idle;
        }
     }
}
