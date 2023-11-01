using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockLogic : MonoBehaviour
{
    public BlockType type;
    public Vector3 rotationPoint;
    GameLogic gameLogic;
    public static int height=22;
    public static int width=10;
    private float counter=0f,sideCounter=0f,sideSpeed=0.15f;
    private bool isMovable=true;
    void Start()
    {
        gameLogic=GameObject.Find("GameController").GetComponent<GameLogic>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameLogic.isMovingLeft || gameLogic.isMovingRight)
        {
            SmoothSideSpeed();
        }else{sideSpeed=0.2f;}
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
                   gameLogic.score++;
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
                    gameLogic.score++;
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
             
             if(sideCounter>=sideSpeed)
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
            
             if(sideCounter>=sideSpeed)
             {
                gameObject.transform.position+=new Vector3(1,0,0);
              if(!isBlockValid())
                {
                    gameObject.transform.position-=new Vector3(1,0,0);
                }
                 sideCounter=0f;
             }
           }
               //rotation
           if(gameLogic.isRotate && type!=BlockType.typeO)
           {
             if(type==BlockType.typeT)
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
             else if(type==BlockType.typeI)
             {
                 transform.RotateAround(transform.TransformPoint(rotationPoint),new Vector3(0,0,1),90);
               if(!isBlockValid())
               {
                 if(transform.position.x>8)
                 {
                    gameObject.transform.position-=new Vector3(2,0,0);
                    if(!isBlockValid())
                    {
                        gameObject.transform.position+=new Vector3(2,0,0);
                        transform.RotateAround(transform.TransformPoint(rotationPoint),new Vector3(0,0,1),-90);
                    }
                 }
                 else if(transform.position.x<1)
                 {
                    gameObject.transform.position+=new Vector3(2,0,0);
                    if(!isBlockValid())
                    {
                        gameObject.transform.position-=new Vector3(2,0,0);
                        transform.RotateAround(transform.TransformPoint(rotationPoint),new Vector3(0,0,1),-90);
                    }
                 }
                 else
                 {
                   transform.RotateAround(transform.TransformPoint(rotationPoint),new Vector3(0,0,1),-90);
                 }
                 
               }else
               {
               
                gameLogic.sound.PlayOneShot(gameLogic.rotS,SaveData.Instance.volume);
               }

                gameLogic.isRotate=false;
             }
             else if(type==BlockType.typeOther)
             {
                 transform.RotateAround(transform.TransformPoint(rotationPoint),new Vector3(0,0,1),90);
               if(!isBlockValid())
               {
                        gameObject.transform.position-=new Vector3(1,0,0);
                     if(!isBlockValid())
                     {
                         gameObject.transform.position+=new Vector3(2,0,0);
                          if(!isBlockValid())
                       {
                         gameObject.transform.position-=new Vector3(1,0,0);
                         transform.RotateAround(transform.TransformPoint(rotationPoint),new Vector3(0,0,1),-90);
                       }
                     }
               }else
               {
               
                gameLogic.sound.PlayOneShot(gameLogic.rotS,SaveData.Instance.volume);
               }

                gameLogic.isRotate=false;
             }
           }


        }
       
        
    }
    void OnFillGrid()
    {
       foreach(Transform block in transform)
        {
            int xPos=Mathf.RoundToInt(block.transform.position.x);
            int yPos=Mathf.RoundToInt(block.transform.position.y);
            if(gameLogic.lines<20){block.GetComponent<SpriteRenderer>().color=new Color(1,1,1,1);}
            else if(gameLogic.lines>=20 && gameLogic.lines<40){block.GetComponent<SpriteRenderer>().color=new Color(0.6f,1f,0.8f,1);}
            else if(gameLogic.lines>=40 && gameLogic.lines<60){block.GetComponent<SpriteRenderer>().color=new Color(0.5f,0.8f,0.8f,1);}
            else if(gameLogic.lines>=60 && gameLogic.lines<80){block.GetComponent<SpriteRenderer>().color=new Color(0.9f,0.9f,0.6f,1);}
            else if(gameLogic.lines>=80 && gameLogic.lines<100){block.GetComponent<SpriteRenderer>().color=new Color(0.85f,0.75f,0.55f,1);}
            else if(gameLogic.lines>=100 && gameLogic.lines<120){block.GetComponent<SpriteRenderer>().color=new Color(0.85f,0.65f,0.9f,1);}
            else if(gameLogic.lines>=120 && gameLogic.lines<140){block.GetComponent<SpriteRenderer>().color=new Color(0.72f,0.6f,0.8f,1);}
            else if(gameLogic.lines>=140 && gameLogic.lines<160){block.GetComponent<SpriteRenderer>().color=new Color(0.85f,0.65f,0.5f,1);}
            else if(gameLogic.lines>=160 && gameLogic.lines<180){block.GetComponent<SpriteRenderer>().color=new Color(0.8f,0.4f,0.4f,1);}
            else if(gameLogic.lines>=180){block.GetComponent<SpriteRenderer>().color=new Color(0.4f,0.04f,0.03f,1);}
            gameLogic.grid[xPos,yPos]=block;
            gameLogic.gridInt[xPos,yPos]=1;
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
    public enum BlockType
    {
        typeI,typeO,typeOther,typeT
    }
    public void SmoothSideSpeed()
   {
      if(sideSpeed>0.03f)
      {
        sideSpeed-=0.008f; 
      }
   }
}
