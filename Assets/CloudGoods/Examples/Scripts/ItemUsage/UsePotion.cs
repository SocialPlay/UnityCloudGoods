using UnityEngine;
using System.Collections;
using CloudGoods.SDK.Item;
using CloudGoods.SDK.Models;
using CloudGoods.SDK.Container;
using CloudGoods.Services;

public class UsePotion : MonoBehaviour, UseItem {

    ItemDataComponent ItemDataComp;

    void Awake()
    {
        ItemDataComp = GetComponent<ItemDataComponent>();
    }

    public void UseItem()
    {
        OwnedItemInformation itemData = ItemDataComp.itemData;

        foreach (ItemInformation.Behaviour behaviour in itemData.Information.Behaviours)
        {
            if (behaviour.Name == "Poison")
            {
                Character.PoisonCharacter();
            }
            else if (behaviour.Name == "Health")
            {
                Character.HealCharacter(20);
            }
        }

        itemData.Amount--;

        if (itemData.Amount <= 0)
        {
            itemData.Amount++;
            ItemContainerManager.RemoveItem(itemData, itemData.OwnerContainer);
        }
        else
        {
            ItemManipulationServices.UpdateItemByStackIds(itemData.StackLocationId, -1, itemData.Location, OnUpdatedStack);
        }
    }

    void OnUpdatedStack(UpdatedStacksResponse response)
    {
        foreach (SimpleItemInfo info in response.UpdatedStackIds)
        {
            Debug.Log("Updated Stack. ID: " + info.StackId + "    Location: " + info.Location + "    Amount: " + info.Amount);
        }
    }
}
