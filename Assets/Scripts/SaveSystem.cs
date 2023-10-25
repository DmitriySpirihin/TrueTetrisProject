using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem 
{

  public static void SaveData(SaveData player)
  {
   BinaryFormatter bf=new BinaryFormatter();
   string path= Application.persistentDataPath + "/TrueTetris.save";
   FileStream file=new FileStream(path,FileMode.Create);

    Data data=new Data(player);

    bf.Serialize(file,data);
    file.Close();
    //Debug.Log(path);
  } 

 public static Data loadData()
{
    string path= Application.persistentDataPath + "/TrueTetris.save";
    if(File.Exists(path))
    {
      BinaryFormatter bf=new BinaryFormatter();
      FileStream file=new FileStream(path,FileMode.Open);
      Data data = bf.Deserialize(file) as Data;
      file.Close();
    return data;
    }
    else
    {return null;}
}

}