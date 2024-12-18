using UnityEngine;

public enum DoorType
{
    key,
    enemy,
    button
}
public class Door : Interactable
{
    [Header("Door Variables")]
    public DoorType thisDoorType;
    public bool open = false;
    public Inventory playerInventory;
    public SpriteRenderer doorSprite;
    public BoxCollider2D physicsCollider;
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindWithTag("Audio").GetComponent<AudioManager>();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(playerInRange && thisDoorType == DoorType.key)
            {
                if(playerInventory.numberOfKeys>0)
                {
                    playerInventory.numberOfKeys--;
                    Open();
                }
            }
        }
    }
    public void Open()
    {
        audioManager.playSFX2(audioManager.doorOpen);
        doorSprite.enabled = false;
        open = true;
        physicsCollider.enabled = false;
    }

    public void Close()
    {
       audioManager.playSFX(audioManager.doorClosed);
       doorSprite.enabled = true;
       open = false;
       physicsCollider.enabled = true;
    }
}
