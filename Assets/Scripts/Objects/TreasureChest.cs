using UnityEngine;
using UnityEngine.UI;
public class TreasureChest : Interactable
{
    public Item contents;
    public Inventory playerInventory;
    public bool isOpen;
    public Signal raiseItem;
    public GameObject dialogBox;
    public Text dialogText;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && playerInRange)
        {
            if(!isOpen)
            {
                //Open the chest
                Openchest();
            }else
            {
                // Chest is alredy open
                ChestAlredyOpen();
            }
        }
    }

    public void Openchest()
    {
        //Dialog window on
        dialogBox.SetActive(true);
        //Dialog text = contenst text
        dialogText.text = contents.itemDescription;
        //add contents to the inventory
        playerInventory.AddItem(contents);
        playerInventory.currentItem = contents;
        //raise signal to the player to animate
        raiseItem.Raise();

        // raise the context clue
        context.Raise();
        // set the chest to opened
        isOpen = true;
        anim.SetBool("opened",true);
    }

    public void ChestAlredyOpen()
    {

        //Dialog off
        dialogBox.SetActive(false);
        // raise the signal to the player to stop animating
        raiseItem.Raise();



    }

    private void  OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") &&  !other.isTrigger && !playerInRange && !isOpen){
            context.Raise();
            playerInRange   = true;

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !other.isTrigger && playerInRange && !isOpen){
            context.Raise();
            playerInRange = false;

        }
    }
}
