using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSSetup : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private int frameRate = 70;
    void Awake()
    {
        Application.targetFrameRate = frameRate;
    }
}
