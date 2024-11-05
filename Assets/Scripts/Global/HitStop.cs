using System.Collections;
using UnityEngine;

public class HitStop : MonoBehaviour
{
    public static HitStop instances;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        instances = this;  //FORMALITAS

    }

    public void init(float time ){
        StartCoroutine(stop(time));
    }

    private IEnumerator stop (float time){
        Time.timeScale = 0; //game diem
        yield return new WaitForSecondsRealtime(time);
        Time.timeScale = 1; //gamenyala
    }

}
