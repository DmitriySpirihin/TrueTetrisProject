using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class PressedButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
  public GameType gameType;
   Button button;
   public GameLogic gameLogic;
   public PuzzleLogic puzz;
   public TimerLogic timer;
   public buttontype type;
   public ReversedLogic rev;

    private void Awake()
    {
      
      button = GetComponent<Button>();
    }
    
    void Start()
    {
        PointerEventData ped=new PointerEventData(EventSystem.current);
        ExecuteEvents.Execute(button.gameObject,ped,ExecuteEvents.pointerDownHandler);
         OnPointerUp(ped);
    }
    
    public void OnPointerDown(PointerEventData ped)
    {
        if(gameType==GameType.classic)
        {
             if(type==buttontype.left)
          {
           gameLogic.isMovingLeft=true; 
         }
          else if(type==buttontype.right)
         {
            gameLogic.isMovingRight=true;
         }
           else if(type==buttontype.drop)
         {
           gameLogic.isDropping=true;
           }
        }
        else if(gameType==GameType.puzzle)
        {
             if(type==buttontype.left)
          {
           puzz.isMovingLeft=true; 
         }
          else if(type==buttontype.right)
         {
            puzz.isMovingRight=true;
         }
           else if(type==buttontype.drop)
         {
           puzz.isDropping=true;
           }
        }
        else if(gameType==GameType.timer)
        {
             if(type==buttontype.left)
          {
           timer.isMovingLeft=true; 
         }
          else if(type==buttontype.right)
         {
            timer.isMovingRight=true;
         }
           else if(type==buttontype.drop)
         {
           timer.isDropping=true;
           }
        }
        else if(gameType==GameType.reversed)
        {
             if(type==buttontype.left)
          {
           rev.isMovingLeft=true; 
         }
          else if(type==buttontype.right)
         {
            rev.isMovingRight=true;
         }
           else if(type==buttontype.drop)
         {
           rev.isDropping=true;
           }
        }
      
    }
    public void OnPointerUp(PointerEventData ped)
    {
      if(gameType==GameType.classic)
      {
           if(type==buttontype.left)
        {
        gameLogic.isMovingLeft=false; 
        }
        else if(type==buttontype.right)
        {
         gameLogic.isMovingRight=false;
        }
        else if(type==buttontype.drop)
        {
          gameLogic.isDropping=false;
        }
      }
        else if(gameType==GameType.puzzle)
      {
           if(type==buttontype.left)
        {
        puzz.isMovingLeft=false; 
        }
        else if(type==buttontype.right)
        {
         puzz.isMovingRight=false;
        }
        else if(type==buttontype.drop)
        {
          puzz.isDropping=false;
        }
      }
       else if(gameType==GameType.timer)
      {
           if(type==buttontype.left)
        {
        timer.isMovingLeft=false; 
        }
        else if(type==buttontype.right)
        {
         timer.isMovingRight=false;
        }
        else if(type==buttontype.drop)
        {
          timer.isDropping=false;
        }
      }
      else if(gameType==GameType.reversed)
      {
           if(type==buttontype.left)
        {
        rev.isMovingLeft=false; 
        }
        else if(type==buttontype.right)
        {
         rev.isMovingRight=false;
        }
        else if(type==buttontype.drop)
        {
          rev.isDropping=false;
        }
      }
    }
    
    public enum buttontype
    {
        left,right,drop
    }
    public enum GameType
    {
        classic,puzzle,reversed,timer
    }
    
}

