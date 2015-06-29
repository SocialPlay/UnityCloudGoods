using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace CloudGoods.SDK.Models
{
    public class ItemBundlePurchaseRequest : IRequestClass
    {
        public int BundleID;
        public int PaymentType;
        public List<int> AccessLocation;
        public int SaveLocation;

        public string ToHashable()
        {
            string locations = "";
            AccessLocation.ForEach(l => locations += l);
            return BundleID.ToString() + PaymentType.ToString() + SaveLocation.ToString() + locations;
        }

        public ItemBundlePurchaseRequest(int bundleId, int paymentType, List<int> accessLocation, int saveLocation)
        {
            BundleID = bundleId;
            PaymentType = paymentType;
            AccessLocation = accessLocation;
            SaveLocation = saveLocation;
        }
    }
}

