using System;
using UnityEngine;

public class Room : MonoBehaviour
{
    public Enemy[] enemies;
    public Pot[] pots;
    public GameObject virtualCamera;

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !other.isTrigger)
        {
            //active all enemies
            Debug.Log("Player Masuk");
            for (int i = 0 ; i<enemies.Length; i++)
            {
                ChangeActivation(enemies[i], true);
            }

            for (int i = 0 ; i<pots.Length; i++)
            {
                ChangeActivation(pots[i], true);
            }
            virtualCamera.SetActive(true);
        }
    }

    public virtual void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !other.isTrigger)
        {
            Debug.Log("Player Keluar");
            //deactive all enemies
            for (int i = 0 ; i<enemies.Length; i++)
            {
                ChangeActivation(enemies[i], false);
            }

            for (int i = 0 ; i<pots.Length; i++)
            {
                ChangeActivation(pots[i], false);
            }
            virtualCamera.SetActive(false);
        }
    }

    public void ChangeActivation(Component component, bool activation)
    {
        component.gameObject.SetActive(activation);
    }

}
