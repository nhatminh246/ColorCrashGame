using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static SoundsManager Instance;
    public AudioSource audioSource;
    public AudioClip bgm, kill, die,shoot;
    void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
    }
    
    // Update is called once per frame
    
}
