using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicMaster : MonoBehaviour
{
    AudioSource sound;
    public float num=1f;
    public bool isGame;
    public GameLogic gl;
    void Start()
    {
        sound=GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isGame)
        {
           sound.volume=num*SaveData.Instance.volume;
        }
        else
        {
            sound.volume=num*SaveData.Instance.volume*gl.musicVolume;
        }
    }
}
