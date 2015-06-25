using UnityEngine;
using System.Collections;

using System.Collections.Generic;
using System;
using LitJson;
using CloudGoods.SDK.Models;
using CloudGoods.Services.WebCommunication;

namespace CloudGoods.SDK.Item
{
    public class ItemGenerator : MonoBehaviour
    {
        public class GeneratedItems
        {
            public List<OwnedItemInformation> generatedItems;
            public int GenerationID;
        }


        public int minEnergy = 1;
        public int maxEnergy = 100;
        public int TotalEnergyToGenerate = 100;

        public List<string> AndTags;
        public List<string> OrTags;
        public List<string> NotTags;

        IGetItems itemPutter;

        void Awake()
        {
            SetupGetItems();
        }

        private void SetupGetItems()
        {
            itemPutter = GetComponent(typeof(IGetItems)) as IGetItems;

            if (itemPutter == null)
                throw new MissingComponentException("This object requires a component which implements " + typeof(IGetItems) + " attached.");
        }

        public void GenerateItems()
        {
            TagSelection tagSelection = new TagSelection()
            {
                AndTags = AndTags,
                OrTags = OrTags,
                NotTags = NotTags
            };
            CreateItemVouchersRequest request = new CreateItemVouchersRequest(TotalEnergyToGenerate, minEnergy, maxEnergy, tagSelection);
            CallHandler.Instance.CreateItemVouchers(request, OnReceivedGeneratedItems);
        }

        public void OnReceivedGeneratedItems(ItemVouchersResponse response)
        {
            foreach (VoucherItemInformation voucherInfo in response.Vouchers)
            {
                Debug.Log(voucherInfo.Information.Name + " has been generated with voucher id: " + voucherInfo.VoucherId);
            }

            itemPutter.GetGameItem(response.Vouchers);
        }
    }
}
