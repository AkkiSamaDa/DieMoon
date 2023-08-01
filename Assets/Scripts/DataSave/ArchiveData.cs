using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArchiveData:BaseManager<ArchiveData>
{
    public int archiveId;
    public Data data;
    public List<BagItem> bagItems;
    //通过ID给不同存档位赋值，没有存档就初始化存档，有就读取数据
    public void LoadArchive()
    {

    }
    public void SaveArchive()
    {

    }
    public void InitArchive()
    {

    }
}
