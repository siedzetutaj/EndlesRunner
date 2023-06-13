using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Database", menuName = "Items System/TestDatabase")]
public class TestDatabase : ScriptableObject, ISerializationCallbackReceiver
{
    [SerializeField]
    public List<GameObject> Objects = new List<GameObject>();


    public void AddObj(GameObject obj)
    {
       Objects.Add(obj);
    }
    public void RemoveObj(GameObject obj)
    {
        Objects.Remove(obj);
    }
    public void OnAfterDeserialize()
    {
    }

    public void OnBeforeSerialize()
    {
    }
}
