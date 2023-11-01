using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Data 

{
   
    public int sprite,col;
   public string playerName;
   public float volume,vibration;
   public int[] records;
   public string[] names;
   public string[] rangs;
   public int score,lines;
   public int[,] grid;

  
   public Data (SaveData player)
 {
  
  col=player.col;
   sprite=player.sprite;
    playerName=player.playerName;
    volume=player.volume;
    vibration=player.vibration;
    records=player.records;
    names=player.names;
    rangs=player.rangs;
    score=player.score;
    lines=player.lines;
    grid=player.grid;
 }
}