using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGo : MonoBehaviour
{
    float angle;
    void Start()
    {
        angle=Random.Range(0.01f,0.2f);
        Destroy(gameObject,8f);
    }

    private void Update()
    {
        transform.eulerAngles-=new Vector3(0,0,angle);
    }
}
