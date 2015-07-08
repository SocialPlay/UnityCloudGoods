using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System;
using CloudGoods.SDK.Models;
using CloudGoods.SDK.Item;


namespace CloudGoods.SDK.Item
{
    public class ItemDrop : MonoBehaviour
    {
        static public GameObject dropParentObject { get { if (mDrop == null) mDrop = new GameObject("DroppedItems"); return mDrop; } }
        static GameObject mDrop;

        public IGameObjectAction postDropObjectAction;

        public void DropItemIntoWorld(VoucherItemInformation item, Vector3 dropPosition, GameObject dropModelDefault, bool isDropSingleAmount)
        {
            Debug.Log("Drop item into world");

            if (item != null)
            {

                if (isDropSingleAmount)
                {
                    for (int i = 0; i < item.Amount; i++)
                        dropPosition = DropItem(item, dropPosition, dropModelDefault, null, 1);
                }

                else
                    dropPosition = DropItem(item, dropPosition, dropModelDefault, null, item.Amount);
            }
        }

        private Vector3 DropItem(VoucherItemInformation item, Vector3 dropPosition, GameObject dropModelDefault, UnityEngine.Object bundleObj, int dropAmount)
        {
            GameObject dropObject = GameObject.Instantiate(bundleObj != null ? bundleObj : dropModelDefault) as GameObject;

            InsertToContainerOnClick insertClickComponent = dropObject.AddComponent<InsertToContainerOnClick>();
            ItemVoucherComponent voucherComponent = dropObject.AddComponent<ItemVoucherComponent>();
            voucherComponent.voucherInformation = item;


            dropObject.name = voucherComponent.voucherInformation.Information.Name + " (ID: " + item.Information.CollectionId + ")";

            dropObject.transform.position = dropPosition;
            dropObject.transform.parent = dropParentObject.transform;

            if (postDropObjectAction != null)
                postDropObjectAction.DoGameObjectAction(dropObject);
            return dropPosition;
        }
    }
}
