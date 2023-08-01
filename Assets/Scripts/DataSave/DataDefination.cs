using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum E_PersistanteType
{
    ReadWrite,Non
}

public class DataDefination : MonoBehaviour
{

    public string ID;
    public E_PersistanteType persistanteType;

    private void OnValidate()
    {
        if(persistanteType == E_PersistanteType.ReadWrite)
        {
            if (ID == string.Empty)
            {
                ID = System.Guid.NewGuid().ToString();
            }
        }
        else
        {
            ID = string.Empty;
        }
    }

}
