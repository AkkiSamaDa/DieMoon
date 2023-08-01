using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArchiveInfoMgr:BaseManager<ArchiveInfoMgr>
{
    public List<ArchiveInfo> archiveInfos;
    public ArchiveInfoMgr()
    {
        LoadArchiveInfo();
    }

    public void SaveArchiveInfo()
    {
        JsonMgr.Instance.SaveData(archiveInfos, "ArachiveInfo.json");
    }
    public void LoadArchiveInfo()
    {
        archiveInfos = JsonMgr.Instance.LoadData<List<ArchiveInfo>>("ArachiveInfo.json");
    }   

    public void SetArchiveInfo(int  id,bool state)
    {
        for (int i = 0; i < archiveInfos.Count; i++)
        {
            if (archiveInfos[i].id == id)
            {
                archiveInfos[i].state = state;
                Debug.Log($"���ô浵��Ϣ�ɹ����浵ID:{id},��ǰ�浵״̬{archiveInfos[i].state}���޸�ֵ{state}");
                SaveArchiveInfo();
            }
        }
    }
}

public class ArchiveInfo
{
    public bool state;
    public int id;
}