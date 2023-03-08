using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class IslandManager : MonoBehaviour
{
    private IslandPrefabDatabase database;
    private IslandSlotGrid[] slots;
    public int initialResources;
    public int maxResources;
    public GameConfigurations gameConfigurations;
    void Start()
    {
        slots = GetComponentsInChildren<IslandSlotGrid>();
        database = GetComponent<IslandPrefabDatabase>();
        
        database.CreateIslandResource();

        if(initialResources > 0 && database.ContainsResources())
        {
            for(int i=0; i< initialResources; i++)
            {
                CreateNewResource();
            }
        }

        StartCoroutine(SpawnResourcesOverTime());
    }

    public void CreateNewResource()
    {
        int slotIndex = Random.Range(0, slots.Length);
        var slot = slots[slotIndex];

        if (!slot.isBusy && CoreGame.instance.DistanceToPlayer(slot.gameObject) > gameConfigurations.distanceToSpawnResource) 
        {

            var resource = Instantiate(database.RandomResource());
            resource.GetComponent<Mine>().SetSlot(slot);
            slot.SetBusy(true);
        }
        else
        {
            CreateNewResource();
        }
    }

    IEnumerator SpawnResourcesOverTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(gameConfigurations.timeToDeleteItemInInventory);
            int count = slots.Count(x => x.isBusy);
            if(count < maxResources)
            {
                CreateNewResource();
            }
        }
    }

    public void GoToCraftMode()
    {
        StopCoroutine(SpawnResourcesOverTime());
        foreach(var slot in slots)
        {
            if (!slot.isBusy)
            {
                slot.ShowBorder(true);
            }
        }
    }

    public void GoToGameplayMode()
    {
        foreach (var slot in slots)
        {
            slot.ShowBorder(false);
        }
        StartCoroutine(SpawnResourcesOverTime());
    }

}
