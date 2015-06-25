using UnityEngine;
using System.Collections;
using CloudGoods.Services.WebCommunication;
using CloudGoods.SDK.Models;
using System.Collections.Generic;
using CloudGoods.SDK.Container;


namespace CloudGoods.SDK.Item
{
    public class InsertToContainerOnClick : MonoBehaviour
    {
        public int ContainerLocation;
        bool isPickedUp = false;

        public void PickUp()
        {
            Debug.Log("Clicked: " + gameObject.name);
            if (!isPickedUp)
            {
                ItemVoucherComponent voucherComponent = GetComponent<ItemVoucherComponent>();
                List<RedeemItemVouchersRequest.ItemVoucherSelection> selectionList = new List<RedeemItemVouchersRequest.ItemVoucherSelection>();
                selectionList.Add(new RedeemItemVouchersRequest.ItemVoucherSelection(voucherComponent.voucherInformation.VoucherId, voucherComponent.voucherInformation.Information.Id, voucherComponent.voucherInformation.Amount, ContainerLocation));
                RedeemItemVouchersRequest request = new RedeemItemVouchersRequest(selectionList, null);
                CallHandler.Instance.RedeemItemVoucher(request, UpdatedStacksCallback);
            }
        }

        void UpdatedStacksCallback(UpdatedStacksResponse response)
        {
            Debug.Log("received items : " + response.UpdatedStackIds.Count);
            ItemContainerManager.RefreshAllPersistentItemContainers();
            Destroy(this.gameObject);
        }
    }
}
