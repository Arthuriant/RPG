using System.Threading;
using UnityEngine;

public class ContextClue : MonoBehaviour
{
    public GameObject contextClue;
    public bool contextActive = false;

    public void ChangeContext()
    {
        contextActive = !contextActive;
        if(contextActive)
        {
            contextClue.SetActive(true);
        }else
        {
            contextClue.SetActive(false); 
        }
    }
}
