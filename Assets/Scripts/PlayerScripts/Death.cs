using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Death : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(death());
    }

    private IEnumerator death()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("StartMenu");
    }
}
