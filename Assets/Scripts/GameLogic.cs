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
   public static int height=22;
   public static int width=10;
   public float speed,dropSpeed,sideSpeed;
   public Transform[,] grid=new Transform[width,height];
   public int[,] gridInt=new int[10,22];
   public List<GameObject> blocks = new List<GameObject>();
   public List<GameObject> blocksPreview = new List<GameObject>();
   public GameObject pauseMenu,simpleBlock;
   public TMP_Text scoreText,speedText,pauseTitle,pauseScore,volumeText,rangtext,nameText,musicText,linesText;
   public float musicVolume=1f;
   private bool isOnPause;
   private string rang;
   //change color of grid
   public Image[] fields = new Image[4];
   public SpriteRenderer gameField;
   // speed and score
   private List<float> speedArray=new List<float>(){0,0.5f,0.45f,0.4f,0.35f,0.3f,0.25f,0.2f,0.15f,0.1f,0.08f};
   private List<int> trekBool=new List<int>(){1,1,1,1,1,1,1,1,1,1,1};
   public int score=0,lines,tempLine;
   private float lineTimer;
   //movment
   public bool  isMovingRight,isMovingLeft,isRotate,isDropping;
   //Random Spawn
   private int nextBlockToSpawn;
   private void Start()
   {
    speed=speedArray[1];
     score=SaveData.Instance.score;
     lines=SaveData.Instance.lines;
     gridInt=SaveData.Instance.grid;
     RefillGrid();
     SetNewBlockColor();
     ChangeSpeed();

     music.clip=treks[Random.Range(0,treks.Length)];
     music.Play();
     
     nextBlockToSpawn=Random.Range(0,blocks.Count);
     OnSpawnBlock();
     gameField.color=new Color(0.08f,0.07f,0.12f,0.96f);
     for(int i=0;i<4;i++){fields[i].color=new Color(0.23f,0.34f,0.42f,1);}
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
        if(tempLine>0 && lineTimer<=0)
      {
         if(tempLine>3){score+=250;sound.PlayOneShot(clear4S,SaveData.Instance.volume*0.5f);}
         else if(tempLine==3){score+=150;sound.PlayOneShot(clear4S,SaveData.Instance.volume*0.5f);}
         else if(tempLine==2){score+=50;sound.PlayOneShot(clear1S,SaveData.Instance.volume*0.5f);}
         else{score+=10;sound.PlayOneShot(clear1S,SaveData.Instance.volume*0.5f);}
         ChangeSpeed();
         tempLine=0;
      }
      linesText.text=$"{lines}";
      scoreText.text=$"{Mathf.Round(score)}";
      speedText.text=$"{speedArray.IndexOf(speed)}";
      if(isDropping)
      {
        SmoothDropSpeed();
      }else
      {
        dropSpeed=0.2f;
      }
      if(score<1000){rang="dweeb";}
        else if(score>1000 && score<2000){rang="liner";}
        else if(score>2000 && score<3000){rang="slammer";}
        else if(score>3000 && score<4000){rang="puzzler";}
        else if(score>4000 && score<5000){rang="blockstar";}
        else if(score>5000 && score<6000){rang="gridmaster";}
        else if(score>6000 && score<7000){rang="blockboss";}
        else if(score>7000 && score<8000){rang="blockKing";}
        else if(score>8000 && score<9000){rang="blockmaestro";}
        else if(score>9000 && score<10000){rang="highblockster";}
        else if(score>10000){rang="tetrisin";}
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
               tempLine++;
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
        gridInt[j,i]=0;
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
           gridInt[j,y-1]=1;
           gridInt[j,y]=0;
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
       SetRecord(0);
       SaveData.Instance.score=0;
       SaveData.Instance.lines=0;
       SaveData.Instance.grid=new int[10,22];
       SaveSystem.SaveData(SaveData.Instance);
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
    lines=0;
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
           gridInt[i,j]=0;
        } 
      }
     }
     
     
     speed=speedArray[1];
     SetNewBlockColor();
     ChangeSpeed();
     pauseMenu.SetActive(false);
   }
   void ChangeSpeed()
   {
     if(lines>=20 && lines<40)
     {speed=speedArray[2];gameField.color=new Color(0.07f,0.09f,0.12f,0.96f);for(int i=0;i<4;i++){fields[i].color=new Color(0.1f,0.2f,0.2f,1);}
       if(trekBool[2]>0){music.clip=treks[Random.Range(0,treks.Length)];music.Play();SetNewBlockColor();trekBool[2]=0;} }
      else if(lines>=40 && lines<60)
      {speed=speedArray[3];gameField.color=new Color(0.07f,0.11f,0.11f,0.96f);for(int i=0;i<4;i++){fields[i].color=new Color(0.1f,0.21f,0.18f,1);}
      if(trekBool[3]>0){music.clip=treks[Random.Range(0,treks.Length)];music.Play();SetNewBlockColor();trekBool[3]=0;}}
      else if(lines>=60 && lines<80)
      {speed=speedArray[4];gameField.color=new Color(0.07f,0.12f,0.10f,0.96f);for(int i=0;i<4;i++){fields[i].color=new Color(0.1f,0.21f,0.13f,1);}
      if(trekBool[4]>0){music.clip=treks[Random.Range(0,treks.Length)];music.Play();SetNewBlockColor();trekBool[4]=0;}}
        else if(lines>=80 && lines<100)
      {speed=speedArray[5];gameField.color=new Color(0.07f,0.12f,0.09f,0.96f);for(int i=0;i<4;i++){fields[i].color=new Color(0.15f,0.21f,0.1f,1);}
      if(trekBool[5]>0){music.clip=treks[Random.Range(0,treks.Length)];music.Play();SetNewBlockColor();trekBool[5]=0;}}
         else if(lines>=100 && lines<120)
         {speed=speedArray[6];gameField.color=new Color(0.09f,0.12f,0.07f,0.96f);for(int i=0;i<4;i++){fields[i].color=new Color(0.21f,0.21f,0.1f,1);}
         if(trekBool[6]>0){music.clip=treks[Random.Range(0,treks.Length)];music.Play();SetNewBlockColor();trekBool[6]=0;}}
         else if(lines>=120 && lines<140)
         {speed=speedArray[7];gameField.color=new Color(0.12f,0.12f,0.07f,0.96f);for(int i=0;i<4;i++){fields[i].color=new Color(0.21f,0.17f,0.1f,1);}
         if(trekBool[7]>0){music.clip=treks[Random.Range(0,treks.Length)];music.Play();SetNewBlockColor();trekBool[7]=0;}}
        else if(lines>=140 && lines<160)
        {speed=speedArray[8];gameField.color=new Color(0.12f,0.10f,0.07f,0.96f);for(int i=0;i<4;i++){fields[i].color=new Color(0.21f,0.13f,0.1f,1);}
        if(trekBool[8]>0){music.clip=treks[Random.Range(0,treks.Length)];music.Play();SetNewBlockColor();trekBool[8]=0;}}
          else if(lines>=160 && lines<180)
          {speed=speedArray[9];gameField.color=new Color(0.12f,0.08f,0.07f,0.96f);for(int i=0;i<4;i++){fields[i].color=new Color(0.21f,0.1f,0.1f,1);}
          if(trekBool[9]>0){music.clip=treks[Random.Range(0,treks.Length)];music.Play();SetNewBlockColor();trekBool[9]=0;}}
          else if(lines>=180){speed=speedArray[10];gameField.color=new Color(0.14f,0.04f,0.09f,0.96f);for(int i=0;i<4;i++){fields[i].color=new Color(0.21f,0.1f,0.16f,1);}
          if(trekBool[10]>0){music.clip=treks[Random.Range(0,treks.Length)];music.Play();SetNewBlockColor();trekBool[10]=0;}}
     
   }

     public void OnExitGame()
{
  if(!IsGameOver())
  {
     SaveData.Instance.score=score;
     SaveData.Instance.lines=lines;
     SaveData.Instance.grid=gridInt;
     SaveSystem.SaveData(SaveData.Instance);
  }
  
        SetRecord(0);
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
     if(!IsGameOver())
    {
     SaveData.Instance.score=score;
     SaveData.Instance.lines=lines;
     SaveData.Instance.grid=gridInt;
     SaveSystem.SaveData(SaveData.Instance);
    }
      SetRecord(0);
     SceneManager.LoadScene("Menu");
   }
