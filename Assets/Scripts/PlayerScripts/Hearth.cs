using UnityEngine;

public class Hearth : PowerUp
{
    public FloatValue playerHealth;
    public FloatValue heartContainers;
    public float amountToIncrease;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public  void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !other.isTrigger)
        {
            audioManager.playSFX(audioManager.hpUp);
            playerHealth.RuntimeValue += amountToIncrease;
            if(playerHealth.initialValue> heartContainers.RuntimeValue * 2f)
            {
                playerHealth.initialValue = heartContainers.RuntimeValue * 2f;
            }
            powerupSignal.Raise();
            Destroy(this.gameObject);
        }
        
    }
}
