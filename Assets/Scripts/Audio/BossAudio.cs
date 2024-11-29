using UnityEngine;

public class BossAudio : MonoBehaviour
{
    [Header("=====Audio Source=====")]
    [SerializeField] AudioSource SFXHand;
    [SerializeField] AudioSource SFXStaff;
    [SerializeField] AudioSource SFXBody;

    [Header("=====Audio Clip=====")]
    public AudioClip red;
    public AudioClip blue;
    public AudioClip purple;
    public AudioClip hurt;
    public AudioClip death;



    public void playSFXHand(AudioClip clip)
    {
        SFXHand.PlayOneShot(clip);
    }

    public void playSFXStaff(AudioClip clip)
    {
        SFXStaff.PlayOneShot(clip);
    }

    public void playSFXBody(AudioClip clip)
    {
        SFXStaff.PlayOneShot(clip);
    }


}
