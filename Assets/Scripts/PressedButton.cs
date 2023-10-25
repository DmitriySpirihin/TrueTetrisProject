using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class PressedButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
   Button button;
   GameLogic gameLogic;
   public buttontype type;
   

    private void Awake()
    {
      gameLogic=GameObject.Find("GameController").GetComponent<GameLogic>();
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
    public void OnPointerUp(PointerEventData ped)
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
    
    public enum buttontype
    {
        left,right,drop
    }
    
}

