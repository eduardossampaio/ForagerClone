using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameConfigurations", menuName = "Scriptable/GameConfigurations", order = 1)]
public class GameConfigurations : ScriptableObject
{
    public float mininumDistanceToInteractWithMine;
    public float timeToDeleteItemInInventory;
}
