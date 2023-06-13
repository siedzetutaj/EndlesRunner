using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[ExecuteInEditMode]
public class GroundItem : MonoBehaviour, ISerializationCallbackReceiver
{
    public int IdToDisplay;
    public Database database;
    public Items item;


    private void OnValidate()
    {
         item = database.ItemObjects[IdToDisplay];
    }
    public void OnAfterDeserialize()
    {
    }

    public void OnBeforeSerialize()
    {
#if UNITY_EDITOR
        GetComponentInChildren<SpriteRenderer>().sprite = item.prefab.GetComponent<SpriteRenderer>().sprite;
        GetComponentInChildren<SpriteRenderer>().color = item.prefab.GetComponent<SpriteRenderer>().color;
        EditorUtility.SetDirty(GetComponentInChildren<SpriteRenderer>());
#endif
    }
}
