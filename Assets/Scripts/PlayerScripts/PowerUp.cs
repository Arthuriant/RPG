using UnityEngine;


public class PowerUp : MonoBehaviour
{
    protected AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindWithTag("Audio").GetComponent<AudioManager>();
    }
    public Signal powerupSignal;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
