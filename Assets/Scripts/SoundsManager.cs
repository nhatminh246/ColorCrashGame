using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static SoundsManager Instance;
    public AudioSource audioSource;
    public AudioClip bgm ;
    public AudioClip shoot;
    public AudioClip kill;
    public AudioClip die;
    void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayOneShotAudioKill()
    {
        audioSource.PlayOneShot(kill);
    }
    public void PlayOneShotAudioDie()
    {
        audioSource.PlayOneShot(die);
    }
    public void PlayOneShotAudioShoot()
    {
        audioSource.PlayOneShot(shoot);
    }

    // Update is called once per frame

}
