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
            print($"ע�ᵱǰ����{saveable}");
            savebleList.Add(saveable);
            saveInfo.Add(saveable.ToString());
        }
    }

    public void UnRegisterSaveData(ISaveable saveable)
    {
        if (savebleList.Contains(saveable))
        {
            print($"ע����ǰ����{saveable}");
            //saveable.RemoveFromData(data);
            savebleList.Remove(saveable);
            saveInfo.Remove(saveable.ToString());
        }
            
    }
    public bool SaveData()
    {
        //���汳������
        PlayerData.Instance.SaveBagItems();

        foreach(var saveble in savebleList)
        {
            //�����Ҫ�洢������
            saveble.GetSaveData(data);
            Debug.Log("���ڴ洢��saveble����Ϊ: " + saveble.ToString());
        }
        //TODO: ����JsonMgr�洢�浵����
        JsonMgr.Instance.SaveData(data, $"data_{ArchiveData.Instance.archiveId}.json");
        print("Josn���ݴ洢�ɹ���");
        return true;
    }
    public void DataSaveOnLoadSceneOver()
    {
        if(SceneLoadMgr.Instance.CurrentSceneName != "MenuScene")
            SaveData();
    }
    public void LoadData()
    {
        print($"��ʼ���ش浵,�浵�ţ�{ArchiveData.Instance.archiveId}���ļ�����data_{ArchiveData.Instance.archiveId}.json�����ڴӱ��ػ������");
        data = JsonMgr.Instance.LoadData<Data>( $"data_{ArchiveData.Instance.archiveId}.json");
        //TODO: �Ѵ浵���ݶ�ȡ��������������ڣ����½��浵
        if (data == null)
        {
            print("��ǰ�浵Ϊ��,��ʼִ�г�ʼ���浵");
            InitData();
            print("��ʼ���浵ִ�гɹ�,��ʼ���س���");
            
        }
        PlayerData.Instance.LoadBagItems();
        UIMgr.Instance.ShowPanel<ProgressbarPanel>();
        //���س���
        SceneLoadMgr.Instance.LoadScene(data.sceneName, () =>
        {
            
            //��ԭ��ǰ�����ɴ浵��������
            foreach (var saveble in savebleList)
            {
                if(saveble!= null)
                    saveble.LoadData(data);
            }
            print("����OnLoadData�¼�");
            //����OnLoadData�¼�
            EventCenter.Instance.EventTrigger("OnLoadData");
        });
    }
    
    public void InitData()
    {
        data = new Data();
        data.sceneName = "Level_2";
        InitSceneDic();
        print("��ʼ�浵���ݳɹ���");
    }
    void InitSceneDic()
    {
        data.SceneDic.Add("Level_2", false);
        data.SceneDic.Add("BossScene", false);
    }
}
