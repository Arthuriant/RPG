using UnityEngine;

public class Switch : MonoBehaviour
{
    public bool active;
    public BoolValue storedValue;
    public Sprite activeSprite;
    private SpriteRenderer mySprite;
    public Door thisDoor;
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindWithTag("Audio").GetComponent<AudioManager>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mySprite = GetComponent<SpriteRenderer>();
        active = storedValue.RuntimeValue;
        if(active)
        {
            ActivateSwitch();
        }
    }

    public void ActivateSwitch()
    {
        audioManager.playSFX(audioManager.switchPushed);
        active = true;
        storedValue.RuntimeValue = active;
        thisDoor.Open();
        mySprite.sprite = activeSprite;

    }

    // Update is called once per frame

    public void  OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            ActivateSwitch();
        }
    }
}
