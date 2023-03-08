using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameConfigurations", menuName = "Scriptable/GameConfigurations", order = 1)]
public class GameConfigurations : ScriptableObject
{
    public float mininumDistanceToInteractWithMine;
    public float timeToDeleteItemInInventory;

    public float distanceToSpawnResource;
    public float timeToSpawnResource;

    public GameState gameState = GameState.GAMEPLAY;

    public void ChangeState(GameState gameState)
    {
        this.gameState = gameState;


    }
}
