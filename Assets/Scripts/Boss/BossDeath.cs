using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossDeath : MonoBehaviour
{
    protected BossAudio audioManager;
    private SpriteRenderer sprite;

    private void Awake()
    {
        audioManager = GameObject.FindWithTag("AudioBoss").GetComponent<BossAudio>();
    }

    void Start()
    {
        // Cari SpriteRenderer di objek ini atau anak-anaknya
        sprite = GetComponent<SpriteRenderer>() ?? GetComponentInChildren<SpriteRenderer>();

        if (sprite == null)
        {
            Debug.LogError("SpriteRenderer not found. Please attach a SpriteRenderer component.");
        }

        StartCoroutine(death());
    }

    private IEnumerator death()
    {
        audioManager.playSFXBody(audioManager.death);

        if (sprite != null)
        {
            StartCoroutine(hitflash());
        }
        else
        {
            Debug.LogWarning("Skipping hitflash due to missing SpriteRenderer.");
        }

        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("StartMenu");
    }

    private IEnumerator hitflash()
    {
        int i = 0;
        while (i < 100)
        {
            sprite.color = Color.red;
            yield return new WaitForSeconds(0.5f);
            sprite.color = Color.white;
            yield return new WaitForSeconds(0.5f);
            i++;
        }
    }
}
