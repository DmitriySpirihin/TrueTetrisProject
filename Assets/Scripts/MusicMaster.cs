using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicMaster : MonoBehaviour
{
    public GameType gameType;
    AudioSource sound;
    public float num=1f;
    public bool isGame;
    public GameLogic gl;
    public PuzzleLogic puzz;
    public TimerLogic timer;
    public ReversedLogic rev;
    void Start()
    {
        sound=GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameType==GameType.classic)
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
        else if(gameType==GameType.puzzle)
        {
           if(!isGame)
         {
           sound.volume=num*SaveData.Instance.volume;
         }
         else
         {
            sound.volume=num*SaveData.Instance.volume*puzz.musicVolume;
         }
        }
        else if(gameType==GameType.timer)
        {
           if(!isGame)
         {
           sound.volume=num*SaveData.Instance.volume;
         }
         else
         {
            sound.volume=num*SaveData.Instance.volume*timer.musicVolume;
         }
        }
        else if(gameType==GameType.reversed)
        {
           if(!isGame)
         {
           sound.volume=num*SaveData.Instance.volume;
         }
         else
         {
            sound.volume=num*SaveData.Instance.volume*rev.musicVolume;
         }
        }
    }


    public enum GameType
    {
        classic,puzzle,reversed,timer
    }
}
