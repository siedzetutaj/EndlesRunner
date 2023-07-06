using System.Collections;
using System.Collections.Generic;
using System.IO.Enumeration;
using UnityEngine;


[CreateAssetMenu(fileName = "New Upgrade", menuName = "Upgrade Staff/Upgrade")]
public class UpgradeStats : ScriptableObject
{
    public Sprite uiDisplay;
    public int cost;
    public int Id =-1;
}
