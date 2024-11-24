using System;
using UnityEngine;

public class DugeonBossRoom : MonoBehaviour
{
    public GameObject Boss;
    public GameObject virtualCamera;

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !other.isTrigger)
        {
            Boss.SetActive(true);
            virtualCamera.SetActive(true);
        }
    }

    public virtual void OnTriggerExit2D(Collider2D other)
    {
        virtualCamera.SetActive(false);
    }

}
