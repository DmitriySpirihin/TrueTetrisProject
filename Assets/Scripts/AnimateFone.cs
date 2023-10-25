using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateFone : MonoBehaviour
{
    public List<GameObject> figure =new List<GameObject>();
    public List<Vector3> vects =new List<Vector3>();
    void Start()
    {
        Invoke("spawnFigure",1f);
    }

   void spawnFigure()
   {
    Instantiate(figure[Random.Range(0,figure.Count)],vects[Random.Range(0,vects.Count)],Quaternion.identity);
     Invoke("spawnFigure",1f);
   }
}
