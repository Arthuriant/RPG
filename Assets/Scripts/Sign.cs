using UnityEngine;
using UnityEngine.UI;

public class Sign : MonoBehaviour
{
    public GameObject dialogBox;
    public Text dialogText;
    public string dialog;
    public bool playerInRange;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   Debug.Log("on running");
        if(Input.GetKeyDown(KeyCode.Space) && playerInRange){
            if(dialogBox.activeInHierarchy){
                dialogBox.SetActive(false);
            }else{
                dialogBox.SetActive(true);
                dialogText.text = dialog;
            }
        }
    }

    void  OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")){
            playerInRange = true;

        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player")){
            playerInRange = false;
            dialogBox.SetActive(false);
        }
    }
}
