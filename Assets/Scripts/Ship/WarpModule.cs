using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarpModule : MonoBehaviour, IInteractable
{
    [SerializeField] Text message;
    [SerializeField] Transform modulePlace;
    [SerializeField] GameObject warpModule;
    [SerializeField] AudioSource rechargeAudio;
    public uint jumpRemains = 3;
    public bool hasItem;
    public void Interact()
    {
        Refuel(hasItem);
        Insert(Inventory.itemList[1]);
    }

    private void Refuel(bool hasItem)
    {
        if (hasItem)
        {
            if (Inventory.itemsAmount[2] != 0)
            {
                rechargeAudio.PlayOneShot(rechargeAudio.clip);

                jumpRemains += Inventory.itemsAmount[2];
                Inventory.itemList[2].DropAll();
                return;
            }
            else
            {
                
                message.text = "<color=red>You need something to refuel the Warp Module!</color>";
            }
        }
    }

    private void Insert(Item warpModule)
    {
        if (!hasItem)
        {
            if (warpModule)
            {
                warpModule.Drop();
                Instantiate(this.warpModule, modulePlace.position, modulePlace.rotation, modulePlace);

                hasItem = true;
            }
            else
                message.text = "<color=red>You don't have a Warp Module!</color>";
        }
    }
}
