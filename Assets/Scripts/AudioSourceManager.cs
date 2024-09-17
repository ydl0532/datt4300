using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceManager : MonoBehaviour
{
    public static AudioSourceManager instance { get; private set; }

    private void Awake()
    {
        instance = this;
    }

}
