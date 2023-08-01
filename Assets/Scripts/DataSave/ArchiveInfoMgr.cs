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
                Debug.Log($"设置存档信息成功，存档ID:{id},当前存档状态{archiveInfos[i].state}，修改值{state}");
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