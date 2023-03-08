using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct IslandLevel
{
    public int minumumLevel;
    public ResourceLoot[] resourceLoot;
    
}

[Serializable]
public struct ResourceLoot
{
    public GameObject resource;
    public int amount;
}

public class IslandPrefabDatabase : MonoBehaviour
{
    public IslandLevel[] IslandLevels;
    private List<GameObject> resourceIsland = new();

    private void Start()
    {
        CreateIslandResource();
    }
    public void CreateIslandResource()
    {
        resourceIsland.Clear();
        foreach(var islandLevelItem in IslandLevels)
        {
            if (CoreGame.instance.player.level >= islandLevelItem.minumumLevel)
            {
                CreateResources(islandLevelItem.resourceLoot);
            }
        }
        
    }

    private void CreateResources(ResourceLoot[] resourceLoot)
    {
        if (resourceLoot.Length > 0)
        {

            foreach (var loot in resourceLoot)
            {
                for (int i = 0; i < loot.amount; i++)
                {
                    resourceIsland.Add(loot.resource);
                }
            }
        }
    }

    public GameObject RandomResource()
    {
        return resourceIsland[UnityEngine.Random.Range(0, resourceIsland.Count)];
    }

    public bool ContainsResources()
    {
        return resourceIsland.Count > 0;
    }

}
