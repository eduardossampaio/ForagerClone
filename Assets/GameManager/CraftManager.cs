using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftManager : MonoBehaviour
{
    public GameObject[] itensToCraft;

    public void CraftItem(int index)
    {
        CoreGame.instance.GoToCraftMode(itensToCraft[index]);
    }
}
