using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data
{
    public string sceneName;
    public Dictionary<string, PositonData> CharacterPosDic = new Dictionary<string, PositonData>();
    public Dictionary<string,float> CharacterFloatDic = new Dictionary<string,float>();
    public Dictionary<string,int> CharacterIntDic = new Dictionary<string,int>();
    public Dictionary<string,bool> BoolDic = new Dictionary<string,bool>();
    public Dictionary<string,bool> SceneDic = new Dictionary<string,bool>();

    public void SaveSceneInfo(List<SceneInfo> leves)
    {
        foreach (SceneInfo info in leves)
        {
            if (SceneDic.ContainsKey(info.sceneName))
            {
                SceneDic[info.sceneName] = info.isLoaded;
            }else
            {
                SceneDic.Add(info.sceneName, info.isLoaded);
            }
        }
    }
}

public class PositonData
{
    public float x; public float y; public float z;
    public PositonData() { }
    public PositonData(float x, float y, float z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }
    public Vector3 ToVector3()
    {
        return new Vector3(x, y, z);
    }
    public static PositonData Vector3ToPositonData(Vector3 vector3)
    {
        return new PositonData(vector3.x, vector3.y, vector3.z);    
    }
    public static implicit operator bool(PositonData v)
    {
        return !(v == null);
    }
}