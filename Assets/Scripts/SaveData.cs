using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public class SaveData : MonoBehaviour
{
    public static SaveData Instance;

   public int record;
   public string dateString;
   public string playerName="player";
   public float volume=1,vibration=1;
   public int[] records=new int[4]{0,0,0,0};
   public string[] names=new string[4]{"-","-","-","-"};
   //rang
   public string[] dates=new string[4]{"-","-","-","-"};


    private void Awake()
   {
    
    string path= Application.persistentDataPath + "/TrueTetris.save";

   if(File.Exists(path))
    {
       Data data = SaveSystem.loadData();

       if(data.record!=null){record=data.record;}
       if(data.playerName!=null){playerName=data.playerName;}
       if(data.vibration!=null){vibration=data.vibration;}
       if(data.volume!=null){volume=data.volume;}
       if(data.records!=null){records=data.records;}
      if(data.names!=null){names=data.names;}
       if(data.dates!=null){dates=data.dates;}
       
    }

     if(Instance ==null)
     {
       DontDestroyOnLoad(gameObject);
       Instance=this;
     } else if(Instance!=this){Destroy(gameObject);}
   }
   
    public void OnRecord(int rec,string rang)
    {
       if(rec>record)
      {
         record=rec;
       
          if(records[0]<record)
          {
            records[3]=records[2];records[2]=records[1];records[1]=records[0];records[0]=record;
            names[3]=names[2];names[2]=names[1];names[1]=names[0];names[0]=playerName;
            dates[3]=dates[2];dates[2]=dates[1];dates[1]=dates[0];dates[0]=rang;
          }
        SaveSystem.SaveData(SaveData.Instance);
       
      }
    }
}