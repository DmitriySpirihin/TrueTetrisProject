using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimerLogic : MonoBehaviour
{
    public AudioSource sound,music;
   public AudioClip rotS,dropS;
   public AudioClip[] treks = new AudioClip[5];
   public static int height=22;
   public static int width=10;
   public float speed,dropSpeed,sideSpeed;

   public Transform[,] grid=new Transform[width,height];

   public List<GameObject> blocks = new List<GameObject>();
   public List<GameObject> blocksPreview = new List<GameObject>();
   public GameObject pauseMenu,simpleBlock;
   public TMP_Text timerText,timerTextOnField,pauseTitle,volumeText,nameText,musicText,scoreText,pauseScore,rangText;
   public float musicVolume=1f;
   private bool isOnPause;
   public string rang;
 
   // speed and score
   private List<float> speedArray=new List<float>(){0,0.5f,0.45f,0.4f,0.35f,0.3f,0.25f,0.2f,0.15f,0.1f,0.08f};
   private List<int> trekBool=new List<int>(){1,1,1,1,1,1,1,1,1,1,1};

   private int score;
   private float timer=10.5f,scoreCounter;
   public bool  isMovingRight,isMovingLeft,isRotate,isDropping;
   //Random Spawn
   private int nextBlockToSpawn;
                                 // start          start
   private void Start()
   {
    
      speed=speedArray[6];
      scoreText.text=$"{score}";
      music.clip=treks[Random.Range(0,treks.Length)];
      music.Play();
     nextBlockToSpawn=Random.Range(0,blocks.Count);
     OnSpawnBlock();
     musicText.text="music:on";
    
  if(SaveData.Instance.volume==0)
  {
    volumeText.text="sound:off";
  }else
  {
    volumeText.text="sound:on";
  }
    nameText.text=SaveData.Instance.playerName;
   }
   private void Update()
    {
     if(timer>1){timer-=0.01f;}
     else
     {
        // put method here
        MoveAllLinesUp();
        SpawnFirstLine();
        timer=10.9f;
     }
      scoreCounter+=Time.deltaTime;
      if(scoreCounter>1f){score+=6;scoreCounter=0f;}
     
     timerText.text=$"{Mathf.Floor(timer)}";
     timerTextOnField.text=$"{Mathf.Floor(timer)}";
     scoreText.text=$"{score}";
      
      if(isDropping)
      {
        SmoothDropSpeed();
      }else
      {
        dropSpeed=0.2f;
      }
      if(score<1000/5){rang="dweeb";}
        else if(score>1000/5 && score<2000/5){rang="liner";}
        else if(score>2000/5 && score<3000/5){rang="slammer";}
        else if(score>3000/5 && score<4000/5){rang="puzzler";}
        else if(score>4000/5 && score<5000/5){rang="blockstar";}
        else if(score>5000/5 && score<6000/5){rang="gridmaster";}
        else if(score>6000/5 && score<7000/5){rang="blockboss";}
        else if(score>7000/5 && score<8000/5){rang="blockKing";}
        else if(score>8000/5 && score<9000/5){rang="blockmaestro";}
        else if(score>9000/5 && score<10000/5){rang="highblockster";}
        else if(score>10000/5){rang="tetrisin";}
    
   }
   public void OnCheckLines()
   {
    sound.PlayOneShot(dropS,SaveData.Instance.volume);
   
     for(int i=height-1;i>-1;i--)
     {
        if(isLineFilled(i))
        {
               OnDeleteLine(i);
               OnMoveLineDown(i);
               
        }
     }
       
   }
   bool isLineFilled(int i)
   {
      for(int j=0;j<width;j++)
      {
        if(grid[j,i]==null)
        {
            return false;
        }
      }
      return true;
   }
   void OnDeleteLine(int i)
   {
     for(int j=0;j<width;j++)
      {
        Destroy(grid[j,i].gameObject);
        grid[j,i]=null;
      }
   }
   void OnMoveLineDown(int i)
   {
     for(int y=i;y<height;y++)
     {
        for(int j=0;j<width;j++)
      {
        if(grid[j,y]!=null)
        {
           grid[j,y-1]=grid[j,y];
           grid[j,y]=null;
           grid[j,y-1].transform.position-=new Vector3(0,1,0);
        }
      }
     }
   }
   public void OnSpawnBlock()
   {
      Instantiate(blocks[nextBlockToSpawn],new Vector3(4,20,0),Quaternion.identity);
      nextBlockToSpawn=Random.Range(0,blocks.Count);
      OnShowPreview(nextBlockToSpawn);
   }
   public void OnRotateBlock(){isRotate=true;}
   public void SmoothDropSpeed()
   {
      if(dropSpeed>0.02f)
      {
        dropSpeed-=0.008f; 
      }
   }
   void OnShowPreview(int i)
   {
     foreach(GameObject obj in blocksPreview)
     {
        obj.SetActive(false);
     }
      blocksPreview[i].SetActive(true);
   }
   bool IsGameOver()
   {
      for(int i=0;i<width;i++)
      {
        if(grid[i,20]!=null)
        {
            return true;
        }
      }
      return false;
   }
   public void OnGameOver()
   {
     if(IsGameOver())
     {
      music.Stop();
      pauseScore.text=$"score:{score}";
        pauseTitle.text="game over";
        rangText.text="rang:"+rang;
        SetRecord(2);
        pauseMenu.SetActive(true);
        Time.timeScale=0;
     }
   }
   public void OnAgain()
   {
    Time.timeScale=1;
     score=0;
     scoreText.text=$"{score}";
     for(int i=0;i<trekBool.Count;i++){trekBool[i]=1;}
     music.clip=treks[Random.Range(0,treks.Length)];
     music.Play();
      for(int i=0;i<height;i++)
     {
        for(int j=0;j<width;j++)
      {
        if(grid[j,i]!=null)
        {
           Destroy(grid[j,i].gameObject);
           grid[j,i]=null;
        } 
      }
     }
     
     speed=speedArray[6];
     pauseMenu.SetActive(false);
   }
   
   //                                                      new timer logic
    void MoveAllLinesUp()
    {
        for(int y=18;y>-1;y--)
     {
        for(int j=0;j<width;j++)
      {
        if(grid[j,y]!=null)
        {
           grid[j,y+1]=grid[j,y];
           grid[j,y]=null;
           grid[j,y+1].transform.position+=new Vector3(0,1,0);
        }
      }
     }
    }


    void SpawnFirstLine()
    {
      
         for(int j=0;j<width;j++)
        {
          
                int num = Random.Range(0,10);
               if(num>2)
               {
                   GameObject brick = Instantiate(simpleBlock,new Vector3((float)j,0,0),Quaternion.identity);
                   grid[j,0]=brick.GetComponent<Transform>();  
               }
        }
        
    }
    
      

     public void OnExitGame()
{
     SetRecord(2);
        Application.Quit();  
}
 
public void SetVolume()
{
  if(SaveData.Instance.volume>0)
  {
    SaveData.Instance.volume=0;
    volumeText.text="sound:off";
  SaveSystem.SaveData(SaveData.Instance);
  }else
  {
    SaveData.Instance.volume=1;
    volumeText.text="sound:on";
  SaveSystem.SaveData(SaveData.Instance);
  }
  
}

public void OnMenu()
   {
     Time.timeScale=1;
     
     SetRecord(2);
     SceneManager.LoadScene("Menu");
   }
public void OnPause()
   {
     if(!isOnPause)
     {
      music.Pause();
     
       pauseMenu.SetActive(true);
       pauseScore.text=$"score:{score}";
       rangText.text="rang:"+rang;
       pauseTitle.text="pause";
      
       Time.timeScale=0;
       isOnPause=true;
     }else
     {
       Time.timeScale=1;
       music.Play();
       pauseMenu.SetActive(false);
       isOnPause=false;
     }
   }
   public void SetMusic()
{
  if(musicVolume>0)
  {
    musicVolume=0;
    musicText.text="music:off";
  }else
  {
    musicVolume=1;
    musicText.text="music:on";
  }
  
}
   void SetRecord(int num)
   {
     if(SaveData.Instance.records[num]<score)
     {
        SaveData.Instance.records[num]=score;
        SaveData.Instance.rangs[num]=rang;
        SaveData.Instance.names[num]=SaveData.Instance.playerName;
        SaveSystem.SaveData(SaveData.Instance);
     }
   }
}
