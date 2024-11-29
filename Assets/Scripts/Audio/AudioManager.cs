using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("=====Audio Source=====")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;
    [SerializeField] AudioSource SFXSource2;

    [Header("=====Audio Clip=====")]
    public AudioClip backgroundDugeon;
    public AudioClip hitPlayer;
    public AudioClip hitEnemy;
    public AudioClip hitEnemydie;
    public AudioClip hitBos;
    public AudioClip swordEffect;
    public AudioClip doorOpen;
    public AudioClip doorClosed;
    public AudioClip switchPushed;
    public AudioClip chestOpen;
    public AudioClip hpUp;
    public AudioClip dugeonDiscovery;


    private void Start()
    {
        musicSource.clip = backgroundDugeon;
        musicSource.Play();
    }

    public void playSFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    public void playSFX2(AudioClip clip)
    {
        SFXSource2.PlayOneShot(clip);
    }

}
