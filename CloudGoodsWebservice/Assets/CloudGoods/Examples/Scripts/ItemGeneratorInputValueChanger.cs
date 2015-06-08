using UnityEngine;
using System.Collections;
using CloudGoods.SDK.Item;

public class ItemGeneratorInputValueChanger : MonoBehaviour {

    public ItemGenerator ItemGenerator;

    public void OnMinEnergyChanged(string minEnergy)
    {
        ItemGenerator.minEnergy = int.Parse(minEnergy);
    }

    public void OnMaxEnergyChanged(string maxEnergy)
    {
        ItemGenerator.maxEnergy = int.Parse(maxEnergy);
    }

    public void OnTotalEnergyChanged(string totalEnergy)
    {
        ItemGenerator.TotalEnergyToGenerate = int.Parse(totalEnergy);
    }

    public void OnAndTagsChanged(string andTags)
    {
        string[] andTagsArray = andTags.Split(',');

        ItemGenerator.AndTags.Clear();
        
        //foreach
    }

    //public void OnMinEnergyChanged(string minEnergy)
    //{
    //    ItemGenerator.minEnergy = int.Parse(minEnergy);
    //}

    //public void OnMinEnergyChanged(string minEnergy)
    //{
    //    ItemGenerator.minEnergy = int.Parse(minEnergy);
    //}

}
