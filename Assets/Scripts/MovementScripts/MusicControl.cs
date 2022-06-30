using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicControl : MonoBehaviour
{

    [FMODUnity.EventRef]
    public string Ambience = "event:/Ambience/Underwater";

    FMOD.Studio.EventInstance musicEV;

    // Start is called before the first frame update
    void Start()
    {
        musicEV = FMODUnity.RuntimeManager.CreateInstance(Ambience);
    }

    public void StartMusic()
    {
        musicEV.start();
    }

    public void StopMusic()
    {
        musicEV.setVolume(0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
