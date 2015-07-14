using UnityEngine;
using System.Collections.Generic;
using CloudGoods.SDK.Models;
using CloudGoods.Services;
using SocialPlay.Bundles;

namespace CloudGoods.SDK.Item
{

    public class GetItemsDropper : MonoBehaviour, IGetItems
    {
        static public GameObject dropParent { get { if (mDrop == null) mDrop = new GameObject("DroppedItems"); return mDrop; } }
        static GameObject mDrop;

        public Transform dropTransform;
        //public ItemPrefabInitilizer prefabinitilizer;   
        public IGameObjectAction postDropObjectAction;

        public bool IsDropSingleAmount = false;

        ItemDrop gameItemDrop;

        void Awake()
        {
            gameItemDrop = GetComponent<ItemDrop>();
            if (gameItemDrop == null) gameItemDrop = gameObject.AddComponent<ItemDrop>();
        }

        public void GetGameItem(List<VoucherItemInformation> items)
        {
            DropItems(items);
        }

        void DropItems(List<VoucherItemInformation> dropItems)
        {
            foreach (VoucherItemInformation dropItem in dropItems)
            {

                if (!string.IsNullOrEmpty(dropItem.Information.AssetBundleURL))
                {
                    BundleSystem.Get(dropItem.Information.AssetBundleURL, x =>
                        {
                            if (x != null)
                                gameItemDrop.DropItemIntoWorld(dropItem, dropTransform.position, (GameObject)x, IsDropSingleAmount);
                            else
                                gameItemDrop.DropItemIntoWorld(dropItem, dropTransform.position, CloudGoodsSettings.DefaultItemDrop, IsDropSingleAmount);
                        });
                }
                else
                    gameItemDrop.DropItemIntoWorld(dropItem, dropTransform.position, CloudGoodsSettings.DefaultItemDrop, IsDropSingleAmount);

            }
        }
    }
}
