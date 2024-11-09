using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class HearthManager : MonoBehaviour
{
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite halfFullHeart;
    public Sprite emptyHeart;
    public FloatValue heartContainers;
    public FloatValue playerCurrentHealth;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitHearts();
    }

    void Update()
    {
        UpdateHearts();
    }



    // Update is called once per frame
    public void InitHearts()
    {
        for (int i=0;i<heartContainers.RuntimeValue;i++)
        {
            hearts[i].gameObject.SetActive(true);
            hearts[i].sprite = fullHeart;
        }
    }

    public void UpdateHearts()
    {
        Debug.Log("full");
        float tempHealth = playerCurrentHealth.RuntimeValue /2;
        for ( int i=0 ; i<heartContainers.RuntimeValue;i++)
        {
            if(i <= tempHealth-1)
            {
                Debug.Log("full");
                hearts[i].sprite = fullHeart;
                //full hearth
            }else if (i>=tempHealth)
            {
                //empty hearth
                 hearts[i].sprite = emptyHeart;
                  Debug.Log("Empty");
            }else
            {
                //half full hearth
                 hearts[i].sprite = halfFullHeart;
                  Debug.Log("halffull");
            }
        }

    }
}
