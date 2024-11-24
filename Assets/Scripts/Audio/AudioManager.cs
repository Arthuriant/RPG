using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("=====Audio Source=====")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("=====Audio Clip=====")]
    public AudioClip backgroundDugeon;
    public AudioClip hitEnemy;
    public AudioClip hitBos;
    public AudioClip swordEffect;

    private void Start()
    {
        musicSource.clip = backgroundDugeon;
        musicSource.Play();
    }

}
