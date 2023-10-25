using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TableSorter : MonoBehaviour
{
    public TMP_Text[] recordTexts=new TMP_Text[4];
    public TMP_Text[] nameTexts=new TMP_Text[4];
    public TMP_Text[] dateTexts=new TMP_Text[4];
    void Start()
    {
        if(SaveData.Instance.records.Length!=null)
        {
           for(int i=0;i<4;i++)
        {
            recordTexts[i].text=$"{SaveData.Instance.records[i]}";
            nameTexts[i].text=SaveData.Instance.names[i];
            dateTexts[i].text=SaveData.Instance.dates[i];
        }
        }
    }
}
