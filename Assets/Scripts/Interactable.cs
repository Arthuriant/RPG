using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
    public bool playerInRange;
    public Signal context;

    private void  OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") &&  !other.isTrigger && !playerInRange){
            context.Raise();
            playerInRange   = true;

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !other.isTrigger && playerInRange){
            context.Raise();
            playerInRange = false;

        }
    }
}


