using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
  public AudioSource sound,music;
  public AudioClip startSound;
    public TMP_Text nametext,volumeText;
    public GameObject aboutFrame;
    public TMP_InputField field;
   
   private void Awake()
   {
       sound=GetComponent<AudioSource>();
   }
   private void Start()
   {
      
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
    
    
   public void OnGame()
   {
     music.Stop();
     sound.PlayOneShot(startSound,SaveData.Instance.volume);
     Invoke("StartTrueGame",1f);
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

 void StartTrueGame()
 {
   SceneManager.LoadScene("Game");
 }

}
