using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using CloudGoods.SDK.Models;


namespace CloudGoods.SDK.Item
{
    public class ItemDataComponent : MonoBehaviour
    {
        [HideInInspector]
        public bool isValid = true;

        public OwnedItemInformation itemData
        {
            get
            {
                if (mData == null)
                {
                    mData = new OwnedItemInformation();
                }
                return mData;
            }
            set
            {
                mData = value;
                SetData(mData);
            }
        }

        protected OwnedItemInformation mData;

        public virtual void SetData(OwnedItemInformation itemData) { }
    }
}
