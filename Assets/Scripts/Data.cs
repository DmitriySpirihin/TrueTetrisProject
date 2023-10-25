using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Data 

{
   
    public int record;
   public string playerName;
   public string dateString;
   public float volume,vibration;
   public int[] records;
   public string[] names;
   public string[] dates;


   public Data (SaveData player)
 {
    record=player.record;
    playerName=player.playerName;
    dateString=player.dateString;
    volume=player.volume;
    vibration=player.vibration;
    records=player.records;
    names=player.names;
    dates=player.dates;
 }
}