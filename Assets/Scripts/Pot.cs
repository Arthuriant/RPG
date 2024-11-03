using System.Collections;
using UnityEngine;

public class Pot : MonoBehaviour
{
    private Animator anim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Smash(){
        anim.SetBool("smash", true);
        StartCoroutine(breakCO());
    }

    IEnumerator breakCO(){
        yield return new WaitForSeconds(.3f);
        this.gameObject.SetActive(false);
    }
}
