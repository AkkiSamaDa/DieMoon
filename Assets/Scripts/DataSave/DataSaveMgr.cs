using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataSaveMgr:MonoBehaviour
{
    List<ISaveable> savebleList = new();
    public List<string> saveInfo = new();
    public Data data = new Data();

    public static DataSaveMgr Instance;
    private void Awake()
    {
        
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    private void OnEnable()
    {
        //EventCenter.Instance.AddListener("LoadOver", DataSaveOnLoadSceneOver);
    }
    private void OnDisable()
    {
        //EventCenter.Instance.RemoveListener("LoadOver", DataSaveOnLoadSceneOver);
    }
    public void RegisterSaveData(ISaveable saveable)
    {
        if (!savebleList.Contains(saveable))
        {
            print($"注册当前对象：{saveable}");
            savebleList.Add(saveable);
            saveInfo.Add(saveable.ToString());
        }
    }

    public void UnRegisterSaveData(ISaveable saveable)
    {
        if (savebleList.Contains(saveable))
        {
            print($"注销当前对象：{saveable}");
            //saveable.RemoveFromData(data);
            savebleList.Remove(saveable);
            saveInfo.Remove(saveable.ToString());
        }
            
    }
    public bool SaveData()
    {
        //储存背包数据
        PlayerData.Instance.SaveBagItems();

        foreach(var saveble in savebleList)
        {
            //获得需要存储的数据
            saveble.GetSaveData(data);
            Debug.Log("正在存储的saveble对象为: " + saveble.ToString());
        }
        //TODO: 利用JsonMgr存储存档数据
        JsonMgr.Instance.SaveData(data, $"data_{ArchiveData.Instance.archiveId}.json");
        print("Josn数据存储成功！");
        return true;
    }
    public void DataSaveOnLoadSceneOver()
    {
        if(SceneLoadMgr.Instance.CurrentSceneName != "MenuScene")
            SaveData();
    }
    public void LoadData()
    {
        print($"开始加载存档,存档号：{ArchiveData.Instance.archiveId}，文件名：data_{ArchiveData.Instance.archiveId}.json，正在从本地获得数据");
        data = JsonMgr.Instance.LoadData<Data>( $"data_{ArchiveData.Instance.archiveId}.json");
        //TODO: 把存档数据读取出来，如果不存在，则新建存档
        if (data == null)
        {
            print("当前存档为空,开始执行初始化存档");
            InitData();
            print("初始化存档执行成功,开始加载场景");
            
        }
        PlayerData.Instance.LoadBagItems();
        UIMgr.Instance.ShowPanel<ProgressbarPanel>();
        //加载场景
        SceneLoadMgr.Instance.LoadScene(data.sceneName, () =>
        {
            
            //复原当前场景可存档物体数据
            foreach (var saveble in savebleList)
            {
                if(saveble!= null)
                    saveble.LoadData(data);
            }
            print("触发OnLoadData事件");
            //触发OnLoadData事件
            EventCenter.Instance.EventTrigger("OnLoadData");
        });
    }
    
    public void InitData()
    {
        data = new Data();
        data.sceneName = "Level_2";
        InitSceneDic();
        print("初始存档数据成功！");
    }
    void InitSceneDic()
    {
        data.SceneDic.Add("Level_2", false);
        data.SceneDic.Add("BossScene", false);
    }
}
