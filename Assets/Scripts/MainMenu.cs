using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
  public List<Sprite> sprites = new List<Sprite>();
  public List<Sprite> colsprites = new List<Sprite>();
  public SpriteRenderer brickChoosen,colChoosen;
  public AudioSource sound,music;
  public AudioClip startSound;
  private bool isChoosen;
    public TMP_Text nametext,volumeText;
    public GameObject aboutFrame,chooseFrame,selectGameFrame,loadPanel;
    public TMP_InputField field;
   
   private void Awake()
   {
       sound=GetComponent<AudioSource>();
   }
   private void Start()
   {
    Application.targetFrameRate=60;
      colChoosen.sprite=colsprites[SaveData.Instance.col];
      brickChoosen.sprite=sprites[SaveData.Instance.sprite];
        sound.volume=SaveData.Instance.volume;
        nametext.text=SaveData.Instance.playerName;
       
  if(SaveData.Instance.volume==0)
  {
    volumeText.text="sound:off";
  }else
  {
    volumeText.text="sound:on";
  }

  
   }
    
    
   public void OnSetSelectGameFrame()
   {
     selectGameFrame.SetActive(true);
   }
   public void OnUnSetSelectGameFrame()
   {
     selectGameFrame.SetActive(false);
   }
   public void OnExitGame()
{
        Application.Quit();  
}
 public void OnAbout()
{
        aboutFrame.SetActive(true);  
}
public void OnAboutQuit()
{
        aboutFrame.SetActive(false);  
}

public void SetName()
{
  nametext.text=field.GetComponentInChildren<TMP_Text>().text.ToLower();
  SaveData.Instance.playerName=nametext.text;
  SaveSystem.SaveData(SaveData.Instance);
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

 public void StartClassic()
 {
  loadPanel.SetActive(true);
  music.Stop();
   SceneManager.LoadScene("Game");
 }
 public void StartPuzzle()
 {
  loadPanel.SetActive(true);
  music.Stop();
   SceneManager.LoadScene("Puzzle");
 }
 public void StartTimer()
 {
  loadPanel.SetActive(true);
  music.Stop();
   SceneManager.LoadScene("Timer");
 }
 public void StartReversed()
 {
  loadPanel.SetActive(true);
  music.Stop();
   SceneManager.LoadScene("Reversed");
 }
  public void OnSpriteChange0()
  {
    SaveData.Instance.sprite=0;
    brickChoosen.sprite=sprites[SaveData.Instance.sprite];
    SaveSystem.SaveData(SaveData.Instance);
  }
  public void OnSpriteChange1()
  {
     SaveData.Instance.sprite=1;
     brickChoosen.sprite=sprites[SaveData.Instance.sprite];
    SaveSystem.SaveData(SaveData.Instance);
  }
  public void OnSpriteChange2()
  {
    SaveData.Instance.sprite=2;
    brickChoosen.sprite=sprites[SaveData.Instance.sprite];
    SaveSystem.SaveData(SaveData.Instance);
  }
  public void OnSpriteChange3()
  {
    SaveData.Instance.sprite=3;
    brickChoosen.sprite=sprites[SaveData.Instance.sprite];
    SaveSystem.SaveData(SaveData.Instance);
  }
  public void OnSpriteChange4()
  {
    SaveData.Instance.sprite=4;
    brickChoosen.sprite=sprites[SaveData.Instance.sprite];
    SaveSystem.SaveData(SaveData.Instance);
  }
  public void OnSpriteChange5()
  {
    SaveData.Instance.sprite=5;
    brickChoosen.sprite=sprites[SaveData.Instance.sprite];
    SaveSystem.SaveData(SaveData.Instance);
  }
  public void OnSpriteChange6()
  {
    SaveData.Instance.sprite=6;
    brickChoosen.sprite=sprites[SaveData.Instance.sprite];
    SaveSystem.SaveData(SaveData.Instance);
  }
  public void OnSpriteChange7()
  {
    SaveData.Instance.sprite=7;
    brickChoosen.sprite=sprites[SaveData.Instance.sprite];
    SaveSystem.SaveData(SaveData.Instance);
  }
  public void OnSpriteChange8()
  {
    SaveData.Instance.sprite=8;
    brickChoosen.sprite=sprites[SaveData.Instance.sprite];
    SaveSystem.SaveData(SaveData.Instance);
  }
  public void OnChoose()
  {
    if(!isChoosen)
    {
       chooseFrame.SetActive(true);
       isChoosen=true;
    }else
    {
       chooseFrame.SetActive(false);
       isChoosen=false;
    }
     
  }
  public void OnColChange0()
  {
    SaveData.Instance.col=0;
    colChoosen.sprite=colsprites[SaveData.Instance.col];
    SaveSystem.SaveData(SaveData.Instance);
  }
  public void OnColChange1()
  {
    SaveData.Instance.col=1;
    colChoosen.sprite=colsprites[SaveData.Instance.col];
    SaveSystem.SaveData(SaveData.Instance);
  }
  public void OnColChange2()
  {
    SaveData.Instance.col=2;
    colChoosen.sprite=colsprites[SaveData.Instance.col];
    SaveSystem.SaveData(SaveData.Instance);
  }
  public void OnColChange3()
  {
    SaveData.Instance.col=3;
    colChoosen.sprite=colsprites[SaveData.Instance.col];
    SaveSystem.SaveData(SaveData.Instance);
  }
  public void OnColChange4()
  {
    SaveData.Instance.col=4;
    colChoosen.sprite=colsprites[SaveData.Instance.col];
    SaveSystem.SaveData(SaveData.Instance);
  }
}
