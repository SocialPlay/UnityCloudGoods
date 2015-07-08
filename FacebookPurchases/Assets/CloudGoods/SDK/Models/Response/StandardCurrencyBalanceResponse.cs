using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CloudGoods.SDK.Models;

namespace CloudGoods.SDK.Models
{
    public class StandardCurrencyBalanceResponse
    {
        public int Total;
        public List<SimpleItemInfo> Items;
    }
}
