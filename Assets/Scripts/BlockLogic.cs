using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockLogic : MonoBehaviour
{
    public Vector3 rotationPoint;
    GameLogic gameLogic;
    public static int height=20;
    public static int width=10;
    private float counter=0f,sideCounter=0f;
    private bool isMovable=true;
    void Start()
    {
        gameLogic=GameObject.Find("GameController").GetComponent<GameLogic>();
    }

    // Update is called once per frame
    void Update()
    {
        // blocks fall
        if(isMovable)
        {
            sideCounter+=Time.deltaTime;
           counter+=Time.deltaTime;
           if(!gameLogic.isDropping)
           {
             if(counter>=gameLogic.speed)
             {
                gameObject.transform.position-=new Vector3(0,1,0);
                if(!isBlockValid())
                {
                    gameObject.transform.position+=new Vector3(0,1,0);
                   
                    OnFillGrid();
                    gameLogic.OnGameOver();
                    gameLogic.OnCheckLines();
                    gameLogic.OnSpawnBlock();
                    isMovable=false;
                }
                counter=0f;
             } 
           }else
           {
              if(counter>=gameLogic.dropSpeed)
             {
                gameObject.transform.position-=new Vector3(0,1,0);
                if(!isBlockValid())
                {
                    gameObject.transform.position+=new Vector3(0,1,0);
                  
                    OnFillGrid();
                    gameLogic.OnGameOver();
                    gameLogic.OnCheckLines();
                    gameLogic.OnSpawnBlock();
                    gameLogic.dropSpeed=0.2f;
                    isMovable=false;
                }
                counter=0f;
             } 
           }
           // moving
           if(gameLogic.isMovingLeft)
           {
             
             if(sideCounter>=0.15f)
             {
                gameObject.transform.position-=new Vector3(1,0,0);
             if(!isBlockValid())
                {
                    gameObject.transform.position+=new Vector3(1,0,0);
                }
               sideCounter=0f;
             }
             
           }
           else if(gameLogic.isMovingRight)
           {
            
             if(sideCounter>=0.15f)
             {
                gameObject.transform.position+=new Vector3(1,0,0);
              if(!isBlockValid())
                {
                    gameObject.transform.position-=new Vector3(1,0,0);
                }
                 sideCounter=0f;
             }
           }

           if(gameLogic.isRotate)
           {
            transform.RotateAround(transform.TransformPoint(rotationPoint),new Vector3(0,0,1),90);
            if(!isBlockValid())
            {
                transform.RotateAround(transform.TransformPoint(rotationPoint),new Vector3(0,0,1),-90);
            }else
            {
               
                gameLogic.sound.PlayOneShot(gameLogic.rotS,SaveData.Instance.volume);
            }

            gameLogic.isRotate=false;
           }


        }
        //rotation
        
    }
    void OnFillGrid()
    {
       foreach(Transform block in transform)
        {
            int xPos=Mathf.RoundToInt(block.transform.position.x);
            int yPos=Mathf.RoundToInt(block.transform.position.y);
            block.GetComponent<SpriteRenderer>().color=new Color(1,1,1,1);
            gameLogic.grid[xPos,yPos]=block;
        }
    }
    
    bool isBlockValid()
    {
        foreach(Transform block in transform)
        {
            int xPos=Mathf.RoundToInt(block.transform.position.x);
            int yPos=Mathf.RoundToInt(block.transform.position.y);
            if(xPos<0 || xPos>=width || yPos < 0 || yPos>=height)
            {
                return false;
            }
            if(gameLogic.grid[xPos,yPos]!=null)
            {
                return false;
            }
        }
        return true;
    }
}
