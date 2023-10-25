using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameLogic : MonoBehaviour
{
   public AudioSource sound,music;
   public AudioClip rotS,dropS,clear1S,clear4S;
   public AudioClip[] treks = new AudioClip[5];
   public static int height=20;
   public static int width=10;
   public float speed,dropSpeed,sideSpeed;
   public Transform[,] grid=new Transform[width,height];
   public List<GameObject> blocks = new List<GameObject>();
   public List<GameObject> blocksPreview = new List<GameObject>();
   public GameObject pauseMenu,againBtn;
   public TMP_Text scoreText,speedText,pauseTitle,pauseScore,volumeText,rangtext,nameText,musicText;
   public float musicVolume=1f;
   private bool isOnPause;
   private string rang;
   //change color of grid
   public Image[] fields = new Image[3];
   public SpriteRenderer gameField;
   // speed and score
   private List<float> speedArray=new List<float>(){0,0.8f,0.6f,0.4f,0.3f,0.2f,0.15f,0.1f,0.08f,0.06f,0.04f};
   private List<int> trekBool=new List<int>(){1,1,1,1,1,1,1,1,1,1,1};
   public int score=0,lines;
   private float lineTimer;
   //movment
   public bool  isMovingRight,isMovingLeft,isRotate,isDropping;
   //Random Spawn
   private int nextBlockToSpawn;
   private void Start()
   {
     Application.targetFrameRate=60;

     music.clip=treks[Random.Range(0,treks.Length)];
     music.Play();
     speed=speedArray[1];
     nextBlockToSpawn=Random.Range(0,blocks.Count);
     OnSpawnBlock();
     gameField.color=new Color(0.08f,0.07f,0.12f,1);
     for(int i=0;i<3;i++){fields[i].color=new Color(0.23f,0.34f,0.42f,1);}
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
      
      //score system
      
      if(lineTimer>0)
      {
        lineTimer-=0.01f;
      }
        if(lines>0 && lineTimer<=0)
      {
         if(lines>3){score+=80;sound.PlayOneShot(clear4S,SaveData.Instance.volume);}
         else if(lines==3){score+=50;sound.PlayOneShot(clear4S,SaveData.Instance.volume);}
         else if(lines==2){score+=30;sound.PlayOneShot(clear1S,SaveData.Instance.volume);}
         else{score+=10;sound.PlayOneShot(clear1S,SaveData.Instance.volume);}
         ChangeSpeed();
         lines=0;
      }
      scoreText.text=$"{score}";
      speedText.text=$"{speedArray.IndexOf(speed)}";
      if(isDropping)
      {
        SmoothDropSpeed();
      }else
      {
        dropSpeed=0.2f;
      }
      if(score<200){rang="dweeb";}
        else if(score>200 && score<400){rang="liner";}
        else if(score>400 && score<700){rang="slammer";}
        else if(score>700 && score<900){rang="puzzler";}
        else if(score>900 && score<1100){rang="blockstar";}
        else if(score>1100 && score<1200){rang="gridmaster";}
        else if(score>1200 && score<1500){rang="blockboss";}
        else if(score>1500){rang="tetrisin";}
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
               lines++;
               lineTimer=0.1f;
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
      Instantiate(blocks[nextBlockToSpawn],new Vector3(4,18,0),Quaternion.identity);
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
        if(grid[i,18]!=null)
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
      SaveData.Instance.OnRecord(score,rang);
      againBtn.SetActive(true);
      music.Stop();
        pauseTitle.text="game over";
        pauseScore.text="score:"+$"{score}";
        rangtext.text="rang:"+rang;
        pauseMenu.SetActive(true);
        Time.timeScale=0;
     }
   }
   public void OnAgain()
   {
    for(int i=0;i<trekBool.Count;i++){trekBool[i]=1;}
    score=0;
    Time.timeScale=1;
     music.clip=treks[Random.Range(0,treks.Length)];
     music.Play();
      for(int i=0;i<width;i++)
     {
        for(int j=0;j<height;j++)
      {
        if(grid[i,j]!=null)
        {
           Destroy(grid[i,j].gameObject);
           grid[i,j]=null;
        } 
      }
     }
     speed=speedArray[1];
     pauseMenu.SetActive(false);
   }
   void ChangeSpeed()
   {
     if(score>=100 && score<300)
     {speed=speedArray[2];gameField.color=new Color(0.07f,0.09f,0.12f,1);for(int i=0;i<3;i++){fields[i].color=new Color(0.1f,0.2f,0.2f,1);}
       if(trekBool[2]>0){music.clip=treks[Random.Range(0,treks.Length)];music.Play();trekBool[2]=0;} }
      else if(score>=300 && score<500)
      {speed=speedArray[3];gameField.color=new Color(0.07f,0.11f,0.11f,1);for(int i=0;i<3;i++){fields[i].color=new Color(0.1f,0.21f,0.18f,1);}
      if(trekBool[3]>0){music.clip=treks[Random.Range(0,treks.Length)];music.Play();trekBool[3]=0;}}
      else if(score>=500 && score<700)
      {speed=speedArray[4];gameField.color=new Color(0.07f,0.12f,0.10f,1);for(int i=0;i<3;i++){fields[4].color=new Color(0.1f,0.21f,0.13f,1);}
      if(trekBool[4]>0){music.clip=treks[Random.Range(0,treks.Length)];music.Play();trekBool[4]=0;}}
        else if(score>=700 && score<900)
      {speed=speedArray[5];gameField.color=new Color(0.07f,0.12f,0.09f,1);for(int i=0;i<3;i++){fields[i].color=new Color(0.15f,0.21f,0.1f,1);}
      if(trekBool[5]>0){music.clip=treks[Random.Range(0,treks.Length)];music.Play();trekBool[5]=0;}}
         else if(score>=900 && score<1100)
         {speed=speedArray[6];gameField.color=new Color(0.09f,0.12f,0.07f,1);for(int i=0;i<3;i++){fields[i].color=new Color(0.21f,0.21f,0.1f,1);}
         if(trekBool[6]>0){music.clip=treks[Random.Range(0,treks.Length)];music.Play();trekBool[6]=0;}}
         else if(score>=1100 && score<1300)
         {speed=speedArray[7];gameField.color=new Color(0.12f,0.12f,0.07f,1);for(int i=0;i<3;i++){fields[i].color=new Color(0.21f,0.17f,0.1f,1);}
         if(trekBool[7]>0){music.clip=treks[Random.Range(0,treks.Length)];music.Play();trekBool[7]=0;}}
        else if(score>=1300 && score<1500)
        {speed=speedArray[8];gameField.color=new Color(0.12f,0.10f,0.07f,1);for(int i=0;i<3;i++){fields[i].color=new Color(0.21f,0.13f,0.1f,1);}
        if(trekBool[8]>0){music.clip=treks[Random.Range(0,treks.Length)];music.Play();trekBool[8]=0;}}
          else if(score>=1500 && score<1700)
          {speed=speedArray[9];gameField.color=new Color(0.12f,0.08f,0.07f,1);for(int i=0;i<3;i++){fields[i].color=new Color(0.21f,0.1f,0.1f,1);}
          if(trekBool[9]>0){music.clip=treks[Random.Range(0,treks.Length)];music.Play();trekBool[9]=0;}}
          else if(score>=1700){speed=speedArray[10];gameField.color=new Color(0.12f,0.07f,0.8f,1);for(int i=0;i<3;i++){fields[i].color=new Color(0.21f,0.1f,0.16f,1);}
          if(trekBool[10]>0){music.clip=treks[Random.Range(0,treks.Length)];music.Play();trekBool[10]=0;}}
     
   }

     public void OnExitGame()
{
   SaveData.Instance.OnRecord(score,rang);
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
     SaveData.Instance.OnRecord(score,rang);
     SceneManager.LoadScene("Menu");
   }
public void OnPause()
   {
     if(!isOnPause)
     {
      music.Pause();
      againBtn.SetActive(false);
       pauseMenu.SetActive(true);
       rangtext.text="rang:"+rang;
       pauseTitle.text="pause";
       pauseScore.text="score:"+$"{score}";
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
}
