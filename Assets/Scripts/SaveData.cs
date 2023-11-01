using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public class SaveData : MonoBehaviour
{
    public static SaveData Instance;

   

   public int sprite=0,col=0;

   public string playerName="player";
   public float volume=1,vibration=1;
   public int[] records=new int[4]{0,0,0,0};
   public string[] names=new string[4]{"-","-","-","-"};
   //rang
   public string[] rangs=new string[4]{"-","-","-","-"};
   
   public int score,lines;
   public int[,] grid=new int[10,22];
   

   

    private void Awake()
   {
    
    string path= Application.persistentDataPath + "/TrueTetris.sav";
 
   if(File.Exists(path))
    {
       Data data = SaveSystem.loadData();
      
       

       if(data.sprite!=null){sprite=data.sprite;}
       if(data.col!=null){col=data.col;}
       if(data.playerName!=null){playerName=data.playerName;}
       if(data.vibration!=null){vibration=data.vibration;}
       if(data.volume!=null){volume=data.volume;}
       if(data.records!=null){records=data.records;}
      if(data.names!=null){names=data.names;}
       if(data.rangs!=null){rangs=data.rangs;}
       if(data.score!=null){score=data.score;}
       if(data.lines!=null){lines=data.lines;}
       if(data.grid!=null){grid=data.grid;}
       
    }
    
     if(Instance ==null)
     {
       DontDestroyOnLoad(gameObject);
       Instance=this;
     } else if(Instance!=this){Destroy(gameObject);}
   }
   
   
}