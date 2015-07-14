using UnityEngine;
using System.Collections;
using CloudGoods.SDK.Models;

public class FacebookCurrencyRequest : IRequestClass
{
    public int BundleId;

    public string ToHashable()
    {
        return BundleId.ToString();
    }
}
