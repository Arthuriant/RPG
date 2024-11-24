using System;
using System.Collections;
using UnityEngine;
using CameraShake;
public enum EnemyState{
    idle,
    walk,
    attack,
    stagger
}
public class Enemy : MonoBehaviour
{   
    public EnemyState currentState;
    public FloatValue maxHealth;
    public float health;
    public string enemyName;
    public int baseAttack;
    public float moveSpeed;
    public GameObject deathEffect;
    public LootTable thisLoot;


    protected Animator anim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    protected SpriteRenderer sprite;

    void Awake()
    {
        health = maxHealth.initialValue;
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

    }

    private void TakeDamage(float damage){
        health -= damage;
        if(health <= 0){
            DeathEffect();
            MakeLoot();
            this.gameObject.SetActive(false);
        }
    }

    private void DeathEffect()
    {
        if(deathEffect != null)
        {
            GameObject effect = Instantiate(deathEffect, transform.position , Quaternion.identity);
            Destroy(effect,1f);
        }
    }
    public void knock(Rigidbody2D myRigdBody, float knockTime, float damage)
    {

        StartCoroutine(knockCo(myRigdBody,knockTime,damage));
    }

    private void MakeLoot()
    {
        if(thisLoot != null)
        {
            PowerUp current = thisLoot.LootPowerup();
            if(current !=null)
            {
                Instantiate(current.gameObject, transform.position, Quaternion.identity);
            }
        }
    }

    private IEnumerator hitflash(){
        int i = 0;
        while(i<5){
            sprite.color = Color.white;
            yield return new WaitForSeconds(0.02f);
            sprite.color = Color.red;
            yield return new WaitForSeconds(0.02f);
            sprite.color = Color.white;
            yield return new WaitForSeconds(0.02f);
            i++;
        }
    }

    private IEnumerator knockCo(Rigidbody2D myRigidBody, float knockTime, float damage)
     {
        if(myRigidBody != null )
        {
            yield return new WaitForSeconds(knockTime);
            myRigidBody .linearVelocity = Vector2.zero;
            currentState = EnemyState.idle;
             TakeDamage(damage);
        }
     }

     private IEnumerator animasi(float Time){
        anim.SetBool("wakeUp",false);
        anim.SetBool("Damaged", true);
        yield return new WaitForSecondsRealtime(Time);
        anim.SetBool("wakeUp",true);
        anim.SetBool("Damaged", false);


     }

     private void OnTriggerEnter2D(Collider2D other)
     {
        if(other.CompareTag("Hitbox")){
            Hurt();
        }
     }

     private void Hurt(){
        CameraShaker.Presets.Explosion2D(20f, 20f, 0.3f);
        StartCoroutine(hitflash());
        StartCoroutine(animasi(0.5f));
        HitStop.instances.init(0.075f);
        

    
     }
}
