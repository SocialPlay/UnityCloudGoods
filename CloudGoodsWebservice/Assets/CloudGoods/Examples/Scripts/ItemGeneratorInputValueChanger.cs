using UnityEngine;
using System.Collections;
using CloudGoods.SDK.Item;

public class ItemGeneratorInputValueChanger : MonoBehaviour {

    public ItemGenerator ItemGenerator;

    public void OnMinEnergyChanged(string minEnergy)
    {
        Debug.Log("min energy: " + minEnergy);
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
        
        for(int i = 0; i < andTagsArray.Length; i++)
        {
            ItemGenerator.AndTags.Add(andTagsArray[i]);
        }
    }

    public void OnOrTagsChanged(string orTags)
    {
        string[] orTagsArray = orTags.Split(',');

        ItemGenerator.OrTags.Clear();

        for (int i = 0; i < orTagsArray.Length; i++)
        {
            ItemGenerator.OrTags.Add(orTagsArray[i]);
        }
    }

    public void OnNotTagsChanged(string NotTags)
    {
        string[] notTagsArray = NotTags.Split(',');

        ItemGenerator.NotTags.Clear();

        for (int i = 0; i < notTagsArray.Length; i++)
        {
            ItemGenerator.NotTags.Add(notTagsArray[i]);
        }
    }

}
