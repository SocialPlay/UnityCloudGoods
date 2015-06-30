using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CloudGoods.SDK.Models;


namespace CloudGoods.SDK.Item
{

    public interface IGetItems
    {

        void GetGameItem(List<VoucherItemInformation> items);
    }
}