public void OnPause()
   {
     if(!isOnPause)
     {
      music.Pause();
     
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
void SetNewBlockColor()
{
  Color newColor=new Color(1,1,1,1);

            if(lines>=20 && lines<40){newColor=new Color(0.6f,1f,0.8f,1);}
            else if(lines>=40 && lines<60){newColor=new Color(0.5f,0.8f,0.8f,1);}
            else if(lines>=60 && lines<80){newColor=new Color(0.9f,0.9f,0.6f,1);}
            else if(lines>=80 && lines<100){newColor=new Color(0.85f,0.75f,0.55f,1);}
            else if(lines>=100 && lines<120){newColor=new Color(0.85f,0.65f,0.9f,1);}
            else if(lines>=120 && lines<140){newColor=new Color(0.72f,0.6f,0.8f,1);}
            else if(lines>=140 &&lines<160){newColor=new Color(0.85f,0.65f,0.5f,1);}
            else if(lines>=160 && lines<180){newColor=new Color(0.8f,0.4f,0.4f,1);}
            else if(lines>=180){newColor=new Color(0.4f,0.04f,0.03f,1);}
    for(int i=0;i<height;i++)
     {
        for(int j=0;j<width;j++)
      {
        if(grid[j,i]!=null)
        {
           grid[j,i].gameObject.GetComponent<SpriteRenderer>().color=newColor;
        }
      }
     }
}
  private void OnApplicationQuit()
  {
   
     SaveData.Instance.score=score;
     SaveData.Instance.lines=lines;
     SaveData.Instance.grid=gridInt;
     SaveSystem.SaveData(SaveData.Instance);
   
  }
  private void RefillGrid()
  {
      for(int i=0;i<height;i++)
     {
        for(int j=0;j<width;j++)
      {
        if(gridInt[j,i]>0)
        {
          GameObject brick = Instantiate(simpleBlock,new Vector3((float)j,(float)i,0),Quaternion.identity);
          grid[j,i]=brick.GetComponent<Transform>();  
        }
      }
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
