using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Upgrades Database", menuName = "Upgrade Staff/UpgradesDatabase")]
public class UpgradesDatabase : ScriptableObject
{


    public UpgradeStats[] upgradeStat;

    [ContextMenu("Update ID's")]
    public void UpdateID()
    {
        for (int i = 0; i < upgradeStat.Length; i++)
        {
            if (upgradeStat[i].Id != i)
                upgradeStat[i].Id = i;
        }
    }
    public void OnAfterDeserialize()
    {
        UpdateID();
    }

    public void OnBeforeSerialize()
    {
    }
}
