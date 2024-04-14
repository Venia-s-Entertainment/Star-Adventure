using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, IInteractable
{
    [SerializeField] int Index;

    PlayerRaycast pr;
    void Start()
    {
        pr = FindObjectOfType<PlayerRaycast>();
    }
    public void Interact()
    {
        PickUp();
    }
    private void PickUp()
    {
        Inventory.itemList[Index] = this;
        Inventory.itemsAmount[Index]++;

        gameObject.SetActive(false);

        pr.Clear();
    }
    public void Drop()
    {
        Inventory.itemsAmount[Index]--;

        if (Inventory.itemsAmount[Index] == 0)
        {
            Inventory.itemList[Index] = null;
        }
    }
    public void DropAll()
    {
        Inventory.itemsAmount[Index] = 0;
        Inventory.itemList[Index] = null;
    }
}

