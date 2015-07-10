using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace CloudGoods.SDK.Models
{

    public class StandardCurrencyBalanceRequest : IRequestClass
    {
        public List<int> AccessLocations;

        public string ToHashable()
        {
            string locations = "";
            if (AccessLocations != null)
                AccessLocations.ForEach(l => locations += l);
            return locations;
        }

        public StandardCurrencyBalanceRequest(List<int> accessLocations)
        {
            AccessLocations = accessLocations;
        }
    }
}
