using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpriteChanger : MonoBehaviour
{
    public bool isSimple;
    public Type type;
    private Color[] col=new Color[17]
    {
        new Color(1f,0.45f,0.45f,1),
        new Color(1f,0.43f,0.7f,1),
        new Color(0.67f,0.9f,0.25f,1),
        new Color(1f,0.7f,0f,1),
        new Color(0.2f,1f,1f,1),
        new Color(0.64f,0.4f,0.9f,1),
        new Color(0.3f,0.5f,0.95f,1),

        new Color(1f,0.61f,0.83f,1),
        new Color(1f,0.6f,0.6f,1),
        new Color(0.5f,0.9f,0.9f,1),
        new Color(0.6f,1f,0.7f,1),
        new Color(0.6f,0.6f,1f,1),
        new Color(1f,1f,0.6f,1),
        new Color(0.9f,0.7f,0.5f,1),
          //14 dark gray
        new Color(0.7f,0.7f,0.7f,1),
        new Color(0f,0f,0f,1),
        new Color(0f,0.5f,0.5f,1)
    };
    public List<Sprite> sprites = new List<Sprite>();
    
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite=sprites[SaveData.Instance.sprite];
      if(!isSimple)
      {
        if(SaveData.Instance.col==0)
        {
            if(type==Type.I){GetComponent<SpriteRenderer>().color=col[0];}
            else if(type==Type.O){GetComponent<SpriteRenderer>().color=col[1];}
            else if(type==Type.L){GetComponent<SpriteRenderer>().color=col[2];}
            else if(type==Type.J){GetComponent<SpriteRenderer>().color=col[3];}
            else if(type==Type.T){GetComponent<SpriteRenderer>().color=col[4];}
            else if(type==Type.S){GetComponent<SpriteRenderer>().color=col[5];}
            else if(type==Type.Z){GetComponent<SpriteRenderer>().color=col[6];}
        }
        else 
        if(SaveData.Instance.col==1)
        {
            if(type==Type.I){GetComponent<SpriteRenderer>().color=col[0+7];}
            else if(type==Type.O){GetComponent<SpriteRenderer>().color=col[1+7];}
            else if(type==Type.L){GetComponent<SpriteRenderer>().color=col[2+7];}
            else if(type==Type.J){GetComponent<SpriteRenderer>().color=col[3+7];}
            else if(type==Type.T){GetComponent<SpriteRenderer>().color=col[4+7];}
            else if(type==Type.S){GetComponent<SpriteRenderer>().color=col[5+7];}
            else if(type==Type.Z){GetComponent<SpriteRenderer>().color=col[6+7];}
        }
        if(SaveData.Instance.col==2)
        {
            if(type==Type.I){GetComponent<SpriteRenderer>().color=col[14];}
            else if(type==Type.O){GetComponent<SpriteRenderer>().color=col[14];}
            else if(type==Type.L){GetComponent<SpriteRenderer>().color=col[14];}
            else if(type==Type.J){GetComponent<SpriteRenderer>().color=col[14];}
            else if(type==Type.T){GetComponent<SpriteRenderer>().color=col[14];}
            else if(type==Type.S){GetComponent<SpriteRenderer>().color=col[14];}
            else if(type==Type.Z){GetComponent<SpriteRenderer>().color=col[14];}
        }
         if(SaveData.Instance.col==3)
        {
            if(type==Type.I){GetComponent<SpriteRenderer>().color=col[15];}
            else if(type==Type.O){GetComponent<SpriteRenderer>().color=col[15];}
            else if(type==Type.L){GetComponent<SpriteRenderer>().color=col[15];}
            else if(type==Type.J){GetComponent<SpriteRenderer>().color=col[15];}
            else if(type==Type.T){GetComponent<SpriteRenderer>().color=col[15];}
            else if(type==Type.S){GetComponent<SpriteRenderer>().color=col[15];}
            else if(type==Type.Z){GetComponent<SpriteRenderer>().color=col[15];}
        }
         if(SaveData.Instance.col==4)
        {
            if(type==Type.I){GetComponent<SpriteRenderer>().color=col[16];}
            else if(type==Type.O){GetComponent<SpriteRenderer>().color=col[16];}
            else if(type==Type.L){GetComponent<SpriteRenderer>().color=col[16];}
            else if(type==Type.J){GetComponent<SpriteRenderer>().color=col[16];}
            else if(type==Type.T){GetComponent<SpriteRenderer>().color=col[16];}
            else if(type==Type.S){GetComponent<SpriteRenderer>().color=col[16];}
            else if(type==Type.Z){GetComponent<SpriteRenderer>().color=col[16];}
        }
      }
        
    }

    
    public enum Type
    {
        I,O,L,J,T,S,Z
    }
}
