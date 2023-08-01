using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISaveable
{
    void RegisterSaveData()=>DataSaveMgr.Instance.RegisterSaveData(this);

    void UnRegisterSaveData()=>DataSaveMgr.Instance.UnRegisterSaveData(this);

    void GetSaveData(Data data);

    void LoadData(Data data);

    void RemoveFromData(Data data);

    DataDefination GetID();
}
