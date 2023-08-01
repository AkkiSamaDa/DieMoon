using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]

[RequireComponent(typeof(CapsuleCollider2D))]
public class DropItem : MonoBehaviour
{
    public int itemID;
    public int itemCount;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            //检测背包是否装满
            if (!PlayerData.Instance.CheckBagFull())
            {
                PlayerData.Instance.AddBagItem(itemID, itemCount);
                UIMgr.Instance.ShowPanel<PopWarnPanel>(E_PanelLayer.System, (panel) =>
                {
                    panel.SetWarnText("获得物品:" + ItemInfoMgr.Instance.GetItemInfo(itemID).name + " *"+itemCount);
                },true);
                Destroy(gameObject);
            }
            else
            {
                UIMgr.Instance.ShowPanel<PopWarnPanel>(E_PanelLayer.System, (panel) =>
                {
                    panel.SetWarnText("背包已满！");
                },true);
            }
        }
    }

}
